using cfg.api.ConfigurationData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConfigurationDataProvider, ConfigurationDataProvider>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cfg-site",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://localhost:4201")
                .AllowAnyHeader()
                .WithMethods("GET, PATCH, DELETE, PUT, POST, OPTIONS");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("cfg-site");

app.Run();

// else tests won't work, due to some deps.json not found / compiled
public partial class Program { }