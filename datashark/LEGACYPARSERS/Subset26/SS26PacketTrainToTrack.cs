namespace TrainShark.Parsers
{
    public enum SS26PacketTrainToTrack
    {
        PositionReport = 0,
        PositionReportBasedOnTwoBaliseGroups = 1,
        OnboardTelephoneNumbers = 3,
        ErrorReporting = 4,
        TrainRunningNumber = 5,
        Level23TransitionInformation = 9,
        ValidatedTrainData = 11,
        DataUsedByApplicationsOutsideTheErtmsEtcsSystem = 44
    }
}