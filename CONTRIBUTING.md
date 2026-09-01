# Contributing to Dedalo

Thank you for your interest in **Dedalo**! This document establishes the workflow rules for both internal and external contributors, keeping the project organized and reducing the maintainers' workload.

## Code of Conduct

Contribute with mutual respect: no discrimination, offensive language, or toxic behavior. Technical disagreements are welcome; personal judgments are not. Treat every contribution — code, art, design — as the work of someone who wants to improve the project, just like you.

## Git Workflow

### Branching

Branch names must never be random. Always use the following format:

```
type/module-description
```

Where `type` is one of the tags listed below and `module` is either `core` or the city name in lowercase.

Valid examples:

- `feat/venezia-acqua-alta`
- `fix/core-pathfinding`
- `feat/matera-passaggi-nascosti`
- `docs/readme-update`
- `chore/gitignore-update`

### Commit Messages (Conventional Commits)

Every commit message must start with one of the following tags:

| Tag        | Usage                                                          |
|------------|----------------------------------------------------------------|
| `feat:`    | New features (e.g. `feat: add tide system to Venezia`)         |
| `fix:`     | Bug fixes (e.g. `fix: correct A* heuristic in Core`)           |
| `chore:`   | Routine updates (e.g. `chore: update .gitignore`)              |
| `refactor:`| Code improvements without behavior changes                     |
| `docs:`    | Documentation                                                  |
| `test:`    | Adding or updating tests                                       |
| `perf:`    | Performance improvements                                       |

Write messages in English, keeping them concise and descriptive.

### Pull Requests

1. Create a branch from `main` following the naming format.
2. Open a PR by filling in the automatic template.
3. Link the PR to an issue (e.g. `Resolves #42`).
4. Tick every box in the checklist before requesting a review.

## Project Architecture

The Unity project lives in the `Unity_Files/` folder. The core rules:

- **Core**: all base code (engine, general UI, AI, pathfinding) goes in `Unity_Files/Assets/Scripts/Core` and its subfolders. The Core must never depend on any city.
- **Cities**: each city module is self-contained. City-exclusive code goes in its own module (e.g. `Unity_Files/Assets/Città/Venezia/Scripts`), map data in `Data/` (JSON or ScriptableObjects), and exclusive visuals in `Art/`.

If a change requires touching both the Core and a city, first discuss with a maintainer whether the generic part should be extracted into the Core.

## Security

Never commit: API keys, tokens, `.env` files, keystores (`.keystore`, `.jks`), certificates (`.p12`, `.mobileprovision`), or personal data. Use `.env.example` as a reference for the required variables. The `.gitignore` is already configured to protect you.

## Getting Started

1. Fork the repository and clone it.
2. Open Unity Hub, select "Add project from disk" and choose the `Unity_Files/` folder.
3. Create a branch following the naming format.
4. Work, commit with Conventional Commits, and open a Pull Request.

Thank you for helping bring the labyrinths of Italy to life!
