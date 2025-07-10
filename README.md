# Margo-Sudoku

Un simulateur de jeu de Sudoku multi-threadé où deux joueurs s'affrontent pour remplir une grille Sudoku 9x9. Le jeu propose un gameplay tour par tour avec validation automatique des coups et système de score.

## Description du Projet

Il s'agit d'une application console C# qui simule un jeu de Sudoku compétitif entre deux joueurs. Le jeu utilise le multi-threading pour gérer les tours des joueurs de manière concurrente et comprend :

- **Grille Sudoku 9x9** avec les règles standard du Sudoku
- **Gameplay compétitif à deux joueurs** avec mécaniques tour par tour
- **Validation automatique des coups** garantissant le respect des règles du Sudoku
- **Suivi des scores** pour chaque joueur
- **Opérations thread-safe** utilisant des verrous pour éviter les conditions de course
- **Génération de coups aléatoires** à des fins de démonstration

## Prérequis

Pour exécuter ce projet, vous avez besoin de :

- **SDK .NET 8.0** ou version ultérieure
- **Windows, macOS, ou Linux** (compatible multi-plateforme)

### Installation de .NET 8.0

1. **Windows** : Téléchargez depuis [Microsoft .NET Downloads](https://dotnet.microsoft.com/download)
2. **macOS** : Utilisez Homebrew : `brew install dotnet`
3. **Linux** : Suivez le [guide d'installation officiel](https://docs.microsoft.com/en-us/dotnet/core/install/linux)

Vérifiez l'installation :
```bash
dotnet --version
```

## Comment Exécuter

### Option 1 : Utilisation de dotnet CLI

1. **Naviguez vers le répertoire du projet** :
   ```bash
   cd SudokuMargo
   ```

2. **Compilez le projet** :
   ```bash
   dotnet build
   ```

3. **Exécutez l'application** :
   ```bash
   dotnet run
   ```

### Option 2 : Utilisation de Visual Studio

1. Ouvrez `SudokuMargo.sln` dans Visual Studio
2. Définissez `SudokuMargo` comme projet de démarrage
3. Appuyez sur F5 ou cliquez sur "Démarrer le débogage"

### Option 3 : Utilisation de Visual Studio Code

1. Ouvrez le dossier du projet dans VS Code
2. Installez l'extension C# si ce n'est pas déjà fait
3. Appuyez sur F5 pour exécuter l'application

## Exécution des Tests

Pour exécuter les tests unitaires :

```bash
cd SudokuMargo.Core.Tests
dotnet test
```

## Structure du Projet

- **`SudokuMargo/`** - Application console principale
- **`SudokuMargo.Core/`** - Logique de jeu Sudoku principale
- **`SudokuMargo.Core.Tests/`** - Tests unitaires pour la logique principale

## Règles du Jeu

- Les joueurs placent à tour de rôle des nombres (1-9) sur la grille Sudoku
- Chaque coup valide rapporte 1 point au joueur
- Les coups invalides (violant les règles du Sudoku) sont ignorés
- Le jeu continue jusqu'à ce que la grille soit pleine ou que le nombre maximum de coups soit atteint
- Règles du Sudoku : Aucun nombre en double dans les lignes, colonnes ou sous-grilles 3x3

## Exemple de Sortie

L'application affichera :
- Les coups des joueurs en temps réel
- Les tentatives de coups invalides
- Les scores finaux des deux joueurs
- La grille Sudoku complétée
