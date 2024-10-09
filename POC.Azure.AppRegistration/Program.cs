using POC.Azure.AppRegistration.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<GraphService>();

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

app.MapGet("/api/users/{userPrincipalName}", async (string userPrincipalName, GraphService graphService) =>
{
	var user = await graphService.GetUserDetailsAsync(userPrincipalName);
	return Results.Ok(user);
})
.WithName("GetUserByUserPrincipalName")
.WithOpenApi();

app.MapPost("/api/users/batch", async (List<string> userPrincipalNames, GraphService graphService) =>
{
	var users = await graphService.GetUserListAsync(userPrincipalNames);
	return Results.Ok(users);
})
.WithName("GetUserListByUserPrincipalNames")
.WithOpenApi();

app.Run();