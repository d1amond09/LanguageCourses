# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8083
EXPOSE 445


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LanguageCourses.WebAPI/LanguageCourses.WebAPI.csproj", "LanguageCourses.WebAPI/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["LanguageCourses.Domain/LanguageCourses.Domain.csproj", "LanguageCourses.Domain/"]
COPY ["LanguageCourses.Application/LanguageCourses.Application.csproj", "LanguageCourses.Application/"]
COPY ["LanguageCourses.Persistence/LanguageCourses.Persistence.csproj", "LanguageCourses.Persistence/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
RUN dotnet restore "./LanguageCourses.WebAPI/LanguageCourses.WebAPI.csproj"
COPY . .
WORKDIR "/src/LanguageCourses.WebAPI"
RUN dotnet build "./LanguageCourses.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LanguageCourses.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LanguageCourses.WebAPI.dll"]