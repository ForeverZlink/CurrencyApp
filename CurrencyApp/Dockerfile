#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CurrencyApp.csproj", "."]
RUN dotnet restore "./CurrencyApp.csproj"
COPY . .
WORKDIR "/src/."

RUN dotnet publish "CurrencyApp.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CurrencyApp.dll"]