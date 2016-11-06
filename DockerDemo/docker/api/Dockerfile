FROM microsoft/aspnetcore:1.0.1
MAINTAINER Kristofer Nissbrandt <nissbran@gmail.com>

WORKDIR /app
COPY /publish .
 
EXPOSE 5000
 
ENTRYPOINT ["dotnet", "DockerDemo.Api.dll"]
