using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.IO;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;

using System.Runtime.CompilerServices;
[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
[assembly: IgnoresAccessChecksTo("BarotraumaCore")]

namespace Graveyard
{
  public partial class Mod : IAssemblyPlugin
  {
    public Harmony harmony = new Harmony("graveyard");

    public void Initialize()
    {
      GhostDetector.Activate();
      PatchAll();
    }

    /// <summary>
    /// Oh, i hope nothing funny happens with these patches
    /// </summary>
    public void PatchAll()
    {
      harmony.Patch(
        original: typeof(GameSession).GetMethod("StartRound", AccessTools.all, new Type[]{
            typeof(LevelData),
            typeof(bool),
            typeof(SubmarineInfo),
            typeof(SubmarineInfo),
          }
        ),
        postfix: new HarmonyMethod(typeof(Mod).GetMethod("GameSession_StartRound_Postfix"))
      );

      harmony.Patch(
        original: typeof(SettingsMenu).GetConstructors(AccessTools.all)[0],
        postfix: new HarmonyMethod(typeof(Mod).GetMethod("SettingsMenu_Constructor_Postfix"))
      );

      harmony.Patch(
        original: typeof(Character).GetConstructors(AccessTools.all)[1],
        postfix: new HarmonyMethod(typeof(Mod).GetMethod("Character_Constructor_Postfix"))
      );

      harmony.Patch(
        original: typeof(CampaignMode).GetMethod("AssignNPCMenuInteraction", AccessTools.all),
        postfix: new HarmonyMethod(typeof(Mod).GetMethod("CampaignMode_AssignNPCMenuInteraction_Postfix"))
      );
    }

    public static void GameSession_StartRound_Postfix()
    {
      GhostDetector.Check();
      Log("GameSession_StartRound_Postfix");
    }

    public static void SettingsMenu_Constructor_Postfix()
    {
      GhostDetector.Check();
      Log("SettingsMenu_Constructor_Postfix");
    }

    public static void Character_Constructor_Postfix()
    {
      GhostDetector.Check();
      Log("Character_Constructor_Postfix");
    }

    public static void CampaignMode_AssignNPCMenuInteraction_Postfix()
    {
      GhostDetector.Check();
      Log("CampaignMode_AssignNPCMenuInteraction_Postfix");
    }

    public static void Log(object msg, Color? color = null)
    {
      color ??= Color.Cyan;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", color * 0.8f, color);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose() { }
  }
}