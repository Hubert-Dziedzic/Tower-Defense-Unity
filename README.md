# Tower Defense Game

## Game Description
**Tower Defense** is a dynamic 2D game where the player's task is to defend a specific area against waves of enemies. The player can strategically place towers to stop enemies from reaching their target. The game offers four different levels of gameplay, each with increasing difficulty.

## Technologies
- **Engine**: Unity
- **Programming Language**: C#
- **Optimization**: Techniques such as **Object Pooling** (storing frequently used components in memory) and limiting the use of physics calculations were implemented to ensure smooth gameplay.

## Graphics
The paths and background were created using free graphic assets by **Michale "Buch" Bucelli**, available on [OpenGameArt.org](https://opengameart.org/).

## Instructions
1. **Game Build**: The repository includes a ready-to-play game build along with the `BuildV1.1` folder. To play, download this folder and run the executable file caled `TowerDefenceUWM`.
2. **Gameplay**: Select a level, strategically place towers, and stop waves of enemies from reaching their target.

## Folder `assets`
The `assets` folder contains all the necessary files for the game, including scripts, scenes, and other resources. Here’s a brief overview of its structure:

- **Scripts**: Contains all C# scripts responsible for game logic, such as tower placement, enemy behavior, and level management.
- **Scenes**: Includes Unity scene files for each level of the game.
- **Graphics**: Stores all visual assets, such as sprites, textures, and animations.
- **Prefabs**: Includes reusable Unity prefabs for towers, enemies, and other game objects.
- **Other Resources**: Additional files such as configuration files, fonts, or any other assets required for the game.

## Note
A `.gitignore` file has been added to exclude unnecessary files from the repository. If you want to build the project yourself, make sure you have the Unity environment installed.
