using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items.Artifacts
{
    internal class MarechausseeHunterFlower4Star : Artifact
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.NettleBurst;

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Purple;
        }
    }
}
