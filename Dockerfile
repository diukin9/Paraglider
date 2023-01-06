FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Paraglider.Application/Paraglider.Application/Paraglider.Application.csproj", "Paraglider.Application/Paraglider.Application/"]
COPY ["Paraglider.Data/Paraglider.Data.EntityFrameworkCore/Paraglider.Data.EntityFrameworkCore.csproj", "Paraglider.Data/Paraglider.Data.EntityFrameworkCore/"]
COPY ["Paraglider.Domain/Paraglider.Domain.RDB/Paraglider.Domain.RDB.csproj", "Paraglider.Domain/Paraglider.Domain.RDB/"]
COPY ["Paraglider.Infrastructure/Paraglider.Infrastructure.Common/Paraglider.Infrastructure.Common.csproj", "Paraglider.Infrastructure/Paraglider.Infrastructure.Common/"]
COPY ["Paraglider.Domain/Paraglider.Domain.Common/Paraglider.Domain.Common.csproj", "Paraglider.Domain/Paraglider.Domain.Common/"]
COPY ["Paraglider.Domain/Paraglider.Domain.NoSQL/Paraglider.Domain.NoSQL.csproj", "Paraglider.Domain/Paraglider.Domain.NoSQL/"]
COPY ["Paraglider.Data/Paraglider.Data.MongoDB/Paraglider.Data.MongoDB.csproj", "Paraglider.Data/Paraglider.Data.MongoDB/"]
COPY ["Paraglider.Infrastructure/Paraglider.Clients.Gorko/Paraglider.Clients.Gorko.csproj", "Paraglider.Infrastructure/Paraglider.Clients.Gorko/"]
COPY ["Paraglider.Infrastructure/Paraglider.MailService/Paraglider.MailService.csproj", "Paraglider.Infrastructure/Paraglider.MailService/"]
COPY ["Paraglider.Application/Paraglider.Application.BackgroundJobs/Paraglider.Application.BackgroundJobs.csproj", "Paraglider.Application/Paraglider.Application.BackgroundJobs/"]
RUN dotnet restore "Paraglider.Application/Paraglider.Application/Paraglider.Application.csproj"
COPY . .
WORKDIR "/src/Paraglider.Application/Paraglider.Application"
RUN dotnet build "Paraglider.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Paraglider.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paraglider.Application.dll"]
