#!/bin/bash
set -e



# Appliquer les migrations pour AppDbContext
dotnet ef database update 

# Démarrer l'application
exec dotnet DuendeServer.dll
