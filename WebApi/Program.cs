using WebApi.MainDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainDemoDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (!builder.Environment.IsDevelopment())
        {
            var hostname = Environment.GetEnvironmentVariable("MSSQL_HOSTNAME");
            var port     = Environment.GetEnvironmentVariable("MSSQL_PORT");
            var dbname   = Environment.GetEnvironmentVariable("MSSQL_DBNAME");
            var username = Environment.GetEnvironmentVariable("MSSQL_USERNAME");
            var password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD");
            connectionString = string.Format(connectionString, hostname, port, dbname, username, password);
        }
        options.UseSqlServer(connectionString);
    }
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MainDemoDbContext>();
    db.Database.Migrate();
}

app.Run();
