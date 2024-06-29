using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items.Artifacts
{
    internal class GildedDreamsFlower5Star : Artifact
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.NettleBurst;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
