using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.SkillAttacks
{
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
}
