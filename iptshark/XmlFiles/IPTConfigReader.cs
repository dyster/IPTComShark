using BitDataParser;
using IPTComShark.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IPTComShark.XmlFiles
{
    public class IPTConfigReader : makeSerializable
    {
        private IPTConfigReader()
        {
            // to support serialization
        }

        public IPTConfigReader(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(cpu));
            var settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;
            using (var fileStream = File.OpenRead(path))
            {
                using (var reader = XmlReader.Create(fileStream, settings))
                {
                    var deserialize = (cpu)xmlSerializer.Deserialize(reader);
                    foreach (cpuBusinterfacelist o in deserialize.Items.Where(i => i is cpuBusinterfacelist))
                        foreach (cpuBusinterfacelistBusinterface businterface in o.Businterface)
                        {
                            foreach(var telegram in businterface.Telegram)
                            {
                                if (!Telegrams.Contains(telegram))
                                    Telegrams.Add(telegram);
                            }
                            
                        }
                    foreach (cpuDatasetlist cpuDatasetlist in deserialize.Items.Where(i => i is cpuDatasetlist))
                        Datasets.AddRange(cpuDatasetlist.Dataset);
                }
            }
        }

        public List<Telegram> Telegrams { get; set; } = new List<Telegram>();

        public List<Dataset> Datasets { get; set; } = new List<Dataset>();

        public Telegram GetTelegramByComId(uint comid)
        {
            foreach (Telegram telegram in Telegrams)
                if (telegram.Comid == comid)
                    return telegram;
            return null;
        }

        public Dataset GetDatasetByComId(uint comid)
        {
            Telegram telegram = GetTelegramByComId(comid);
            if (telegram == null)
                return null;
            foreach (Dataset dataset in Datasets)
                if (dataset.Datasetid == telegram.Datasetid)
                    return dataset;
            return null;
        }

        public DataSetCollection GetDataSetCollection()
        {
            var c = new DataSetCollection();


            var datasetdic = new Dictionary<string, DataSetDefinition>();
            //var triplets = new Tuple<string, DataSetDefinition, List<int>>();

            foreach (Dataset dataset in Datasets)
            {
                var set = ExtractDataset(dataset);
                var d = new DataSetDefinition();
                d.BitFields = set;
                var serial = d.Serialize();
                if (!datasetdic.ContainsKey(serial))
                {
                    datasetdic.Add(serial, d);
                }
            }


            foreach (var t in Telegrams)
            {
                var comid = t.Comid.ToString();
                var set = Datasets.First(dataset => dataset.Datasetid == t.Datasetid);

                var dud = new DataSetDefinition() {BitFields = ExtractDataset(set)};
                var serial = dud.Serialize();

                var datasetdef = datasetdic[serial];

                //var name = Regex.Replace(t.Name, @"\d+$", "NN");
                var name = t.Name;
                if(Regex.IsMatch(name, @"^[io][A-Z]"))
                {
                    name = Regex.Replace(name, @"^[io]", "");
                }

                if (string.IsNullOrEmpty(datasetdef.Name))
                    datasetdef.Name = name;               
                else if (!datasetdef.Name.Contains(name))
                    datasetdef.Name += "," + name;

                if (!datasetdef.Identifiers.Contains(comid))
                    datasetdef.Identifiers.Add(comid);
            }

            var outlist = datasetdic.Values.ToList();
            var removed = outlist.RemoveAll(d => d.Identifiers.Count == 0);

            foreach(var dataset in outlist)
            {
                c.DataSets.Add(dataset);
            }

            


            return c;
        }

        private static List<BitField> ExtractDataset(Dataset set)
        {
            var list = new List<BitField>();
            foreach (ProcessVariable processVariable in set.Processvariable)
            {
                int arraysize = int.Parse(processVariable.Arraysize);


                if (processVariable.Type == "CHAR8")
                {
                    list.Add(new BitField
                    {
                        Name = processVariable.Name,
                        BitFieldType = BitFieldType.StringAscii,
                        Length = arraysize * 8
                    });
                }
                else if (processVariable.Type == "CHAR16")
                {
                    list.Add(new BitField
                    {
                        Name = processVariable.Name,
                        BitFieldType = BitFieldType.StringBigEndUtf16,
                        Length = arraysize * 16
                    });
                }
                else
                {
                    for (int i = 0; i < arraysize; i++)
                    {
                        var field = new BitField();
                        field.Name = processVariable.Name;
                        if (arraysize > 1)
                        {
                            field.Name += "[" + i + "]";
                        }

                        switch (processVariable.Type)
                        {
                            case "BOOLEAN1":
                            case "BOOL1":
                                field.BitFieldType = BitFieldType.Bool;
                                field.Length = 1;
                                break;
                            case "BOOLEAN8":
                            case "BOOL8":
                                field.BitFieldType = BitFieldType.Bool;
                                field.Length = 8;
                                break;
                            case "UINT8":
                                field.BitFieldType = BitFieldType.UInt8;
                                field.Length = 8;
                                break;
                            case "INT8":
                                field.BitFieldType = BitFieldType.Int8;
                                field.Length = 8;
                                break;
                            case "UINT16":
                                field.BitFieldType = BitFieldType.UInt16;
                                field.Length = 16;
                                break;
                            case "INT16":
                                field.BitFieldType = BitFieldType.Int16;
                                field.Length = 16;
                                break;
                            case "UINT32":
                                field.BitFieldType = BitFieldType.UInt32;
                                field.Length = 32;
                                break;
                            case "INT32":
                                field.BitFieldType = BitFieldType.Int32;
                                field.Length = 32;
                                break;
                            case "REAL32":
                                field.BitFieldType = BitFieldType.Float32;
                                field.Length = 32;
                                break;
                            case "TIME48":
                                field.BitFieldType = BitFieldType.HexString;
                                field.Length = 48;
                                break;
                            default:
                                throw new Exception("Unknown Type!");
                                break;
                        }

                        list.Add(field);
                    }
                }
            }

            return list;
        }
    }

    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class cpu
    {
        /// <remarks />
        [XmlElement("bus-interface-list", typeof(cpuBusinterfacelist), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("data-set-list", typeof(cpuDatasetlist), Form = XmlSchemaForm.Unqualified)]
        public object[] Items { get; set; }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class cpuBusinterfacelist
    {
        /// <remarks />
        [XmlElement("bus-interface", Form = XmlSchemaForm.Unqualified)]
        public cpuBusinterfacelistBusinterface[] Businterface { get; set; }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class cpuBusinterfacelistBusinterface
    {
        /// <remarks />
        [XmlElement("telegram", Form = XmlSchemaForm.Unqualified)]
        public Telegram[] Telegram { get; set; }

        /// <remarks />
        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Telegram : IEquatable<Telegram>
    {
        /// <remarks />
        [XmlAttribute("size")]
        public string Size { get; set; }

        /// <remarks />
        [XmlAttribute("data-set-id")]
        public string Datasetid { get; set; }

        /// <remarks />
        [XmlAttribute("com-id")]
        public uint Comid { get; set; }

        /// <remarks />
        [XmlAttribute("name")]
        public string Name { get; set; }

        public bool Equals(Telegram other)
        {
            return Size.Equals(other.Size) && Datasetid.Equals(other.Datasetid) && Comid.Equals(other.Comid) && Name.Equals(other.Name);
        }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class cpuDatasetlist
    {
        /// <remarks />
        [XmlElement("data-set", Form = XmlSchemaForm.Unqualified)]
        public Dataset[] Dataset { get; set; }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Dataset
    {
        /// <remarks />
        [XmlElement("process-variable", Form = XmlSchemaForm.Unqualified)]
        public ProcessVariable[] Processvariable { get; set; }

        /// <remarks />
        [XmlAttribute("data-set-id")]
        public string Datasetid { get; set; }
    }

    /// <remarks />
    [GeneratedCode("xsd", "4.6.1055.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ProcessVariable
    {
        /// <remarks />
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <remarks />
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <remarks />
        [XmlAttribute("array-size")]
        public string Arraysize { get; set; }

        /// <remarks />
        [XmlAttribute("offset")]
        public string Offset { get; set; }
    }
}


/*
<? xml version="1.0" encoding="utf-8"?>
<xs:schema id = "cpu" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name = "cpu" msdata:IsDataSet="true" msdata:Locale="en-US">
    <xs:complexType>
      <xs:choice minOccurs = "0" maxOccurs="unbounded">
        <xs:element name = "bus-interface-list" >
          < xs:complexType>
            <xs:sequence>
              <xs:element name = "bus-interface" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name = "telegram" minOccurs="0" maxOccurs="unbounded">
                      <xs:complexType>
                        <xs:attribute name = "size" type="xs:string" />
                        <xs:attribute name = "data-set-id" type="xs:string" />
                        <xs:attribute name = "com-id" type="xs:string" />
                        <xs:attribute name = "name" type="xs:string" />
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                  <xs:attribute name = "type" type="xs:string" />
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name = "data-set-list" >
          < xs:complexType>
            <xs:sequence>
              <xs:element name = "data-set" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name = "process-variable" minOccurs="0" maxOccurs="unbounded">
                      <xs:complexType>
                        <xs:attribute name = "name" type="xs:string" />
                        <xs:attribute name = "type" type="xs:string" />
                        <xs:attribute name = "array-size" type="xs:string" />
                        <xs:attribute name = "offset" type="xs:string" />
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                  <xs:attribute name = "data-set-id" type="xs:string" />
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>
*/