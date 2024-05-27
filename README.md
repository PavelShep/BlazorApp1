## First Blazor WebAssemly App

### Opis Projektu
Ten projekt jest prostą aplikacją webową, która umożliwia wykonywanie operacji CRUD (Create, Read, Update, Delete) na obiektach Villas. Wykorzystuje on bazę danych SQL Server do przechowywania danych oraz framework MudBlazor do interfejsu użytkownika.

### Technologie
- SQL Server
- MudBlazor
- C#

### Funkcje
- Dodawanie nowych Villas
- Wyświetlanie listy Villas
- Edycja istniejących Villas
- Usuwanie Villas

### Instrukcje uruchomienia
1. Sklonuj repozytorium na swój lokalny komputer.
2. Zmień path do bazy danych, a potem uruchom skrypt migracji bazy danych SQL Server, aby przygotować schemat bazy danych. Użyj komendy:
```
dotnet ef migrations add NazwaMigracji
dotnet ef database update
```
3. Uruchom aplikację za pomocą Visual Studio
