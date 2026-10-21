# GitHub Copilot Instructions for [AFLD]AnimalsPreferGrazing (Continued)

## Mod Overview and Purpose
The [AFLD]AnimalsPreferGrazing mod aims to adjust the eating behavior of animals in RimWorld by making them prefer live plants for grazing when available. This ensures that animals, such as those in pastures, will consume accessible grass before resorting to other food sources like hay. An additional feature in version 1.3 makes corpse-eating animals prioritize unreserved corpses.

## Key Features and Systems
- **Grazing Preference**: Animals will prioritize eating live plants that are accessible, ensuring a more natural and efficient feeding process.
- **Corpse Eating Behavior (v1.3)**: Animals that consume corpses will first seek out unreserved corpses, assisting in managing resources better.
- **Integration via Harmony**: The mod uses Harmony patches to modify the behavior of existing game code without altering the core files.
- **Mod Compatibility**: Carefully designed to minimize conflicts, though it is recommended to test mod interactions individually.

## Coding Patterns and Conventions
- **Code Structure**: Organized into well-defined classes with separate files for settings and main operations:
  - `Main.cs` handles the initialization of the feature.
  - `Settings` related code is encapsulated in `_AFLD_AnimalsPreferGrazingSettings.cs` and `_AFLD_AnimalsPreferGrazingMod.cs`.
- **Modularity**: Patches and settings are designed to be easily extendable, following standard C# coding practices.
- **Naming Conventions**: Classes and methods use a clear and descriptive naming convention, reflecting their purpose (e.g., `FoodUtility_TryFindBestFoodSourceFor_Patch`).

## XML Integration
- **About.xml**: The XML file defines mod metadata and its dependencies, such as `brrainz.harmony`, crucial for proper loading and functional dependency injection.

## Harmony Patching
- **Prefix Patching**: The core functionality relies on a prefix patch for `FoodUtility.TryFindBestFoodSourceFor`. This patch alters logic for animal food sourcing, where:
  - Animals that eat plants will bypass the function under specific conditions.
- **Patch Location**: Implemented in `FoodUtility_TryFindBestFoodSourceFor.cs`, with validators defined for different food types.
- **Limitations**: The patch effectively bypasses some behaviors for plant-eating animals, so users should consider this in their gameplay and mod interactions.

## Suggestions for Copilot
- **Refactor and Optimize Code**: Use Copilot to suggest optimizations, particularly in logic flow and efficient use of Harmony patches.
- **Error Handling Enhancements**: Consider enhancing error handling in patching code to gracefully manage any unexpected scenarios.
- **Documentation Improvements**: Leveraging Copilot for generating comments and documentation to assist with mod maintainability and clarity.
- **Testing Support**: Employ it to draft unit tests that ensure each component behaves as expected under different scenarios.

By following these guidelines and utilizing GitHub Copilot's capabilities effectively, contributions and enhancements to the [AFLD]AnimalsPreferGrazing mod can be more streamlined and robust.


This markdown document provides an encompassing view of the mod's purpose, key systems, and how developers can use Copilot to assist in the development and enhancement process. The instructions emphasize structure, conventions, and usage, offering a comprehensive resource for contributing developers.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

