#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FakeUserApi/FakeUserApi.csproj", "FakeUserApi/"]
RUN dotnet restore "FakeUserApi/FakeUserApi.csproj"
COPY . .
WORKDIR "/src/FakeUserApi"
RUN dotnet build "FakeUserApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeUserApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeUserApi.dll"]

ENV DefaultConnection="Server=172.17.0.2,1433;Database=FakeUser;MultipleActiveResultSets=true;User Id = SA; Password = p97B98100";