# How to fix the self signed certificate warning in postman

If you get the following error: "System.Net.Http.HttpRequestException: The SSL connection could not be established, see inner exception.
---> System.Security.Authentication.AuthenticationException: The remote certificate is invalid because of errors in the certificate chain: UntrustedRoot"


In the configuration of the services you need to add the following :

```c#
services.AddHttpClient<ExampleClient>(cfg =>
    {
        cfg.BaseAddress = new Uri("https://api-example.local.com");
    })
    .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
    {
        AllowAutoRedirect = false,
        ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
        SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
    });
```