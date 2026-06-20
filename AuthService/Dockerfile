FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /src

COPY AuthService/AuthService.csproj AuthService/
RUN dotnet restore AuthService/AuthService.csproj

COPY AuthService/ AuthService/
WORKDIR /src/AuthService

EXPOSE 5167

CMD ["dotnet", "run", "--no-restore", "--urls", "http://0.0.0.0:5167"]
