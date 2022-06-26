FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DriversExam.API/DriversExam.API.csproj", "DriversExam.API/"]
RUN dotnet restore "DriversExam.API/DriversExam.API.csproj"
COPY . .
WORKDIR "/src/DriversExam.API"
RUN dotnet build "DriversExam.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DriversExam.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DriversExam.API.dll"]
