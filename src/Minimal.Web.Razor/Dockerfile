#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Minimal.Web.Razor/Minimal.Web.Razor.csproj", "Minimal.Web.Razor/"]
RUN dotnet restore "Minimal.Web.Razor/Minimal.Web.Razor.csproj"
COPY . .
WORKDIR "/src/Minimal.Web.Razor"
RUN dotnet build "Minimal.Web.Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Minimal.Web.Razor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minimal.Web.Razor.dll"]