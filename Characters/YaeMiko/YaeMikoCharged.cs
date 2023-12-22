using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.YaeMiko
{
	// We're going to be using this projectile as the one that spawns the ones that come after it
	// Not doing this in an Item because NPCs need to be able to use this attack too
	// So the one projectile will handle everything
	internal class YaeMikoCharged : ModProjectile
	{
		// How many extra lightning bolts to spawn after this one
		private int Offset = 0;

		public override string Texture => "GenshinMod/Items/Invisible";
		public override void SetDefaults()
		{
			Projectile.width = 0;
			Projectile.height = 0;
			Projectile.aiStyle = -1;
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.light = 0.5f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
		}

		public override void AI()
		{
			// TODO: Ensure line of sight checks for each bolt
			if(Projectile.timeLeft % 10 == 0)
            {
				Offset += 32;
				Vector2 spawnPosition = Projectile.Center + new Vector2(Offset * Projectile.ai[0], -320);
				if(Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPosition, Vector2.Zero, ModContent.ProjectileType<YaeMikoChargedBolt>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
			}
		}
	}

    internal class YaeMikoChargedBolt : ModProjectile
    {
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.ThunderSpearShot;
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 60;
			Projectile.aiStyle = -1;
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.light = 0.5f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame
		}

        public override void AI()
        {
			Projectile.velocity.Y = 32f;
        }
    }
}
