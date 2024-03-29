FROM mcr.microsoft.com/dotnet/core/sdk AS publish
WORKDIR /src

COPY . .
RUN dotnet publish "src/buckstore.auth.service.api.v1" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/core/aspnet

WORKDIR /app
COPY --from=publish /app/publish .

# EXPOSE 5000
# EXPOSE 5001
# ENTRYPOINT ["dotnet", "buckstore.auth.service.api.v1.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet buckstore.auth.service.api.v1.dll
