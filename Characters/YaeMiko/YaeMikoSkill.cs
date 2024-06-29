using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.YaeMiko
{
    internal class YaeMikoSkill : ModProjectile
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.FoxMask;
		public override void SetStaticDefaults()
		{
			
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;

			Projectile.damage = 0;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 840;
			Projectile.penetrate = -1;

			Projectile.ai[1] = -1;
		}

        public override void AI()
        {
			int startAttackRange = 700;
			int attackTarget = -1;
			Projectile.Minion_FindTargetInRange(startAttackRange, ref attackTarget, false);
			
			if(attackTarget != -1)
            {
				NPC target = Main.npc[attackTarget];
				if(Projectile.ai[0] % 130 == 0)
                {
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position + new Vector2(0, -320), Vector2.Zero, ModContent.ProjectileType<YaeMikoChargedBolt>(), 50, 0, Main.myPlayer);
				}
				Projectile.ai[0]++;

			}
			else
            {
				Projectile.ai[0] = 0;
			}
		}
    }
}
