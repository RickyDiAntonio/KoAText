KoAText: A Console-Based Narrative RPG

KoAText is a single-player, console-based text RPG built in C# as a first experience into the dotnet/C# language. Was designed to play in less than 15 minutes but could be expanded much further given time

Features

Notable Design Decisions

Turn system built around a queue to cleanly separate actor turns and logic
Abilities created via a factory pattern for consistency and expandability
TownRegistry to centralize and modularize connections between towns, wilderness areas, shops, and quests
Trait-based enemy design for diverse combat experiences

💪 Turn-Based Combat System
Dynamic initiative queue using a custom battle state controller
Support for player and enemy abilities, including damage-over-time (DoT), healing, shielding, and status effects
AI-controlled monster behavior with randomized ability selection

📅 Player Progression
Experience points and leveling system
Destiny-based stat growth paths (e.g., Might, Sorcery, Finesse)-unique combinations build unique characters based on progression totals and hybrid choices
Stat-driven character growth including Attack, Defense, Speed, Mana, and HP

📖 Narrative and World
Town systems with shops, unique inventories, and local randomized quests based off town registry entries
Journal/Quest log and area tracking for immersive exploration

🚀 Inventory and Loot
Inventory system supporting item collection, buying/selling, and quest items
Loot drops and rewards from enemies and key events
Gold economy system and town-specific shop inventories

🔎 Modular Design
Core interfaces (IActor, Actions) enabling polymorphic combat and shared behaviors
Event-driven quest tracking system for Kill and Collect quests
ASCII art for town identity and monster flavor
Technologies Used

C# (.NET 7)
Visual Studio
Console application architecture
Unit testing via MSTest




Future Plans
Expanded quest types and faction reputation system
Enhanced player choice consequences and story branches
Procedural wilderness encounters and randomized loot tables

How to Run

Clone the repository

Open in Visual Studio

Set the startup project to KoAText

Run the application and begin your journey

Developed as part of a solo project to deepen C# skills and explore game system architecture.

"You are not one thing. The Zone doesn't allow it. The Zone builds complexity—from bone, from code, from ash."
