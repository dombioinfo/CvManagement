# Build project
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app
COPY . ./
RUN dotnet publish "./BlazorBase/BlazorBase.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 8080
COPY --from=build-env ./app/publish .
ENTRYPOINT ["dotnet", "BlazorBase.dll"]
