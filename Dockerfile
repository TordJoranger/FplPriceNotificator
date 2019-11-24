#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY FplPriceNotificator/*.csproj ./
RUN dotnet restore 

COPY FplPriceNotificator/. ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FplPriceNotificator.dll"]