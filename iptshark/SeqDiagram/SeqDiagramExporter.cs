/*using Svg;
using Svg.DataTypes;
using Svg.Pathing;*/

using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using TrainShark.Parsers;

namespace TrainShark.SeqDiagram
{
    public class SeqDiagramExporter
    {
        public static void MakeSVG(List<CapturePacket> packets, string fileName, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            MessageBox.Show("DISABLED!");
            /* DISABLED
            const int baselinestep = 250; // distance between vertical lines
            const int horizontalBase = 130; // upper range for the vertical lines
            const int unitBoxWidth = 120; // width of device box
            const int unitBoxHeight = 40; // height of device box
            const int firstarrowdistance = 50; // vertical distance before the first sequence arrow
            const int maxboxwidth = 1000; // maximum width of description textboxes

            var font = new Font("Calibri",
                9); // SVG and C# have different ideas of what a fontsize is apparently, so we set this lower and hope for the best

            int baselinegen =
                -100; // iterator for making the vertical lines, the first line does one step from this value
            int arrowgen = horizontalBase + firstarrowdistance; // iterator for making sequence arrows

            // iterate through the data and gather what we need into a little struct for convenience
            List<Sequence> list = GetSequences(packets, baselinestep, out var devices, ref baselinegen, backStore, parserFactory);

            // after all vertical lines have been placed, make one for the description boxes
            int descriptionBaseline = baselinegen + 50;

            var svg = new SvgDocument();
            var groupDevices = new SvgGroup
            {
                FontFamily = "Calibri",
                FontSize = 16
            };
            var groupArrows = new SvgGroup
            {
                ID = "arrows",
                Stroke = new SvgColourServer(Color.Black),
                StrokeWidth = 2
            };
            var groupArrowTexts = new SvgGroup
            {
                ID = "arrowtexts",
                FontFamily = "Calibri",
                FontSize = 11
            };
            var groupBaselines = new SvgGroup
            {
                ID = "baselines",
                Stroke = new SvgColourServer(Color.Black),
                StrokeWidth = 2,
                StrokeDashArray = new SvgUnitCollection {10, 10}
            };
            var groupDescriptionBoxes = new SvgGroup
            {
                ID = "descriptionboxes",
                Fill = new SvgColourServer(Color.Transparent),
                Stroke = new SvgColourServer(Color.Black),
                StrokeWidth = 2
            };
            var groupDescriptionTexts = new SvgGroup
            {
                ID = "descriptiontexts",
                FontFamily = "Calibri",
                FontSize = 12
            };

            svg.Children.Add(groupDevices);
            svg.Children.Add(groupArrows);
            svg.Children.Add(groupArrowTexts);
            svg.Children.Add(groupBaselines);
            svg.Children.Add(groupDescriptionBoxes);
            svg.Children.Add(groupDescriptionTexts);

            var svgDefinitionList = new SvgDefinitionList();
            var svgMarker = new SvgMarker
            {
                ID = "arrow",
                MarkerWidth = 10,
                MarkerHeight = 10,
                RefX = 9,
                RefY = 3,
                Orient = new SvgOrient {IsAuto = true},
                MarkerUnits = SvgMarkerUnits.StrokeWidth
            };
            var svgPath = new SvgPath
            {
                PathData = new SvgPathSegmentList
                {
                    new SvgMoveToSegment(new PointF(0, 0)),
                    new SvgLineSegment(new PointF(0, 0), new PointF(0, 6)),
                    new SvgLineSegment(new PointF(0, 0), new PointF(9, 3)),
                    new SvgClosePathSegment()
                },
                Fill = new SvgColourServer(Color.Black)
            };
            svgMarker.Children.Add(svgPath);
            svgDefinitionList.Children.Add(svgMarker);
            svg.Children.Add(svgDefinitionList);

            // make the device boxes
            foreach (KeyValuePair<IPAddress, int> device in devices)
            {
                groupDevices.Children.Add(new SvgRectangle
                {
                    Fill = new SvgColourServer(Color.White),
                    Stroke = new SvgColourServer(Color.Black),
                    StrokeWidth = 2,
                    X = device.Value - unitBoxWidth / 2,
                    Y = horizontalBase - unitBoxHeight,
                    Width = unitBoxWidth,
                    Height = unitBoxHeight
                });
                groupDevices.Children.Add(new SvgText
                {
                    Text = device.Key.ToString(),
                    Stroke = new SvgColourServer(Color.Black),
                    X = new SvgUnitCollection {new SvgUnit(device.Value)},
                    Y = new SvgUnitCollection {new SvgUnit(horizontalBase - unitBoxHeight / 2)},
                    TextAnchor = SvgTextAnchor.Middle
                });
            }

            foreach (Sequence sequence in list)
            {
                // get the x axis of the two devices we are relating
                int one = devices[sequence.From];
                int two = devices[sequence.To];

                groupArrows.Children.Add(new SvgLine
                {
                    StartX = one,
                    StartY = arrowgen,
                    EndX = two,
                    EndY = arrowgen,
                    MarkerEnd = new Uri("url(#arrow)", UriKind.Relative)
                });

                // get middle of arrow
                int right = Math.Max(one, two);
                int left = Math.Min(one, two);
                int middle = (right - left) / 2;
                middle = left + middle;
                groupArrowTexts.Children.Add(new SvgText
                {
                    Text = sequence.Name,
                    X = new SvgUnitCollection {new SvgUnit(middle)},
                    Y = new SvgUnitCollection {new SvgUnit(arrowgen - 2)},
                    TextAnchor = SvgTextAnchor.Middle
                });

                var svgBoxText = new SvgText
                {
                    X = new SvgUnitCollection {new SvgUnit(descriptionBaseline + 5)},
                    Y = new SvgUnitCollection {new SvgUnit(arrowgen - 12)}
                };
                svgBoxText.Children.Add(new SvgTextSpan
                {
                    Text = sequence.Time,
                    TextDecoration = SvgTextDecoration.Underline,
                    X = new SvgUnitCollection {new SvgUnit(descriptionBaseline + 5)},
                    Dy = new SvgUnitCollection {new SvgUnit(SvgUnitType.Em, 1.2f)}
                });

                int maxstrlen = TextRenderer.MeasureText(sequence.Time, font).Width;
                var lines = 1;
                foreach (KeyValuePair<string, string> pair in sequence.Dic)
                {
                    var both = new SvgTextSpan
                    {
                        X = new SvgUnitCollection {new SvgUnit(descriptionBaseline + 5)},
                        Dy = new SvgUnitCollection {new SvgUnit(SvgUnitType.Em, 1.2f)}
                    };

                    string textKey = pair.Key + ": ";
                    string textValue = Conversions.RemoveInvalidXMLChars(pair.Value);

                    int strlen = TextRenderer.MeasureText(textKey + textValue, font).Width;

                    if (strlen > maxboxwidth)
                    {
                        // ooof
                        string tv1 = textValue;
                        var tv2 = "";
                        while (TextRenderer.MeasureText(textKey + tv1, font).Width > maxboxwidth)
                        {
                            tv2 = tv1[tv1.Length - 1] + tv2;
                            tv1 = tv1.Substring(0, tv1.Length - 1);
                        }

                        var keySpan = new SvgTextSpan {Text = textKey, FontWeight = SvgFontWeight.Bold};
                        var valueSpan1 = new SvgTextSpan {Text = tv1};
                        var valueSpan2 = new SvgTextSpan
                        {
                            Text = tv2,
                            X = new SvgUnitCollection {new SvgUnit(descriptionBaseline + 5)},
                            Dy = new SvgUnitCollection {new SvgUnit(SvgUnitType.Em, 1.2f)}
                        };
                        both.Children.Add(keySpan);
                        both.Children.Add(valueSpan1);
                        svgBoxText.Children.Add(both);

                        svgBoxText.Children.Add(valueSpan2);
                        lines += 2;
                    }
                    else
                    {
                        var keySpan = new SvgTextSpan {Text = textKey, FontWeight = SvgFontWeight.Bold};
                        var valueSpan = new SvgTextSpan {Text = textValue};
                        both.Children.Add(keySpan);
                        both.Children.Add(valueSpan);
                        svgBoxText.Children.Add(both);
                        lines++;
                    }

                    if (strlen > maxstrlen)
                        maxstrlen = strlen;
                }

                // box height calc, 30 margin + 14 per line. Then have 20 between box bottom and next arrow (at least)

                var boxheight = (int) (lines * font.Height * 1.02);

                // draw the box
                groupDescriptionBoxes.Children.Add(new SvgRectangle
                {
                    X = descriptionBaseline,
                    Y = arrowgen - 10,
                    Width = maxstrlen,
                    Height = boxheight
                });

                // add text after box
                groupDescriptionTexts.Children.Add(svgBoxText);

                // setup next arrow
                arrowgen += boxheight + 30;
            }

            // finally, draw the baselines
            foreach (KeyValuePair<IPAddress, int> device in devices)
                groupBaselines.Children.Add(new SvgLine
                {
                    StartX = device.Value,
                    StartY = horizontalBase,
                    EndX = device.Value,
                    EndY = arrowgen
                });

            // now lets make SVG!!!
            svg.Width = descriptionBaseline + maxboxwidth + 5;
            svg.Height = arrowgen + 5;
            svg.ViewBox = new SvgViewBox(0, 0, descriptionBaseline + maxboxwidth + 5, arrowgen + 5);

            using (FileStream fileStream = File.Create(fileName))
            {
                svg.Write(fileStream);
            }
        }*/

            /*
            /// <summary>
            ///     Exports into a xml file that can be read by UMLet
            /// </summary>
            /// <param name="devicelogs"></param>
            /// <param name="fileName"></param>
            public static void MakeUML(List<CapturePacket> packets, string fileName)
            {
                const int baselinestep = 250; // distance between vertical lines
                const int horizontalBase = 130; // upper range for the vertical lines
                const int unitBoxWidth = 120; // width of device box
                const int unitBoxHeight = 40; // height of device box
                const int firstarrowdistance = 50; // vertical distance before the first sequence arrow
                const int maxboxwidth = 1000; // maximum width of description textboxes

                var font = new Font("Microsoft Sans Serif",
                    11); // the approximate font UMLet is using, for text measurement

                var baselinegen = 0; // iterator for making the vertical lines, the first line does one step from this value
                int arrowgen = horizontalBase + firstarrowdistance; // iterator for making sequence arrows

                // iterate through the data and gather what we need into a little struct for convenience
                List<Sequence> list = GetSequences(packets, baselinestep, out var devices, ref baselinegen);

                // after all vertical lines have been placed, make one for the description boxes
                int descriptionBaseline = baselinegen + 50;

                var umlFile = new UMLFile(); // make the xml for UMLet

                // make the device boxes
                foreach (KeyValuePair<string, int> device in devices)
                    umlFile.Elements.Add(UmlGenericElement.Create(
                        new Coordinates
                        {
                            X = device.Value - unitBoxWidth / 2,
                            Y = horizontalBase - unitBoxHeight,
                            W = unitBoxWidth,
                            H = unitBoxHeight
                        }, device.Key, "orange"));

                foreach (Sequence sequence in list)
                {
                    // get the x axis of the two devices we are relating
                    int one = devices[sequence.From];
                    int two = devices[sequence.To];

                    // and determine direction of arrow
                    if (one < two)
                        umlFile.Elements.Add(RelationElement.CreateRightArrow(new Coordinates
                        {
                            X = one,
                            Y = arrowgen,
                            W = two - one,
                            H = 0
                        }, sequence.Name));
                    else
                        umlFile.Elements.Add(RelationElement.CreateLeftArrow(new Coordinates
                        {
                            X = two,
                            Y = arrowgen,
                            W = one - two,
                            H = 0
                        }, sequence.Name));

                    // make key value pairs and squeeze them into the box
                    var strs = new List<string>();
                    foreach (KeyValuePair<string, object> pair in sequence.Dic)
                        strs.Add($"{pair.Key}: {pair.Value}");

                    var desc = "";
                    var curline = "";
                    var lines = 1;
                    foreach (string str in strs)
                        if (TextRenderer.MeasureText(curline + str, font).Width > maxboxwidth)
                        {
                            if (desc.Length == 0)
                            {
                                desc = curline;
                                curline = str;
                            }
                            else
                            {
                                desc += "\r\n" + curline;
                                curline = str;
                                lines++;
                            }
                        }
                        else
                        {
                            if (curline.Length > 0)
                                curline += " " + str;
                            else
                                curline = str;
                        }
                    if (desc.Length == 0)
                    {
                        desc = curline;
                    }
                    else if (curline.Length > 0)
                    {
                        desc += "\r\n" + curline;
                        lines++;
                    }

                    // remove any invalid xml chars present
                    desc = Functions.RemoveInvalidXMLChars(desc);

                    // draw box width to either text width or max size if one line
                    int strwidth;
                    strwidth = lines == 1 ? TextRenderer.MeasureText(desc, font).Width : maxboxwidth;

                    // box height calc, title is 30, c.a 15 pre line, 15 margin bottom. Then have 20 between box bottom and next arrow (at least)
                    int boxheight = 30 + lines * 15 + 15;

                    // draw the box
                    umlFile.Elements.Add(UmlFrameElement.Create(
                        new Coordinates {X = descriptionBaseline, Y = arrowgen - 20, W = strwidth, H = boxheight},
                        sequence.Time, desc));

                    // setup next arrow
                    arrowgen += boxheight + 20;
                }

                // finally, draw the baselines
                foreach (KeyValuePair<string, int> device in devices)
                    umlFile.Elements.Add(RelationElement.CreateBaselineArrow(
                        new Coordinates
                        {
                            X = device.Value,
                            Y = horizontalBase,
                            W = 0,
                            H = arrowgen - firstarrowdistance - horizontalBase
                        }
                    ));

                // serialize that mother
                using (XmlWriter xmlWriter = XmlWriter.Create(File.Create(fileName), new XmlWriterSettings {Indent = true}))
                {
                    var serializer = new XmlSerializer(typeof(UMLFile));
                    var xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(string.Empty, string.Empty);

                    serializer.Serialize(xmlWriter, umlFile, xmlns);
                }
            }*/

            /* DISABLED
        private static List<Sequence> GetSequences(List<CapturePacket> packets, int baselinestep,
            out Dictionary<IPAddress, int> devices, ref int baselinegen, BackStore.BackStore backStore, ParserFactory parserFactory)
        {
            var list = new List<Sequence>();
            devices = new Dictionary<IPAddress, int>();
            foreach (CapturePacket packet in packets)
            {
                if (packet.Source == null || packet.Destination == null)
                    continue;

                if (!devices.ContainsKey(new IPAddress(packet.Source)))
                    devices.Add(new IPAddress(packet.Source), baselinegen += baselinestep);
                if (!devices.ContainsKey(new IPAddress(packet.Destination)))
                    devices.Add(new IPAddress(packet.Destination), baselinegen += baselinestep);

                var payload = backStore.GetPayload(packet.No);
                Parse? parse = parserFactory.DoPacket(packet.Protocol, payload);

                if (!parse.HasValue)
                    continue;

                //TODO fix so it uses all datasets
                var now = parse.Value.ParsedData[0].GetStringDictionary()
                    .Where(pair => pair.Key != "MMI_M_PACKET" && pair.Key != "MMI_L_PACKET")
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

                Dictionary<string, string> dic = new Dictionary<string, string>();

                if (packet.Previous != null)
                {
                    var payloadOld = backStore.GetPayload(packet.Previous.No);
                    Parse? parseOld = parserFactory.DoPacket(packet.Previous.Protocol, payloadOld);

                    if (parseOld.HasValue)
                    {
                        // TODO fix so it uses all datasets
                        var before = parseOld.Value.ParsedData[0].GetStringDictionary();

                        foreach (KeyValuePair<string, string> pair in now)
                        {
                            if (before.ContainsKey(pair.Key))
                            {
                                if (pair.Value != before[pair.Key])
                                    dic.Add(pair.Key, pair.Value);
                            }
                        }
                    }
                    else
                    {
                        dic = now;
                    }
                }

                var sequence = new Sequence
                {
                    From = new IPAddress(packet.Source),
                    To = new IPAddress(packet.Destination),
                    Time = packet.Date.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                sequence.Name = packet.Name;

                sequence.Dic = dic;

                // This can create a problem where a device is created that has no data sent to or from it, but I doubt this will ever happen
                if (sequence.Dic.Count > 0)
                    list.Add(sequence);
            }

            return list;*/
        }

        /// <summary>
        ///     Struct used for MakeUML and MakeSVG
        /// </summary>
        private struct Sequence
        {
            public IPAddress From;
            public IPAddress To;
            public string Name;
            public Dictionary<string, string> Dic;
            public string Time;
        }
    }
}