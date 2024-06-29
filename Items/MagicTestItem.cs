using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items
{
    internal class MagicTestItem : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.CursedFlames;
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Test weapon for magic attacks. skills, etc");
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
			//Item.shoot = ModContent.ProjectileType<Characters.YaeMiko.YaeMikoCharged>(); 
			//Item.shootSpeed = 14f; 
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

        public override bool? UseItem(Player player)
		{
			Projectile.NewProjectile(player.GetSource_FromThis(), player.position, Vector2.Zero, ModContent.ProjectileType<Characters.YaeMiko.YaeMikoSkill>(), 50, 0, Main.myPlayer, ai0:player.direction);
			return true;
		}
    }

	// The Charged Blaster Cannon works since the cannon itself is a projectile that contains AI for holding down the mouse button
	// So this dummy projectile is needed to replicate that effect
	internal class DummyBook : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dummy Book");
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
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<Characters.Yanfei.YanfeiProjectile>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
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
						if (player.ownedProjectileCounts[ModContent.ProjectileType<Characters.Yanfei.YanfeiCharged>()] < 1 && !hasUsedChargeAttack)
						{
							if(Collision.CanHitLine(player.position, 0, 0, Main.MouseWorld, 0, 0))
							{
								hasUsedChargeAttack = true;
								Projectile.NewProjectile(Projectile.InheritSource(Projectile), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Characters.Yanfei.YanfeiCharged>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
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
}
