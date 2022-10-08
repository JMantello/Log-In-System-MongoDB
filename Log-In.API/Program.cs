using Log_In.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MongoCrud db = new MongoCrud("LoginDb", builder.Configuration.GetConnectionString("AWS-Oregon-Cluster"));
builder.Services.AddSingleton(db);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(IApplicationBuilder =>
{
    IApplicationBuilder.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
