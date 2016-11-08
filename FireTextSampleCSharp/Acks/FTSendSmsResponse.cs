using System.Linq;


namespace FireTextSampleCSharp.Acks
{
    public class FTSendSmsResponse : FTResponseBase
    {
        /// <summary>
        /// If the case of sendsms, this is the number of messages sent.
        /// </summary>
        private int _numberOfMessagesQueued;


        public FTSendSmsResponse(string responseMessage) : base(responseMessage)
        {
            _numberOfMessagesQueued = 0;

            if (StatusCode == 0)
            {
                var splitMessage = ResponseMessage.Split(' ');
                if (splitMessage != null && splitMessage.Count() > 1)
                {
                    int numSmsSent;
                    bool isSmsNumberPresent = int.TryParse(splitMessage[0], out numSmsSent);
                    if (isSmsNumberPresent)
                    {
                        _numberOfMessagesQueued = numSmsSent;
                    }
                }
            }
        }

        public int GetNumberOfMessagesQueued()
        {
            return _numberOfMessagesQueued;
        }


        public bool IsInsufficientCreditError()
        {
            return StatusCode == 7;
        }



        #region "Example calls"
        //0:1 SMS successfully queued
        //0:  SMS successfully queued
        //1:  Authentication error
        //2:  Destination number(s)error
        //3:  From error
        //4:  Group not recognised
        //5:  Message error
        //6:  Send time error(YYYY - MM - DD HH: MM)
        //7:  Insufficient credit
        //8:  Invalid delivery receipt URL
        //9:  Sub - account error(not recognised)
        //10:  Repeat expiry/ interval error(not recognised)
        //11:  Repeat expiry error(YYYY - MM - DD)

        #endregion
    }
}
