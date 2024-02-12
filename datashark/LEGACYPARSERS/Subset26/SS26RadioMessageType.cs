namespace TrainShark.Parsers
{
    public enum SS26RadioMessageType
    {
        // Train to Track radio messages
        ValidatedTrainData = 129,
        RequestForShunting = 130,
        MARequest = 132,
        TrainPositionReport = 136,
        RequestToShortenMAIsGranted = 137,
        RequestToShortenMAIsRejected = 138,
        Acknowledgement = 146,
        AcknowledgementOfEmergencyStop = 147,
        TrackAheadFreeGranted = 149,
        EndOfMission = 150,
        RadioInfillRequest = 153,
        NoCompatibleVersionSupported = 154,
        InitiationOfACommunicationSessionToTrain = 155,
        TerminationOfACommunicationSession = 156,
        SoMPositionReport = 157,
        TextMessageAcknowledgedByDriver = 158,
        SessionEstablished = 159,

        // Track to Train radio messages

        SrAuthorisation = 2,
        MovementAuthority = 3,
        RecognitionOfExitFromTripMode = 6,
        AcknowledgementOfTrainData = 8,
        RequestToShortenMa = 9,
        ConditionalEmergencyStop = 15,
        UnconditionalEmergencyStop = 16,
        RevocationOfEmergencyStop = 18,
        GeneralMessage = 24,
        ShRefused = 27,
        ShAuthorised = 28,
        MaWithShiftedLocationReference = 33,
        TrackAheadFreeRequest = 34,
        InfillMa = 37,
        TrainRejected = 40,
        RbcRiuSystemVersion = 32,
        InitiationOfACommunicationSessionToRBC = 38,
        AcknowledgementOfTerminationOfACommunicationSession = 39,
        TrainAccepted = 41,
        SomPositionReportConfirmedByRbc = 43,
        AssignmentOfCoordinateSystem = 45
    }
}