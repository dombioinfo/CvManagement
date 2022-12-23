# Build project
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app
COPY . ./
RUN dotnet publish "./BlazorBaseApi/BlazorBaseApi.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 7031
COPY --from=build-env ./app/publish .
ENV ASPNETCORE_URLS=http://+:7031
ENTRYPOINT ["dotnet", "BlazorBaseApi.dll"]