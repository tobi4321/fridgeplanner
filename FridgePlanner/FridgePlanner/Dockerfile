FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FridgePlanner/FridgePlanner.csproj", "FridgePlanner/"]
RUN dotnet restore "FridgePlanner/FridgePlanner.csproj"
COPY . .
WORKDIR "/src/FridgePlanner"
RUN dotnet build "FridgePlanner.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FridgePlanner.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FridgePlanner.dll"]