using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Kokomi
{
	internal class KokomiTestWeapon : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.RazorbladeTyphoon;

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<KokomiNormalAttack>();
			Item.shootSpeed = 15f;
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
	}

	internal class KokomiNormalAttack : ModProjectile
    {

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;

			Projectile.ai[1] = -1;
		}

        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation();
        }
	}
}
