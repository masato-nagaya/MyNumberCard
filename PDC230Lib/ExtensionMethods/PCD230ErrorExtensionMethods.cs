namespace PDC230Lib {
	internal static class SCardErrorExtensionMethods
	{
		internal static void ThrowIfNotSuccess(this PDC230Error sc) {
			if (sc == PDC230Error.Success) {
				return;
			}
			sc.Throw();
		}

		internal static void Throw(this PDC230Error sc) {
			//switch (sc) {
			//	case PDC230Error.Success:
			//		throw new SuccessException(sc);
			//	case PDC230Error.InvalidHandle:
			//		throw new InvalidContextException(sc);
			//	case PDC230Error.InvalidParameter:
			//		throw new InvalidProtocolException(sc);
			//	case PDC230Error.InvalidValue:
			//		throw new InvalidValueException(sc);
			//	case PDC230Error.NoService:
			//		throw new NoServiceException(sc);
			//	case PDC230Error.NoSmartcard:
			//		throw new NoSmartcardException(sc);
			//	case PDC230Error.NoReadersAvailable:
			//		throw new NoReadersAvailableException(sc);
			//	case PDC230Error.NotReady:
			//		throw new NotReadyException(sc);
			//	case PDC230Error.ReaderUnavailable:
			//		throw new ReaderUnavailableException(sc);
			//	case PDC230Error.SharingViolation:
			//		throw new SharingViolationException(sc);
			//	case PDC230Error.UnknownReader:
			//		throw new UnknownReaderException(sc);
			//	case PDC230Error.UnsupportedCard:
			//		throw new UnsupportedFeatureException(sc);
			//	case PDC230Error.CommunicationError:
			//		throw new CommunicationErrorException(sc);
			//	case PDC230Error.InternalError:
			//		throw new InternalErrorException(sc);
			//	case PDC230Error.UnpoweredCard:
			//		throw new UnpoweredCardException(sc);
			//	case PDC230Error.UnresponsiveCard:
			//		throw new UnresponsiveCardException(sc);
			//	case PDC230Error.RemovedCard:
			//		throw new RemovedCardException(sc);
			//	case PDC230Error.InsufficientBuffer:
			//		throw new InsufficientBufferException(sc);
			//	default:
			//		throw new PCSCException(sc); // Unexpected / unknown error
			//}
		}
	}
}