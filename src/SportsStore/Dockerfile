FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/SportsStore/SportsStore.csproj src/SportsStore/
RUN dotnet restore src/SportsStore/SportsStore.csproj
COPY . .
WORKDIR /src/src/SportsStore
RUN dotnet build SportsStore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SportsStore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SportsStore.dll"]
