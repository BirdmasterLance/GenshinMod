using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Kaeya
{
	internal class KaeyaSkillWeapon : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.Frostbrand;

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<KaeyaSkillProjectile>();
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
			position.X += player.direction * 50;
		}
	}

		internal class KaeyaSkillProjectile : ModProjectile
    {
		public override string Texture => "GenshinMod/Items/Invisible";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frostgnaw");
		}

		public override void SetDefaults()
		{
			Projectile.width = 70; // Hitbox width of projectile
			Projectile.height = 50; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.hostile = false;
			Projectile.timeLeft = 5; // Time it takes for projectile to expire
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Magic; // Projectile is a melee projectile
		}

		public override void AI()
		{
			for (int i = 0; i < 5; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.IceTorch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].noLight = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Projectile.direction * 15f;
			}
		}
	}
}
