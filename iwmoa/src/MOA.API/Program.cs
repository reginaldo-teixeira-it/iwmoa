using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder( args );

// Add services
builder.Services.AddCors();
builder.Services.AddResponseCompression( options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat( new[] { "application/json" } );
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat( new[] { "image/jpeg", "image/png", "application/font-woff2", "image/svg+xml" } );
    options.EnableForHttps = true;
} );

//var key = Encoding.ASCII.GetBytes( Settings.Secret );
//builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
//    .AddJwtBearer( x =>
//    {
//        x.RequireHttpsMetadata = false;
//        x.SaveToken = true;
//        x.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey( key ),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    } );

// === Context
//builder.Services.AddDbContext<DataContext>( opt => opt.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );
//builder.Services.AddScoped<DataContext, DataContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc( "v1", new OpenApiInfo
    {
        Title = "IWMOA - Infowest Manager Onion Architecture"
        ,
        Description = "Api para testes de arquitetura Onion"
        ,
        Version = "1.0.0"
    } );
    c.CustomSchemaIds( ( type ) => type.ToString()
        .Replace( "[", "_" )
        .Replace( "]", "_" )
    .Replace( ",", "-" )
        .Replace( "`", "_" ) );
    c.ResolveConflictingActions( apiDescriptions => apiDescriptions.First() );
} );

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
 
app.UseSwaggerUI( c =>
{
    c.SwaggerEndpoint( "/swagger/v1/swagger.json", "MOA API V1" );
    c.DocumentTitle = "IW Manager Onion Architecture";
} );

app.UseRouting();

// global cors policy
app.UseCors( x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader() );

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();
app.UseStaticFiles();

app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
} );

app.Run();
