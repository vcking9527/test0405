ARG APP_VERSION=1.0.0

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
LABEL stage=intermediate
ARG APP_VERSION

# install npm && nuxt
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs

WORKDIR /source
  
# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/ELK.Demo.WebApi/*.csproj ./src/ELK.Demo.WebApi/

# execute nuget dll restore, avoid testing
RUN dotnet restore src/ELK.Demo.WebApi/ELK.Demo.WebApi.csproj
  
# can't not include folder obj/**, use the .dockerignore or it will cause error
COPY src/client-app/. ./src/client-app/
COPY src/ELK.Demo.WebApi/. ./src/ELK.Demo.WebApi/

# 設定npm套件的使用權限，否則preinstall、postinstall會失敗
RUN npm config set unsafe-perm true

# -c: Configuration
# -o: output file
# --no-restore: no use dotnet restore
RUN dotnet publish src/ELK.Demo.WebApi/ELK.Demo.WebApi.csproj -c release -o /app -p:Version=${APP_VERSION} --no-restore -nowarn:CS8618

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ARG APP_VERSION
ARG PORT=5001

ENV ASPNETCORE_URLS=http://*:${PORT}
LABEL AppVersion=${APP_VERSION}

# install System.Drawing.Common
# RUN apt-get update && apt-get install -y libgdiplus libc6-dev && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app
COPY --from=build /app ./
 
EXPOSE ${PORT}
ENTRYPOINT ["dotnet", "ELK.Demo.WebApi.dll"]