using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Extensions;
using My.Cms.Extensions.Authentications;
using Nikcio.UHeadless;
using Nikcio.UHeadless.Defaults.ContentItems;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    // My Custom
    .AddBackOfficeKeycloakAuthentication()
    .AddUHeadless(opt =>
    {
        opt.DisableAuthorization = true;
        opt.AddDefaults();
        opt.AddQuery<ContentByRouteQuery>();
        opt.AddQuery<ContentByGuidQuery>();
    })
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

// My Custom App
GraphQLEndpointConventionBuilder graphQlEndpointBuilder = app.MapUHeadless();
// Only enable the GraphQL IDE in development
if (!builder.Environment.IsDevelopment())
{
    graphQlEndpointBuilder.WithOptions(new GraphQLServerOptions()
    {
        Tool =
        {
            Enable = false,
        }
    });
}

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
