# Publish before building docker image
# If we build inside docker image, then breakpoints are unbound since sourcemaps differ

# dotnet publish --configuration Debug --output /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt-get update && apt-get install -y unzip curl procps && curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
COPY /app ./
ENTRYPOINT ["dotnet", "shopping-microservice.dll"]