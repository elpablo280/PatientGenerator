FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PatientGenerator/PatientGenerator.csproj", "PatientGenerator/"]
RUN dotnet restore "PatientGenerator/PatientGenerator.csproj"
COPY . .
WORKDIR "/src/PatientGenerator"
RUN dotnet build "PatientGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PatientGenerator.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientGenerator.dll"]