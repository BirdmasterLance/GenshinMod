using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.RaidenShogun
{
    internal class RaidenShogunSkillWeapon : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.ThunderSpear;

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<RaidenShogunSkillProjectile>();
			Item.shootSpeed = 0f;
			Item.width = 8;
			Item.height = 28;
			Item.consumable = false;
			Item.UseSound = SoundID.Item1;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.value = Item.buyPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Blue;
			Item.SetWeaponValues(50, 10f, 32);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			position.X += player.direction + 48;
        }

        public override bool? UseItem(Player player)
        {
			player.AddBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>(), 119);
			player.AddBuff(ModContent.BuffType<RaidenShogunSkillBuff>(), 1500);
			Rectangle projRect = new Rectangle((int)player.position.X, (int)player.position.Y, 500, 500);
			for(int i = 0; i < 200; i++)
            {
		
				if(projRect.Intersects(Main.npc[i].getRect()) &&  Main.npc[i].friendly)
                {
					Main.npc[i].AddBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>(), 119);
					Main.npc[i].AddBuff(ModContent.BuffType<RaidenShogunSkillBuff>(), 1500);
				}
            }
			return true;
        }
    }

    internal class RaidenShogunSkillProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Transcendence: Baleful Omen");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16; // Hitbox width of projectile
			Projectile.height = 16; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.hostile = false;
			Projectile.timeLeft = 65; // Time it takes for projectile to expire
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Magic; // Projectile is a melee projectile
		}

		public override void AI()
		{
			if(Projectile.timeLeft > 5)
            {
				// TODO: ensure animation goes here
				Projectile.damage = 0;
				for (int i = 0; i < 5; i++)
				{
					int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
					Main.dust[flameDust].noGravity = true;
					Main.dust[flameDust].noLight = true;
					Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
					Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
					Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 5f;
				}
			}
			else // At the last 1 second, actually do the AOE damage
            {
				Projectile.tileCollide = false;
				Projectile.Resize(150, 150);
				Projectile.damage = 50;
				Projectile.knockBack = 10f;

				for (int i = 0; i < 75; i++)
				{
					int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
					Main.dust[flameDust].noGravity = true;
					Main.dust[flameDust].noLight = true;
					Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
					Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
					Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 40f;
				}
			}

        }
    }

	internal class RaidenShogunSkill2Projectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Stormy Judgment");
		}

		public override void SetDefaults()
		{
			Projectile.width = 75; // Hitbox width of projectile
			Projectile.height = 75; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.hostile = false;
			Projectile.timeLeft = 30; // Time it takes for projectile to expire
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Magic; // Projectile is a melee projectile
		}

        public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 40; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].noLight = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 40f;
			}
		}
	}

	internal class RaidenShogunSkillBuff : ModBuff
	{ 
		public override string Texture => "Terraria/Images/Buff_" + BuffID.WindPushed;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Stormy Judgment");
			Description.SetDefault("Your attacks unleash additional Electro damage");
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
			int flameDust = Dust.NewDust(npc.position, npc.width, npc.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
			Main.dust[flameDust].noGravity = true;
			Main.dust[flameDust].noLight = true;
			Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
			Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
			Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 5f;
		}
    }

	internal class RaidenShogunSkillAttackCooldownBuff : ModBuff
	{
		public override string Texture => "Terraria/Images/Buff_" + BuffID.WindPushed;

		public override void SetStaticDefaults()
		{
			
		}
	}

	internal class RaidenShogunSkill : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
			if(projectile.ai[1] != -1)
            {
				NPC npc = Main.npc[(int) projectile.ai[1]];
				if (npc.HasBuff(ModContent.BuffType<RaidenShogunSkillBuff>()) && !npc.HasBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>()))
                {
					if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
						Projectile.NewProjectile(npc.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<RaidenShogunSkill2Projectile>(), 20, 5, Main.myPlayer, ai1:npc.whoAmI);
					}
					npc.AddBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>(), 54);
                }
            }
			if(projectile.owner == Main.myPlayer)
            {
				Player player = Main.player[Main.myPlayer];
				if (player.HasBuff(ModContent.BuffType<RaidenShogunSkillBuff>()) && !player.HasBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>()))
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<RaidenShogunSkill2Projectile>(), 20, 5, Main.myPlayer, ai1:Main.myPlayer);
					}
					player.AddBuff(ModContent.BuffType<RaidenShogunSkillAttackCooldownBuff>(), 54);
				}
			}
		}
    }
}
