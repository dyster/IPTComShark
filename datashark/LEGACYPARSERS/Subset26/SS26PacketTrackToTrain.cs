﻿namespace IPTComShark.Parsers
{
    public enum SS26PacketTrackToTrain
    {
        VirtualBaliseCoverMarker = 0,
        SystemVersionOrder = 2,
        NationalValues = 3,
        Linking = 5,
        VirtualBaliseCoverOrder = 6,
        Level1MovementAuthority = 12,
        StaffResponsibleDistanceInformationFromLoop = 13,
        Level23MovementAuthority = 15,
        RepositioningInformation = 16,
        GradientProfile = 21,
        InternationalStaticSpeedProfile = 27,
        TrackConditionChangeOfTractionSystem = 39,
        TrackConditionChangeOfAllowedCurrentConsumption = 40,
        LevelTransitionOrder = 41,
        SessionManagement = 42,
        DataUsedByApplicationsOutsideTheErtmsEtcsSystem = 44,
        RadioNetworkRegistration = 45,
        ConditionalLevelTransitionOrder = 46,
        ListOfBalisesForShArea = 49,
        AxleLoadSpeedProfile = 51,
        PermittedBrakingDistanceInformation = 52,
        MovementAuthorityRequestParameters = 57,
        PositionReportParameters = 58,
        ListOfBalisesInSrAuthority = 63,
        InhibitionOfRevocableTsrsFromBalisesInL23 = 64,
        TemporarySpeedRestriction = 65,
        TemporarySpeedRestrictionRevocation = 66,
        TrackConditionBigMetalMasses = 67,
        TrackCondition = 68,
        TrackConditionStationPlatforms = 69,
        RouteSuitabilityData = 70,
        AdhesionFactor = 71,
        PacketForSendingPlainTextMessages = 72,
        PacketForSendingFixedTextMessages = 76,
        GeographicalPositionInformation = 79,
        ModeProfile = 80,
        LevelCrossingInformation = 88,
        TrackAheadFreeUpToLevel23TransitionLocation = 90,
        RbcTransitionOrder = 131,
        DangerForShuntingInformation = 132,
        RadioInfillAreaInformation = 133,
        EolmPacket = 134,
        StopShuntingOnDeskOpening = 135,
        InfillLocationReference = 136,
        StopIfInStaffResponsible = 137,
        ReversingAreaInformation = 138,
        ReversingSupervisionInformation = 139,
        TrainRunningNumberFromRbc = 140,
        DefaultGradientForTemporarySpeedRestriction = 141,
        SessionManagementWithNeighbouringRadioInfillUnit = 143,
        InhibitionOfBaliseGroupMessageConsistencyReaction = 145,
        LssmaDisplayToggleOrder = 180,
        GenericLsFunctionMarker = 181,
        DefaultBaliseLoopOrRiuInformation = 254
    }
}