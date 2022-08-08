using BitDataParser;
using IPTComShark.DataSets;
using PacketDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTComShark
{
    internal class ProfiPacket : PacketDotNet.Packet, iPacket, iTraveller
    {
        public ProfiPacket(byte[] data)
        {            
            ProtocolInfo = "profibuz"; // will be overwritten
            this.Header = new ByteArraySegment(data, 0, 7);

            var pos = 0;

            if (data[pos++] == 0x68)
            {
                // frame with variable data field length

                var length = data[pos++];
                DisplayFields.Add(new DisplayField("len", length));

                if (length != data[pos++])
                {
                    // fark!
                }
                if(length < 4)
                {
                    // fark!
                }
                if(0x68 != data[pos++])
                {
                    // fark!
                }

                var DA = new BitSet(data[pos++]);
                var SA = new BitSet(data[pos++]);

                bool DAextension = DA[0].Value;
                bool SAextension = SA[0].Value;
                var DAaddress = DA.GetField(1, 7);
                var SAaddress = SA.GetField(1, 7);

                this.Source = new byte[4];
                this.Destination = new byte[4];
                this.Source[0] = SAaddress;
                this.Destination[0] = DAaddress;
                

                var FC = new BitSet(data[pos++]);
                var fcRes = FC[0].Value;
                if(fcRes != false)
                {
                    // fark!
                }

                // 1 Request, Send/Request Frame
                // 0 Acknowledgement, Response Frame
                var fcFrameType = FC[1].Value;
                var fcFCB = FC[2].Value;
                var fcFCV = FC[3].Value;
                var fcFunc = FC.GetField(4, 4);

                // temp check to make sure functions align
                var temp = Functions.FieldGetter(data, 6 * 8 + 5, 4);
                if(temp != fcFunc)
                {
                    throw new Exception("Code breakdown!");
                }

                
                if (fcFrameType)
                {
                    var functionCode = "UNKNOWN";
                    switch (fcFunc)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 7:
                        case 8:
                        case 10:
                        case 11:
                            functionCode = "Reserved";
                            break;
                        case 3:
                            functionCode = "Send data ack low";
                            break;
                        case 4:
                            functionCode = "Send data no ack low";
                            break;
                        case 5:
                            functionCode = "Send data ack high";
                            break;
                        case 6:
                            functionCode = "send data no ack high";
                            break;                        
                        case 9:
                            functionCode = "Request FDL status with reply";
                            break;
                        case 12:
                            functionCode = "Send and Request Data low";
                            break;
                        case 13:
                            functionCode = "Send and Request Data high";
                            break;
                        case 14:
                            functionCode = "Request Ident with Reply";
                            break;
                        case 15:
                            functionCode = "Request LSAP Status with Reply";
                            break;

                    }
                    ProtocolInfo = "Req=" + functionCode;
                }
                else
                {
                    DisplayFields.Add(new DisplayField("type", "ack"));
                    ProtocolInfo = "Ack=" + fcFunc;
                    
                }
                

                // to keep track of how much of the data length the extension octets occupy
                var extensionLength = 0;
                if(DAextension)
                {
                    var ext1 = new BitSet(data[pos++]);
                    extensionLength++;

                    this.Destination[1] = ext1.GetField(1, 7);
                    if (ext1[0].Value)
                    {
                        var ext2 = new BitSet(data[pos++]);
                        extensionLength++;

                        this.Destination[2] = ext2.GetField(1, 7);
                        if (ext2[0].Value)
                        {
                            // not allowed?
                        }
                    }
                }
                if (SAextension)
                {
                    var ext1 = new BitSet(data[pos++]);
                    extensionLength++;

                    this.Source[1] = ext1.GetField(1, 7);
                    if (ext1[0].Value)
                    {
                        var ext2 = new BitSet(data[pos++]);
                        extensionLength++;

                        this.Source[2] = ext2.GetField(1, 7);
                        if (ext2[0].Value)
                        {
                            // not allowed?
                        }
                    }
                }

                

                // advertised length, minus DA,SA and FC octets, and whatever extension addresses were read
                var remain = length - 3 - extensionLength;
                var datablock = new byte[remain];

                for(int i = 0; i < remain; i++)
                {
                    datablock[i] = data[pos++];
                }

                //DisplayFields.Add(new DisplayField("data", BitConverter.ToString(datablock)));

                this.PayloadData = datablock;
                

                if (length - pos == -4)
                {
                    // header is 4 bytes, so we have reached end of data
                }
                else
                    DisplayFields.Add(new DisplayField("remaining data", length - pos));
            }



            

            



            
        }
                

        public string Name { get; set; }

        public string ProtocolInfo { get; set; }

        public List<DisplayField> DisplayFields { get; set; } = new List<DisplayField>();
        public ProtocolType Protocol { get => ProtocolType.Profibus; }
        public byte[] Source { get; set; }
        public byte[] Destination { get; set; }

        public string ASCII()
        {
            //return BitConverter.ToString(Header.ActualBytes()) + " || " +  ASCIIEncoding.ASCII.GetString(this.PayloadData);
            return ASCIIEncoding.ASCII.GetString(this.PayloadData);
        }
    }
}
