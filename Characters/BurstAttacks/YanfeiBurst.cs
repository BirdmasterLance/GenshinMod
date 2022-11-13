using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.BurstAttacks
{
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
				int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 150, default(Color), 4f);
				Main.dust[dustnumber].noGravity = true;
				Main.dust[dustnumber].scale = Main.rand.NextFloat() * 3f;
				Main.dust[dustnumber].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[dustnumber].velocity *= Main.rand.NextFloat() * 9f;
				Main.dust[dustnumber].noLight = true;

			}
			base.Kill(timeLeft);
		}
	}
}
