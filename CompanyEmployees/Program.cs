// builder is amember of type class webapplicationbuilder that responsaple for many thing such(configration and service)

using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

        //repository manegar manger
builder.Services.ConfigureRepositoryManager();

        // registers only the controllers in IServiceCollection
builder.Services.AddControllers();
        //method in extention
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();





var app = builder.Build();


if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
         //HTTP Strict Transport Security "Protect websites from attacks by phishing attackers"
    app.UseHsts();

app.UseHttpsRedirection();
        //enables using static files for the request
app.UseStaticFiles();
        //help us during application deployment and authontication.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
