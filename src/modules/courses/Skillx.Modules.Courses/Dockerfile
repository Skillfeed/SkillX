FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Skillx.sln ./
COPY src/modules/courses/Skillx.Modules.Courses/Skillx.Modules.Courses.csproj src/modules/courses/Skillx.Modules.Courses/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/src/modules/courses/Skillx.Modules.Courses
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skillx.Modules.Courses.dll"]
