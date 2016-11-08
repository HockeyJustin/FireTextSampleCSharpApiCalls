using System;
using System.Linq;

namespace FireTextSampleCSharp.Acks
{
    public abstract class FTResponseBase
    {
        public int StatusCode { get; private set; }
        public string ResponseMessage { get; protected set; }

        public FTResponseBase(string responseMessage)
        {
            StatusCode = -1;
            ResponseMessage = "";

            if (!String.IsNullOrWhiteSpace(responseMessage))
            {
                string[] splitResponse = responseMessage.Split(':');
                if (splitResponse != null && splitResponse.Count() > 1)
                {
                    int statusCode;
                    bool hasStatusCode = int.TryParse(splitResponse[0], out statusCode);
                    if (hasStatusCode)
                    {
                        StatusCode = statusCode;
                    }

                    ResponseMessage = String.Join(":", splitResponse.Skip(1)).Trim();
                }
            }

            if (StatusCode < 0)
            {
                throw new Exception("Response format unknown.");
            }
        }


        public virtual bool IsASuccessMessage()
        {
            return StatusCode == 0;
        }
    }
}
