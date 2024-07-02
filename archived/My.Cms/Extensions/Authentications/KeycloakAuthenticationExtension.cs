using AspNet.Security.OAuth.Keycloak;
using My.Cms.Configurations;
using My.Cms.Extensions.LoginProviders;
using Umbraco.Cms.Api.Management.Security;

namespace My.Cms.Extensions.Authentications;

public static class KeycloakAuthenticationExtension
{
    public static IUmbracoBuilder AddBackOfficeKeycloakAuthentication(this IUmbracoBuilder builder)
    {
        var configuration = new KeycloakConfiguration(builder.Config);
        builder.Services.ConfigureOptions<KeycloakMemberExternalLoginProviderOptions>();
        
        builder.AddBackOfficeExternalLogins(logins =>
        {
            logins.AddBackOfficeLogin(
                backOfficeAuthenticationBuilder =>
                {
                    var schemeName =
                        BackOfficeAuthenticationBuilder.SchemeForBackOffice(KeycloakMemberExternalLoginProviderOptions
                            .SchemeName);
                    ArgumentNullException.ThrowIfNull(schemeName);
                    backOfficeAuthenticationBuilder.AddKeycloak(
                        schemeName,
                        options =>
                        {
                            options.ClientId = configuration.ClientId;
                            options.ClientSecret = configuration.ClientSecret;
                            options.AccessType = KeycloakAuthenticationAccessType.Confidential;
                            options.BaseAddress = new Uri(configuration.BaseAddress);
                            options.Realm = configuration.Realm;
                            options.Version = new Version(configuration.Version);
                        });
                });
        });
        return builder;
    }
}