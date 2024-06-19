using Microsoft.Azure.Cosmos;
using CandidateApplication.Services;

var builder = WebApplication.CreateBuilder(args);

var cosmosDbSection = builder.Configuration.GetSection("CosmosDb");
builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClintAsync(cosmosDbSection).GetAwaiter().GetResult());
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ICandidateResponseService, CandidateResponseService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
static async Task<CosmosdbService> InitializeCosmosClintAsync(IConfigurationSection configurationSection)
{
    string databaseName = configurationSection["DatabaseName"];
    string questionContainerName = configurationSection["QuestionContainerName"];
    string responseContainerName = configurationSection["ResponseContainerName"];
    string account = configurationSection["Account"];
    string key = configurationSection["Key"];
    var client = new CosmosClient(account, key);
    var cosmosDbService = new CosmosdbService(client, databaseName, questionContainerName, responseContainerName);
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(questionContainerName, "/ApplicationId");
    await database.Database.CreateContainerIfNotExistsAsync(responseContainerName, "/CandidateId");
    return cosmosDbService;
}
