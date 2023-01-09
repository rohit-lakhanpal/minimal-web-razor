# A Minimal Web Application (Razor Pages)

## Overview
This is a Minimal ASP.NET (7.0.0.0) Web Application that:

- Exposes health checks via the /healthz (via Microsoft.Extensions.Diagnostics.HealthChecks)
- Redirects all other requests to a catch-all Razor Page that outputs the http connection, header and query properties.

You can use this application when testing different hosting configurations.

## How to run this sample
1. Clone the repo
    > git clone https://github.com/rohit-lakhanpal/minimal-web-razor.git

2. Build the solution
    > cd minimal-web-razor/src

    > dotnet build Minimal.Web.sln

3. Run the project
    > dotnet run Minimal.Web.sln --project Minimal.Web.Razor

![Run Output](docs/run.jpg)

## Expected output
After navigating to the url (based on `dotnet run Minimal.Web.sln --project Minimal.Web.Razor`) this is what you can expect.
![Previewt](docs/preview.jpg)

## About the code
This minimal application is built on the ASP.NET core base template. I have additionally added the following packages:
- Microsoft.Extensions.Diagnostics.HealthChecks