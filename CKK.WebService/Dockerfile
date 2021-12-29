FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CKK.WebService/CKK.WebService.csproj", "CKK.WebService/"]
RUN dotnet restore "CKK.WebService/CKK.WebService.csproj"
COPY . .
WORKDIR "/src/CKK.WebService"
RUN dotnet build "CKK.WebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CKK.WebService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CKK.WebService.dll"]
