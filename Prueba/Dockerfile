FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "./Prueba.csproj"
RUN dotnet publish "./Prueba.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Prueba.dll" ]