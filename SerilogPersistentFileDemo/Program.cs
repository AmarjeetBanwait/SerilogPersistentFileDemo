using Serilog;
using SerilogPersistentFileDemo.Components;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
	.WriteTo.PersistentFile("Logs/webP.log", persistentFileRollingInterval: PersistentFileRollingInterval.Minute, preserveLogFilename: true, rollOnEachProcessRun: false)
	.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
