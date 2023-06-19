using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{

	internal class AnemoBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.WindPushed;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anemo");
			Description.SetDefault("You are affected by Anemo");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Color SwirlColor = new Color(90, 240, 190);
			if (player.HasBuff(ModContent.BuffType<ElectroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
				CombatText.NewText(player.getRect(), SwirlColor, "Swirl");
			}
			else if (player.HasBuff(BuffID.Wet))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(player.getRect(), SwirlColor, "Swirl");
			}
			else if (player.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(player.getRect(), SwirlColor, "Swirl");
			}
			else if (player.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(player.getRect(), SwirlColor, "Swirl");
			}
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			Color SwirlColor = new Color(90, 240, 190);
			if (npc.HasBuff(ModContent.BuffType<ElectroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
				CombatText.NewText(npc.getRect(), SwirlColor, "Swirl");
				Vector2 projSpawnPos = new Vector2(npc.position.X, npc.position.Y);
				if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.GetSource_FromAI(), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<ElectroFriendlySwirl>(), 25, 20, Main.myPlayer);
			}
			else if (npc.HasBuff(BuffID.Wet))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(npc.getRect(), SwirlColor, "Swirl");
				Vector2 projSpawnPos = new Vector2(npc.position.X, npc.position.Y);
				if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.GetSource_FromAI(), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<HydroFriendlySwirl>(), 25, 20, Main.myPlayer);
			}
			else if (npc.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(npc.getRect(), SwirlColor, "Swirl");
				Vector2 projSpawnPos = new Vector2(npc.position.X, npc.position.Y);
				if(Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.GetSource_FromAI(), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<PyroFriendlySwirl>(), 25, 20, Main.myPlayer);
			}
			else if (npc.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(npc.getRect(), SwirlColor, "Swirl");
				Vector2 projSpawnPos = new Vector2(npc.position.X, npc.position.Y);
				if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.GetSource_FromAI(), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<CryoFriendlySwirl>(), 25, 20, Main.myPlayer);
			}
		}
	}

	internal class GeoBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Stoned;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Geo");
			Description.SetDefault("You are affected by Geo");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Color CrystalizeColor = new Color(200, 140, 50);

			if (player.HasBuff(ModContent.BuffType<ElectroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
				CombatText.NewText(player.getRect(), CrystalizeColor, "Crystalize");
			}
			else if (player.HasBuff(BuffID.Wet))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(player.getRect(), CrystalizeColor, "Crystalize");
			}
			else if (player.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(player.getRect(), CrystalizeColor, "Crystalize");
			}
			else if (player.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(player.getRect(), CrystalizeColor, "Crystalize");
			}
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			Color CrystalizeColor = new Color(200, 140, 50);

			if (npc.HasBuff(ModContent.BuffType<ElectroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
				CombatText.NewText(npc.getRect(), CrystalizeColor, "Crystalize");
			}
			else if (npc.HasBuff(BuffID.Wet))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(npc.getRect(), CrystalizeColor, "Crystalize");
			}
			else if (npc.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(npc.getRect(), CrystalizeColor, "Crystalize");
				Item.NewItem(npc.GetSource_FromThis(), npc.getRect(), ModContent.ItemType<CrystalizePyroItem>());
			}
			else if (npc.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(npc.getRect(), CrystalizeColor, "Crystalize");
			}
		}
	}

	internal class ElectroBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Electrified;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Electro");
			Description.SetDefault("You are affected by Electro");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Color ElectroColor = new Color(229, 153, 255);
			Color OverloadColor = new Color(250, 125, 170);

			if (player.HasBuff(ModContent.BuffType<DendroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<DendroBuff>()));
				CombatText.NewText(player.getRect(), ElectroColor, "Quicken");
			}
			else if (player.HasBuff(BuffID.Wet))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(player.getRect(), ElectroColor, "Electro-Charged");
			}
			else if (player.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(player.getRect(), OverloadColor, "Overload");

			}
			else if (player.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(player.getRect(), ElectroColor, "Super-Conduct");
			}

			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, new Color(175, 142, 193), 1.5f);
			}
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
			Color ElectroColor = new Color(229, 153, 255);
			Color OverloadColor = new Color(250, 125, 170);

			if (npc.HasBuff(ModContent.BuffType<DendroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<DendroBuff>()));
				CombatText.NewText(npc.getRect(), ElectroColor, "Quicken");
			}
			else if (npc.HasBuff(BuffID.Wet))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(BuffID.Wet));
				CombatText.NewText(npc.getRect(), ElectroColor, "Electro-Charged");
			}
			else if (npc.HasBuff(ModContent.BuffType<PyroBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<PyroBuff>()));
				CombatText.NewText(npc.getRect(), OverloadColor, "Overload");
				Vector2 projSpawnPos = new Vector2(npc.position.X, npc.position.Y);
				if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(npc.GetSource_FromAI(), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<OverloadFriendlyProjectile>(), 50, 50, Main.myPlayer);
			}
			else if (npc.HasBuff(ModContent.BuffType<CryoBuff>()))
			{
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<CryoBuff>()));
				CombatText.NewText(npc.getRect(), ElectroColor, "Superconduct");
			}

			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.MagicMirror, 0f, 0f, 150, new Color(175, 142, 193), 1.5f);
			}
		}
    }

	internal class DendroBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.DryadsWard;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dendro");
			Description.SetDefault("You are affected by Dendro");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{


			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, new Color(165, 200, 59), 1.5f);
			}
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.MagicMirror, 0f, 0f, 150, new Color(165, 200, 59), 1.5f);
			}
		}
	}

	internal class PyroBuff : ModBuff
    {
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Inferno;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyro");
			Description.SetDefault("You are affected by Pyro");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Color PyroColor = new Color(229, 137, 20);
			Color OverloadColor = new Color(250, 125, 170);
			if (player.HasBuff(ModContent.BuffType<DendroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<DendroBuff>()));
				CombatText.NewText(player.getRect(), PyroColor, "Burning");
			}
			else if (player.HasBuff(ModContent.BuffType<ElectroBuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.DelBuff(player.FindBuffIndex(ModContent.BuffType<ElectroBuff>()));
				CombatText.NewText(player.getRect(), OverloadColor, "Overload");
				
			}

			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 150, default(Color), 1.5f);
			}
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, 0f, 0f, 150, default(Color), 1.5f);
			}
		}
	}

	internal class CryoBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.Frozen;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cryo");
			Description.SetDefault("You are affected by Cryo");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			// Because of how often the player might be affected by Cryo,
			// we are not implementing the speed debuff as it would increase
			// the difficulty of ice related enemies and areas by a lot
			//player.moveSpeed -= player.moveSpeed * 0.15f;
			//player.wingAccRunSpeed -= player.wingAccRunSpeed * 0.15f;
			//player.accRunSpeed -= player.accRunSpeed * 0.15f;

			player.GetAttackSpeed(DamageClass.Generic) -= player.GetAttackSpeed(DamageClass.Generic) * 0.15f;

			if (player.HasBuff(BuffID.Wet))
			{
				player.DelBuff(player.FindBuffIndex(BuffID.Wet));
				player.DelBuff(buffIndex);
				buffIndex -= 1;
				player.AddBuff(BuffID.Frozen, 60);
				CombatText.NewText(player.getRect(), new Color(159, 214, 227), "Frozen");
			}

			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, new Color(165, 200, 59), 1.5f);
			}
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.stepSpeed -= npc.stepSpeed * 0.15f;

			if(npc.HasBuff(BuffID.Wet))
            {
				npc.DelBuff(npc.FindBuffIndex(BuffID.Wet));
				npc.DelBuff(buffIndex);
				buffIndex -= 1;
				npc.AddBuff(BuffID.Frozen, 180);
				CombatText.NewText(npc.getRect(), new Color(159, 214, 227), "Frozen");
            }

			for (int d = 0; d < 1; d++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.MagicMirror, 0f, 0f, 150, new Color(165, 200, 59), 1.5f);
			}
		}
	}
}
