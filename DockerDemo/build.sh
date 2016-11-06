#!/bin/bash

dotnet restore src/DockerDemo.Api
dotnet build src/DockerDemo.Api
dotnet publish src/DockerDemo.Api -o publish/api

rm -rf docker/api/publish
cp -r publish/api docker/api/publish
docker build docker/api/ -t dockerdemo/api

dotnet restore src/DockerDemo.Consumer
dotnet build src/DockerDemo.Consumer
dotnet publish src/DockerDemo.Consumer -o publish/consumer

rm -rf docker/consumer/publish
cp -r publish/consumer docker/consumer/publish
docker build docker/consumer/ -t dockerdemo/consumer
