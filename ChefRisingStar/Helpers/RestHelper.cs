using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static ChefRisingStar.Helpers.RestHelper;

namespace ChefRisingStar.Helpers
{
    internal class RestHelper
    {
        //private const string RemoteUrl = "https://localhost:44322/";
        private const string RemoteUrl = "https://ltdc.azurewebsites.net/";
        private string _bearerToken;

        internal class Routes
        {
            internal const string Users = "api/Users";
        }

        internal enum RestVerbs
        {
            Get,
            Post,
            Put,
            Delete
        }

        internal event EventHandler AuthenticationRequired;
        
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

        internal Task<TR> Put<T, TR>(T obj, string endpoint)
        {
            return RestCall<T, TR>(RestVerbs.Put, obj, endpoint);
        }

        private async Task<TR> RestCall<T, TR>(RestVerbs verb, T obj, string endpoint)
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

                    switch (verb)
                    {
                        case RestVerbs.Post:
                            response = await client.PostAsync($"{RemoteUrl}{endpoint}", content);
                            break;
                        case RestVerbs.Put:
                            response = await client.PutAsync($"{RemoteUrl}{endpoint}", content);
                            break;
                        case RestVerbs.Delete:
                            response = await client.DeleteAsync($"{RemoteUrl}{endpoint}");
                            break;
                    }

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
                    else
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
                        {
                            if (AuthenticationRequired != null)
                                AuthenticationRequired(this, null);
                        }

                        //return response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending rest {verb}:{ex}");
                }
            }

            return default(TR);
        }

        internal async Task<T> Get<T>(string route)
        {
            return await Get<T>(route, string.Empty);
        }
        internal async Task<T> Get<T>(string route, string urlParameters)
        {
            try
            {
                using (HttpClient client = ClientFactory())
                {
                    client.BaseAddress = new Uri($"{RemoteUrl}{route}");

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
