Source code for [barotrauma mod](https://steamcommunity.com/sharedfiles/filedetails/?id=3449389548) that's made to demonstrate so called "Inconsistent Bad Pointer References" in assembly C# mods and a way to detect them using GhostDetector

It's a [rare bug](https://github.com/evilfactory/LuaCsForBarotrauma/issues/245) that seems to appear only in c# assembly mods and is very hard to detect

### To use GhostDetector in your mod:
- Copy-paste [GhostDetector class](https://github.com/SomeRandomNoobKekeke/Barotrauma-Ghost-Detector/blob/main/CSharp/Client/GhostDetector.cs) into your mod
- Call GhostDetector.Activate()
- Call GhostDetector.Check() from inside your harmony patches

GhostDetector.Check() return true if it detects a ghost, so you could prevent execution of dead code
It'll also print to console that ghost is detected, if you don't want it just change the file or replace GhostDetector.OnDetect Action
