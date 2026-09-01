# Contribuire a Dedalo

Grazie per l'interesse in **Dedalo**! Questo documento stabilisce le regole del flusso di lavoro per collaboratori interni ed esterni, così da mantenere il progetto ordinato e ridurre il carico dei manutentori.

## Codice di Condotta

Contribuisci con rispetto reciproco: nessuna discriminazione, linguaggio offensivo o comportamento tossico. Le discussioni tecniche sono benvenute, i giudizi personali no. Tratta ogni contributo — codice, art, design — come il lavoro di una persona che vuole migliorare il progetto, esattamente come te.

## Flusso di lavoro Git

### Branching

I branch non devono avere nomi casuali. Usa sempre il formato:

```
tipo/modulo-descrizione
```

Dove `tipo` è uno dei tag elencati sotto e `modulo` è `core` oppure il nome della città in minuscolo.

Esempi validi:

- `feat/venezia-acqua-alta`
- `fix/core-pathfinding`
- `feat/matera-passaggi-nascosti`
- `docs/readme-update`
- `chore/gitignore-update`

### Messaggi di Commit (Conventional Commits)

Ogni messaggio di commit deve iniziare con uno dei seguenti tag:

| Tag        | Uso                                                      |
|------------|----------------------------------------------------------|
| `feat:`    | Nuove funzionalità (es. `feat: add tide system to Venezia`) |
| `fix:`     | Bug risolti (es. `fix: correct A* heuristic in Core`)     |
| `chore:`   | Aggiornamenti di routine (es. `chore: update .gitignore`) |
| `refactor:`| Ottimizzazioni del codice senza cambi di comportamento    |
| `docs:`    | Documentazione                                            |
| `test:`    | Aggiunta o modifica di test                               |
| `perf:`    | Miglioramenti delle prestazioni                           |

Scrivi i messaggi in inglese, concisi e descrittivi.

### Pull Request

1. Crea un branch dal main con il formato corretto.
2. Apri una PR compilando il template automatico.
3. Collega la PR a un'issue (es. `Risolve #42`).
4. Verifica tutte le caselle della checklist prima di richiedere la review.

## Architettura del progetto

Il progetto Unity vive nella cartella `Unity_Files/`. Le regole fondamentali:

- **Core**: tutto il codice base (motore, UI generale, IA, pathfinding) va in `Unity_Files/Assets/Scripts/Core` e sottocartelle. Il Core non deve dipendere da nessuna città.
- **Città**: ogni modulo città è autonomo. Il codice esclusivo di una città va nel suo modulo (es. `Unity_Files/Assets/Città/Venezia/Scripts`), i dati mappa in `Data/` (JSON o ScriptableObjects) e le grafiche esclusive in `Art/`.

Se una modifica richiede di toccare sia il Core sia una città, valuta prima con un manutentore se la parte generica vada estratta nel Core.

## Sicurezza

Non committare mai: chiavi API, token, file `.env`, keystore (`.keystore`, `.jks`), certificati (`.p12`, `.mobileprovision`) o dati personali. Usa `.env.example` come riferimento per le variabili necessarie. Il `.gitignore` è già configurato per proteggerti.

## Primi passi

1. Fai il fork del repository e clonalo.
2. Apri Unity Hub, seleziona "Add project from disk" e scegli la cartella `Unity_Files/`.
3. Crea un branch con il formato corretto.
4. Lavora, committa con Conventional Commits e apri una Pull Request.

Grazie per contribuire a rendere i labirinti d'Italia più vivi!
