// builder is amember of type class webapplicationbuilder that responsaple for many thing such(configration and service)

using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

        //repository manegar manger
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext(builder.Configuration);

// registers only the controllers in IServiceCollection
//and add refrence to presentation layer to know where controller is
builder.Services.AddControllers()
.AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

//using automapper
builder.Services.AddAutoMapper(typeof(Program));
//method in extention
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
//identity
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

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

//adds endpoints for controller actions without specifying any routes.
app.MapControllers();

app.Run();
