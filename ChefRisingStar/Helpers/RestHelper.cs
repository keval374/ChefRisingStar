using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChefRisingStar.Helpers
{
    internal class RestHelper
    {
        //private const string RemoteUrl = "https://localhost:44322/";
        private const string RemoteUrl = "https://ltdc.azurewebsites.net/";
        private string _bearerToken;
        
        internal void SetBearer(string token)
        {
            _bearerToken = token;
        }
        
        internal async void Post<T>(T obj, string endpoint)
        {
            using (HttpClient client = ClientFactory())
            {
                try
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{endpoint}");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(_bearerToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                    // Initialization
                    HttpResponseMessage response = new HttpResponseMessage();

                    var objAsJson = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

                    // HTTP POST  
                    response = await client.PostAsync($"{RemoteUrl}{endpoint}", content);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var jsonStr = response.Content.ReadAsStringAsync().Result;

                        if (string.IsNullOrEmpty(jsonStr))
                        {
                            throw new ArgumentNullException("jsonStr", "Unable to post");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending post:{ex}");
                }
            }
        }

        internal async Task<TR> Post<T, TR>(T obj, string endpoint)
        {
            using (HttpClient client = ClientFactory())
            {
                try
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{endpoint}");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(_bearerToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                    // Initialization
                    HttpResponseMessage response = new HttpResponseMessage();

                    var objAsJson = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

                    // HTTP POST  
                    response = await client.PostAsync($"{RemoteUrl}{endpoint}", content);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var jsonStr = response.Content.ReadAsStringAsync().Result;

                        if (string.IsNullOrEmpty(jsonStr))
                        {
                            throw new ArgumentNullException("jsonStr", "Unable to post");
                        }

                        TR jsonObject = JsonConvert.DeserializeObject<TR>(jsonStr);
                        return jsonObject;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending post:{ex}");
                }
            }

            return default(TR);
        }

        internal async Task<T> Get<T>(string subType, string urlParameters)
        {
            try
            {
                using (HttpClient client = ClientFactory())
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{subType}");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(_bearerToken))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

                    // List data response.
                    var response = await client.GetAsync(urlParameters);  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var jsonStr = response.Content.ReadAsStringAsync().Result;

                        T jsonObject = JsonConvert.DeserializeObject<T>(jsonStr);
                        return jsonObject;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetDevices: {ex}");
            }

            return default(T);
        }

        private HttpClient ClientFactory()
        {
#if DEBUG
         
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                var client = new HttpClient(httpClientHandler);
                return client;
         
#endif

            return new HttpClient();

        }

    }
}
