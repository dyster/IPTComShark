using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TrainShark.Classes
{
    internal static class Conversions
    {
        /// <summary>
        ///     Converts a size to a short representation
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string PrettyPrintSize(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return Math.Sign(byteCount) * num + suf[place];
        }

        /// <summary>
        ///     Converts a size to a short representation
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string PrettyPrintSize(ulong byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];

            int place = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
            double num = Math.Round(byteCount / Math.Pow(1024, place), 1);
            return num + suf[place];
        }

        // filters control characters but allows only properly-formed surrogate sequences
        private static readonly Regex InvalidXMLChars = new Regex(
            @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
            RegexOptions.Compiled);

        /// <summary>
        ///     Removes any unusual unicode characters that can't be encoded into XML
        /// </summary>
        /// <param name="text">The string to be examine</param>
        /// <returns>A string stripped of invalid chars</returns>
        public static string RemoveInvalidXMLChars(string text)
        {
            return String.IsNullOrEmpty(text) ? "" : InvalidXMLChars.Replace(text, "");
        }

        /// <summary>
        ///     Detects if there are invalid chars in the string that can not be written to xml
        /// </summary>
        /// <param name="text">The string to be examined</param>
        /// <returns>True if there are invalid chars present</returns>
        public static bool ContainsInvalidXMLChars(string text)
        {
            return InvalidXMLChars.IsMatch(text);
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            var bytes = new byte[NumberChars / 2];
            for (var i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }

    public class IPJsonConverter : JsonConverter<IPAddress>
    {
        public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return IPAddress.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
