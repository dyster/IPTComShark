using BitDataParser;
using datashark.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BitDataParser.Crackers;
using static IPTComShark.Parsers.CIPParser;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IPTComShark.Parsers
{
    public class CIPParser : IParser
    {
        /* Communication Ports */
        // ENIP_ENCAP_PORT    44818 /* EtherNet/IP located on port 44818    */
        // ENIP_SECURE_PORT   2221  /* EtherNet/IP TLS/DTLS port            */
        // ENIP_IO_PORT       2222  /* EtherNet/IP IO located on port 2222  */

        /// <summary>
        /// EtherNet/IP function codes 
        /// </summary>
        public enum ENIP_Function_Codes
        {
            NOP = 0x0000,
            LIST_SERVICES = 0x0004,
            LIST_IDENTITY = 0x0063,
            LIST_INTERFACES = 0x0064,
            REGISTER_SESSION = 0x0065,
            UNREGISTER_SESSION = 0x0066,
            SEND_RR_DATA = 0x006F,
            SEND_UNIT_DATA = 0x0070,
            START_DTLS = 0x00C8
        }        

        /// <summary>
        /// EtherNet/IP status codes
        /// </summary>
        public enum ENIP_Status_Codes
        {
            SUCCESS = 0x0000,
            INVALID_CMD = 0x0001,
            NO_RESOURCES = 0x0002,
            INCORRECT_DATA = 0x0003,
            INVALID_SESSION = 0x0064,
            INVALID_LENGTH = 0x0065,
            UNSUPPORTED_PROT_REV = 0x0069,
            ENCAP_HEADER_ERROR = 0x006A
        }        
        
        /* EtherNet/IP Common Packet Format Type IDs */
        public enum ENIP_CPF_Types
        {
            CPF_ITEM_NULL = 0x0000,
            CPF_ITEM_CIP_IDENTITY = 0x000C,
            CPF_ITEM_CIP_SECURITY = 0x0086,
            CPF_ITEM_ENIP_CAPABILITY = 0x0087,
            CPF_ITEM_ENIP_USAGE = 0x0088,
            CPF_ITEM_CONNECTED_ADDRESS = 0x00A1,
            CPF_ITEM_CONNECTED_DATA = 0x00B1,
            CPF_ITEM_UNCONNECTED_DATA = 0x00B2,
            CPF_ITEM_LIST_SERVICES_RESP = 0x0100,
            CPF_ITEM_SOCK_ADR_INFO_OT = 0x8000,
            CPF_ITEM_SOCK_ADR_INFO_TO = 0x8001,
            CPF_ITEM_SEQUENCED_ADDRESS = 0x8002,
            CPF_ITEM_UNCONNECTED_MSG_DTLS = 0x8003
        }
        

        public ProtocolType ProtocolType => ProtocolType.CIP;

        public Parse Extract(byte[] data, iPacket iPacket)
        {
            var parse = new Parse();

            var pds = new ParsedDataSet();
            parse.ParsedData = new List<ParsedDataSet> { pds };
            
            parse.DisplayFields = new List<DisplayField>();

            var function = (ENIP_Function_Codes)CrackUInt16(data, 0);
            pds.ParsedFields.Add(ParsedField.Create("Function", function));
            parse.DisplayFields.Add(new DisplayField("Function", function));

            var length = CrackUInt16(data, 2);
            pds.ParsedFields.Add(ParsedField.Create("Length", length));

            var sessionHandle = CrackUInt32(data, 4);
            pds.ParsedFields.Add(ParsedField.Create("Session Handle", sessionHandle));
            parse.DisplayFields.Add(new DisplayField("Session", sessionHandle));

            var status = CrackUInt32(data, 8);
            pds.ParsedFields.Add(ParsedField.Create("Status", status));

            var context = CrackUInt64(data, 12);
            pds.ParsedFields.Add(ParsedField.Create("Context", context));
            parse.DisplayFields.Add(new DisplayField("Context", context));

            var options = CrackUInt32(data, 20);
            pds.ParsedFields.Add(ParsedField.Create("Options", options));




            

            return parse;
        }

        
    }

    public class CIPIOParser : IParser
    {
        public ProtocolType ProtocolType => ProtocolType.CIPIO;

        public Parse Extract(byte[] data, iPacket iPacket)
        {
            var traveller = (iTraveller)iPacket;
            
            var parse = new Parse();

            var pds = new ParsedDataSet();
            parse.ParsedData = new List<ParsedDataSet> { pds };            
            parse.DisplayFields = new List<DisplayField>();

            int pos = 0;

            var count = CrackUInt16(data, pos);
            pds.ParsedFields.Add(ParsedField.Create("Count", count));
            pos += 2;

            for(int i = 0; i < count; i++)
            {
                var CPFtype = (ENIP_CPF_Types)CrackUInt16(data, pos);
                pos += 2;
                pds.ParsedFields.Add(ParsedField.Create("Type", CPFtype));

                // I don't know if all types below in the switch actually has a length variable
                // but I'll assume so until proven otherwise!
                var length = BitConverter.ToUInt16(data, pos);
                pos += 2;
                pds.ParsedFields.Add(ParsedField.Create("Length", length));


                switch (CPFtype)
                {
                    case ENIP_CPF_Types.CPF_ITEM_SEQUENCED_ADDRESS:
                        

                        var connid = CrackUInt32(data, pos);
                        pos += 4;
                        pds.ParsedFields.Add(ParsedField.Create("Connection ID", connid));
                        parse.BackLinkIdentifier = connid.ToString();

                        var seqno = CrackUInt32(data, pos);
                        pos += 4;
                        pds.ParsedFields.Add(ParsedField.Create("Encapsulation Sequence #", seqno));
                        break;
                    case ENIP_CPF_Types.CPF_ITEM_CONNECTED_DATA:

                        // OK this seems to be where the CIP data is sent, this should probably be a separate class then!

                        var cipseqno = CrackUInt16(data, pos);
                        pos += 2;
                        pds.ParsedFields.Add(ParsedField.Create("CIP Sequence Count", cipseqno));

                        // second disclaimer, doing a dreadful hackbodge for now where we just parse data based on IP, rather than identifying the right info from the connection manager init

                        if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 10)
                        {
                            // from tpws

                            var cipioheader = CrackUInt32(data, pos);
                            pos += 4;
                            pds.ParsedFields.Add(ParsedField.Create("32-bit header", cipioheader));

                            if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 101)
                            {
                                // to DMI left

                                var pd = CIP.TPWStoDMI.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                            }
                            else if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 102)
                            {
                                // to DMI right

                                var pd = CIP.TPWStoDMI.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                            }
                        }
                        else if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 101)
                        {
                            // from dmi left

                            if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 10)
                            {
                                // to tpws

                                var pd = CIP.DMItoTPWS.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                            }
                            
                        }
                        else if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 102)
                        {
                            // from dmi right

                            if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 10)
                            {
                                // to tpws

                                var pd = CIP.DMItoTPWS.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                            }

                        }

                        break;
                    case ENIP_CPF_Types.CPF_ITEM_NULL:
                        
                    case ENIP_CPF_Types.CPF_ITEM_CIP_IDENTITY:
                        
                    case ENIP_CPF_Types.CPF_ITEM_CIP_SECURITY:
                        
                    case ENIP_CPF_Types.CPF_ITEM_ENIP_CAPABILITY:
                        
                    case ENIP_CPF_Types.CPF_ITEM_ENIP_USAGE:
                       
                    case ENIP_CPF_Types.CPF_ITEM_CONNECTED_ADDRESS:
                       
                    
                    case ENIP_CPF_Types.CPF_ITEM_UNCONNECTED_DATA:
                        
                    case ENIP_CPF_Types.CPF_ITEM_LIST_SERVICES_RESP:
                        
                    case ENIP_CPF_Types.CPF_ITEM_SOCK_ADR_INFO_OT:
                       
                    case ENIP_CPF_Types.CPF_ITEM_SOCK_ADR_INFO_TO:
                        
                    
                    case ENIP_CPF_Types.CPF_ITEM_UNCONNECTED_MSG_DTLS:
                                        
                    default:
                        pds.ParsedFields.Add(ParsedField.CreateError(CPFtype + " not implemented"));
                        parse.DisplayFields.Add(new DisplayField("ERROR", CPFtype + " not implemented"));
                        break;
                }

            }

            return parse;
        }
    }

    
        
}
