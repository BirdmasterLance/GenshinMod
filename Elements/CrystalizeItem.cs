using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
    internal class CrystalizePyroItem : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.ToxicFlask;

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 1000; // Makes the item worth 1 gold.
			Item.rare = ItemRarityID.Orange;
		}

        public override bool OnPickup(Player player)
        {
			player.AddBuff(ModContent.BuffType<PyroBuff>(), 600);
			return false;
        }

        public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
		}
	}
}
