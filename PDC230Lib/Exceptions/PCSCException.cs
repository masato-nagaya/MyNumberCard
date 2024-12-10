using System;
using System.Runtime.Serialization;

namespace PDC210Lib {
    /// <summary>
    /// A general PC/SC exception.
    /// </summary>
    [Serializable]
    public class PDC210Exception : Exception
    {
	    protected const string PDC210_ERROR_SERIALIZATION_NAME = "PDC210Error";

	    /// <summary>
        /// The system's error code.
        /// </summary>
        public PDC210Error PDC210Error { get; protected set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="serr">System's error code</param>
        public PDC210Exception(PDC210Error serr)
            : base(SCardHelper.StringifyError(serr)) {
            PDC210Error = serr;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="serr">System's error code</param>
        /// <param name="message">An error message text.</param>
        public PDC210Exception(PDC210Error serr, string message)
            : base(message) {
            PDC210Error = serr;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PCSCException"/> class.
        /// </summary>
        /// <param name="serr">System's error code</param>
        /// <param name="message">An error message text.</param>
        /// <param name="innerException">The inner exception.</param>
        public PDC210Exception(PDC210Error serr, string message, Exception innerException)
            : base(message, innerException) {
            PDC210Error = serr;
        }

		/// <summary>
		/// Serialization constructor
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
	    protected PDC210Exception(SerializationInfo info, StreamingContext context) 
			: base(info, context)
	    {
		    PDC210Error = (PDC210Error)info.GetValue(PDC210_ERROR_SERIALIZATION_NAME, typeof (PDC210Error));
	    }

	    /// <summary>
		/// Gets data for serialization
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
	    public override void GetObjectData(SerializationInfo info, StreamingContext context)
	    {
		    base.GetObjectData(info, context);
		    info.AddValue(PDC210_ERROR_SERIALIZATION_NAME, PDC210Error, typeof (PDC210Error));
	    }
    }
}