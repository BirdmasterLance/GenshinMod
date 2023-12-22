using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Elements
{
	internal class EnergyParticle : ModProjectile
	{

		public override string Texture => "Terraria/Images/Item_" + ItemID.FragmentVortex;

		// Using this to store what element the particle is
		public ref float Element => ref Projectile.ai[1];

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Energy Particle");
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;

			Projectile.damage = 0;
			Projectile.knockBack = 0;

			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;

			Element = -1;
		}

		public override bool? CanHitNPC(NPC target)
		{
			return target.type == ModContent.NPCType<Characters.Yanfei.YanfeiNPC>() ||
				target.type == ModContent.NPCType<Characters.Barbara.BarbaraNPC>();
        }

        public override bool CanHitPlayer(Player target)
        {
			return false;
        }

		public override bool CanHitPvp(Player target)
		{
			return false;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override bool? CanDamage()
        {
			return false;
        }

		public override bool? CanCutTiles()
		{
			return false;
		}

        public override void OnKill(int timeLeft)
        {


			base.OnKill(timeLeft);
        }

        public override void AI()
		{

			float distanceFromTarget = 700f;
			Vector2 targetCenter = Projectile.position;
			bool foundTarget = false;
			NPC target = null;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];

				if (npc.type == ModContent.NPCType<Characters.Yanfei.YanfeiNPC>() ||
					npc.type == ModContent.NPCType<Characters.Barbara.BarbaraNPC>())
				{
					float between = Vector2.Distance(npc.Center, Projectile.Center);
					bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;
					bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
					// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
					// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
					bool closeThroughWall = between < 100f;

					if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
					{
						distanceFromTarget = between;
						targetCenter = npc.Center;
						target = npc;
						foundTarget = true;
					}
				}
			}

			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;

					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}

                // Distance checking needed because making CanDamage false means theres no collision between npc and projectile
                if (Projectile.Center.Distance(targetCenter) < 10f)
                {
					float energyGained = 0; // Save how much energy the active character got so we can give everyone else less of this

					// Find active characters and give them their energy
					List<Character> activeCharacters = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().GetActiveCharacters();
					for (int i = 0; i < activeCharacters.Count; i++)
					{
						if (activeCharacters[i].GetNPCID() == target.whoAmI)
						{
							// Formula for calculating how much energy to give 
							// It gives more energy if the particle's element is the same as the character's
							energyGained = 1 * (1 + activeCharacters[i].energyRecharge / 100);
							if ((int)activeCharacters[i].Element == Element) target.ai[2] += energyGained * 3;
							else target.ai[2] += energyGained;
						}
					}

					// Find non active characters and give them their reduced energy
					List<Character> partyCharacters = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().GetPartyCharacters();
					for (int i = 0; i < partyCharacters.Count; i++)
					{
						if (partyCharacters[i].GetNPCID() != target.whoAmI)
						{
							// Formula for calculating how much energy to give 
							// Reduction is based on a percent based on how many characters are in the party
							int reducedEnergyGained = (int)(energyGained * (1 - (0.1 * partyCharacters.Count)));
							partyCharacters[i].energy += reducedEnergyGained;
						}
					}

					Projectile.Kill();
                }
            }
			else
            {
				Projectile.velocity = Vector2.Zero;
			}
		}
	}

	internal class EnergyOrb : ModProjectile
	{

		public override string Texture => "Terraria/Images/Item_" + ItemID.FragmentStardust;

		// Using this to store what element the orb is
		public ref float Element => ref Projectile.ai[1];

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Energy Orb");
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;

			Projectile.damage = 0;
			Projectile.knockBack = 0;

			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.8f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;
		}

		public override bool? CanHitNPC(NPC target)
		{
			return target.type == ModContent.NPCType<Characters.Yanfei.YanfeiNPC>() ||
				target.type == ModContent.NPCType<Characters.Barbara.BarbaraNPC>();
		}

		public override bool CanHitPlayer(Player target)
		{
			return false;
		}

		public override bool CanHitPvp(Player target)
		{
			return false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override bool? CanDamage()
		{
			return false;
		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override void OnKill(int timeLeft)
		{


			base.OnKill(timeLeft);
		}

		public override void AI()
		{

			float distanceFromTarget = 700f;
			Vector2 targetCenter = Projectile.position;
			bool foundTarget = false;
			NPC target = null;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];

				if (npc.type == ModContent.NPCType<Characters.Yanfei.YanfeiNPC>() ||
					npc.type == ModContent.NPCType<Characters.Barbara.BarbaraNPC>())
				{
					float between = Vector2.Distance(npc.Center, Projectile.Center);
					bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;
					bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
					// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
					// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
					bool closeThroughWall = between < 100f;

					if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
					{
						distanceFromTarget = between;
						targetCenter = npc.Center;
						target = npc;
						foundTarget = true;
					}
				}
			}

			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;

					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}

				// Distance checking needed because making CanDamage false means theres no collision between npc and projectile
				if (Projectile.Center.Distance(targetCenter) < 10f)
				{
					int energyGained = 0; // Save how much energy the active character got so we can give everyone else less of this

					// Find active characters and give them their energy
					List<Character> activeCharacters = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().GetActiveCharacters();
					for (int i = 0; i < activeCharacters.Count; i++)
					{
						if (activeCharacters[i].GetNPCID() == target.whoAmI)
						{
							// Formula for calculating how much energy to give 
							// It gives more energy if the particle's element is the same as the character's
							energyGained = 3 * (1 + activeCharacters[i].energyRecharge / 100);
							if ((int)activeCharacters[i].Element == Element) target.ai[2] += energyGained * 3;
							else target.ai[2] += energyGained;
						}
					}

					// Find non active characters and give them their reduced energy
					List<Character> partyCharacters = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().GetPartyCharacters();
					for (int i = 0; i < partyCharacters.Count; i++)
					{
						if (partyCharacters[i].GetNPCID() != target.whoAmI)
						{
							// Formula for calculating how much energy to give 
							// Reduction is based on a percent based on how many characters are in the party
							int reducedEnergyGained = (int) (energyGained * (1-(0.1* partyCharacters.Count)));
							partyCharacters[i].energy += reducedEnergyGained;
						}
					}

					Projectile.Kill();
				}
			}
			else
			{
				Projectile.velocity = Vector2.Zero;
			}
        }
    }

    internal class EnergyGlobalProjectile : GlobalProjectile
    {
		int baseChance = 105;

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.ai[1] != -1)
            {
				NPC shooter = Main.npc[(int) projectile.ai[1]];
				if(shooter.type == ModContent.NPCType<Characters.Yanfei.YanfeiNPC>() ||
					shooter.type == ModContent.NPCType<Characters.Barbara.BarbaraNPC>())
				{
					// Use .Find() to find a character based on a function defined (if their ID matches the whoAmI of the shooter)
					Character character = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().activeCharacters.Find(chara => chara.GetNPCID() == shooter.whoAmI);
					//Main.NewText(baseChance);
					Random rand = new Random();
					if(baseChance > rand.Next(100))
                    {
						// Spawn a random amount of particles on hit
						for(int i = 0; i < rand.Next(4); i++)
                        {
							if(Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(target.GetSource_FromThis(), target.position, new Vector2(4 * rand.Next(-1, 1), 4 * rand.Next(-1, 1)), ModContent.ProjectileType<EnergyParticle>(), 0, 0, ai1: (int) character.Element);
						}
						baseChance = 5;
					}
					else // If it failed, increase the chance by a number
                    {
						baseChance += 5;
                    }
                }
            }
        }
    }

}
