FROM microsoft/dotnet:1.0.1-core
MAINTAINER Kristofer Nissbrandt <nissbran@gmail.com>

WORKDIR /app
COPY /publish .
 
ENTRYPOINT ["dotnet", "DockerDemo.Consumer.dll"]
