using HarpenTech.Services.SecureServiceStorage;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HarpenTech.Services.RequestProvider;

// RequestProvider class implements the IRequestProvider interface for making HTTP requests within the application
public class RequestProvider : IRequestProvider
{    
    // HttpClient instance for performing HTTP requests
    private HttpClient Client;
    private  ISecureStorageService _secureStorageService;

    // Constructor for the RequestProvider class
    public RequestProvider()
    {
       
        // Initialize the HttpClient instance and clear default request headers
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
    }

    // Adds the default "Accept: application/json" header to HTTP requests
    public void AddAcceptJSONHeader()
    {
        MediaTypeWithQualityHeaderValue AcceptJSON = new MediaTypeWithQualityHeaderValue("application/json");
        Client.DefaultRequestHeaders.Accept.Add(AcceptJSON);
    }

    // Adds a custom "Accept" header value to HTTP requests
    public void AddAcceptJSONHeader(string header)
    {
        MediaTypeWithQualityHeaderValue AcceptJSON = new MediaTypeWithQualityHeaderValue(header);
        Client.DefaultRequestHeaders.Accept.Add(AcceptJSON);
    }

    // Adds default request headers for all HTTP requests
    public void AddDefaultRequestHeaders(string Name, string Value)
    {
        Client.DefaultRequestHeaders.Add(Name, Value);
    }

    // Adds authorization headers for HTTP requests
    public void AddAuthorizationHeaders(string Name, string Value)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Name, Value);
    }

    // Performs an HTTP GET request and returns the result of the specified type
    public T Get<T>(string RequestURL)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Set a timeout of 3 seconds
                client.Timeout = TimeSpan.FromSeconds(1);

                HttpResponseMessage httpResponse = client.GetAsync(RequestURL).Result;

                // Rest of your code...

                return Convert<T>(httpResponse);
            }
        }
        catch (TaskCanceledException ex)
        {
            // Handle timeout exception
            throw new TimeoutException("The API call timed out.", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new Exception("An error occurred while making the API call.", ex);
        }
    }

    #region Get Commented Code For Testing
    // Performs an HTTP GET request and returns the result of the specified type
    //public T Get<T>(string RequestURL)
    //{
    //    HttpResponseMessage httpResponse = Client.GetAsync(RequestURL).Result;

    //    //if (httpResponse.Headers.TryGetValues("Last-Save-Date-Time", out var lastSaveDateTimeHeaderValues))
    //    //{
    //    //    var lastSaveDateTimeHeaderValue = lastSaveDateTimeHeaderValues.FirstOrDefault();

    //    //    if(lastSaveDateTimeHeaderValue != null)
    //    //    {
    //    //        // Parse the header value as needed
    //    //        if (DateTimeOffset.TryParseExact(lastSaveDateTimeHeaderValue, "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var lastSaveDateTime))
    //    //        {
    //    //            _secureStorageService = new SecureStorageService();
    //    //            // Use the lastSaveDateTime as needed
    //    //            _secureStorageService.SaveLastUpdateDateTimeOffSet(lastSaveDateTime);
    //    //        }
    //    //    }

    //    //}

    //    return Convert<T>(httpResponse);
    //}
    #endregion



    // Performs an HTTP GET request and returns the HttpResponseMessage with XML response
    public HttpResponseMessage GetWithXmlResponse(string RequestURL)
    {
        return Client.GetAsync(RequestURL).Result;
    }

    // Performs an authenticated HTTP GET request and returns the result of the specified type
    public T Get<T>(string RequestURL, string token)
    {
        HttpClient httpClient1 = new HttpClient();
        httpClient1.BaseAddress = null;
        httpClient1.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        HttpResponseMessage httpResponse = httpClient1.GetAsync(RequestURL).Result;
        return Convert<T>(httpResponse);
    }

    // Performs an HTTP POST request with a payload and returns the result of the specified type
    public ReturnType Post<ReturnType, PayloadType>(string RequestURL, PayloadType Payload)
    {        
        // Serialize the payload to JSON and create StringContent
        string JSON = JsonConvert.SerializeObject(Payload);
        var content = new StringContent(JSON, Encoding.UTF8, "application/json");
        // Perform the POST request and return the result
        HttpResponseMessage httpResponse = Client.PostAsync(RequestURL, content).Result;
        return Convert<ReturnType>(httpResponse);
    }

    // Performs an HTTP POST request with payload, ignoring null properties, and returns the result of the specified type
    public ReturnType PostWithIgnoreNull<ReturnType, PayloadType>(string RequestURL, PayloadType Payload)
    {
        // Serialize the payload to JSON with ignoring null properties and create StringContent
        string JSON = JsonConvert.SerializeObject(Payload, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        var content = new StringContent(JSON, Encoding.UTF8, "application/json");

        // Perform the POST request and return the result
        HttpResponseMessage httpResponse = Client.PostAsync(RequestURL, content).Result;
        return Convert<ReturnType>(httpResponse);
    }

    // Performs an HTTP POST request with form-url-encoded content and returns the result of the specified type
    public ReturnType PostAsFormUrlEncoded<ReturnType, PayloadType>(string RequestURL, FormUrlEncodedContent Payload)
    {
        // Perform the POST request and return the result
        HttpResponseMessage httpResponse = Client.PostAsync(RequestURL, Payload).Result;
        string apiResponse = httpResponse.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<ReturnType>(apiResponse);
    }

    // Performs an authenticated HTTP POST request and returns the result of the specified type
    public ReturnType Post<ReturnType>(string RequestURL, string ClientID, string SecretID)
    {

        // Create form-url-encoded content with client credentials
        FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", ClientID },
                    { "client_secret", SecretID }
                });

        // Perform the POST request and return the result
        HttpResponseMessage httpResponse = Client.PostAsync(RequestURL, content).Result;
        return Convert<ReturnType>(httpResponse);
    }

    // Asynchronously performs an HTTP POST request with payload and returns the result of the specified type
    public async Task<ReturnType> PostAsync<ReturnType, PayloadType>(string RequestURL, PayloadType Payload)
    {
        HttpResponseMessage httpResponse;
        try
        {
            // Serialize the payload to JSON and create StringContent
            string JSON = JsonConvert.SerializeObject(Payload);
            var content = new StringContent(JSON, Encoding.UTF8, "application/json");

            // Perform the POST request asynchronously
            httpResponse = await Client.PostAsync(RequestURL, content);
        }
        catch (Exception)
        {
            throw;
        }

        // Return the result of the POST request
        return await ConvertAsync<ReturnType>(httpResponse);
    }


    //*********** Rest of the methods for handling different HTTP requests...***********
    public ReturnType PostWithoutAsync<ReturnType, PayloadType>(string RequestURL, PayloadType Payload)
    {
        string JSON = JsonConvert.SerializeObject(Payload);
        var content = new StringContent(JSON, Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = Client.PostAsync(RequestURL, content).Result;

        return Convert<ReturnType>(httpResponse);
    }
    public ReturnType Put<ReturnType, PayloadType>(string RequestURL, PayloadType Payload)
    {
        string JSON = JsonConvert.SerializeObject(Payload);
        var content = new StringContent(JSON, Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = Client.PutAsync(RequestURL, content).Result;

        return Convert<ReturnType>(httpResponse);
    }
    public void SetBaseAddress(string BaseAddress)
    {
        Client.BaseAddress = new Uri(BaseAddress);
    }


    // Helper method to convert HttpResponseMessage to the specified type
    private T Convert<T>(HttpResponseMessage httpResponse)
    {
        T ResponseObject = default(T);

        // Check if the HTTP response is successful
        if (httpResponse.IsSuccessStatusCode)
        {
            // Read the JSON data from the response and deserialize it to the specified type
            string JSON_Data = httpResponse.Content.ReadAsStringAsync().Result;
            ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
        }
        else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {

            // Read the JSON data from the response and deserialize it to the specified type
            string JSON_Data = httpResponse.Content.ReadAsStringAsync().Result;
            ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
        }
        else
        {

            // Read the JSON data from the response and deserialize it to the specified type
            string JSON_Data = httpResponse.Content.ReadAsStringAsync().Result;
            ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
        }

        return ResponseObject;
    }

    // Helper method to asynchronously convert HttpResponseMessage to the specified type
    private async Task<T> ConvertAsync<T>(HttpResponseMessage httpResponse)
    {
        // Initialize the response object with the default value for the specified type
        T ResponseObject = default(T);
        try
        {
            // Check if the HTTP response is successful
            if (httpResponse.IsSuccessStatusCode)
            {
                // Read the JSON data from the response asynchronously
                string JSON_Data = await httpResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON data to the specified type
                ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
            }
            else
            {
                // Read the JSON data from the response asynchronously
                string JSON_Data = await httpResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON data to the specified type
                ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
            }
        }
        catch (Exception ex)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                // Read the JSON data from the response asynchronously
                string JSON_Data = await httpResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON data to the specified type
                ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
            }
            else
            {
                // Read the JSON data from the response asynchronously
                string JSON_Data = await httpResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON data to the specified type
                ResponseObject = JsonConvert.DeserializeObject<T>(JSON_Data);
            }
        }

        // Return the deserialized response object
        return ResponseObject;
    }

    // Clears all default request headers in the HttpClient instance
    public void ClearDefaultHeaders()
    {
        Client.DefaultRequestHeaders.Clear();
    }

    // Checks if an authentication header is set in the HttpClient instance
    public bool IsAuthenticationHeaderSet()
    {
        return Client.DefaultRequestHeaders.Authorization != null;
    }

    // Adds a basic authorization header to the HttpClient instance using the provided authentication bytes
    public void AddBasicAuthorization(string authenticationBytes)
    {
        Client.DefaultRequestHeaders.Add("Authorization", "Basic " + authenticationBytes);
    }


}