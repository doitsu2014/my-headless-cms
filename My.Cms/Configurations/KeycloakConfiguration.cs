namespace My.Cms.Configurations;

public class KeycloakConfiguration
{
    private readonly IConfiguration _configuration;

    public KeycloakConfiguration(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string BaseAddress => _configuration.GetValue<string>("Keycloak:BaseAddress");
    public string ClientId => _configuration.GetValue<string>("Keycloak:ClientId");
    public string ClientSecret => _configuration.GetValue<string>("Keycloak:ClientSecret");
    public string Realm => _configuration.GetValue<string>("Keycloak:Realm");
    public string Version => _configuration.GetValue<string>("Keycloak:Version");
}