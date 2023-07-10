using System.Collections.Generic;
using Terraria.ModLoader;

namespace GenshinMod.Items.Weapons
{
    internal class GenshinItems : ILoadable
    {
        public static List<int> AllWeapons = new();
        public static List<int> AllArtifacts = new();

        public void Load(Mod mod)
        {
            AllWeapons.AddRange(new int[]
            {
                ModContent.ItemType<LostPrayerToSacredWinds>()
            });
        }

        public void Unload()
        {
        }
    }
}
