#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/MudBlazorApp.Server.csproj", "Server/"]
COPY ["Client/MudBlazorApp.Client.csproj", "Client/"]
COPY ["../../../MicroManager/src/Common/Services/Common.Shared/Common.Shared.csproj", "../../../MicroManager/src/Common/Services/Common.Shared/"]
COPY ["Infrastructure/MudBlazorApp.Infrastructure.csproj", "Infrastructure/"]
COPY ["Shared/MudBlazorApp.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/MudBlazorApp.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "MudBlazorApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MudBlazorApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WUW.MicroManagerWasm.MudBlazorApp.Server.dll"]