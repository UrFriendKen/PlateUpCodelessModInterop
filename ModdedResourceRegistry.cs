using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;
using CodelessModInterop.Customs;

namespace CodelessModInterop
{
    public static class ModdedResourceRegistry
    {
        private static Dictionary<int, string> _moddedGDO = new Dictionary<int, string>();  // Value is mod name the GDO belongs to
        private static Dictionary<string, Material> _moddedMaterials = new Dictionary<string, Material>();
        private static Dictionary<string, UnlockEffectSet> _unlockEffectSets = new Dictionary<string, UnlockEffectSet>();
        private static Dictionary<string, Localisation> _localisations = new Dictionary<string, Localisation>();
        private static Dictionary<string, IEffectType> _effectTypes = new Dictionary<string, IEffectType>();

        public static bool TryGetModdedGDO<T>(int id, out T gdo, out string modName, bool codelessOnly = false) where T : GameDataObject
        {
            if (_moddedGDO.TryGetValue(id, out modName))
            {
                GameData.Main.TryGet(id, out gdo, warn_if_fail: true);
                return true;
            }
            else if (!codelessOnly && CustomGDO.GDOs.ContainsKey(id))
            {
                modName = CustomGDO.GDOs[id].ModName;
                GameData.Main.TryGet(id, out gdo, warn_if_fail: true);
                return true;
            }
            gdo = null;
            return false;
        }

        public static bool IsModdedGDO(int id, bool codelessOnly = false)
        {
            return TryGetModdedGDO(id, out GameDataObject _, out string _, codelessOnly);
        }

        public static bool RegisterModdedGDO(string modName, GameDataObject gdo)
        {
            if (_moddedGDO.ContainsKey(gdo.ID))
            {
                Main.LogWarning($"Modded GDO with ID {gdo.ID} already registered.");
                return false;
            }
            _moddedGDO.Add(gdo.ID, modName);
            return true;
        }

        public static bool TryGetModdedMaterial(string materialName, out Material material, bool codelessOnly = false)
        {
            if (_moddedMaterials.TryGetValue(materialName, out material))
            {
                return true;
            }
            if (!codelessOnly && CustomMaterials.CustomMaterialsIndex.TryGetValue(materialName, out material))
            {
                return true;
            }
            Main.LogWarning($"Failed to find Material with Name {materialName}.");
            material = null;
            return false;
        }

        public static bool IsModdedMaterial(string materialName, bool codelessOnly = false)
        {
            return TryGetModdedMaterial(materialName, out Material _, codelessOnly);
        }

        public static bool RegisterModdedMaterial(Material material)
        {
            if (_moddedMaterials.ContainsKey(material.name))
            {
                Main.LogWarning($"Modded Material with Name {material.name} already registered.");
                return false;
            }
            _moddedMaterials.Add(material.name, material);
            return true;
        }

        public static bool TryGetModdedUnlockEffectSet(string unlockEffectSetName, out IEnumerable<UnlockEffect> unlockEffects)
        {
            if (_unlockEffectSets.TryGetValue(unlockEffectSetName, out UnlockEffectSet unlockEffectSet))
            {
                unlockEffects = unlockEffectSet.UnlockEffects;
                return true;
            }
            Main.LogWarning($"Failed to find UnlockEffectSet with Name {unlockEffectSetName}.");
            unlockEffects = null;
            return false;
        }

        public static bool RegisterModdedUnlockEffectSet(string unlockEffectSetName, IEnumerable<UnlockEffect> unlockEffects)
        {
            if (_unlockEffectSets.ContainsKey(unlockEffectSetName))
            {
                Main.LogWarning($"Modded UnlockEffectSet with Name {unlockEffectSetName} already registered.");
                return false;
            }
            _unlockEffectSets.Add(unlockEffectSetName, new UnlockEffectSet(unlockEffects));
            return true;
        }

        public static bool TryGetModdedLocalisation(string localisationName, out Localisation localisation)
        {
            if (_localisations.TryGetValue(localisationName, out localisation))
            {
                return true;
            }
            Main.LogWarning($"Failed to find Localisation with Name {localisationName}.");
            localisation = null;
            return false;
        }

        public static bool RegisterModdedLocalisation(string localisationName, Localisation localisation)
        {
            if (_localisations.ContainsKey(localisationName))
            {
                Main.LogWarning($"Modded Localisation with Name {localisationName} already registered.");
                return false;
            }
            _localisations.Add(localisationName, localisation);
            return true;
        }

        public static bool TryGetModdedEffectType(string effectTypeName, out IEffectType effectType)
        {
            if (_effectTypes.TryGetValue(effectTypeName, out effectType))
            {
                return true;
            }
            Main.LogWarning($"Failed to find EffectType with Name {effectTypeName}.");
            effectType = null;
            return false;
        }

        public static bool RegisterModdedEffectType(string effectTypeName, IEffectType effectType)
        {
            if (_effectTypes.ContainsKey(effectTypeName))
            {
                Main.LogWarning($"Modded EffectType with Name {effectTypeName} already registered.");
                return false;
            }
            _effectTypes.Add(effectTypeName, effectType);
            return true;
        }
    }
}
