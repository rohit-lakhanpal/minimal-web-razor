using Microsoft.ApplicationInsights.DataContracts;

var builder = WebApplication.CreateBuilder(args);

// Add health checks
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddRazorPages();

// Log http request headers that start with X-AZURE or X-FORWARDED
#region CustomAppInsightsMiddlewareHandler
var addHttpHeadersToAppInsights =
    (RequestTelemetry telemetry, IHeaderDictionary headers) => headers
        .Where(h => h.Key.StartsWith("X-AZURE", StringComparison.InvariantCultureIgnoreCase) ||
                    h.Key.StartsWith("X-FORWARDED", StringComparison.InvariantCultureIgnoreCase))
        .ToList()
        .ForEach(h => telemetry.Properties.Add(h.Key.Replace("-", string.Empty), h.Value));

var customAppInsightsMiddlewareHandler = async (HttpContext c, Func<Task> next) =>
{
    await next.Invoke();
    var requestTelemetry = c.Features.Get<RequestTelemetry>();
    if (requestTelemetry != null)
        addHttpHeadersToAppInsights(requestTelemetry, c.Request.Headers);
};
#endregion AppInsightsMiddlewareHandler

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Map health checks
app.MapHealthChecks("/healthz");

// Map custom app insights middlware
app.Use(customAppInsightsMiddlewareHandler);

app.Run();
