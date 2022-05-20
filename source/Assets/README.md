# Reorderer
![](https://img.shields.io/badge/Mod_Version-{ReleaseVersion}-blue.svg)
![](https://img.shields.io/badge/Built_for_RimWorld-{GameVersion}-blue.svg)
![](https://img.shields.io/badge/Powered_by_Harmony-{HarmonyVersion}-blue.svg)

![Steam Subscribers](https://img.shields.io/badge/dynamic/xml.svg?label=Steam+Subscribers&query=//table/tr[2]/td[1]&colorB=blue&url=https://steamcommunity.com/sharedfiles/filedetails/%3Fid=1907019753&suffix=+total)
![GitHub Downloads](https://img.shields.io/github/downloads/Jaxe-Dev/Reorderer/total.svg?colorB=blue&label=GitHub+Downloads)

---

Ever wanted to rearrange the main button tabs at the bottom of the screen? How about architect categories?

This mod allows reordering those and even hiding main button tabs, including any added by other mods.
It should be 100% compatible with any mod, does not affect savegames in any way and removing this mod would reset orders back to default.

---

##### STEAM INSTALLATION
- **[Go to the Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=1907019753) and subscribe to the mod.**

---

##### NON-STEAM INSTALLATION
- **[Download the latest release](https://github.com/Jaxe-Dev/Reorderer/releases/latest) and unzip it into your *RimWorld/Mods* folder.**

---

The following base methods are patched with Harmony:
```C#
Postfix : RimWorld.MainButtonsRoot.Ctor
```
