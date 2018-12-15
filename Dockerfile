FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY SCOS.API/*.csproj ./SCOS.API/
#COPY utils/*.csproj ./utils/
WORKDIR /app/SCOS.API
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY SCOS.API/. ./SCOS.API/
#COPY utils/. ./utils/
WORKDIR /app/SCOS.API
RUN dotnet publish -c Release -o out


# test application -- see: dotnet-docker-unit-testing.md
#FROM build AS testrunner
#WORKDIR /app/tests
#COPY tests/. .
#ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/SCOS.API/out ./
ENTRYPOINT ["dotnet", "SCOS.API.dll"]
