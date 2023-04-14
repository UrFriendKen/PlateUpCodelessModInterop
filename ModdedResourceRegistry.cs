using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace CodelessModInterop
{
    public static class ModdedResourceRegistry
    {
        private static Dictionary<int, string> _moddedGDO = new Dictionary<int, string>();  // Value is mod name the GDO belongs to
        private static Dictionary<string, Material> _moddedMaterials = new Dictionary<string, Material>();

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
                Main.LogWarning($"Modded material with Name {material.name} already registered.");
                return false;
            }
            _moddedMaterials.Add(material.name, material);
            return true;
        }
    }
}
