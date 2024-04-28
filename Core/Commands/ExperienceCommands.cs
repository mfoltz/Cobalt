using Bloodstone.API;
using ProjectM;
using ProjectM.Network;
using Unity.Entities;
using VampireCommandFramework;
using VRising.GameData.Utils;

namespace Cobalt.Core.Commands
{
    public static class ExperienceCommands
    {

        [Command(name: "getExperienceProgress", shortHand: "gep", adminOnly: false, usage: ".gep", description: "Display your current experience progress.")]
        public static void GetExperienceCommand(ChatCommandContext ctx)
        {
            var SteamID = ctx.Event.User.PlatformId;
            if (DataStructures.PlayerExperience.TryGetValue(SteamID, out var xpData))
            {
                ctx.Reply($"You have <color=white>{xpData.Key}</color> experience points.");
            }
            else
            {
                ctx.Reply("You haven't earned any mastery points yet.");
            }
        }

        [Command(name: "logExperienceProgress", shortHand: "lep", adminOnly: false, usage: ".lep", description: "Toggles experience progress logging.")]
        public static void LogExperienceCommand(ChatCommandContext ctx)
        {
            var SteamID = ctx.Event.User.PlatformId;

            if (DataStructures.PlayerBools.TryGetValue(SteamID, out var bools))
            {
                bools["ExperienceLogging"] = !bools["ExperienceLogging"];
            }
            ctx.Reply($"Experience progress logging is now {(bools["ExperienceLogging"] ? "<color=green>enabled</color>" : "<color=red>disabled</color>")}.");
        }

        [Command(name: "setExperiencePoints", shortHand: "sep", adminOnly: true, usage: ".sep [Player] [ExperiencePoints]", description: "Sets player experience points.")]
        public static void MasterySetCommand(ChatCommandContext ctx, string name, int value)
        {

        }

        [Command(name: "logPrefabComponents", shortHand: "logprefab", adminOnly: true, usage: ".logprefab [#]", description: "WIP")]
        public static void LogUnitStats(ChatCommandContext ctx, int prefab)
        {
            PrefabGUID toLog = new(prefab);
        
            Entity entity = VWorld.Server.GetExistingSystem<PrefabCollectionSystem>()._PrefabGuidToEntityMap[toLog];
            if (entity == Entity.Null)
            {
                ctx.Reply("Entity not found.");
                return;
            }
            else
            {
                entity.LogComponentTypes();
                ctx.Reply("Components logged.");
            }
            
        }
        public class BuildingCostsToggle
        {
            private static bool buildingCostsFlag = false;

            private static SetDebugSettingEvent BuildingCostsDebugSetting = new()
            {
                SettingType = (DebugSettingType)5, // Assuming this is the correct DebugSettingType for building costs
                Value = false
            };

            [Command(name: "toggleBuildingCosts", shortHand: "tbc", adminOnly: true, usage: ".tbc", description: "Toggles building costs, useful for setting up a castle linked to your heart easily.")]
            public static void ToggleBuildingCostsCommand(ChatCommandContext ctx)
            {
                User user = ctx.Event.User;

                DebugEventsSystem existingSystem = VWorld.Server.GetExistingSystem<DebugEventsSystem>();
                buildingCostsFlag = !buildingCostsFlag; // Toggle the flag

                BuildingCostsDebugSetting.Value = buildingCostsFlag;
                existingSystem.SetDebugSetting(user.Index, ref BuildingCostsDebugSetting);
          
                ctx.Reply($"BuildingCostsDisabled: {BuildingCostsDebugSetting.Value}");
            }
        }
    }
}
