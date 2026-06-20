FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /src

COPY RegistrosService/RegistrosService.csproj RegistrosService/
RUN dotnet restore RegistrosService/RegistrosService.csproj

COPY RegistrosService/ RegistrosService/
WORKDIR /src/RegistrosService

EXPOSE 5026

CMD ["dotnet", "run", "--no-restore", "--urls", "http://0.0.0.0:5026"]
