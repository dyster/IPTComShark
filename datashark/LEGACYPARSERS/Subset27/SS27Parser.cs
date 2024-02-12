using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using BitDataParser;
using TrainShark.DataSets;
using static BitDataParser.Functions;

namespace TrainShark.Parsers
{
    public class SS27Parser //: IDataParser
    {
        /// <summary>
        /// The currently used packet for parsing, useful for recovering a failed parsing
        /// </summary>
        public SS27Packet LastParsedPacket { get; private set; }

        public SS27Packet ParseData(byte[] data)
        {
            var parsed = new SS27Packet
            {
                //RawData = data
            };

            LastParsedPacket = parsed;


            var parsedHeader = Subset27.Header.Parse(data);

            var NID_MESSAGE = (byte) parsedHeader.GetField("NID_MESSAGE").Value;

            parsed.Name = "JRU-" + NID_MESSAGE;

            bool tryParse0 = Enum.TryParse(NID_MESSAGE.ToString(), out SS27MsgType typeEnum);
            parsed.MsgType = tryParse0 ? typeEnum : SS27MsgType.INVALID;


            var L_MESSAGE = (ushort) parsedHeader.GetField("L_MESSAGE").Value;

            //if (L_MESSAGE < data.Length)
            //{
            //    Logger.Log(NID_MESSAGE + ": More data in byte array then defined by L_MESSAGE", Severity.Info);
            //    var temp = new byte[data.Length - L_MESSAGE];
            //    Array.Copy(data, L_MESSAGE, temp, 0, temp.Length);
            //    Logger.Log("Garbage: " + BitConverter.ToString(temp), Severity.Info);
            //}
            if (L_MESSAGE > data.Length)
            {
                parsed.Header.ParsedFields.Add(ParsedField.CreateError("L_MESSAGE exceeds length of data"));
                return parsed;
            }


            DateTime dateTime = ExtractDateTime(data);
            parsedHeader.GetField("Date").Value = dateTime;
            parsed.DateTime = dateTime;

            var tempheader = new[]
            {
                parsedHeader.GetField("Q_SCALE"),
                parsedHeader.GetField("NID_LRBG"),
                parsedHeader.GetField("D_LRBG"),
                parsedHeader.GetField("Q_DIRLRBG"),
                parsedHeader.GetField("Q_DLRBG"),
                parsedHeader.GetField("L_DOUBTOVER"),
                parsedHeader.GetField("L_DOUBTUNDER"),
                parsedHeader.GetField("DRIVER_ID"),
                parsedHeader.GetField("NID_ENGINE"),
                parsedHeader.GetField("M_VERSION")
            };

            parsed.Header = parsedHeader;

            parsed.V_TRAIN = Convert.ToUInt16(parsedHeader.GetField("V_TRAIN").Value);

            parsed.Level = parsedHeader.GetField("M_LEVEL").Value.ToString();
            parsed.Mode = parsedHeader.GetField("M_MODE").Value.ToString();

            //var LEVEL = (ushort) parsedDataSet.DataDictionary["LEVEL"];
            //bool tryParse1 = Enum.TryParse(LEVEL.ToString(), out SS27Level levelEnum);
            //parsed.Level = tryParse1 ? levelEnum : SS27Level.INVALID;
            //
            //
            //var MODE = (ushort) parsedDataSet.DataDictionary["MODE"];
            //bool tryParse2 = Enum.TryParse(MODE.ToString(), out SS27Mode modeEnum);
            //parsed.Mode = tryParse2 ? modeEnum : SS27Mode.INVALID;

            // payload then starts at bit 309
            int payloadlength = L_MESSAGE * 8 - 308;

            // copy into new array for convenience
            byte[] payload = SubArrayGetterX(data, 309, payloadlength);

            


            DoPackets(typeEnum, parsed, payload);

            return parsed;
        }

        public class Position
        {
            public int BitPosition { get; set; } = 1;
            public void Increment()
            {
                BitPosition++;
            }
            public void Add(int value)
            {
                BitPosition += value;
            }
        }

        public static void DoPackets(SS27MsgType typeEnum, SS27Packet parsed, byte[] payload)
        {
            var position = new Position();

            switch (typeEnum)
            {
                case SS27MsgType.General:
                    break;
                case SS27MsgType.TRAINDATA: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    parsed.Events.Add(new ETCSEvent("Train Data entered"));
                    goto default;
                case SS27MsgType.EMERGENCYBRAKECOMMANDSTATE:
                    parsed.SubMessage = Subset27.EBrakeCommandState.Parse(payload);
                    if ((bool) parsed.SubMessage.GetField("M_BRAKE_COMMAND_STATE").Value)
                        parsed.Events.Add(new ETCSEvent("Emergency Brake Applied", ETCSEventType.Main));
                    else
                        parsed.Events.Add(new ETCSEvent("Emergency Brake Released", ETCSEventType.Main));


                    break;
                case SS27MsgType.SERVICEBRAKECOMMANDSTATE:
                    parsed.SubMessage = Subset27.SBrakeCommandState.Parse(payload);
                    if ((bool) parsed.SubMessage.GetField("M_BRAKE_COMMAND_STATE").Value)
                        parsed.Events.Add(new ETCSEvent("Service Brake Applied", ETCSEventType.Main));
                    else
                        parsed.Events.Add(new ETCSEvent("Service Brake Released", ETCSEventType.Main));


                    break;
                case SS27MsgType.MESSAGETORADIOINFILLUNIT:
                    parsed.SubMessage = Subset27.MessageToRadioInfillUnit.Parse(payload);
                    break;
                case SS27MsgType.MESSAGEFROMRADIOINFILLUNIT:
                    parsed.SubMessage = Subset27.MessageFromRadioInfillUnit.Parse(payload);
                    break;
                case SS27MsgType.TELEGRAMFROMBALISE:                    
                    var dataSet = Subset26.BaliseHeader.Parse(payload);
                    var nidbg = dataSet.GetField("NID_BG").Value;
                    var npig = dataSet.GetField("N_PIG").Value;
                    parsed.Events.Add(new ETCSEvent($"Balise {nidbg}:{npig} passed", ETCSEventType.Wayside));
                    position.Add(dataSet.BitsRead);
                    dataSet.ParsedFields.Add(ParsedField.Create(new BitField {Name = "BalisePayload"},
                        BitConverter.ToString(payload)));

                    parsed.SubMessage = dataSet;
                    GoDoPackets(payload, parsed, true, position);
                    break;
                case SS27MsgType.MESSAGEFROMEUROLOOP: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.MESSAGEFROMRBC:
                    GoParseRadioMessage(payload, parsed);
                    break;
                case SS27MsgType.MESSAGETORBC:
                    GoParseRadioMessage(payload, parsed);
                    break;
                case SS27MsgType.DRIVERSACTIONS:
                    parsed.SubMessage = Subset27.DriversActions.Parse(payload);
                    parsed.Events.Add(new ETCSEvent("Driver Action: " + parsed.SubMessage.ParsedFields.First().Value,
                        ETCSEventType.Main));
                    break;
                case SS27MsgType.BALISEGROUPERROR:
                    parsed.SubMessage = Subset27.BaliseGroupError.Parse(payload);
                    parsed.Events.Add(new ETCSEvent("Balise Error: " + parsed.SubMessage.GetField("M_ERROR").Value,
                        ETCSEventType.Failure));
                    break;
                case SS27MsgType.RADIOERROR:
                    parsed.SubMessage = Subset27.RadioError.Parse(payload);
                    parsed.Events.Add(new ETCSEvent("Radio Error: " + parsed.SubMessage.GetField("M_ERROR").Value,
                        ETCSEventType.Failure));
                    break;
                case SS27MsgType.STMINFORMATION: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.INFORMATIONFROMCOLDMOVEMENTDETECTOR:
                    parsed.SubMessage = Subset27.InformationFromColdMovementDetector.Parse(payload);
                    break;
                case SS27MsgType.STARTDISPLAYINGFIXEDTEXTMESSAGE:
                    parsed.SubMessage = Subset27.StartDisplayingFixedTextMessage.Parse(payload);
                    parsed.Events.Add(
                        new ETCSEvent("Fixed Text Message: " + parsed.SubMessage.ParsedFields.First().Value));
                    break;
                case SS27MsgType.STOPDISPLAYINGFIXEDTEXTMESSAGE:
                    parsed.SubMessage = Subset27.StopDisplayingFixedTextMessage.Parse(payload);
                    break;
                case SS27MsgType.STARTDISPLAYINGPLAINTEXTMESSAGE:
                    parsed.SubMessage = Subset27.StartDisplayingPlainTextMessage.Parse(payload);
                    var textmsg = parsed.SubMessage.GetField("X_TEXT").Value.ToString();
                    var regex = new Regex(@"EVC Message#(\d+)");
                    if (regex.IsMatch(textmsg))
                    {
                        int txtid = int.Parse(regex.Match(textmsg).Groups[1].Value);
                        if (VSIS210.MMI_Q_TEXT.LookupTable.ContainsKey(regex.Match(textmsg).Groups[1].Value))
                        {
                            parsed.Events.Add(new ETCSEvent("Plain Text: " +
                                                            VSIS210.MMI_Q_TEXT.LookupTable[
                                                                regex.Match(textmsg).Groups[1].Value]));
                        }
                        else
                        {
                            parsed.Events.Add(new ETCSEvent("Plain Text: Unknown EVC Text -> " + textmsg));
                        }
                    }
                    else
                    {
                        parsed.Events.Add(new ETCSEvent("Plain Text: " + textmsg));
                    }


                    break;
                case SS27MsgType.STOPDISPLAYINGPLAINTEXTMESSAGE:
                    parsed.SubMessage = Subset27.StopDisplayingPlainTextMessage.Parse(payload);
                    break;
                case SS27MsgType.SPEEDANDDISTANCEMONITORINGINFORMATION:
                    parsed.SubMessage = Subset27.SpeedAndDistanceMonitoringInformation.Parse(payload);
                    break;
                case SS27MsgType.DMISYMBOLSTATUS:
                    parsed.SubMessage = Subset27.DmiSymbolStatus.Parse(payload);

                    if (parsed.SubMessage.ParsedFields.Count > 0)
                    {
                        parsed.Events.Add(new ETCSEvent("DMI Symbol: " +
                                                        string.Join(", ",
                                                            parsed.SubMessage.ParsedFields.Select(p => p.Name))));
                    }

                    goto default;
                case SS27MsgType.DMISOUNDSTATUS:
                    parsed.SubMessage = Subset27.DmiSoundStatus.Parse(payload);
                    if ((bool) parsed.SubMessage.GetField("Sound Overspeed").Value)
                        parsed.Events.Add(new ETCSEvent("Sound played: Overspeed"));
                    if ((bool) parsed.SubMessage.GetField("Sound Warning").Value)
                        parsed.Events.Add(new ETCSEvent("Sound played: Warning"));
                    break;
                case SS27MsgType.DMISYSTEMSTATUSMESSAGE:
                    parsed.SubMessage = Subset27.DmiSystemStatusMessage.Parse(payload);
                    if (parsed.SubMessage.ParsedFields.Count > 0)
                        parsed.Events.Add(new ETCSEvent("System Status Message: " +
                                                        string.Join(", ",
                                                            parsed.SubMessage.ParsedFields.Select(p => p.Name))));
                    break;
                case SS27MsgType.ADDITIONALDATA:
                    parsed.SubMessage = Subset27.AdditionalData.Parse(payload);
                    goto default;
                case SS27MsgType.SRSPEEDDISTANCEENTEREDBYTHEDRIVER: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.NTCSELECTED:
                    parsed.SubMessage = Subset27.NtcSelected.Parse(payload);
                    parsed.Events.Add(new ETCSEvent("NTC selected: " + parsed.SubMessage.ParsedFields.First().Value));
                    break;
                case SS27MsgType.SAFETYCRITICALFAULTINMODESLNLORPS:
                    // does not contain any data
                    parsed.Events.Add(
                        new ETCSEvent("Safety Critical Fault in mode SL, NL or PS", ETCSEventType.Failure));
                    break;
                case SS27MsgType.VIRTUALBALISECOVERSETBYTHEDRIVER: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.VIRTUALBALISECOVERREMOVEDBYTHEDRIVER: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.SLEEPINGINPUT:
                    parsed.SubMessage = Subset27.SleepingInput.Parse(payload);
                    parsed.Events.Add(new ETCSEvent("Sleeping Input: " + parsed.SubMessage.ParsedFields.First().Value));
                    break;
                case SS27MsgType.PASSIVESHUNTINGINPUT: //TODO implement
                    parsed.SubMessage = Subset27.PassiveShuntingInput.Parse(payload);
                    goto default;
                case SS27MsgType.NONLEADINGINPUT: //TODO implement
                    parsed.SubMessage = Subset27.NonLeadingInput.Parse(payload);
                    goto default;
                case SS27MsgType.REGENERATIVEBRAKESTATUS: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.MAGNETICSHOEBRAKESTATUS: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.EDDYCURRENTBRAKESTATUS: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.ELECTROPNEUMATICBRAKESTATUS: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.ADDITIONALBRAKESTATUS: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.CABSTATUS:
                    parsed.SubMessage = Subset27.CabStatus.Parse(payload);
                    if ((bool) parsed.SubMessage.ParsedFields.First().Value)
                        parsed.Events.Add(new ETCSEvent("Cab Activated", ETCSEventType.Main));
                    else
                        parsed.Events.Add(new ETCSEvent("Cab Deactivated", ETCSEventType.Main));
                    break;
                case SS27MsgType.DIRECTIONCONTROLLERPOSITION:
                    parsed.SubMessage = Subset27.DirectionControllerPosition.Parse(payload);
                    parsed.Events.Add(
                        new ETCSEvent("Direction Controller: " + parsed.SubMessage.ParsedFields.First().Value));
                    break;
                case SS27MsgType.TRACTIONSTATUS: // TODO implement correctly
                    parsed.SubMessage = Subset27.CabStatus.Parse(payload);
                    break;
                case SS27MsgType.TYPEOFTRAINDATA:
                    parsed.SubMessage = Subset27.TypeOfTrainDataEntry.Parse(payload);
                    break;
                case SS27MsgType.NATIONALSYSTEMISOLATION:
                    parsed.SubMessage = Subset27.NationalSystemIsolation.Parse(payload);
                    break;
                case SS27MsgType.TRACTIONCUTOFFCOMMANDSTATE:
                    parsed.SubMessage = Subset27.TCOState.Parse(payload);
                    parsed.Events.Add(
                        new ETCSEvent("Traction Cut-Off: " + parsed.SubMessage.ParsedFields.First().Value));
                    break;
                case SS27MsgType.LOWESTSUPERVISEDSPEEDWITHINTHEMOVEMENTAUTHORITY: //TODO implement
                    parsed.SubMessage = ParsedDataSet.CreateError("Not Implemented");
                    goto default;
                case SS27MsgType.PROPRIETARY:
                    parsed.SubMessage = Proprietary.PropJRU.Parse(payload);
                    position.Add(parsed.SubMessage.BitsRead);
                    
                    var subMsgNr = parsed.SubMessage.GetField("SubMsgNr");


                    if (subMsgNr != null)
                    {
                        if ((ushort) subMsgNr.TrueValue == 3) // NTC Data
                        {
                            // NTC_DATA
                            var l_message = (ushort) parsed.SubMessage.GetField("L_MESSAGE").Value;

                            var remain = SubArrayGetter(payload, position.BitPosition);

                            var packetid = parsed.SubMessage.GetField("NID_PACKET");

                            if ((ushort) packetid.TrueValue == 15)
                            {
                                parsed.ExtraMessages.Add(Proprietary.STM15.Parse(remain));
                                position.Add(parsed.ExtraMessages.Last().BitsRead);                                
                            }
                            else if ((ushort) packetid.TrueValue == 161)
                            {
                                parsed.ExtraMessages.Add(Proprietary.STM161.Parse(remain));
                                position.Add(parsed.ExtraMessages.Last().BitsRead);
                            }

                            if (payload.Length * 8 - position.BitPosition > 8)
                            {
                                uint secondPacketId = FieldGetter(payload, position.BitPosition, 8);
                                position.Add(8);

                                var remain2 = SubArrayGetter(payload, position.BitPosition);

                                if (secondPacketId == 15)
                                {
                                    parsed.ExtraMessages.Add(Proprietary.STM15.Parse(remain2));
                                    position.Add(parsed.ExtraMessages.Last().BitsRead);
                                }
                                else if (secondPacketId == 161)
                                {
                                    parsed.ExtraMessages.Add(Proprietary.STM161.Parse(remain2));
                                    position.Add(parsed.ExtraMessages.Last().BitsRead);
                                }
                            }
                        }
                        else if ((ushort)subMsgNr.TrueValue == 6) // ABDO
                        {
                            parsed.Events.Add(new ETCSEvent("ABDO"));
                        }
                    }

                    // TODO fix this, it is in extramessages now
                    if (parsed.SubMessage.GetField("AWS South Pole Detection") != null)
                    {
                        var awsstring = new List<string>();

                        if ((bool) parsed.SubMessage.GetField("AWS South Pole Detection").Value)
                            awsstring.Add("South Pole");
                        //if ((bool)parsed.SubMessage.GetField("AWS North Pole Detection").Value)
                        //    awsstring.Add("North Pole");
                        if ((bool) parsed.SubMessage.GetField("AWS Reset").Value)
                            awsstring.Add("Reset");
                        if ((bool) parsed.SubMessage.GetField("F1_Tone").Value)
                            awsstring.Add("F1");
                        if ((bool) parsed.SubMessage.GetField("F2_Tone").Value)
                            awsstring.Add("F2");
                        if ((bool) parsed.SubMessage.GetField("F3_Tone").Value)
                            awsstring.Add("F3");
                        if ((bool) parsed.SubMessage.GetField("F4_Tone").Value)
                            awsstring.Add("F4");
                        if ((bool) parsed.SubMessage.GetField("F5_Tone").Value)
                            awsstring.Add("F5");
                        if ((bool) parsed.SubMessage.GetField("F6_Tone").Value)
                            awsstring.Add("F6");

                        if (awsstring.Count > 0)
                            parsed.Events.Add(new ETCSEvent("TPWS: " + string.Join(", ", awsstring),
                                ETCSEventType.Wayside));
                    }


                    break;
                case SS27MsgType.ParseError:
                case SS27MsgType.INVALID:
                default:
                    //parsed.DictionaryData = null;
                    parsed.PayLoad = payload;
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="payload1"></param>
        /// <param name="position"></param>
        /// <param name="parsed"></param>
        /// <returns></returns>
        static bool RickRoll(DataSetDefinition dataset, byte[] payload1, Position position, SS27Packet parsed)
        {
            var parsedDataSet = dataset.Parse(SubArrayGetter(payload1, position.BitPosition));

            parsed.ExtraMessages.Add(parsedDataSet);
            position.Add(parsedDataSet.BitsRead);

            if (parsedDataSet.ParsedFields.Any(field => field.Name == "L_PACKET"))
            {
                var L_PACKET =
                    Convert.ToInt32(parsedDataSet.ParsedFields.Last(field => field.Name == "L_PACKET").Value);
                if (L_PACKET != parsedDataSet.BitsRead)
                {
                    parsedDataSet.ParsedFields.Add(ParsedField.CreateError("The number of bits read does not match the value of L_PACKET"));                    
                    return false;
                }
            }

            /* Messages are not fully parsed by a dataset, so this will never add upp
            if (parsedDataSet.ParsedFields.Any(field => field.Name == "L_MESSAGE"))
            {
                var L_MESSAGE = Convert.ToInt32(parsedDataSet.ParsedFields.First(field => field.Name == "L_MESSAGE").Value);
                if (L_MESSAGE != parsedDataSet.BitsRead * 8)
                {
                    throw new InvalidDataException("The number of bits read does not match the value of L_MESSAGE");
                }
            }*/


            // assign the enum instead of the int
            //parsedDataSet.ParsedFields.First(field => field.Name == "NID_PACKET").Value = toTrain;

            
            return true;
        }

        private static void GoDoPackets(byte[] payload, SS27Packet parsed, bool trackToTrain, Position position)
        {
            int infiniteloop = 0;

            // smallest possible message is 14, leaves a gap between 14 and 16 bits which leads to the extra check after id has been determined below
            while (payload.Length * 8 - position.BitPosition >= 14)
            {
                if (infiniteloop++ > 500)
                {
                    throw new Exception("Infinite loop");
                }

                // peek without moving pointer
                ushort id = BitConverter.ToUInt16(SubArrayGetter(payload, position.BitPosition, 8, 2).ToArray(), 0);
                if (id == 255)
                    return;

                int remain = payload.Length * 8 - position.BitPosition;
                if (trackToTrain && id != 0 && remain <= 23)
                {
                    // not enough data left for track to train messages that are not VBC marker
                    return;
                }

                if (!trackToTrain && remain <= 29)
                {
                    // not enough data left for train to track messages
                    return;
                }

                var currentPointer = position.BitPosition;

                void DieDie()
                {
                    parsed.ExtraMessages.Add(ParsedDataSet.CreateError(
                        $"Packet {id} is not implemented, {payload.Length * 8 - (currentPointer - 1)} bits follow"));
                }

                if (trackToTrain)
                {
                    var toTrain = (SS26PacketTrackToTrain) id;

                    // copy ref value for use in anonymous method
                    //var currentPointer = pointer;


                    switch (toTrain)
                    {
                        case SS26PacketTrackToTrain.VirtualBaliseCoverMarker:
                            if (!RickRoll(Subset26.Packet0VirtualBaliseCoverMarker, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Virtual Balise Cover Marker",
                                ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.SystemVersionOrder:
                            if (!RickRoll(Subset26.Packet2SystemVersionOrder, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.NationalValues:
                            if (!RickRoll(Subset26.Packet3NationalValues, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: National Values", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.Linking:
                            if (!RickRoll(Subset26.Packet5Linking, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Linking Info", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.VirtualBaliseCoverOrder:
                            if (!RickRoll(Subset26.Packet6VirtualBaliseCoverOrder, payload, position, parsed))
                                return;
                            parsed.Events.Add(
                                new ETCSEvent("Packet: Virtual Balise Cover Order", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.Level1MovementAuthority:
                            if (!RickRoll(Subset26.Packet12Level1MovementAuthority, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.StaffResponsibleDistanceInformationFromLoop:
                            parsed.Events.Add(new ETCSEvent("Packet: Staff Responsible Distance Information From Loop",
                                ETCSEventType.Wayside));
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.Level23MovementAuthority:
                            if (!RickRoll(Subset26.Packet15Level23MovementAuthority, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.RepositioningInformation:
                            if (!RickRoll(Subset26.Packet16RepositioningInformation, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Repositioning Information",
                                ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.GradientProfile:
                            if (!RickRoll(Subset26.Packet21GradientProfile, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Gradient Profile", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.InternationalStaticSpeedProfile:
                            if (!RickRoll(Subset26.Packet27InternationalStaticSpeedProfile, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: International Static Speed Profile",
                                ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.TrackConditionChangeOfTractionSystem:
                            parsed.Events.Add(new ETCSEvent("Packet: TC Change of Tracion System",
                                ETCSEventType.Wayside));
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.TrackConditionChangeOfAllowedCurrentConsumption:
                            if (!RickRoll(Subset26.Packet40TrackConditionChangeOfAllowedCurrentConsumption,
                                payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: TC Change of allowed current consumption",
                                ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.LevelTransitionOrder:
                            if (!RickRoll(Subset26.Packet41LevelTransitionOrder, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Level Transition Order", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.SessionManagement:
                            if (!RickRoll(Subset26.Packet42SessionManagement, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent(
                                "Packet: Session Management: " + parsed.ExtraMessages.Last().GetField("Q_RBC").Value,
                                ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.DataUsedByApplicationsOutsideTheErtmsEtcsSystem:

                            // Skip the rickroll function since Packet44 dataset doesn't match the full length of L_PACKET
                            var parsedDataSet = Subset26.Packet44TrackToTrain.Parse(SubArrayGetter(payload, position.BitPosition));
                            position.Add(parsedDataSet.BitsRead);
                            
                            parsed.ExtraMessages.Add(parsedDataSet);
                            var fourfourlength = (UInt16) parsed.ExtraMessages.Last().ParsedFields
                                .Last(f => f.Name == "L_PACKET").Value;

                            parsed.ExtraMessages.Add(new ParsedDataSet
                            {
                                ParsedFields = new List<ParsedField>
                                {
                                    ParsedField.Create("Packet44 Payload",
                                        BitConverter.ToString(SubArrayGetterX(payload, position.BitPosition, fourfourlength - 32)))
                                }
                            });
                            position.Add(fourfourlength - 32);                            
                            break;
                        case SS26PacketTrackToTrain.RadioNetworkRegistration:
                            if (!RickRoll(Subset26.Packet45RadioNetworkRegistration, payload, position, parsed))
                                return;
                            parsed.Events.Add(
                                new ETCSEvent("Packet: Radio Network Registration", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.ConditionalLevelTransitionOrder:
                            if (!RickRoll(Subset26.Packet46ConditionalLevelTransitionOrder, payload, position,
                                parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Conditional Level Transition Order",
                                ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.ListOfBalisesForShArea:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.AxleLoadSpeedProfile:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.PermittedBrakingDistanceInformation:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.MovementAuthorityRequestParameters:
                            if (!RickRoll(Subset26.Packet57MovementAuthorityRequestParameters, payload, position,
                                parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.PositionReportParameters:
                            if (!RickRoll(Subset26.Packet58PositionReportParameters, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.ListOfBalisesInSrAuthority:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.InhibitionOfRevocableTsrsFromBalisesInL23:
                            if (!RickRoll(Subset26.Packet64InhibitionOfRevocableTsrsFromBalisesInL23, payload,
                                position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.TemporarySpeedRestriction:
                            if (!RickRoll(Subset26.Packet65TemporarySpeedRestriction, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Temporary Speed Restriction", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.TemporarySpeedRestrictionRevocation:
                            if (!RickRoll(Subset26.Packet66TemporarySpeedRestrictionRevocation, payload, position,
                                parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Temporary Speed Restriction Revocation",
                                ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.TrackConditionBigMetalMasses:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.TrackCondition:
                            if (!RickRoll(Subset26.Packet68TrackCondition, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Track Conditions", ETCSEventType.Wayside));
                            return;
                            break;
                        case SS26PacketTrackToTrain.TrackConditionStationPlatforms:
                            parsed.Events.Add(new ETCSEvent("Packet: Track Condition Station Platform",
                                ETCSEventType.Wayside));
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.RouteSuitabilityData:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.AdhesionFactor:
                            if (!RickRoll(Subset26.Packet71AdhesionFactor, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Adhesion Factor", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.PacketForSendingPlainTextMessages:
                            if (!RickRoll(Subset26.Packet72PacketForSendingPlainTextMessages, payload, position,
                                parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Plain Text Message", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.PacketForSendingFixedTextMessages:
                            parsed.Events.Add(new ETCSEvent("Packet: Fixed Text Message", ETCSEventType.Main));
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.GeographicalPositionInformation:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.ModeProfile:
                            if (!RickRoll(Subset26.Packet80ModeProfile, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Mode Profile", ETCSEventType.Wayside));
                            break;
                        case SS26PacketTrackToTrain.LevelCrossingInformation:
                            parsed.Events.Add(new ETCSEvent("Packet: Level Crossing Information", ETCSEventType.Main));
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.TrackAheadFreeUpToLevel23TransitionLocation:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrackToTrain.RbcTransitionOrder:
                            if (!RickRoll(Subset26.Packet131RbcTransitionOrder, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: RBC Transition Order", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.DangerForShuntingInformation:
                            if (!RickRoll(Subset26.Packet132DangerForShuntingInformation, payload, position,
                                parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.RadioInfillAreaInformation:
                            if (!RickRoll(Subset26.Packet133RadioInfillAreaInformation, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.EolmPacket:
                            if (!RickRoll(Subset26.Packet134EolmPacket, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.StopShuntingOnDeskOpening:
                            if (!RickRoll(Subset26.Packet135StopShuntingOnDeskOpening, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.InfillLocationReference:
                            if (!RickRoll(Subset26.Packet136InfillLocationReference, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.StopIfInStaffResponsible:
                            if (!RickRoll(Subset26.Packet137StopIfInStaffResponsible, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Stop if in SR", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.ReversingAreaInformation:
                            if (!RickRoll(Subset26.Packet138ReversingAreaInformation, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.ReversingSupervisionInformation:
                            if (!RickRoll(Subset26.Packet139ReversingSupervisionInformation, payload, position,
                                parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.TrainRunningNumberFromRbc:
                            if (!RickRoll(Subset26.Packet140TrainRunningNumberFromRbc, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: TRN from RBC", ETCSEventType.Main));
                            break;
                        case SS26PacketTrackToTrain.DefaultGradientForTemporarySpeedRestriction:
                            if (!RickRoll(Subset26.Packet141DefaultGradientForTemporarySpeedRestriction, payload,
                                position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.SessionManagementWithNeighbouringRadioInfillUnit:
                            if (!RickRoll(Subset26.Packet143SessionManagementWithNeighbouringRadioInfillUnit,
                                payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.InhibitionOfBaliseGroupMessageConsistencyReaction:
                            if (!RickRoll(Subset26.Packet145InhibitionOfBaliseGroupMessageConsistencyReaction,
                                payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.LssmaDisplayToggleOrder:
                            if (!RickRoll(Subset26.Packet180LssmaDisplayToggleOrder, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.GenericLsFunctionMarker:
                            if (!RickRoll(Subset26.Packet181GenericLsFunctionMarker, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrackToTrain.DefaultBaliseLoopOrRiuInformation:
                            if (!RickRoll(Subset26.Packet254DefaultBaliseLoopOrRiuInformation, payload, position,
                                parsed))
                                return;
                            break;
                        default:
                            parsed.ExtraMessages.Add(ParsedDataSet.CreateError($"Packet {id} is UNKNOWN, data: " +
                                                                               BitConverter.ToString(
                                                                                   SubArrayGetter(payload, position.BitPosition))));

                            return;
                            break;
                    }
                }
                else
                {
                    var toTrack = (SS26PacketTrainToTrack) id;

                    switch (toTrack)
                    {
                        case SS26PacketTrainToTrack.PositionReport:
                            if (!RickRoll(Subset26.Packet0PositionReport, payload, position, parsed))
                                return;
                            break;
                        case SS26PacketTrainToTrack.PositionReportBasedOnTwoBaliseGroups:
                            if (!RickRoll(Subset26.Packet1PositionReportBasedOnTwoBaliseGroups, payload, position,
                                parsed))
                                return;
                            break;
                        case SS26PacketTrainToTrack.OnboardTelephoneNumbers:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrainToTrack.ErrorReporting:
                            if (!RickRoll(Subset26.Packet4ErrorReporting, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Error Report", ETCSEventType.Main));
                            break;
                        case SS26PacketTrainToTrack.TrainRunningNumber:
                            if (!RickRoll(Subset26.Packet5TrainRunningNumber, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: TRN", ETCSEventType.Main));
                            break;
                        case SS26PacketTrainToTrack.Level23TransitionInformation:
                            if (!RickRoll(Subset26.Packet9Level23TransitionInformation, payload, position, parsed))
                                return;
                            parsed.Events.Add(new ETCSEvent("Packet: Level 2/3 Transition Info", ETCSEventType.Main));
                            break;
                        case SS26PacketTrainToTrack.ValidatedTrainData:
                            DieDie();
                            return;
                            break;
                        case SS26PacketTrainToTrack.DataUsedByApplicationsOutsideTheErtmsEtcsSystem:
                            DieDie();
                            return;
                            break;
                        default:
                            parsed.ExtraMessages.Add(ParsedDataSet.CreateError($"Packet {id} is UNKNOWN, data: " +
                                                                               BitConverter.ToString(
                                                                                   SubArrayGetter(payload, position.BitPosition))));

                            return;
                            break;
                    }
                }
            }
        }

        private static void GoParseRadioMessage(byte[] payload, SS27Packet parsed)
        {            
            var position = new Position();

            var header = new DataSetDefinition
            {
                BitFields = new List<BitField>
                {
                    Subset26.NID_C,
                    Subset26.NID_RBC
                }
            };

            //ParsedField nidMessage = fields.First(head => head.Name == "NID_MESSAGE");
            //Enum.TryParse(nidMessage.Value.ToString(), out SS26RadioMessageType msgType);
            //nidMessage.Value = msgType;

            ParsedDataSet parsedDataSet = header.Parse(payload);
            parsed.SubMessage = parsedDataSet;

            position.Add(parsedDataSet.BitsRead);

            // peek
            ushort NID_MESSAGE = BitConverter.ToUInt16(SubArrayGetter(payload, position.BitPosition, 8, 2).ToArray(), 0);
            var msgType = (SS26RadioMessageType) NID_MESSAGE;


            switch (msgType)
            {
                // From RBC, or Track To Train
                case SS26RadioMessageType.SrAuthorisation:
                    if (!RickRoll(Subset26.Message2SRAuthorisation, payload, position, parsed))
                        return;

                    var qscale = Convert.ToUInt32(parsed.ExtraMessages.Last().GetField("Q_SCALE").Value);
                    var dsr = Convert.ToUInt32(parsed.ExtraMessages.Last().GetField("D_SR").Value);
                    parsed.Events.Add(new ETCSEvent(
                        "RBC: SR Authorisation for " + Subset26.ApplyQScale(dsr, qscale) + " m", ETCSEventType.Main));

                    GoDoPackets(payload, parsed, true, position);

                    break;
                case SS26RadioMessageType.MovementAuthority:
                    if (!RickRoll(Subset26.Message3MovementAuthority, payload, position, parsed))
                        return;
                    if (!RickRoll(Subset26.Packet15Level23MovementAuthority, payload, position, parsed))
                        return;

                    parsed.Events.Add(new ETCSEvent("RBC: MA-> " + AnalyseMA(parsed.ExtraMessages.Last())));

                    GoDoPackets(payload, parsed, true, position);
                    break;


                case SS26RadioMessageType.RecognitionOfExitFromTripMode:
                    if (!RickRoll(Subset26.Message6RecognitionOfExitFromTRIPMode, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Recognition of Exit from Trip Mode"));
                    break;

                case SS26RadioMessageType.AcknowledgementOfTrainData:
                    if (!RickRoll(Subset26.Message8AcknowledgementOfTrainData, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Ack of Train Data"));
                    break;

                case SS26RadioMessageType.RequestToShortenMa:
                    if (!RickRoll(Subset26.Message9RequestToShortenMA, payload, position, parsed))
                        return;
                    if (!RickRoll(Subset26.Packet15Level23MovementAuthority, payload, position, parsed))
                        return;

                    parsed.Events.Add(new ETCSEvent("RBC: Shorten MA-> " + AnalyseMA(parsed.ExtraMessages.Last())));

                    GoDoPackets(payload, parsed, true, position);
                    break;

                case SS26RadioMessageType.ConditionalEmergencyStop:
                    if (!RickRoll(Subset26.Message15ConditionalEmergencyStop, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Conditional Emergency Stop", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.UnconditionalEmergencyStop:
                    if (!RickRoll(Subset26.Message16UnconditionalEmergencyStop, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Unconditional Emergency Stop", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.RevocationOfEmergencyStop:
                    if (!RickRoll(Subset26.Message18RevocationOfEmergencyStop, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Revocation of Emergency Stop", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.GeneralMessage:
                    if (!RickRoll(Subset26.Message24GeneralMessage, payload, position, parsed))
                        return;
                    GoDoPackets(payload, parsed, true, position);
                    break;

                case SS26RadioMessageType.ShRefused:
                    if (!RickRoll(Subset26.Message27SHRefused, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: SH Refused", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.ShAuthorised:
                    if (!RickRoll(Subset26.Message28SHAuthorised, payload, position, parsed))
                        return;
                    GoDoPackets(payload, parsed, true, position);
                    parsed.Events.Add(new ETCSEvent("RBC: SH Authorised", ETCSEventType.Main));

                    break;

                case SS26RadioMessageType.RbcRiuSystemVersion:
                    if (!RickRoll(Subset26.Message32RBCRIUSystemVersion, payload, position, parsed))
                        return;
                    break;

                case SS26RadioMessageType.MaWithShiftedLocationReference:
                    if (!RickRoll(Subset26.Message33MAWithShiftedLocationReference, payload, position, parsed))
                        return;
                    if (!RickRoll(Subset26.Packet15Level23MovementAuthority, payload, position, parsed))
                        return;

                    parsed.Events.Add(new ETCSEvent("RBC: Shifted MA-> " + AnalyseMA(parsed.ExtraMessages.Last())));

                    GoDoPackets(payload, parsed, true, position);
                    break;

                case SS26RadioMessageType.TrackAheadFreeRequest:
                    if (!RickRoll(Subset26.Message34TrackAheadFreeRequest, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Track Ahead Free Request"));
                    break;

                case SS26RadioMessageType.InfillMa:
                    if (!RickRoll(Subset26.Message37InfillMA, payload, position, parsed))
                        return;
                    if (!RickRoll(Subset26.Packet136InfillLocationReference, payload, position, parsed))
                        return;
                    if (!RickRoll(Subset26.Packet12Level1MovementAuthority, payload, position, parsed))
                        return;

                    parsed.Events.Add(new ETCSEvent("RBC: Infill MA-> " + AnalyseMA(parsed.ExtraMessages.Last())));

                    GoDoPackets(payload, parsed, true, position);
                    break;

                case SS26RadioMessageType.InitiationOfACommunicationSessionToRBC:
                    if (!RickRoll(Subset26.Message38InitiationOfACommunicationSession, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Initiation of Communication Session"));
                    break;

                case SS26RadioMessageType.AcknowledgementOfTerminationOfACommunicationSession:
                    if (!RickRoll(Subset26.Message39AcknowledgementOfTerminationOfACommunicationSession, payload,
                        position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Ack of Termination of Communication Session"));
                    break;

                case SS26RadioMessageType.TrainRejected:
                    if (!RickRoll(Subset26.Message40TrainRejected, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Train Rejected", ETCSEventType.Failure));
                    break;

                case SS26RadioMessageType.TrainAccepted:
                    if (!RickRoll(Subset26.Message41TrainAccepted, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Train Accepted", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.SomPositionReportConfirmedByRbc:
                    if (!RickRoll(Subset26.Message43SoMPositionReportConfirmedByRBC, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: SoM Position Report Confirmed"));
                    break;

                case SS26RadioMessageType.AssignmentOfCoordinateSystem:
                    if (!RickRoll(Subset26.Message45AssignmentOfCoordinateSystem, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("RBC: Assignment of Coordinate System"));
                    break;

                // To RBC, or Train To Track

                case SS26RadioMessageType.ValidatedTrainData:
                    if (!RickRoll(Subset26.Message129ValidatedTrainData, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.ExtraMessages.Add(ParsedDataSet.CreateError("TrainData not implemented"));
                    parsed.Events.Add(new ETCSEvent("To RBC: Validated Train Data"));
                    break;


                case SS26RadioMessageType.RequestForShunting:
                    if (!RickRoll(Subset26.Message130RequestForShunting, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Request for Shunting", ETCSEventType.Main));
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    break;

                case SS26RadioMessageType.MARequest:
                    if (!RickRoll(Subset26.Message132MARequest, payload, position, parsed))
                        return;

                    var mareq = parsed.ExtraMessages.Last();
                    var mareqlist = new List<string>();
                    for (int i = 1; i <= 5; i++)
                    {
                        if (mareq.GetField("Q_MARQSTREASON" + i) != null)
                        {
                            mareqlist.Add(mareq.GetField("Q_MARQSTREASON" + i).Value.ToString());
                        }
                    }

                    parsed.Events.Add(new ETCSEvent("To RBC: MA request -> " + string.Join(", ", mareqlist),
                        ETCSEventType.Main));

                    if (!Packet0or1(payload, parsed, position))
                        return;
                    GoDoPackets(payload, parsed, false, position);
                    break;


                // cases with Packet 0 or 1 followed by optional packets
                case SS26RadioMessageType.TrainPositionReport:
                    if (!RickRoll(Subset26.Message136TrainPositionReport, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;

                    var posrep = parsed.ExtraMessages.Last();
                    var qscale2 = (ushort) posrep.GetField("Q_SCALE").Value;
                    var dlrbg = (ushort) posrep.GetField("D_LRBG").Value;
                    var distance = Subset26.ApplyQScale(dlrbg, qscale2);

                    if (dlrbg == 32767)
                        parsed.Events.Add(new ETCSEvent("To RBC: Position Unknown"));
                    else
                        parsed.Events.Add(new ETCSEvent("To RBC: Position " + dlrbg + "m"));

                    GoDoPackets(payload, parsed, false, position);
                    break;

                // cases with T_TRAIN and then Packet 0 or 1
                case SS26RadioMessageType.RequestToShortenMAIsGranted:
                    if (!RickRoll(Subset26.Message137RequestToShortenMAIsGranted, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Shorten MA Granted"));
                    break;

                case SS26RadioMessageType.RequestToShortenMAIsRejected:
                    if (!RickRoll(Subset26.Message138RequestToShortenMASsRejected, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Shorten MA Rejected"));
                    break;

                case SS26RadioMessageType.Acknowledgement:
                    if (!RickRoll(Subset26.Message146Acknowledgement, payload, position, parsed))
                        return;
                    break;
                case SS26RadioMessageType.AcknowledgementOfEmergencyStop:
                    if (!RickRoll(Subset26.Message147AcknowledgementOfEmergencyStop, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    break;

                case SS26RadioMessageType.TrackAheadFreeGranted:
                    if (!RickRoll(Subset26.Message149TrackAheadFreeGranted, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Track Ahead Free Granted"));
                    break;
                case SS26RadioMessageType.EndOfMission:
                    if (!RickRoll(Subset26.Message150EndOfMission, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: EOM", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.RadioInfillRequest:
                    if (!RickRoll(Subset26.Message153RadioInfillRequest, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Radio Infill Request"));
                    break;

                // cases with no extra data beyond header
                case SS26RadioMessageType.NoCompatibleVersionSupported:
                    if (!RickRoll(Subset26.Message154NoCompatibleVersionSupported, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: No Compatible Version Supported", ETCSEventType.Failure));
                    break;
                case SS26RadioMessageType.InitiationOfACommunicationSessionToTrain:
                    if (!RickRoll(Subset26.Message155InitiationOfACommunicationSession, payload, position, parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Initiation of Comm Session"));
                    break;
                case SS26RadioMessageType.TerminationOfACommunicationSession:
                    if (!RickRoll(Subset26.Message156TerminationOfACommunicationSession, payload, position,
                        parsed))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Termination of Comm Session"));
                    break;

                case SS26RadioMessageType.SoMPositionReport:
                    if (!RickRoll(Subset26.Message157SoMPositionReport, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    GoDoPackets(payload, parsed, false, position);
                    parsed.Events.Add(new ETCSEvent("To RBC: SoM Position Report", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.TextMessageAcknowledgedByDriver:
                    if (!RickRoll(Subset26.Message158TextMessageAcknowledgedByDriver, payload, position, parsed))
                        return;
                    if (!Packet0or1(payload, parsed, position))
                        return;
                    parsed.Events.Add(new ETCSEvent("To RBC: Text Message Acked by Driver", ETCSEventType.Main));
                    break;

                case SS26RadioMessageType.SessionEstablished:
                    if (!RickRoll(Subset26.Message159SessionEstablished, payload, position, parsed))
                        return;
                    GoDoPackets(payload, parsed, false, position);
                    parsed.Events.Add(new ETCSEvent("To RBC: Session Established"));
                    break;


                default:
                    parsed.ExtraMessages.Add(ParsedDataSet.CreateError("Unexpected Message Type"));
                    break;
            }
        }

        private static string AnalyseMA(ParsedDataSet mapacket)
        {
            string analys;
            var niter2 = Convert.ToUInt32(mapacket.GetField("N_ITER").Value);

            if (niter2 > 0)
            {
                analys = "MA with multiple segments";
            }
            else
            {
                var qscale2 = Convert.ToUInt32(mapacket.GetField("Q_SCALE").Value);
                var lsection2 = Convert.ToUInt32(mapacket.GetField("L_ENDSECTION").Value);
                var madistance = Subset26.ApplyQScale(lsection2, qscale2);
                analys = "MA for " + madistance + "m";
            }

            return analys;
        }


        private static bool Packet0or1(byte[] subLoad, SS27Packet ss27Packet, Position position)
        {
            bool success;
            var NID_PACKET = FieldGetter(subLoad, position.BitPosition, 8);
            if (NID_PACKET == 0)
            {
                success = RickRoll(Subset26.Packet0PositionReport, subLoad, position, ss27Packet);
            }
            else if (NID_PACKET == 1)
            {
                success = RickRoll(Subset26.Packet1PositionReportBasedOnTwoBaliseGroups, subLoad, position, ss27Packet);
            }
            else
            {
                success = false;
                ss27Packet.ExtraMessages.Add(ParsedDataSet.CreateError("Packet 0 or 1 space contains invalid packet number " + NID_PACKET));                
            }

            return success;
        }

        public static DateTime ExtractDateTime(byte[] data)
        {
            //int Year = (int) FieldGetter(data, 20, 7)+ 2000;
            var y1 = data[2] << 3 & 0xFF;
            y1 = y1 >> 1;

            var y2 = data[3] >> 6;
            int Year = y1 | y2;

            //int Month = (int) FieldGetter(data, 27, 4);
            var m1 = data[3] << 2 & 0xFF;
            var Month = m1 >> 4;

            //int Day = (int)FieldGetter(data, 31, 5);
            var d1 = data[3] << 6 & 0xFF;
            d1 = d1 >> 3;
            var d2 = data[4] >> 5;
            int Day = d1 | d2;

            //int Hour = (int)FieldGetter(data, 36, 5);
            int Hour = data[4] & 0x1F;

            //int Minutes = (int)FieldGetter(data, 41, 6);
            int Minutes = data[5] >> 2;

            //int Seconds = (int)FieldGetter(data, 47, 6);
            var s1 = data[5] << 6 & 0xFF;
            s1 = s1 >> 2;

            var s2 = data[6] >> 4;
            int Seconds = s1 | s2;

            //int ms = (int)FieldGetter(data, 53, 5) * 50;
            var ms1 = data[6] << 4 & 0xFF;
            ms1 = ms1 >> 3;

            var ms2 = data[7] >> 7;
            int ms = ms1 | ms2;
            ms = ms * 50;

            if (ms > 950)
                throw new Exception("Date fraction over 950ms");

            return new DateTime(Year + 2000, Month, Day, Hour, Minutes, Seconds, ms);
        }
    }

    /*
    /// <summary>
    /// 0 Full Supervision
    /// 1 On Sight
    /// 2 Staff Responsible
    /// 3 Shunting
    /// 4 Unfitted
    /// 5 Sleeping
    /// 6 Stand By
    /// 7 Trip
    /// 8 Post Trip
    /// 9 System Failure
    /// 10 Isolation
    /// 11 Non Leading
    /// 12 Limited Supervision
    /// 13 National System
    /// 14 Reversing
    /// 15 Passive Shunting
    /// </summary>
    public enum SS27Mode
    {
        FullSupervision = 0,
        OnSight = 1,
        StaffResponsible = 2,
        Shunting = 3,
        Unfitted = 4,
        Sleeping = 5,
        StandBy = 6,
        Trip = 7,
        PostTrip = 8,
        SystemFailure = 9,
        Isolation = 10,
        NonLeading = 11,
        LimitedSupervision = 12,
        NationalSystem = 13,
        Reversing = 14,
        PassiveShunting = 15,
        INVALID = 16
    }

    /// <summary>
    /// 0 Level 0
    /// 1 Level NTC specified by NID_NTC
    /// 2 Level 1
    /// 3 Level 2
    /// 4 Level 3
    /// </summary>
    public enum SS27Level
    {
        Level0 = 0,
        LevelNTC = 1,
        Level1 = 2,
        Level2 = 3,
        Level3 = 4,
        INVALID = 5
    } */

    /* Types of JRU messages
     * 
     * 1 GENERAL MESSAGE 18
        2 TRAIN DATA 18
        3 EMERGENCY BRAKE COMMAND STATE 27
        4 SERVICE BRAKE COMMAND STATE 27
        5 MESSAGE TO RADIO INFILL UNIT 28
        6 TELEGRAM FROM BALISE 28
        7 MESSAGE FROM EUROLOOP 28
        8 MESSAGE FROM RADIO INFILL UNIT 28
        9 MESSAGE FROM RBC 29
        10 MESSAGE TO RBC 29
        11 DRIVER’S ACTIONS 29
        12 BALISE GROUP ERROR 31
        13 RADIO ERROR 32
        14 STM INFORMATION 32
        15 INFORMATION FROM COLD MOVEMENT DETECTOR 35
        16 START DISPLAYING FIXED TEXT MESSAGE 36
        17 STOP DISPLAYING FIXED TEXT MESSAGE 36
        18 START DISPLAYING PLAIN TEXT MESSAGE 36
        19 STOP DISPLAYING PLAIN TEXT MESSAGE 36
        20 SPEED AND DISTANCE MONITORING INFORMATION 37
        21 DMI SYMBOL STATUS 39
        22 DMI SOUND STATUS 41
        23 DMI SYSTEM STATUS MESSAGE 42
        24 ADDITIONAL DATA 43
        25 SR SPEED/DISTANCE ENTERED BY THE DRIVER 44
        26 NTC SELECTED 45
        27 SAFETY CRITICAL FAULT IN MODE SL, NL OR PS 45
        28 VIRTUAL BALISE COVER SET BY THE DRIVER 45
        29 VIRTUAL BALISE COVER REMOVED BY THE DRIVER 45
        30 SLEEPING INPUT 46
        31 PASSIVE SHUNTING INPUT 46
        32 NON LEADING INPUT 46
        33 REGENERATIVE BRAKE STATUS 47
        34 MAGNETIC SHOE BRAKE STATUS 47
        35 EDDY CURRENT BRAKE STATUS 48
        36 ELECTRO PNEUMATIC BRAKE STATUS 48
        37 ADDITIONAL BRAKE STATUS 49
        38 CAB STATUS 49
        39 DIRECTION CONTROLLER POSITION 50
        40 TRACTION STATUS 51
        41 TYPE OF TRAIN DATA 51
        42 NATIONAL SYSTEM ISOLATION 52
        43 TRACTION CUT OFF COMMAND STATE 52
        44 LOWEST SUPERVISED SPEED WITHIN THE MOVEMENT
        AUTHORITY
        53
        45-254 SPARE
        255 ETCS ON-BOARD PROPRIETARY JURIDICAL DATA 53
     * 
     */
}