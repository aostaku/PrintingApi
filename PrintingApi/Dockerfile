# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5600
EXPOSE 5681

COPY ./https/aspnetapp.pfx /https/aspnetapp.pfx
RUN dotnet dev-certs https --trust

# Image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY PrintingApi/PrintingApi.csproj PrintingApi/
RUN dotnet restore "PrintingApi/PrintingApi.csproj"

# Copy the entire application and build
COPY . .
WORKDIR "/src/PrintingApi"
RUN dotnet build "PrintingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PrintingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrintingApi.dll"]