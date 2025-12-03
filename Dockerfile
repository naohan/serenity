# Usar la imagen oficial de .NET 9.0 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY ["serenity.sln", "./"]
COPY ["serenity/serenity.csproj", "./serenity/"]
COPY ["serenity.Application/serenity.Application.csproj", "./serenity.Application/"]
COPY ["serenity.Domain/serenity.Domain.csproj", "./serenity.Domain/"]
COPY ["serenity.Infrastructure/serenity.Infrastructure.csproj", "./serenity.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "serenity.sln"

# Copiar todo el código fuente
COPY . .

# Compilar la solución
WORKDIR "/src/serenity"
RUN dotnet build "serenity.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "serenity.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Render inyecta PORT automáticamente
# .NET Core detecta automáticamente la variable PORT si está disponible
# Si no está disponible, usa el puerto por defecto
ENV ASPNETCORE_URLS=http://+:8080

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "serenity.dll"]

