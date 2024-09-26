var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.ConfigureHealthChecks(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//HealthCheck Middleware
app.MapHealthChecks(
    "/api/health",
    new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);
app.UseHealthChecksUI(
    delegate(Options options)
    {
        options.UIPath = "/healthcheck-ui";
        //options.AddCustomStylesheet("./HealthCheck/Custom.css");
    }
);

app.MapControllers();

app.Run();
