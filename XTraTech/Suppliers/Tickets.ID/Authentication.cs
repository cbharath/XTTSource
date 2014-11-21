using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adapters;

namespace Tickets.ID
{
    public class Authentication
    {
        /// <summary>
        /// this will return the token.
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            HttpPostApi http = new HttpPostApi();
            ParseResponse parseResponse = new ParseResponse();
            string token = string.Empty;

            string postString = Configurations.URL + "apiv1/payexpress?method=getToken&secretkey=" + Configurations.SecToken + "&output=" + Configurations.OutPut;

            string ResponseXML = http.invokeURL(postString, HttpMethod.Post, ContentType.textXml);

            token = parseResponse.ParseToken(ResponseXML);
            return token;
        }
    }
}
