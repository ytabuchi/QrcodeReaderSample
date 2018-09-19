using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QrcodeReader.Models
{
    public class WebApiClient
    {
        public static WebApiClient Instance { get; set; } = new WebApiClient();

        private Uri baseAddress = new Uri("https://someapi");
        private string Token = "";
        private object locker = new object();

        private readonly string _name = "admin";
        private readonly string _password = "p@ssw0rd";

        private WebApiClient() { }

        private void Initialize(string name, string password)
        {
            lock (locker)
            {
                if (string.IsNullOrEmpty(Token))
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = baseAddress;

                            var authContent = new StringContent($"grant_type=password&username={name}&password={password}");
                            authContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                            var authResponse = client.PostAsync("/Token", authContent).Result;
                            authResponse.EnsureSuccessStatusCode();
                            var authResult = authResponse.Content.ReadAsStringAsync().Result;

                            Token = JsonConvert.DeserializeObject<AuthResult>(authResult).AccessToken;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"【InitializeError】{ex.Source},{ex.Message},{ex.InnerException}");
                    }
                }
            }
        }

        public async Task<long> PostFormDataAsync(FormData formData)
        {
            Initialize(_name, _password);
            long _id = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(formData));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("api/xxx", content);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    _id = JsonConvert.DeserializeObject<FormData>(result).Id;

                    return _id;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"【PostError】{ex.Source},{ex.Message},{ex.InnerException}");
                    throw;
                }
            }
        }
    }

    class AuthResult
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
