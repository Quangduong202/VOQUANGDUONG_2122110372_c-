# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj và restore (tối ưu cache)
COPY ["ConnetDB/ConnetDB.csproj", "ConnetDB/"]
RUN dotnet restore "ConnetDB/ConnetDB.csproj"

# Copy toàn bộ source
COPY . .
WORKDIR "/src/ConnetDB"

# Build và publish
RUN dotnet publish "ConnetDB.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime (nhẹ hơn)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Chạy với user không phải root (tốt cho security)
USER app

# Copy từ stage build
COPY --from=build /app/publish .

# Port mặc định của Render thường là 10000
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ConnetDB.dll"]