FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY *.csproj .
COPY *.config .
RUN dotnet restore --disable-parallel --configfile ./nuget.config 

COPY . .
RUN dotnet publish -c Release -o build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/build .
ENTRYPOINT [ "dotnet", "AngularDncApp.dll" ]