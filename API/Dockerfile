FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./ContainerBase64.Contracts/ContainerBase64.Contracts.csproj", "src/ContainerBase64.Contracts/"]
COPY ["./ContainerBase64.Core/ContainerBase64.Core.csproj", "src/ContainerBase64.Core/"]
COPY ["./ContainerBase64.API/ContainerBase64.API.csproj", "src/ContainerBase64.API/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/ContainerBase64.API/ContainerBase64.API.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/ContainerBase64.API/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l

# Set the environment variable to listen on port 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT [ "dotnet", "ContainerBase64.API.dll" ]