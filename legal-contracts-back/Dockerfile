# 1️⃣ Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código
COPY . ./

# Publicar en Release
RUN dotnet publish -c Release -o /app/publish

# 2️⃣ Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar la app publicada
COPY --from=build /app/publish ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "legal-contracts-back.dll"]