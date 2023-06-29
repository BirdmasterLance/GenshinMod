using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
	internal class CrystalizeElectroItem : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Amethyst;

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
		}

		public override bool OnPickup(Player player)
		{
			player.AddBuff(ModContent.BuffType<ElectroBuff>(), 600);
			return false;
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange *= 2;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
		}
	}

	internal class CrystalizeHydroItem : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Sapphire;

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
		}

		public override bool OnPickup(Player player)
		{
			player.AddBuff(BuffID.Wet, 600);
			return false;
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange *= 2;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
		}
	}

	internal class CrystalizePyroItem : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.Ruby;

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
		}

        public override bool OnPickup(Player player)
        {
			player.AddBuff(ModContent.BuffType<PyroBuff>(), 600);
			return false;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
			grabRange *= 2;
        }

        public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
		}
	}

	internal class CrystalizeCryoItem : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Diamond;

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
		}

		public override bool OnPickup(Player player)
		{
			player.AddBuff(ModContent.BuffType<CryoBuff>(), 600);
			return false;
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange *= 2;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
		}
	}
}
