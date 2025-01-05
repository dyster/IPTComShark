# IPTComShark

A wireshark style data analyzer made specifically for IPTCom, but still does regular Ethernet traffic as well.

Has rudementary support for CIP, and TRDP is planned.

The main differences to Wireshark and the reason I started this project is
+ The ability to easily define custom data sets (can be done in C# or XML currently, but any serialization type would work)
+ Data visualization that focuses on the application data, not the underlying transport
+ For data that repeats, only show the CHANGE in data from packet to packet
+ Performance, existing IPTCOM plugins for wireshark when adding custom data are unbelievably slow

This project relies on the following
+ BitDataParser https://github.com/dyster/BitDataParser (MIT)
+ BustPCap https://github.com/dyster/BustPCap (MIT)
+ SharpCompress https://github.com/adamhathcock/sharpcompress (MIT)
+ PacketDotNet https://github.com/dotpcap/packetnet (MPL-2.0)
+ EPPlus https://github.com/EPPlusSoftware/EPPlus (poly non-commercial)
+ SharpPcap https://github.com/dotpcap/sharppcap (MIT, I think)
+ SSH.NET https://github.com/sshnet/SSH.NET (MIT)
+ ObjectListView https://objectlistview.sourceforge.net/cs/index.html (GPL-3)

Note that while this project is under MIT licence, it includes projects with more restrictive licenses
