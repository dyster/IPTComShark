using BitDataParser;
using datashark.DataSets;
using TrainShark.DataSets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BitDataParser.Crackers;
using static TrainShark.Parsers.CIPCommon;
using static TrainShark.Parsers.CIPParser;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainShark.Parsers
{
    public static class CIPCommon
    {
        /* Communication Ports */
        public const ushort ENIP_ENCAP_PORT = 44818; /* EtherNet/IP located on port 44818    */
        public const ushort ENIP_SECURE_PORT = 2221;  /* EtherNet/IP TLS/DTLS port            */
        public const ushort ENIP_IO_PORT = 2222;  /* EtherNet/IP IO located on port 2222  */

        public enum ENIP_PACKET_TYPE
        {
            ENIP_REQUEST_PACKET,
            ENIP_RESPONSE_PACKET,
            ENIP_CANNOT_CLASSIFY
        }

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

        /*
        #define GENERIC_SC_LIST \
   { SC_GET_ATT_ALL,          "Get Attributes All" }, \
   { SC_SET_ATT_ALL,          "Set Attributes All" }, \
   { SC_GET_ATT_LIST,         "Get Attribute List" }, \
   { SC_SET_ATT_LIST,         "Set Attribute List" }, \
   { SC_RESET,                "Reset" }, \
   { SC_START,                "Start" }, \
   { SC_STOP,                 "Stop" }, \
   { SC_CREATE,               "Create" }, \
   { SC_DELETE,               "Delete" }, \
   { SC_MULT_SERV_PACK,       "Multiple Service Packet" }, \
   { SC_APPLY_ATTRIBUTES,     "Apply Attributes" }, \
   { SC_GET_ATT_SINGLE,       "Get Attribute Single" }, \
   { SC_SET_ATT_SINGLE,       "Set Attribute Single" }, \
   { SC_FIND_NEXT_OBJ_INST,   "Find Next Object Instance" }, \
   { SC_RESTOR,               "Restore" }, \
   { SC_SAVE,                 "Save" }, \
   { SC_NO_OP,                "Nop" }, \
   { SC_GET_MEMBER,           "Get Member" }, \
   { SC_SET_MEMBER,           "Set Member" }, \
   { SC_INSERT_MEMBER,        "Insert Member" }, \
   { SC_REMOVE_MEMBER,        "Remove Member" }, \
   { SC_GROUP_SYNC,           "Group Sync" }, \ 
        
         /* CIP Service Codes */
        /*
#define SC_GET_ATT_ALL           0x01
#define SC_SET_ATT_ALL           0x02
#define SC_GET_ATT_LIST          0x03
#define SC_SET_ATT_LIST          0x04
#define SC_RESET                 0x05
#define SC_START                 0x06
#define SC_STOP                  0x07
#define SC_CREATE                0x08
#define SC_DELETE                0x09
#define SC_MULT_SERV_PACK        0x0A
#define SC_APPLY_ATTRIBUTES      0x0D
#define SC_GET_ATT_SINGLE        0x0E
#define SC_SET_ATT_SINGLE        0x10
#define SC_FIND_NEXT_OBJ_INST    0x11
#define SC_RESTOR                0x15
#define SC_SAVE                  0x16
#define SC_NO_OP                 0x17
#define SC_GET_MEMBER            0x18
#define SC_SET_MEMBER            0x19
#define SC_INSERT_MEMBER         0x1A
#define SC_REMOVE_MEMBER         0x1B
#define SC_GROUP_SYNC            0x1C
        */

        public static ENIP_PACKET_TYPE ClassifyPacket(iTraveller traveller)
        {
            /* see if nature of packets can be derived from src/dst ports */
            /* if so, return as found */
            if (((ENIP_ENCAP_PORT == traveller.SourcePort && ENIP_ENCAP_PORT != traveller.DestinationPort)) ||
                ((ENIP_SECURE_PORT == traveller.SourcePort && ENIP_SECURE_PORT != traveller.DestinationPort)))
            {
                return ENIP_PACKET_TYPE.ENIP_RESPONSE_PACKET;
            }
            else if (((ENIP_ENCAP_PORT != traveller.SourcePort && ENIP_ENCAP_PORT == traveller.DestinationPort)) ||
                     ((ENIP_SECURE_PORT != traveller.SourcePort && ENIP_SECURE_PORT == traveller.DestinationPort)))
            {
                return ENIP_PACKET_TYPE.ENIP_REQUEST_PACKET;
            }
            else
            {
                return ENIP_PACKET_TYPE.ENIP_CANNOT_CLASSIFY;
            }
        }

        public static void ParseCommonFormat(byte[] data, int pos, Parse parse, iTraveller traveller)
        {
            var baseSet = new DataSetDefinition { Name = "Common Format" };
            var pds = ParsedDataSet.Create(baseSet);
            parse.ParsedData.Add(pds);

            var itemcount = CrackUInt16(data, pos);
            pos += 2;
            pds.ParsedFields.Add(ParsedField.Create("Item Count", itemcount));

            for(int i = 0; i < itemcount; i++)
            {
                var itemtype = (ENIP_CPF_Types)CrackUInt16(data, pos);
                pos += 2;
                pds.ParsedFields.Add(ParsedField.Create("Item Type", itemtype));

                var itemlength = CrackUInt16(data, pos);
                pos += 2;
                pds.ParsedFields.Add(ParsedField.Create("Item Length", itemlength));

                var startpos = pos;

                switch (itemtype)
                {
                    case ENIP_CPF_Types.CPF_ITEM_NULL:
                        break;                                        
                        
                    case ENIP_CPF_Types.CPF_ITEM_CONNECTED_DATA:
                        var cipseqno = CrackUInt16(data, pos);
                        pos += 2;
                        pds.ParsedFields.Add(ParsedField.Create("CIP Sequence Count", cipseqno));

                        // second disclaimer, doing a dreadful hackbodge for now where we just parse data based on IP, rather than identifying the right info from the connection manager init

                        //if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 10)
                        if (traveller.Source[0] == 192 && traveller.Source[1] == 168 && traveller.Source[2] == 1 && traveller.Source[3] == 30)
                        {
                            // from tpws

                            var cipioheader = CrackUInt32(data, pos);
                            pos += 4;
                            pds.ParsedFields.Add(ParsedField.Create("32-bit header", cipioheader));

                            //if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 101)
                            if (traveller.Destination[0] == 192 && traveller.Destination[1] == 168 && traveller.Destination[2] == 1 && traveller.Destination[3] == 101)
                            {
                                // to DMI left

                                var pd = CIP.TPWStoDMI.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                                parse.BackLinkIdentifier = "171";
                                parse.AutoGenerateDeltaFields = true;
                                parse.Name = "TPWS->DMI-L";
                            }
                            //else if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 102)
                            else if (traveller.Destination[0] == 192 && traveller.Destination[1] == 168 && traveller.Destination[2] == 1 && traveller.Destination[3] == 102)
                            {
                                // to DMI right

                                var pd = CIP.TPWStoDMI.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                                parse.BackLinkIdentifier = "181";
                                parse.AutoGenerateDeltaFields = true;
                                parse.Name = "TPWS->DMI-R";
                            }
                        }
                        //else if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 101)
                        else if (traveller.Source[0] == 192 && traveller.Source[1] == 168 && traveller.Source[2] == 1 && traveller.Source[3] == 101)
                        {
                            // from dmi left

                            var cipioheader = CrackUInt32(data, pos);
                            pos += 4;
                            pds.ParsedFields.Add(ParsedField.Create("32-bit header", cipioheader));

                            //if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 10)
                            if (traveller.Destination[0] == 192 && traveller.Destination[1] == 168 && traveller.Destination[2] == 1 && traveller.Destination[3] == 30)
                            {
                                // to tpws

                                var pd = CIP.DMItoTPWS.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                                parse.BackLinkIdentifier = "666";
                                parse.AutoGenerateDeltaFields = true;
                                parse.Name = "DMI-L->TPWS";
                            }
                        }
                        //else if (traveller.Source[0] == 172 && traveller.Source[1] == 27 && traveller.Source[2] == 43 && traveller.Source[3] == 102)
                        else if (traveller.Source[0] == 192 && traveller.Source[1] == 168 && traveller.Source[2] == 1 && traveller.Source[3] == 102)
                        {
                            // from dmi right

                            //if (traveller.Destination[0] == 172 && traveller.Destination[1] == 27 && traveller.Destination[2] == 43 && traveller.Destination[3] == 10)
                            if (traveller.Destination[0] == 192 && traveller.Destination[1] == 168 && traveller.Destination[2] == 1 && traveller.Destination[3] == 30)
                            {
                                // to tpws

                                var pd = CIP.DMItoTPWS.Parse(data, false, pos * 8);
                                parse.ParsedData.Add(pd);
                                parse.BackLinkIdentifier = "777";
                                parse.AutoGenerateDeltaFields = true;
                                parse.Name = "DMI-R->TPWS";
                            }
                        }
                        break;
                    case ENIP_CPF_Types.CPF_ITEM_UNCONNECTED_DATA:
                        var service = data[pos];
                        pos++;

                        // 0 is request, 1 is response
                        var reqrsp = Functions.GetBit(service, 0);

                        if(reqrsp)
                        {
                            pds.ParsedFields.Add(ParsedField.CreateError("Response type not implemented"));
                        }
                        else
                        {
                            var reqpathsize = data[pos] * 2; // the length is specified in words, which looks like they are 2 bytes
                            pos++;
                            // skipping the request data, it is quite involved
                            pos += reqpathsize;

                            if(service == 0x02)
                            {
                                // Set Attributes All

                                
                                var buffer = new byte[itemlength - reqpathsize - 2];
                                Array.Copy(data, pos, buffer, 0, buffer.Length);

                                pos += buffer.Length;

                                if (traveller.Source[0] == 192 && traveller.Source[1] == 168 && traveller.Source[2] == 1 && traveller.Source[3] == 30 &&
                                    traveller.Destination[0] == 192 && traveller.Destination[1] == 168 && traveller.Destination[2] == 1 && traveller.Destination[3] == 31)
                                {
                                    // TPWS to JRU

                                    var reversebuffer = Functions.ReverseBits(buffer);

                                    var ss27Parser = new SS27Parser();
                                    var ss27 = (SS27Packet)ss27Parser.ParseData(reversebuffer);

                                    parse.Name = "TPWS->JRU";
                                    parse.AutoGenerateDeltaFields = true;

                                    // TODO standardise, this is copied from the constructor of the JRU parser, duplication OMG

                                    if (ss27.Events.Count == 0)
                                    {
                                        // if there is no event, chuck some other data in there, maybe
                                        // ParsedData = new ParsedDataSet() { ParsedFields = new List<ParsedField>(ss27.Header) };
                                    }
                                    else
                                    {
                                        // TODO FIX THIS SO IT WORKS
                                        ss27.Events.ForEach(e =>
                                            parse.DisplayFields.Add(new DisplayField(e.EventType.ToString(),
                                                e.Description)));
                                    }

                                    if (ss27.Header != null) parse.ParsedData.Add(ss27.Header);
                                    if (ss27.SubMessage != null) parse.ParsedData.Add(ss27.SubMessage);
                                    if (ss27.ExtraMessages != null && ss27.ExtraMessages.Count > 0)
                                        parse.ParsedData.AddRange(ss27.ExtraMessages);

                                    parse.BackLinkIdentifier = ss27.MsgType.ToString();
                                }
                                else
                                {
                                    var hex = BitConverter.ToString(buffer);
                                    pds.ParsedFields.Add(ParsedField.Create("HEX", hex));
                                }

                                
                            }
                            else
                            {
                                pds.ParsedFields.Add(ParsedField.CreateError("Service type not implemented"));
                            }

                        }
                        
                        break;
                    
                    case ENIP_CPF_Types.CPF_ITEM_SEQUENCED_ADDRESS: // 1st item for: Class 0/1 connected data
                        if(itemlength != 8)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        var connid = CrackUInt32(data, pos);
                        pos += 4;
                        pds.ParsedFields.Add(ParsedField.Create("Connection ID", connid));
                        parse.BackLinkIdentifier = connid.ToString();

                        var seqno = CrackUInt32(data, pos);
                        pos += 4;
                        pds.ParsedFields.Add(ParsedField.Create("Encapsulation Sequence #", seqno));
                        // wireshark does more meta detection here in dissect_item_sequenced_address in terms of direction
                        break;

                    case ENIP_CPF_Types.CPF_ITEM_UNCONNECTED_MSG_DTLS: 
                    case ENIP_CPF_Types.CPF_ITEM_CONNECTED_ADDRESS:
                    case ENIP_CPF_Types.CPF_ITEM_CIP_IDENTITY:
                    case ENIP_CPF_Types.CPF_ITEM_CIP_SECURITY:
                    case ENIP_CPF_Types.CPF_ITEM_ENIP_CAPABILITY:
                    case ENIP_CPF_Types.CPF_ITEM_ENIP_USAGE:
                    case ENIP_CPF_Types.CPF_ITEM_LIST_SERVICES_RESP:
                    case ENIP_CPF_Types.CPF_ITEM_SOCK_ADR_INFO_OT:
                    case ENIP_CPF_Types.CPF_ITEM_SOCK_ADR_INFO_TO:
                    default:
                        pds.ParsedFields.Add(ParsedField.CreateError(itemtype + " not implemented"));
                        parse.DisplayFields.Add(new DisplayField("ERROR", itemtype + " not implemented"));
                        break;



                }

                if(startpos + itemlength != pos)
                {
                    pds.ParsedFields.Add(ParsedField.CreateError($"startpos {startpos} + itemlength {itemlength} != pos {pos}"));
                    pos = startpos + itemlength;
                }
            }

            


        }
    }

    public class CIPParser : IParser
    {
        public ProtocolType ProtocolType => ProtocolType.CIP;

        public Parse Extract(byte[] data, iPacket iPacket)
        {
            var parse = new Parse();
            var enip_packet_type = ClassifyPacket(iPacket as iTraveller);

            var baseSet = new DataSetDefinition { Name = "Industrial Ethernet" };
            var pds = ParsedDataSet.Create(baseSet);

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

            switch (function)
            {
                case ENIP_Function_Codes.NOP:
                    // not sure what to do with this, nothing? well, that's what wireshark does
                    break;

                case ENIP_Function_Codes.LIST_SERVICES:
                case ENIP_Function_Codes.LIST_IDENTITY:
                case ENIP_Function_Codes.LIST_INTERFACES:
                    if (enip_packet_type == ENIP_PACKET_TYPE.ENIP_RESPONSE_PACKET)
                    {
                        ParseCommonFormat(data, 24, parse, iPacket as iTraveller);
                    }
                    break;

                case ENIP_Function_Codes.REGISTER_SESSION:
                    // TODO two variables to parse here
                    goto default;
                case ENIP_Function_Codes.UNREGISTER_SESSION:
                    // nothing to parse here
                    break;
                case ENIP_Function_Codes.SEND_RR_DATA:
                case ENIP_Function_Codes.SEND_UNIT_DATA: // wireshark does a slight difference here opposed to RR, it sends a created subtree down to the parser, but not sure why. problem for another day!
                    var interface_handle = CrackUInt32(data, 24);
                    pds.ParsedFields.Add(ParsedField.Create("Interface Handle", interface_handle));
                    var timeout = CrackUInt16(data, 28);
                    pds.ParsedFields.Add(ParsedField.Create("Timeout", timeout));
                    ParseCommonFormat(data, 30, parse, iPacket as iTraveller);
                    break;

                case ENIP_Function_Codes.START_DTLS:
                    if (enip_packet_type == ENIP_PACKET_TYPE.ENIP_RESPONSE_PACKET)
                    {
                        // TODO implement
                    }
                    goto default;

                default:
                    pds.ParsedFields.Add(ParsedField.CreateError(function + " not implemented"));
                    parse.DisplayFields.Add(new DisplayField("ERROR", function + " not implemented"));
                    break;
            }
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

            ParseCommonFormat(data, pos, parse, iPacket as iTraveller);

            return parse;
        }
    }
}