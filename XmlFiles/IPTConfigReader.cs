using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IPTComShark.XmlFiles
{
    public class IPTConfigReader
    {
        public IPTConfigReader(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(cpu));
            var deserialize = (cpu) xmlSerializer.Deserialize(File.OpenRead(path));
            foreach (cpuBusinterfacelist o in deserialize.Items.Where(i => i is cpuBusinterfacelist))
            foreach (cpuBusinterfacelistBusinterface businterface in o.Businterface)
                Telegrams.AddRange(businterface.Telegram);
            foreach (cpuDatasetlist cpuDatasetlist in deserialize.Items.Where(i => i is cpuDatasetlist))
                Datasets.AddRange(cpuDatasetlist.Dataset);
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
    public class Telegram
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