FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PINChat.App/Server/PINChat.App.Server.csproj", "PINChat.App/Server/"]
COPY ["PINChat.App/Client/PINChat.App.Client.csproj", "PINChat.App/Client/"]
COPY ["PINChat.App/Shared/PINChat.App.Shared.csproj", "PINChat.App/Shared/"]
RUN dotnet restore "PINChat.App/Server/PINChat.App.Server.csproj"
COPY . .
WORKDIR "/src/PINChat.App/Server"
RUN dotnet build "PINChat.App.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PINChat.App.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PINChat.App.Server.dll"]
