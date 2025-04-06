# Use .NET 9 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app

# Use .NET 9 SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o /app

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app .

# Expose port 8080 for Railway
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApplication1.dll"]
