FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build


# Install packages required by the image
RUN apk add --no-cache --virtual .bin-deps \
    bash \
    coreutils \
    curl \
    jq \
    openssl \
    socat
    
# Install acme.sh
COPY /install_acme.sh /app/install_acme.sh
RUN chmod +rx /app/install_acme.sh \
    && sync \
    && /app/install_acme.sh \
    && rm -f /app/install_acme.sh


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
