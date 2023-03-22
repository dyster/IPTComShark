using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;

namespace IPTComShark
{
    public sealed class Logger
    {
        /// <summary>
        /// The max time before log flushes to disk in seconds
        /// </summary>
        private const int MaxLogAge = 60;

        /// <summary>
        /// Maximum items in Queue before it is flushed to disk
        /// </summary>
        private const int QueueSize = 20;

        //private readonly ExceptionResolver _exceptionResolver = new ExceptionResolver();

        // use backing field to make sure it is instantiated before properties
        private static readonly Logger _instance = new Logger();

        private readonly LinkedList<LogRoll> _log;
        private readonly object _logLockObject;
        private readonly Queue<LogRoll> _logQueue;
        private readonly AutoResetEvent _waitForWrite;

        // file writing stuff
        private bool _enableDiskWrite;

        private bool _isWriting;
        private DateTime _lastFlushed;
        private string _logPathPrefix;


        /// <summary>
        ///     Private constructor to prevent instance creation
        /// </summary>
        private Logger()
        {
            _waitForWrite = new AutoResetEvent(false);
            _logQueue = new Queue<LogRoll>();
            _log = new LinkedList<LogRoll>();
            Severity = Severity.Debug;
            _lastFlushed = DateTime.Now;
            _logLockObject = new object();
        }

        public Severity Severity { get; set; }

        /// <summary>
        ///     The Singleton
        /// </summary>
        public static Logger Instance => _instance;

        ~Logger()
        {
            // Make sure to flush if application dies
            // Application should call Flush when shutting down gracefully to avoid the emergency flush
            FlushLog(true);
        }

        [DebuggerStepThrough]
        public static void Log(string message, Severity severity, Exception e = null, byte[] data = null)
        {
            var stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            string reflectedTypeName = methodBase.ReflectedType?.Name;

            if (severity >= Instance.Severity)
            {
                Instance.WriteToLog(new LogRoll(message, severity, reflectedTypeName, e, data));
            }
        }

        public static void Log(Exception e, Severity severity = Severity.Error, byte[] data = null)
        {
            var stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            string reflectedTypeName = methodBase.ReflectedType?.Name;

            if (severity >= Instance.Severity)
            {
                Instance.WriteToLog(new LogRoll(e, severity, reflectedTypeName, data));
            }
        }

        public event EventHandler<LogRoll> LogAdded;

        /// <summary>
        ///     Gets a copy of the current log
        /// </summary>
        /// <returns>A list of Log items</returns>
        public List<LogRoll> GetLog()
        {
            lock (_logLockObject)
            {
                return _log.ToList();
            }
        }

        /// <summary>
        /// Flushes the current log to disk, call this for graceful shutdown
        /// </summary>
        public static void Flush()
        {
            if (!Instance._enableDiskWrite)
                return;

            // check the count here so we don't wait for nothing
            if (Instance._logQueue.Count <= 0) return;

            // todo implement await instead
            Instance.FlushLog(false);

            // wait for writer to finish
            Instance._waitForWrite.WaitOne(5000);
        }

        /// <summary>
        ///     The single instance method that writes to the log file
        /// </summary>
        /// <param name="logEntry"></param>
        private void WriteToLog(LogRoll logEntry)
        {
            lock (_logLockObject)
            {
                _log.AddFirst(logEntry);

                // if log is getting too big, cap it
                if (_log.Count > 500)
                    _log.RemoveLast();

                if (_enableDiskWrite)
                    _logQueue.Enqueue(logEntry);
            }

            // If we have reached the Queue Size then flush the Queue
            if (_logQueue.Count >= QueueSize || DoPeriodicFlush())
            {
                FlushLog(false);
            }


            OnLogAdded(logEntry);
        }

        /// <summary>
        /// Calling this method will start logging to disk, anything previous in the log (up to the max limit) will also be written
        /// This cannot be stopped once started
        /// Application should call Flush when closing gracefully
        /// </summary>
        /// <param name="path">The directory where files will be stored</param>
        /// <param name="prefix">Files will be prefixed with this</param>
        public void EnableDiskWriting(string path, string prefix)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            _logPathPrefix = directoryInfo.FullName + "\\" + prefix;

            lock (_logLockObject)
            {
                // if there are already items in the log, enqueue them for writing
                foreach (LogRoll log in _log)
                {
                    _logQueue.Enqueue(log);
                }
            }

            _enableDiskWrite = true;
            Log("Disk writing enabled, path: " + path, Severity.Info);
        }

        /// <summary>
        /// Check if it is time to flush the log
        /// </summary>
        /// <returns>True if we should flush</returns>
        private bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - _lastFlushed;
            return logAge.TotalSeconds >= MaxLogAge;
        }

        /// <summary>
        /// Flushes the Queue to the physical log file
        /// </summary>
        private void FlushLog(bool isTerminating)
        {
            if (_logQueue.Count == 0)
                return;

            // if we are busy and not desperate, just wait til next time
            if (_isWriting && !isTerminating)
                return;

            var list = new List<LogRoll>();

            // get a buffer and then release the que again before starting to write
            lock (_logLockObject)
            {
                list.AddRange(_logQueue.ToList());
                _logQueue.Clear();
            }


            // if we are shutting down, do a do or die write on current thread
            if (isTerminating)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<LogRoll>));

                // make a nasty filename to make sure it is unique
                var filename = $"{_logPathPrefix}_{DateTime.Now:yyyy-MM-dd HHmmss fff}.xml";

                using (XmlWriter xmlWriter =
                    XmlWriter.Create(filename, new XmlWriterSettings { Indent = true }))
                {
                    serializer.WriteObject(xmlWriter, list);
                }
            }
            else
            {
                // if not, lets try and be a bit more graceful

                ThreadPool.QueueUserWorkItem(x =>
                {
                    _isWriting = true;

                    try
                    {
                        //DataContractSerializer serializer = new DataContractSerializer(typeof(List<Log>), null, int.MaxValue, false, false, null, _exceptionResolver);
                        DataContractSerializer serializer = new DataContractSerializer(typeof(List<LogRoll>));

                        var filename = $"{_logPathPrefix}_{DateTime.Now:yyyy-MM-dd HH}.xml";

                        if (File.Exists(filename))
                        {
                            // read the existing file and get the log entries
                            List<LogRoll> tempList;
                            using (var filestream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                tempList = (List<LogRoll>)serializer.ReadObject(filestream);
                            }

                            // swaparoo
                            tempList.AddRange(list);
                            list = tempList;
                        }

                        using (XmlWriter xmlWriter =
                            XmlWriter.Create(filename, new XmlWriterSettings { Indent = true }))
                        {
                            serializer.WriteObject(xmlWriter, list);
                        }

                        _lastFlushed = DateTime.Now;
                    }
                    catch (SerializationException se)
                    {
                        // lets go full circle
                        WriteToLog(new LogRoll(se, Severity.Error, "Logger"));
                    }
                    catch (Exception e)
                    {
                        // lets go full circle
                        WriteToLog(new LogRoll(e, Severity.Error, "Logger"));
                    }
                    finally
                    {
                        _isWriting = false;

                        // release if there is a waiter
                        _waitForWrite.Set();
                    }
                });
            }
        }

        private void OnLogAdded(LogRoll e)
        {
            EventHandler<LogRoll> handler = LogAdded;
            handler?.Invoke(this, e);
        }

        public string GetCSV()
        {
            var builder = new StringBuilder();
            lock (_logLockObject)
            {
                foreach (LogRoll log in _log)
                {
                    builder.AppendFormat("\"{0}\",\"{1}\",\"{2}\"", log.Time, log.Severity,
                        log.Message.Replace("\"", "\"\""));
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
    }

    public enum Severity
    {
        Debug,
        Info,
        Warning,
        Error,
        None
    }

    [DataContract]
    public class LogRoll : EventArgs
    {
        public LogRoll(Exception e, Severity severity, object caller, byte[] data = null)
        {
            if (caller != null) Caller = caller.ToString();
            Exception = e;
            Time = DateTime.Now;
            Severity = severity;
            Data = data;
        }

        public LogRoll(string message, Severity severity, object caller, Exception e = null, byte[] data = null)
        {
            if (caller != null) Caller = caller.ToString();
            Exception = e;
            Message = message;
            Time = DateTime.Now;
            Severity = severity;
            Data = data;
        }

        [DataMember(EmitDefaultValue = false)] public string Message { get; set; }

        [DataMember] public DateTime Time { get; set; }

        [DataMember] public Severity Severity { get; set; }

        [DataMember(EmitDefaultValue = false)] public Exception Exception { get; set; }

        [DataMember] public string Caller { get; set; }

        /// <summary>
        /// Some optional relevant data
        /// </summary>
        [DataMember]
        public byte[] Data { get; set; }

        public override string ToString()
        {
            if (Exception != null && string.IsNullOrEmpty(Message))
                return Time + " " + Severity + " " + Caller + ": " + Exception.Message;
            if (Exception != null)
                return Time + " " + Severity + " " + Caller + ": " + Message + " - " + Exception.Message;
            return Time + " " + Severity + " " + Caller + ": " + Message;
        }
    }
}