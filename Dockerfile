FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY AppointmentBooking.API/*.csproj AppointmentBooking.API/
COPY AppointmentBooking.Application/*.csproj AppointmentBooking.Application/
COPY AppointmentBooking.Infrastructure/*.csproj AppointmentBooking.Infrastructure/
COPY AppointmentBooking.Domain/*.csproj AppointmentBooking.Domain/

RUN dotnet restore AppointmentBooking.API/AppointmentBooking.API.csproj

COPY . .

RUN dotnet publish AppointmentBooking.API/AppointmentBooking.API.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out ./

EXPOSE 3000

CMD ["dotnet", "AppointmentBooking.API.dll"]
