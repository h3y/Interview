using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using InterviewTask.Models.Geo;
using Microsoft.Extensions.Configuration;

namespace InterviewTask.API
{
    public class APIManager
    {
        private static APIManager _apiManager;
	    private string _token;
	    private string _baseUrl;


	    public static APIManager Current() => _apiManager ?? throw new Exception("Init class before using");

        public APIManager(IConfiguration configuration)
	    {
		    _apiManager = this;
            _token = configuration["ConnectionSettings:APIKey"];
		    _baseUrl = configuration["ConnectionSettings:BaseURL"];
	    }

	    private async Task<T> _makeApiCall<T>(HttpRequestMessage request, bool noLog = false)
        {
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.SendAsync(request))
            {
                if (!noLog)
                    Console.WriteLine($"Sending request:{request.RequestUri}");
                string responseString;
                try
                {
                    responseString = await response.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error reading response ({request.RequestUri})", ex);
                }

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(responseString);
                else
                    throw new Exception($"Error making API call ({response.StatusCode} [{(int)response.StatusCode}]):{Environment.NewLine}{responseString}");
            }
        }

	    public async Task<ResponseGeo> GetGeoPosition(string address)
	    {
		    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/geocode/json?&address={address}&key={_token}"))
			    return await _makeApiCall<ResponseGeo>(request);
	    }

    }
}
