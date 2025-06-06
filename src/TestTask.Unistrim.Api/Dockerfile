FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5044

ENV ASPNETCORE_URLS=http://+:5044

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["TestTask.Unistrim.Api/TestTask.Unistrim.Api.csproj", "TestTask.Unistrim.Api/"]
RUN dotnet restore "TestTask.Unistrim.Api/TestTask.Unistrim.Api.csproj"
COPY . .
WORKDIR "/src/TestTask.Unistrim.Api"
RUN dotnet build "TestTask.Unistrim.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TestTask.Unistrim.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.Unistrim.Api.dll"]