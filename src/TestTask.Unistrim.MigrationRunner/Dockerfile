FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5044

ENV ASPNETCORE_URLS=http://+:5044

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["TestTask.Unistrim.MigrationRunner/TestTask.Unistrim.MigrationRunner.csproj", "TestTask.Unistrim.MigrationRunner/"]
RUN dotnet restore "TestTask.Unistrim.MigrationRunner/TestTask.Unistrim.MigrationRunner.csproj"
COPY . .
WORKDIR "/src/TestTask.Unistrim.MigrationRunner"
RUN dotnet build "TestTask.Unistrim.MigrationRunner.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TestTask.Unistrim.MigrationRunner.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.Unistrim.MigrationRunner.dll"]