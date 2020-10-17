#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HorrorVue.Web/HorrorVue.Web.csproj", "HorrorVue.Web/"]
COPY ["HorrorVue.Data/HorrorVue.Data.csproj", "HorrorVue.Data/"]
COPY ["HorrorVue.Services/HorrorVue.Services.csproj", "HorrorVue.Services/"]
RUN dotnet restore "HorrorVue.Web/HorrorVue.Web.csproj"
COPY . .
WORKDIR "/src/HorrorVue.Web"
RUN dotnet build "HorrorVue.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HorrorVue.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HorrorVue.Web.dll"]