#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorApp1/BlazorApp1.csproj", "BlazorApp1/"]
COPY ["Common/Common.csproj", "Common/"]
#RUN dotnet workload install wasm-tools
RUN dotnet restore "BlazorApp1/BlazorApp1.csproj"
COPY . .
WORKDIR "/src/BlazorApp1"
RUN dotnet build "BlazorApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp1.csproj" -c Release -o /app/publish

FROM docker.io/nginx:stable-alpine AS final
WORKDIR "/usr/share/nginx/html"
COPY --from=publish app/publish/wwwroot .
COPY BlazorApp1/nginx.conf /etc/nginx/nginx.conf