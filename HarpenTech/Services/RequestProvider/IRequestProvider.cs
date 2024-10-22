namespace HarpenTech.Services.RequestProvider;

// IRequestProvider interface defines the contract for making HTTP requests within the application
public interface IRequestProvider
{
    // Performs an HTTP GET request and returns the result of the specified type
    ReturnType Get<ReturnType>(string RequestURL);
    

    // Performs an HTTP GET request and returns the HttpResponseMessage with XML response
    HttpResponseMessage GetWithXmlResponse(string RequestURL);

    // Performs an authenticated HTTP GET request and returns the result of the specified type
    ReturnType Get<ReturnType>(string RequestURL, string token);

    // Performs an HTTP POST request with a payload and returns the result of the specified type
    ReturnType Post<ReturnType, PayloadType>(string RequestURL, PayloadType Payload);

    // Performs an HTTP POST request with payload, ignoring null properties, and returns the result of the specified type
    ReturnType PostWithIgnoreNull<ReturnType, PayloadType>(string RequestURL, PayloadType Payload);

    // Performs an authenticated HTTP POST request and returns the result of the specified type
    ReturnType Post<ReturnType>(string RequestURL, string ClientID, string SecretID);

    // Performs an HTTP PUT request with payload and returns the result of the specified type
    ReturnType Put<ReturnType, PayloadType>(string RequestURL, PayloadType Payload);

    // Sets the base address for the HTTP requests
    void SetBaseAddress(string BaseAddress);

    // Adds default request headers for all HTTP requests
    void AddDefaultRequestHeaders(string Name, string Value);

    // Clears all default request headers
    void ClearDefaultHeaders();

    // Adds authorization headers for HTTP requests
    void AddAuthorizationHeaders(string Name, string Value);

    // Adds basic authentication headers for HTTP requests
    void AddBasicAuthorization(string authenticationBytes);

    // Adds a default "Accept: application/json" header to HTTP requests
    void AddAcceptJSONHeader();

    // Adds a custom "Accept" header value to HTTP requests
    void AddAcceptJSONHeader(string header);

    // Asynchronously performs an HTTP POST request with payload and returns the result of the specified type
    Task<ReturnType> PostAsync<ReturnType, PayloadType>(string RequestURL, PayloadType Payload);

    // Performs an HTTP POST request without asynchronous execution and returns the result of the specified type
    ReturnType PostWithoutAsync<ReturnType, PayloadType>(string RequestURL, PayloadType Payload);

    // Performs an HTTP POST request with form-url-encoded content and returns the result of the specified type
    ReturnType PostAsFormUrlEncoded<ReturnType, PayloadType>(string RequestURL, FormUrlEncodedContent Payload);

    // Checks if the authentication header is set for HTTP requests
    bool IsAuthenticationHeaderSet();
}
