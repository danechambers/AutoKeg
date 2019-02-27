FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy sln and restore as distinct layers
COPY *.sln ./
COPY AutoKeg.ISR.Service/*.csproj ./AutoKeg.ISR.Service/
COPY AutoKeg.ISR.Snapshot/*.csproj ./AutoKeg.ISR.Snapshot/
COPY AutoKeg.Configuration/*.csproj ./AutoKeg.Configuration/
COPY AutoKeg.Tests/*.csproj ./AutoKeg.Tests/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out -r linux-arm

# Build runtime image
FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build-env /app/AutoKeg.ISR.Service/out .
ENTRYPOINT ["./autokeg"]
