using System.Linq;

namespace FireTextSampleCSharp.Acks
{
    public class FTCreditResponse : FTResponseBase
    {
        /// <summary>
        /// If the case of sendsms, this is the number of messages sent.
        /// </summary>
        private int _numberOfCreditsLeft;

        public FTCreditResponse(string responseMessage) : base(responseMessage)
        {
            _numberOfCreditsLeft = 0;

            if (StatusCode == 0)
            {
                var splitMessage = ResponseMessage.Split(' ');
                if (splitMessage != null && splitMessage.Count() > 1)
                {
                    int numSmsSent;
                    bool isSmsNumberPresent = int.TryParse(splitMessage[0], out numSmsSent);
                    if (isSmsNumberPresent)
                    {
                        _numberOfCreditsLeft = numSmsSent;
                    }
                }
            }
        }


        public int GetCreditsLeft()
        {
            return _numberOfCreditsLeft;
        }
    }
}
