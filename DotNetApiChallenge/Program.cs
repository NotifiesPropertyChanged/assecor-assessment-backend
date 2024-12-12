using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.DataContext;
using DotNetApiChallenge.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Very poorly done, this should have been done via a config option but here I was running out of time
var csvLocation = Path.Combine(Directory.GetCurrentDirectory(), "sample-input.csv");

//Probably should be an Scoped service
builder.Services.AddTransient<DbSeedService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//You can use any DB here, make sure that if neeeded, the server gets connected.
//Connection strings could be predefined in appsettings.json, be passed as a launch set of parameters
//or be provided through an options provider who would read registry keys,
//configuration file or anything

//builder.Services.AddDbContext<ApiChallengeDataContext>(options
//        => options.UseSqlite($"Data Source={builder.Configuration.GetConnectionString("SqLiteConnection") }"));
//For ease of testing/deployment I just used an Inmemory DB
builder.Services.AddDbContext<ApiChallengeDataContext>(opt =>
    opt.UseInMemoryDatabase("DotNetApiChallengeDB"));
var app = builder.Build();

ApiChallengeDataContext datac = null; 
SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    IServiceScope scope = scopedFactory.CreateScope();
    var dataAdapter = new CsvPersonDataAdapter();
    dataAdapter.SetDataSource(csvLocation);

    var service = scope.ServiceProvider.GetService<DbSeedService>();

    service.SetDataAdapter(dataAdapter);
    service.Seed();
    datac = service.GetContext();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
