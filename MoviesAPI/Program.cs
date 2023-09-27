using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MoviesAPI.Models;
using MoviesAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IGenreService,GenreService>();
builder.Services.AddCors();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", info: new OpenApiInfo
    {
        Version = "v1",
        Title = "TestApi",
        Description = "My first API",
        TermsOfService = new Uri("https://www.google.com"),
        Contact =new OpenApiContact
        {
            Email= "test@domain.com",
            Name="Dema",
            Url= new Uri("https://www.google.com")
        },
        License=new OpenApiLicense
        {
            Name= " My License",
            Url= new Uri("https://www.google.com")
        }
        
    }) ;
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description="Enter your JWT key"

    }) ;


    options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name= "Bearer",
                In= ParameterLocation.Header
            },
            new List<string>()
        }

    });






});

 static void Main()
{
  int z=  SumMethod(3, 1);
}
 static int SumMethod(int y, int x)
{
    int sum = 0;
    return y + x;


}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());//to allow any url ||netowrk to access this app
app.UseAuthorization();

app.MapControllers();

app.Run();
