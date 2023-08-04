using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{

	#region Swirl AOE Projectiles

	internal class ElectroFriendlySwirl : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Electro Swirl");
		}

		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class HydroFriendlySwirl : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydro Swirl");
		}

		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class PyroFriendlySwirl : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pyro Swirl");
		}

		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class CryoFriendlySwirl : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryo Swirl");
		}

		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	#endregion

	internal class OverloadFriendlyProjectile : ModProjectile
    {
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Overload");
		}

		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class OverloadHostileProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Overload");
		}

		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class SuperconductFriendlyProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Superconduct");
		}

		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(ModContent.BuffType<SuperconductBuff>(), 720);
        }

        public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}

	internal class SuperconductHostileProjectile : ModProjectile
	{
		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Superconduct");
		}

		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;

			Projectile.aiStyle = -1;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			target.AddBuff(ModContent.BuffType<SuperconductBuff>(), 720);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 100; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}
			base.Kill(timeLeft);
		}
	}
}
