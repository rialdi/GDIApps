FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app

COPY ./api .
RUN dotnet restore

WORKDIR /app/GDIApps
RUN dotnet publish -c release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS runtime
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "GDIApps.dll"]




# COPY ptgdi.crt /usr/local/share/certificates/ptgdi.crt
# RUN update-ca-certificates
