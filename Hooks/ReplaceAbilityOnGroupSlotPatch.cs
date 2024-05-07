﻿using Cobalt.Core;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;

namespace Cobalt.Hooks
{
    [HarmonyPatch(typeof(ReplaceAbilityOnSlotSystem), "OnUpdate")]
    public class ReplaceAbilityOnSlotSystem_Patch
    {
        private static void Prefix(ReplaceAbilityOnSlotSystem __instance)
        {
            Plugin.Log.LogInfo("ReplaceAbilityOnSlotSystemPrefix called...");
            NativeArray<Entity> entities = __instance.__query_1482480545_0.ToEntityArray(Allocator.TempJob);
            try
            {
                foreach(Entity entity in entities)
                {
                    entity.LogComponentTypes();
                }
            }
            finally
            {
                entities.Dispose();
            }
        }
    }
}