# OcelotApiGatewayNetCore
mkdir OcelotApiGatewayNetCore
cd OcelotApiGatewayNetCore

dotnet new sln
dotnet new webapi -o ApiGateway
dotnet new webapi -o PersonApi
dotnet new webapi -o ReportApi

cd ApiGateway
dotnet add package Ocelot
dotnet restore

cd ..

dotnet sln OcelotApiGatewayNetCore.sln add ./ApiGateway/ApiGateway.csproj
dotnet sln OcelotApiGatewayNetCore.sln add ./PersonApi/PersonApi.csproj
dotnet sln OcelotApiGatewayNetCore.sln add ./ReportApi/ReportApi.csproj
dotnet restore