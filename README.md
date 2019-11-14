# Reorderer
![](https://img.shields.io/badge/Mod_Version-1.2-blue.svg)
![](https://img.shields.io/badge/Built_for_RimWorld-1.0-blue.svg)
![](https://img.shields.io/badge/Powered_by_Harmony-1.2.0.1-blue.svg)

---

Ever wanted to rearrange the main button tabs at the bottom of the screen? How about architect categories?

This mod allows reordering those and even hiding main button tabs, including any added by other mods.
It should be 100% compatible with any mod, does not affect savegames in any way and removing this mod would reset orders back to default.

---

##### INSTALLATION
- **[Download the latest release](https://github.com/Jaxe-Dev/Reorderer/releases/latest) and unzip it into your *RimWorld/Mods* folder.**

---

The following base methods are patched with Harmony:
```C#
Postfix : RimWorld.MainButtonsRoot.Ctor
```
