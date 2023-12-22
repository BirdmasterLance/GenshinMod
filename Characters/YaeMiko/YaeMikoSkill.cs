using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.YaeMiko
{
    internal class YaeMikoSkill : ModProjectile
    {
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			
		}

		public override void SetDefaults()
		{
			Projectile.width = 150;
			Projectile.height = 50;

			Projectile.damage = 0;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 55;
			Projectile.penetrate = -1;

			Projectile.ai[1] = -1;
		}
	}
}
