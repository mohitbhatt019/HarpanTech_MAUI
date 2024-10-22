namespace HarpenTech.Services.RequestProvider;

// HttpRequestExceptionEx class extends the standard HttpRequestException to include additional information about the HTTP status code
public class HttpRequestExceptionEx : HttpRequestException
{
    // Gets the HTTP status code associated with the exception
    public System.Net.HttpStatusCode HttpCode { get; }

    // Constructor for HttpRequestExceptionEx with only the HTTP status code
    public HttpRequestExceptionEx(System.Net.HttpStatusCode code) : this(code, null, null)
    {
    }

    // Constructor for HttpRequestExceptionEx with the HTTP status code and a custom message
    public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message) : this(code, message, null)
    {
    }

    // Constructor for HttpRequestExceptionEx with the HTTP status code, a custom message, and an inner exception
    public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message, Exception inner) : base(message, inner)
    {
        HttpCode = code;
    }
}
