#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["host/EasyAbp.Abp.TagHelperPlus.Web.Unified/EasyAbp.Abp.TagHelperPlus.Web.Unified.csproj", "host/EasyAbp.Abp.TagHelperPlus.Web.Unified/"]
COPY ["host/EasyAbp.Abp.TagHelperPlus.Host.Shared/EasyAbp.Abp.TagHelperPlus.Host.Shared.csproj", "host/EasyAbp.Abp.TagHelperPlus.Host.Shared/"]
COPY ["src/EasyAbp.Abp.TagHelperPlus/EasyAbp.Abp.TagHelperPlus.csproj", "src/EasyAbp.Abp.TagHelperPlus/"]
RUN dotnet restore "host/EasyAbp.Abp.TagHelperPlus.Web.Unified/EasyAbp.Abp.TagHelperPlus.Web.Unified.csproj"
COPY . .
WORKDIR "/src/host/EasyAbp.Abp.TagHelperPlus.Web.Unified"
RUN dotnet build "EasyAbp.Abp.TagHelperPlus.Web.Unified.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyAbp.Abp.TagHelperPlus.Web.Unified.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyAbp.Abp.TagHelperPlus.Web.Unified.dll"]