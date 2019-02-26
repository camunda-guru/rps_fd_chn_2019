FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 33268
EXPOSE 44398

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore


# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


FROM build AS publish
RUN dotnet publish "musicstore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "musicstore.dll"]