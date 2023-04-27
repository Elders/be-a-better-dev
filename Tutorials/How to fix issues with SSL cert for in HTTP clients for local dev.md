- This is an example of how to configure HTTP Client for local development if there is an issue with SSL certificate
    1. Add this when registering the HTTP client
`
if (env.IsDevelopment())
{
    services
    .AddHttpClient("your_client_name", client => client.BaseAddress = address)
    .AddClientAccessTokenHandler("your_client_name")
    .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
    {
        AllowAutoRedirect = false,
        ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
        SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
    });
}
else
{
    services
    .AddHttpClient("your_client_name", client => client.BaseAddress = address)
    .AddClientAccessTokenHandler("your_client_name");
}
`