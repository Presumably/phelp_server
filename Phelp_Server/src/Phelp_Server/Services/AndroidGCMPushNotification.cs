using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phelp_Server.Services
{
    public class AndroidGCMPushNotification
    {
        private const string SERVER = "https://gcm-http.googleapis.com/gcm/send";

        //AQUI SERÀ O NOSSO ID GOOGLE APP E SENDER
        private const string GOOGLE_APP_ID = "AIzaSyDJUvYm_3TSpViNey1sYvg4A9E1DNl47BU";
        private const string SENDER_ID = "218667633387";

        public async Task<string> Send<T>(T postObject)
        {
            HttpResponseMessage result = null;
            using (var http = new HttpClient())
            {
                Uri theUri = new Uri(SERVER);
                http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + GOOGLE_APP_ID);
                http.DefaultRequestHeaders.TryAddWithoutValidation("Sender", "id=" + SENDER_ID);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Host = theUri.Host;
                try
                {
                    result = await http.PostAsync(theUri, postObject, new JsonMediaTypeFormatter());
                }
                catch (Exception e)
                {
                    string s = e.StackTrace;
                }
            }

            return result.Content.ToString();
        }
    }
}
