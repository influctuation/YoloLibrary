using YoloLibrary.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IYoloService, YoloService>();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => {
    var yoloService = app.Services.GetRequiredService<IYoloService>();
    Console.WriteLine("start warmup");
    yoloService.Detector.Warmup();
});

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