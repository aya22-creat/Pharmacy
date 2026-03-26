FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/Pharmacy.Web.Mvc/Pharmacy.Web.Mvc.csproj", "src/Pharmacy.Web.Mvc/"]
COPY ["src/Pharmacy.Web.Core/Pharmacy.Web.Core.csproj", "src/Pharmacy.Web.Core/"]
COPY ["src/Pharmacy.Application/Pharmacy.Application.csproj", "src/Pharmacy.Application/"]
COPY ["src/Pharmacy.Core/Pharmacy.Core.csproj", "src/Pharmacy.Core/"]
COPY ["src/Pharmacy.EntityFrameworkCore/Pharmacy.EntityFrameworkCore.csproj", "src/Pharmacy.EntityFrameworkCore/"]
WORKDIR "/src/src/Pharmacy.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Pharmacy.Web.Mvc", "src/Pharmacy.Web.Mvc"]
COPY ["src/Pharmacy.Web.Core", "src/Pharmacy.Web.Core"]
COPY ["src/Pharmacy.Application", "src/Pharmacy.Application"]
COPY ["src/Pharmacy.Core", "src/Pharmacy.Core"]
COPY ["src/Pharmacy.EntityFrameworkCore", "src/Pharmacy.EntityFrameworkCore"]
WORKDIR "/src/src/Pharmacy.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Pharmacy.Web.Mvc.dll"]
