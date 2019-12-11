FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

WORKDIR /app

COPY ./ ./

RUN dotnet publish ./Gotham3 -c Release -o out
RUN dotnet test ./Gotham3/Gotham3.UnitTests

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS runtime

WORKDIR /app

COPY --from=build ./app/out .

EXPOSE 6002

ENTRYPOINT ["dotnet" , "Gotham3.web.dll"]
