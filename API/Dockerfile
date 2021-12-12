FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./ContainerNinja.Contracts/ContainerNinja.Contracts.csproj", "src/ContainerNinja.Contracts/"]
COPY ["./ContainerNinja.Migrations/ContainerNinja.Migrations.csproj", "src/ContainerNinja.Migrations/"]
COPY ["./ContainerNinja.Infrastructure/ContainerNinja.Infrastructure.csproj", "src/ContainerNinja.Infrastructure/"]
COPY ["./ContainerNinja.Core/ContainerNinja.Core.csproj", "src/ContainerNinja.Core/"]
COPY ["./ContainerNinja.API/ContainerNinja.API.csproj", "src/ContainerNinja.API/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/ContainerNinja.API/ContainerNinja.API.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/ContainerNinja.API/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "ContainerNinja.API.dll" ]