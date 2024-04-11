<!--
This README was made using Louis3797's awesome-readme-template
-->
<div align="center">
  <h1>Demo Nickel Mod</h1>
  <img src="assets/characters/demomod_character_neutral_0.png" alt="logo" width="auto" height="auto" />
  <img src="assets/characters/demomod_character_squint_0.png" alt="logo" width="auto" height="auto" />
  <p>
    A demo mod for Cobalt Core 
  </p>
</div>


<!-- Table of Contents -->
# :notebook_with_decorative_cover: Table of Contents

- [About the project](#star2-about-the-demo-mod)
  * [What is Demo Mod](#books-what-is-demo-mod)
  * [Features](#dart-features)
  * [Screenshots](#camera-screenshots)
- [Making a mod](#running-getting-started)
  * [What is Nickel](#question-what-is-nickel)
  * [Prerequisites](#bangbang-prerequisites)
  * [Using Demo Mod as a template](#eyes-using-demo-mod-as-a-template)
  * [Basic files and folders](#file-folder-basic-files-and-foldes)
  * [Tips](#bulb-tips)
- [Other mods](gem-other-mods)


<!-- About -->
## :star2: About the project


<!-- What is Demo Mod -->
### :books: What is Demo Mod
Demo Mod is a small, simple mod for Cobalt Core with basic features, created so modders can use it as a starting point.


<!-- Features -->
### :dart: Features

- 1 Character
- 1 Ship
- 2 Cards
- 2 Artifacts


<!-- Screenshots -->
### :camera: Screenshots

<div align="center"> 
  <img src="assets/screenshots/newrunoptions.png" alt="screenshot" width="960" height="540" />
  <img src="assets/screenshots/cardexample.png" alt="screenshot" width="960" height="540" />
</div>


<!-- Making a Mod -->
## 	:wrench: Making a Mod


<!-- What is Nickel -->
### :question: What is Nickel
[Nickel](https://github.com/Shockah/Nickel/releases) is a mod loader for Cobalt Core developed by Shockah.
It allows the community to play and create mods with ease.
You will want to have Nickel downloaded in your machine before you start modding using this template.


<!-- Prerequisites -->
### :bangbang: Prerequisites
You will want to compile C#, and so it is recommended to use an IDE or similar.
There are many out there, but if you're new to modding, we recommend [Visual Studio Community](https://visualstudio.microsoft.com/vs/getting-started/).
_If you want to use VS, then make sure to check the '.NET desktop development' box during installation._


<!-- Using Demo Mod as a template-->
### :eyes: Using Demo Mod as a template
You can download this repository and start modding right away.


<!-- Basic files and folders -->
### :file_folder: Basic files and folders
`nickel.json` is used by Nickel to identify a mod's .dll file, it contains relevant info such as mod version and potential dependencies.
Here are some examples:
##### Shockah's Dracula mod
```
{
    "UniqueName": "Shockah.Dracula",
    "Version": "0.1.0",
    "RequiredApiVersion": "0.1.0",
    "EntryPointAssembly": "Dracula.dll",
    "LoadPhase": "AfterDbInit",
    "Dependencies": [
        {
            "UniqueName": "Shockah.Kokoro",
            "Version": "1.2.1"
        }
    ]
}
```
##### KBraid's Braid and Eili mod
```
{
  "UniqueName": "KBraid.BraidEili",
  "Version": "0.2.8",
  "RequiredApiVersion": "0.1.0",
  "EntryPointAssembly": "BraidEili.dll",
  "LoadPhase": "AfterDbInit",
  "Dependencies": [
    {
      "UniqueName": "Shockah.Kokoro",
      "Version": "1.2.1"
    }
  ]
}
```
##### Arin's Randall mod
```
{
    "UniqueName": "Arin.Randall",
    "Version": "0.0.1",
    "RequiredApiVersion": "0.2.0",
    "EntryPointAssembly": "RandallMod.dll",
}
```
`.csproj` encapsulates the project's settings and configuration.
If you need to manually feed the mod loader path to your project, ìn the same top folder as your `.csproj`, you can create the file `Configuration.props.user` with the following: (And you can modify the path to your modloader location)
```
<Project>
  <PropertyGroup>
    <ModLoaderPath>/PATH/TO/NICKEL/BINARIES/</ModLoaderPath>
  </PropertyGroup>
</Project>
```


<!-- Tips -->
### :bulb: Tips
#### Vanilla card count

| CREW MEMBER | # COMMON | # UNCOMMON | # RARE
|
| ![#44a5fc](https://via.placeholder.com/10/44a5fc?text=+) DIZZY | 9 | 7 | 5
| ![#fbbc04](https://via.placeholder.com/10/fbbc04?text=+) RIGGS | 9 | 7 | 5
| ![#c651f6](https://via.placeholder.com/10/c651f6?text=+) PERI | 12 | 7 | 5
| ![#5df7a1](https://via.placeholder.com/10/5df7a1?text=+) ISAAC | 10 | 7 | 5
| ![#f35281](https://via.placeholder.com/10/f35281?text=+) DRAKE | 9 | 7 | 5
| ![#9496ff](https://via.placeholder.com/10/9496ff?text=+) MAX | 10 | 7 | 5
| ![#fff3ce](https://via.placeholder.com/10/fff3ce?text=+) BOOKS | 11 | 7 | 5 
| ![#d4e8f4](https://via.placeholder.com/10/d4e8f4?text=+) CAT | 12 | 7 | 4 

#### Vanilla artifact count

| CREW MEMBER | # COMMON | # BOSS
|
| ![#44a5fc](https://via.placeholder.com/10/44a5fc?text=+) DIZZY | 4 | 2
| ![#fbbc04](https://via.placeholder.com/10/fbbc04?text=+) RIGGS | 3 | 2
| ![#c651f6](https://via.placeholder.com/10/c651f6?text=+) PERI | 3 | 2
| ![#5df7a1](https://via.placeholder.com/10/5df7a1?text=+) ISAAC | 4 | 2
| ![#f35281](https://via.placeholder.com/10/f35281?text=+) DRAKE | 5 | 1
| ![#9496ff](https://via.placeholder.com/10/9496ff?text=+) MAX | 4 | 3
| ![#fff3ce](https://via.placeholder.com/10/fff3ce?text=+) BOOKS | 5 | 1
| ![#d4e8f4](https://via.placeholder.com/10/d4e8f4?text=+) CAT | 3 | 1

<!-- Other mods -->
## :gem: Other mods
- [Shockah's Dracula WIP mod](https://github.com/Shockah/Cobalt-Core-Mods/tree/dev/dracula)
- [Arin's Randall WIP mod](https://github.com/UnicornArin/CobaltCoreRandall)
- [KBraid's Braid and Eili WIP mod](https://github.com/KBraid/cobalt-core-mods)