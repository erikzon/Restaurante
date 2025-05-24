# Restaurante

# comandos de instalacion
dotnet tool install --global dotnet-ef
dotnet ef database drop
dotnet ef migrations add InitialModel
dotnet ef database update

# Configuracion base de datos
instalar la version SqlLocalDB de sqlserver
y apuntar a ella desde el appsettings.json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RestauranteDB;Trusted_Connection=True;MultipleActiveResultSets=true"


dotnet ef migrations add ConfigurarEliminacionEnCascada