namespace PDC230Lib {

	public enum PDC230Error {
        Success = 0,
        NoData =11,
        NotReadData = 12,
        IsCashcardCreditcard = 13,
        NotWrite = 21,
        CashcardCreditcardData = 23,
        InvalidCommand = 81,
        InvalidParameter = 82,
        DataLengthOver = 83,
        FailureTransferMachine = 84,
        SensorError1 = 93,
        SensorError2 = 94,
        SensorError3 = 95,
        CardMalposition = 96,
        LongCard = 97,
        ShortCard = 98
    }
}
