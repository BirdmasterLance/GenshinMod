using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Klee
{
	internal class KleeWeapon : ModItem 
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.LavaBomb;

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<KleeNormalAttack>();
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			//Vector2 targetPos = Main.MouseWorld;
			//Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
			//float heightDifference = targetPos.Y - playerCenter.Y;
			//// 0.0125 comes from the physics equation for projectile motion
			//// where its 1/2 * gravity * time^2
			//// which in terraria standards, assuming we want it to fly for 0.5 seconds is
			//// 1/2 * 0.1 * 0.5^2
			//// this calculation for initiali velocity also comes from the physics equation
			//// where we are solving for initial velocity
			//float initialVelocity = 2 * (heightDifference + 0.0125f);
			//Vector2 newVel = new Vector2((float)(initialVelocity * Math.Cos(45)), (float)(initialVelocity * Math.Sin(45));
			//Projectile proj = Main.projectile[Item.shoot];
			//Projectile.NewProjectile(source, player.Center, new Vector2((float)(initialVelocity * Math.Cos(45)), (float)(initialVelocity * Math.Sin(45))), proj.type, proj.damage, proj.knockBack, Main.myPlayer);
			//return false;
			return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			
			//velocity = Vector2.Zero;

            // Use this code for spawning charged attack
            //position.Y = player.RotatedRelativePoint(player.MountedCenter).Y - 64; // Spawns above player
            //Vector2 targetPos = Main.MouseWorld;
            //Vector2 direction = targetPos - position;
            //direction.Normalize();
            //direction *= Item.shootSpeed;
            //velocity = velocity + direction;
			// Use this code for spawning charged attack



			// Leftover code for normal attack, not recommended to use
            // Vector2 targetPos = Main.MouseWorld;
            // Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
            // float vertDistance = (targetPos.Y - playerCenter.Y) / 16; // Converted in tiles
            // float horizDistance = (targetPos.X - playerCenter.X) / 16; // Converted in tiles

            // float timeToTarget = 1.5f;

            // float horizontalSpeed = horizDistance / timeToTarget;
            // float gravity = -1f;
            // //float verticalSpeed = (vertDistance + ((0.5f * gravity) * (timeToTarget * timeToTarget))) / timeToTarget;
            // float verticalSpeed = vertDistance / timeToTarget + timeToTarget * gravity / 2;

            // velocity = new Vector2(horizontalSpeed, verticalSpeed);
			// Leftover code for normal attack, not recommended to use
	
			// Ignore below
            //float initialVelocity = xDistance / 0.5f;
            //float initialVelocityY = heightDifference / 0.5f;
            //double initialAngle = 0.5 * Math.Asin(0.1 * xDistance / (initialVelocity*initialVelocity)) * (180.0 / Math.PI);
            //Vector2 newVel = new Vector2(initialVelocity, initialVelocityY);
            //Main.NewText("Velocity");
            //Main.NewText(newVel.X);
            //Main.NewText(newVel.Y);

            //float R = Math.Abs((targetPos.X - playerCenter.X) / 16);
            //float G = 1f;
            //float tanAlpha = (float) Math.Tan(20 * (180.0 / Math.PI));
            //float H = -(targetPos.Y - playerCenter.Y)/16; // Converted in tiles
            //Main.NewText(R + " || " + G + " || " + tanAlpha + " || " + H);

            //// calculate the local space components of the velocity 
            //// required to land the projectile on the target object 
            //float Vx = (float) Math.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
            //float Vy = tanAlpha * Vx;

            //// create the velocity vector in local space and get it in global space
            //Vector2 localVelocity = new Vector2(Vx * player.direction, Vy);
            //Main.NewText("velocity");
            //Main.NewText(localVelocity.X);
            //Main.NewText(localVelocity.Y);
            //velocity = localVelocity;
        }
	}

	internal class KleeNormalAttack : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.LavaBomb;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kaboom!");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16; // Hitbox width of projectile
			Projectile.height = 16; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.hostile = false;
			Projectile.timeLeft = 10000; // Time it takes for projectile to expire
			Projectile.penetrate = 1;
			Projectile.tileCollide = true; 
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Magic; // Projectile is a melee projectile
		}

        public override void AI()
        {
			int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 150, default(Color), 2.5f);
			Main.dust[dustnumber].noGravity = true;

			Projectile.velocity.Y = Projectile.velocity.Y + 0.1f; 
			if (Projectile.velocity.Y > 16f) 
			{
				Projectile.velocity.Y = 16f;
			}
		}

        public override void Kill(int timeLeft)
        {
			if(Projectile.owner == Main.myPlayer)
            {
				Projectile.alpha = 255;
				Projectile.tileCollide = false;
				Projectile.Resize(50, 50);
				Projectile.damage = 50;
				Projectile.knockBack = 10f;

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


                    if (Projectile.Colliding(projRect, Main.npc[i].getRect()))
					{
						NPC.HitInfo hitInfo = new NPC.HitInfo();
						hitInfo.Damage = Projectile.damage;
						hitInfo.Knockback = Projectile.knockBack;
						hitInfo.HitDirection = Projectile.direction;
						hitInfo.Crit = Main.rand.Next(1, 101) <= Main.player[Projectile.owner].GetCritChance(DamageClass.Magic);
						Main.npc[i].StrikeNPC(hitInfo);
					}
				}
			}

			for (int i = 0; i < 25; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}

		}
    }

	internal class KleeChargedAttack : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.LavaBomb;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charged Kaboom!");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16; // Hitbox width of projectile
			Projectile.height = 16; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.hostile = false;
			Projectile.timeLeft = 10000; // Time it takes for projectile to expire
			Projectile.penetrate = 1;
			Projectile.tileCollide = true;
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Magic; // Projectile is a melee projectile
		}

		public override void AI()
		{

		}

		public override void Kill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.alpha = 255;
				Projectile.tileCollide = false;
				Projectile.Resize(100, 100);
				Projectile.damage = 50;
				Projectile.knockBack = 10f;

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

					
					if (Projectile.Colliding(projRect, Main.npc[i].getRect()))
					{
						NPC.HitInfo hitInfo = new NPC.HitInfo();
						hitInfo.Damage = Projectile.damage;
						hitInfo.Knockback = Projectile.knockBack;
						hitInfo.HitDirection = Projectile.direction;
						hitInfo.Crit = Main.rand.Next(1, 101) <= Main.player[Projectile.owner].GetCritChance(DamageClass.Magic);
						Main.npc[i].StrikeNPC(hitInfo);
					}
				}
			}

			for (int i = 0; i < 25; i++)
			{
				int flameDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, DustID.Torch, 0f, 0f, 150, default(Color), 8f);
				Main.dust[flameDust].noGravity = true;
				Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
				Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
				Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 20f;
			}

		}
	}
}
