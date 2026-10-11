# Keep_It_Short_API
This is a Mock API project used for URL shortening

## Requisiti

| Cosa | Versione | Note |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | 8.0 o superiore | Senza questo non si riesce nè a compilare nè ad avviare l'API (include l'ASP.NET Core) |
| Pacchetti NuGet | - | Vengono già fetchati in automatico in build phase |

<br/>

## Compilazione

Aprire con VS Pro / VSCode la solution in `.\KeepItShort\KeepItShort.sln` e compilare manualmente in debug, oppure da terminale lanciare i seguenti comandi:

```bash
dotnet restore
dotnet build --configuration Debug
```

<br/>

## Esecuzione

```bash
dotnet run --launch-profile https
```

L'app ascolta su:

- `https://localhost:7074`
- `http://localhost:5177` (reindirizza a al primo ip in caso di tentativo)

Le porte sono configurate nel profilo `https` di `Properties/launchSettings.json` (`applicationUrl`) e possono essere cambiate lì.

Al primo avvio con HTTPS verrà chiesto di fidarsi del certificato di sviluppo, basterà confermare i Dialog che appaiono.

<br/>

## Documentazione API

Le API sono documentate (in automatico) da Swagger che si apre di base all'avvio in ambiente `Development` all'url: `https://localhost:7074/swagger`.

<br/>

## Note

- I dati sono salvati **in memoria**: al riavvio dell'app si perdono.
- Lo stesso url può essere associato a codici diversi (EG: 1245 => url1, 6789 => url1)
