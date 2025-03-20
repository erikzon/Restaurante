# Restaurante
dotnet tool install --global dotnet-ef
dotnet ef database drop
dotnet ef migrations add InitialModel
dotnet ef database update
