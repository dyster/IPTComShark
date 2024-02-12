﻿namespace TrainShark.Parsers
{
    public enum SS27MsgType
    {
        ParseError = 0,
        General = 1,
        TRAINDATA = 2,
        EMERGENCYBRAKECOMMANDSTATE = 3,
        SERVICEBRAKECOMMANDSTATE = 4,
        MESSAGETORADIOINFILLUNIT = 5,
        TELEGRAMFROMBALISE = 6,
        MESSAGEFROMEUROLOOP = 7,
        MESSAGEFROMRADIOINFILLUNIT = 8,
        MESSAGEFROMRBC = 9,
        MESSAGETORBC = 10,
        DRIVERSACTIONS = 11,
        BALISEGROUPERROR = 12,
        RADIOERROR = 13,
        STMINFORMATION = 14,
        INFORMATIONFROMCOLDMOVEMENTDETECTOR = 15,
        STARTDISPLAYINGFIXEDTEXTMESSAGE = 16,
        STOPDISPLAYINGFIXEDTEXTMESSAGE = 17,
        STARTDISPLAYINGPLAINTEXTMESSAGE = 18,
        STOPDISPLAYINGPLAINTEXTMESSAGE = 19,
        SPEEDANDDISTANCEMONITORINGINFORMATION = 20,
        DMISYMBOLSTATUS = 21,
        DMISOUNDSTATUS = 22,
        DMISYSTEMSTATUSMESSAGE = 23,
        ADDITIONALDATA = 24,
        SRSPEEDDISTANCEENTEREDBYTHEDRIVER = 25,
        NTCSELECTED = 26,
        SAFETYCRITICALFAULTINMODESLNLORPS = 27,
        VIRTUALBALISECOVERSETBYTHEDRIVER = 28,
        VIRTUALBALISECOVERREMOVEDBYTHEDRIVER = 29,
        SLEEPINGINPUT = 30,
        PASSIVESHUNTINGINPUT = 31,
        NONLEADINGINPUT = 32,
        REGENERATIVEBRAKESTATUS = 33,
        MAGNETICSHOEBRAKESTATUS = 34,
        EDDYCURRENTBRAKESTATUS = 35,
        ELECTROPNEUMATICBRAKESTATUS = 36,
        ADDITIONALBRAKESTATUS = 37,
        CABSTATUS = 38,
        DIRECTIONCONTROLLERPOSITION = 39,
        TRACTIONSTATUS = 40,
        TYPEOFTRAINDATA = 41,
        NATIONALSYSTEMISOLATION = 42,
        TRACTIONCUTOFFCOMMANDSTATE = 43,
        LOWESTSUPERVISEDSPEEDWITHINTHEMOVEMENTAUTHORITY = 44,

        PROPRIETARY = 255,
        INVALID = 256
    }
}