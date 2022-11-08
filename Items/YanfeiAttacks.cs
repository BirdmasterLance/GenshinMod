using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
    internal class YanfeiAttacks : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.CursedFlames;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Test weapon for attacks. skills, etc");
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Magic; // Makes the damage register as magic. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type.
			Item.width = 34;
			Item.height = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item71;			
			Item.shoot = ModContent.ProjectileType<DummyBook>(); 
			Item.shootSpeed = 14f; 
			Item.crit = 4;
			Item.autoReuse = false;
			Item.channel = true;
			//Item.noUseGraphic = true;
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            //velocity = Vector2.Zero;
            //position = Main.MouseWorld;
        }
    }

	// The Charged Blaster Cannon works since the cannon itself is a projectile that contains AI for holding down the mouse button
	// So this dummy projectile is needed to replicate that effect
	internal class DummyBook : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dummy Book");
		}

		public override void SetDefaults()
		{
			Projectile.width = 0;
			Projectile.height = 0;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		private bool hasUsedRegularAttack = false;
		private bool hasUsedChargeAttack = false;

		public override void AI()
		{
			if (Main.myPlayer == Projectile.owner)
			{
				Player player = Main.player[Projectile.owner];

				if (!hasUsedRegularAttack)
				{
					hasUsedRegularAttack = true;
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<YanfeiProjectile>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
				}

				Vector2 rotatePoint = player.RotatedRelativePoint(player.MountedCenter);
				float num8 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
				Vector2 value = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - rotatePoint;
				if (player.gravDir == -1f)
				{
					value.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - rotatePoint.Y;
				}
				Projectile.velocity = Vector2.Normalize(value) * 0.001f;

                Projectile.rotation = Projectile.velocity.ToRotation();
                if (Projectile.direction == -1)
                {
                    Projectile.rotation += (float)Math.PI;
                }

                if (player.channel)
                {
					Projectile.ai[0] += 1f;
					if (Projectile.ai[0] >= 30f)
					{
						if (player.ownedProjectileCounts[ModContent.ProjectileType<YanfeiCharged>()] < 1 && !hasUsedChargeAttack)
						{
							if(Collision.CanHitLine(player.position, 0, 0, Main.MouseWorld, 0, 0))
							{
								hasUsedChargeAttack = true;
								Projectile.NewProjectile(Projectile.InheritSource(Projectile), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<YanfeiCharged>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
							}
						}
					}
				}
				else
                {
					Projectile.ai[0] = 0f;
					hasUsedChargeAttack = false;
					hasUsedRegularAttack = false;
					Projectile.Kill();
                }							
			}
        }
    }

    internal class YanfeiBurst : ModProjectile
    {
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Done Deal");
		}

		public override void SetDefaults()
		{
			Projectile.width = 500;
			Projectile.height = 500;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 150, default(Color), 4f);
				Main.dust[dustnumber].noGravity = true;
				Main.dust[dustnumber].scale = Main.rand.NextFloat() * 3f;
				Main.dust[dustnumber].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[dustnumber].velocity *= Main.rand.NextFloat() * 9f;
				Main.dust[dustnumber].noLight = true;

			}
			base.Kill(timeLeft);
		}
	}

    internal class YanfeiSkill : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Signed Edict");
		}

		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 50; i++)
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Torch, 0f, 0f, 150, default(Color), 4f);
				Main.dust[dustnumber].noGravity = true;
				Main.dust[dustnumber].scale = Main.rand.NextFloat() * 3f;
				Main.dust[dustnumber].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[dustnumber].velocity.X *= Main.rand.NextFloat() * 4f;
				Main.dust[dustnumber].velocity.Y *= Main.rand.NextFloat() * 9f;

				int smokeDust = Dust.NewDust(Projectile.position, 1, 10, DustID.Cloud, 0, 0, 0, new Color(255, 80, 35), 3f);
				Main.dust[smokeDust].noGravity = true;
				// Main.dust[smokeDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[smokeDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[smokeDust].velocity.X *= Main.rand.NextFloat() * 10f;
				Main.dust[smokeDust].velocity.Y *= Main.rand.NextFloat() * 3f;
			}
			base.Kill(timeLeft);
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.myPlayer == Projectile.owner)
			{
				Player player = Main.player[Projectile.owner];
				player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>(), 600);
			}
		}
    }

    internal class YanfeiCharged : ModProjectile
    {
		private NPC alreadyHit;

		public override string Texture => "GenshinMod/Items/YanfeiChargedAttack";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seal of Approval");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.scale = 2;
		}

		public override void AI()
		{
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 150, default(Color), 3f);
			Main.dust[dustnumber].noGravity = true;

			Projectile.velocity.Y = Projectile.velocity.Y + 16f; // 0.1f for arrow gravity, 0.4f for knife gravity
			if (Projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				Projectile.velocity.Y = 16f;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			alreadyHit = target;
			if (Main.myPlayer == Projectile.owner)
			{
				Player player = Main.player[Projectile.owner];
				if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>()))
				{
					damage = (int)(damage * 1.18);
					knockback = (float)(knockback * 1.18);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>()))
				{
					damage = (int)(damage * 1.36);
					knockback = (float)(knockback * 1.36);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>()))
				{
					damage = (int)(damage * 1.54);
					knockback = (float)(knockback * 1.54);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>()))
				{
					damage = (int)(damage * 1.72);
					knockback = (float)(knockback * 1.72);
				}

				if (player.HasBuff(ModContent.BuffType<Buffs.BrillianceBuff>()))
				{
					damage = (int)(damage * 1.5);
					knockback = (float)(knockback * 1.25);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Damage();

			for (int i = 0; i < 25; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height/2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;

				
			}
			base.Kill(timeLeft);
		}

		private void Damage()
        {
			int damage = Projectile.damage;
			float knockback = Projectile.knockBack;

			if (Main.myPlayer == Projectile.owner)
			{
				Player player = Main.player[Projectile.owner];
				if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>()))
				{
					damage = (int)(damage * 1.18);
					knockback = (float)(knockback * 1.18);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>()))
				{
					damage = (int)(damage * 1.36);
					knockback = (float)(knockback * 1.36);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>()))
				{
					damage = (int)(damage * 1.54);
					knockback = (float)(knockback * 1.54);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>()))
				{
					damage = (int)(damage * 1.72);
					knockback = (float)(knockback * 1.72);
				}

				if (player.HasBuff(ModContent.BuffType<Buffs.BrillianceBuff>()))
				{
					damage = (int)(damage * 1.5);
					knockback = (float)(knockback * 1.25);
				}

				if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>()))
				{
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>());
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>()))
				{
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>());
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>()))
				{
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>());
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>()))
				{
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>());
				}
			}

			Projectile.width = 80;
			Projectile.height = 80;

			// From explosive bullet code
			Rectangle projRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
			int[] localImmunities = Projectile.localNPCImmunity;
			for (int i = 0; i < 200; i++)
			{
				bool flag = (!Projectile.usesLocalNPCImmunity && !Projectile.usesIDStaticNPCImmunity) || (Projectile.usesLocalNPCImmunity && localImmunities[i] == 0) || (Projectile.usesIDStaticNPCImmunity && Projectile.IsNPCIndexImmuneToProjectileType(Projectile.type, i));
				if (!(Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag) || (Main.npc[i].aiStyle == 112 && Main.npc[i].ai[2] > 1f) || Main.npc[i].friendly || Projectile.ownerHitCheck)
				{
					continue;
				}
				if(alreadyHit != null && Main.npc[i] == alreadyHit)
                {
					continue;
                }


				if (Projectile.Colliding(projRect, Main.npc[i].getRect()))
				{
					Main.npc[i].StrikeNPC(damage, knockback, Projectile.direction, Main.rand.Next(1, 101) <= Main.player[Projectile.owner].GetCritChance(DamageClass.Magic));
				}
			}
		}
    }

	internal class YanfeiProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seal of Approval");
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;
		}

		public override void AI()
		{
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, 6, 0f, 0f, 150, default(Color), 2.5f);
			Main.dust[dustnumber].noGravity = true;

			// Auto target code from Flamelash
			Vector2 vector = Vector2.Zero;
			float amount = 1;
			int target = Projectile.FindTargetWithLineOfSight();
			if (Main.npc.IndexInRange(target))
			{
				NPC nPC = Main.npc[target];
				if (nPC.CanBeChasedBy(this))
				{
					vector = nPC.Center;
					float t = Projectile.Distance(nPC.Center);
					float num7 = Utils.GetLerpValue(0f, 100f, t, clamped: true) * Utils.GetLerpValue(600f, 400f, t, clamped: true);
					amount = MathHelper.Lerp(0f, 0.2f, Utils.GetLerpValue(200f, 20f, 1f - num7, clamped: true));
				}
				else
				{
					Projectile.netUpdate = true;
				}
			}
			if (vector.X != 0 && vector.Y != 0)
			{
				Vector2 value = vector;
				if (Projectile.Distance(value) >= 64f)
				{
					Vector2 v = value - Projectile.Center;
					Vector2 vector2 = v.SafeNormalize(Vector2.Zero);
					float num8 = Math.Min(10, v.Length()); // Set the number in the left side based on the shootSpeed of whatever shot it
					Vector2 value2 = vector2 * num8;
					if (Projectile.velocity.Length() < 4f)
					{
						Projectile.velocity += Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(0.7853981852531433).SafeNormalize(Vector2.Zero) * 4f;
					}
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, value2, amount);
					Projectile.netUpdate = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, 6, 0f, 0f, 150, default(Color), 4f);
				Main.dust[dustnumber].noGravity = true;
				Main.dust[dustnumber].scale = Main.rand.NextFloat() * 3f;
				Main.dust[dustnumber].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[dustnumber].velocity *= Main.rand.NextFloat() * 9f;
			}
			base.Kill(timeLeft);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.myPlayer == Projectile.owner)
			{
				Player player = Main.player[Projectile.owner];
				if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>()))
				{
					player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>(), 600);
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>()))
				{
					player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff4>(), 600);
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>());
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>()))
				{
					player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff3>(), 600);
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>());
				}
				else if (player.HasBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>()))
				{
					player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff2>(), 600);
					player.ClearBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>());
				}
				else
                {
					player.AddBuff(ModContent.BuffType<Buffs.ScarletSealBuff1>(), 600);
				}
			}
		}
    }
}
