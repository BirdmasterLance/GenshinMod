using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using System;

namespace GenshinMod.NPCs
{
	class TestEnemy2 : ModNPC
	{
		public override string Texture => "Terraria/Images/NPC_" + NPCID.PossessedArmor;



		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Test Enemy 2");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
			//Main.npcFrameCount[NPC.type] = 9;
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.width = 18; //18 //30 doesnt work when facing left //31 doesnt work when facing right and left
			NPC.height = 25; //40 
			NPC.aiStyle = -1;
			NPC.damage = 24;
			NPC.defense = 8;
			NPC.lifeMax = 40;
			NPC.HitSound = SoundID.NPCHit7;
			NPC.knockBackResist = 0.6f;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 1000f;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[24] = true;
			NPC.noGravity = false;
			//animationType = NPCID.PossessedArmor;
			NPC.direction = -1;
			NPC.scale = 1f;
			//npc = ProjectileType<Projectiles.TestEnemyProjectile>();
			//npc.reflectingProjectiles = true;
		}

		/**public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.aiStyle == -1 ||
				projectile.aiStyle == 1 ||
				projectile.aiStyle == 2 ||
				projectile.aiStyle == 3)
			{
				damage = -1;
			}
		}**/

		/**public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.aiStyle == -1 ||
				projectile.aiStyle == 1 ||
				projectile.aiStyle == 2 ||
				projectile.aiStyle == 3)
			{
				return false;
			}
			return true;
		}**/

		/**public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			projectile.penetrate = 2;
			projectile.maxPenetrate = 2;
			projectile.hostile = true;
			projectile.friendly = false;
			Vector2 vector = Main.player[projectile.owner].Center - projectile.Center;
			vector.Normalize();
			vector *= projectile.oldVelocity.Length();
			projectile.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			projectile.velocity.Normalize();
			Projectile obj2 = projectile;
			obj2.velocity *= vector.Length();
			Projectile obj3 = projectile;
			obj3.velocity += vector * 20f;
			projectile.velocity.Normalize();
			Projectile obj4 = projectile;
			obj4.velocity *= vector.Length();
			projectile.damage /= 2;
			/**if (projectile.aiStyle == -1 ||
				projectile.aiStyle == 1 || 
				projectile.aiStyle == 2 ||
				projectile.aiStyle == 3 )
			{
				projectile.penetrate = 2;
				projectile.maxPenetrate = 2;
				projectile.hostile = true;
				projectile.friendly = false;
				projectile.velocity = -projectile.velocity;
			}

			//Projectile.NewProjectile(npc.Center, -projectile.velocity, projectile.type, damage, knockback, Main.myPlayer, 0, 0);
			//projectile.velocity = -projectile.velocity;
		}**/

		/*
		 * npc.ai[] might just refer to states the enemy may have, and you can add as any as you want, so 0 might not be idle, 
		 * but we might as well leave it as the idle state
		 */

		private float detectionRange = 300f; //60
		private float attackRange = 60f;
		private float speed = 1.5f; //1.5
		private float inertia = 0.07f; //0.07
		private int timer = 60;
		private bool targeting = true;
		/*
		 * phase = 0 - Default phase
		 * phase = 1 - Powered up phase
		 */
		private int phase = 0;

		public override void AI()
		{
			if (!Collision.CanHit(NPC.Center, NPC.width, NPC.height, Main.player[NPC.target].Center, Main.player[NPC.target].width, Main.player[NPC.target].height))
			{
				NPC.TargetClosest(false);
			}
			else
			{
				NPC.TargetClosest(true);
			}

			if (NPC.life < NPC.lifeMax / 2) //buff up stats when we're below half health
			{
				//NPC.noGravity = true;
				//detectionRange = 600;
				//attackRange = 300;
				//speed = 5;
				//timer = 20;
				//phase = 1;
			}

			if (NPC.ai[0] == 0f) //idle phase
			{
				//npc.TargetClosest(true);
				// Now we check the make sure the target is still valid and within our specified notice range

				if (phase == 0)
				{
					AI_003(speed, inertia, 5); //5, 0.2, 0.7 //speed, inertia, 5
				}
				else if (phase == 1)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					direction *= speed;
					float inertia = 10f;
					NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
				}

				if (Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
				{
					//npc.TargetClosest(true);
					targeting = true;
					/**npc.ai[2]++;
					if(npc.ai[2] > 1200)
					{
						npc.TargetClosest(false);
						targeting = false;
						npc.ai[2] = 0;
					}**/
				}
				else
				{
					NPC.TargetClosest(false);
					//targeting = false;
					//npc.ai[2] = 0;
				}
				if (NPC.justHit)
				{
					//targeting = true;
					//npc.ai[2] = 0;
				}

				if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < detectionRange)
				{

					if (Main.player[NPC.target].Distance(NPC.Center) < attackRange)
					{
						if (phase == 0)
						{
							NPC.ai[0] = Main.rand.Next(new int[] { 1, 2 });
							NPC.netUpdate = true;
						}
						else if (phase == 1)
						{
							NPC.ai[0] = 3;
						}
					}
				}
			}
			//else if (NPC.ai[0] == 1f) //attacking phase
			//{
			//	NPC.velocity.X *= 0.01f;
			//	/*
			//	 * Timer that counts only when the enemy is targetting the player
			//	 * Goes up by 1 each frame as long as we are in this state (assuming we're running 60 frames per second)
			//	 */
			//	NPC.ai[1]++;
			//	if (NPC.ai[1] % timer == 0)
			//	{
					
			//	}
			//	if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > attackRange)
			//	{
			//		NPC.ai[0] = 0f;
			//	}
			//}
			//else if (NPC.ai[0] == 2f) //attacking phase
			//{
			//	NPC.velocity.X = NPC.direction * 10;
			//	if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > attackRange)
			//	{
			//		NPC.ai[0] = 0f;
			//	}
			//}
			//else if (NPC.ai[0] == 3f)
			//{
			//	NPC.noGravity = true;
			//	NPC.velocity *= (float)Math.Pow(0.9, 40.0 / 10);

			//	NPC.ai[1]++;
			//	if (NPC.ai[1] % timer == 0)
			//	{
					
			//	}

			//	if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > attackRange)
			//	{
			//		NPC.ai[0] = 0f;
			//	}
			//}
		}

		private int count = 0; //setting ur own count variable makes it more relaible
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			if (NPC.ai[0] == 0f)
			{
				if (NPC.velocity.Y < 0)
				{
					NPC.frame.Y = 2 * frameHeight;
				}
				else if (NPC.velocity.Y > 0)
				{
					NPC.frame.Y = 3 * frameHeight;
				}
				else
				{
					count++;
					if (count < 10)
					{
						NPC.frame.Y = 0 * frameHeight;
					}
					else if (count < 20)
					{
						NPC.frame.Y = 1 * frameHeight;
					}
					else
					{
						count = 0;
					}

				}
			}
			else if (NPC.ai[0] > 0)
			{
				NPC.frame.Y = 5 * frameHeight;
			}
		}

		public void DrawDust(Vector2 position)
		{
			for (int i = 0; i < 20; i++)
			{
				//Dust.NewDust(position, npc.width, npc.height, 57, npc.velocity.X * 0.25f, npc.velocity.Y * 0.25f, 150, Color.Fuchsia, 0.7f);
				int dust = Dust.NewDust(position, 1, 1, 178, 0f, 0f, 0, Color.Fuchsia, 1f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = position;
				Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Main.dust[dust].velocity *= 0.2f;
			}
		}

		public void AI_003(float speed, float inertia, float value)
		{
			if (NPC.velocity.X == 0f)
			{
				if (NPC.velocity.Y == 0f)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= 2f)
					{
						NPC.direction *= -1;
						NPC.spriteDirection = NPC.direction;
						NPC.ai[1] = 0f;
					}
				}
			}
			else
			{
				NPC.ai[1] = 0f;
			}

			//Basic X-Movement for Enemies
			//Possessed Armor = 1.5 speed & 0.07 Inertia, Armored Skeleton = 2 speed & 0.07 Inertia
			//Bone Lee = 5 speed, & 0.2 inertia
			if (NPC.velocity.X < -speed || NPC.velocity.X > speed)
			{
				if (NPC.velocity.Y == 0f)
				{
					//maybe this slows it down?
					NPC.velocity *= value; //idk wat this does Bone Lee's is 0.7 & P. Armor is 0.8
				}
			}
			else if (NPC.velocity.X < speed && NPC.direction == 1)
			{
				NPC.velocity.X = NPC.velocity.X + inertia;
				if (NPC.velocity.X > speed)
				{
					NPC.velocity.X = speed;
				}
			}
			else if (NPC.velocity.X > -speed && NPC.direction == -1)
			{
				NPC.velocity.X = NPC.velocity.X - inertia;
				if (NPC.velocity.X < -speed)
				{
					NPC.velocity.X = -speed;
				}
			}

			bool flag12 = false;
			if (NPC.velocity.Y == 0)
			{

				int num199 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
				int num200 = (int)NPC.position.X / 16;
				int num201 = (int)(NPC.position.X + (float)NPC.width) / 16;
				for (int num202 = num200; num202 <= num201; num202++)
				{
					if (Main.tile[num202, num199] == null)
					{
						return;
					}
					if (Main.tile[num202, num199].HasUnactuatedTile && Main.tileSolid[Main.tile[num202, num199].TileType])
					{
						flag12 = true;
						break;
					}
				}

				if (flag12)
				{
					//Infront of the NPC Hitbox (based on direction)
					int tileX = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * NPC.direction)) / 16f);
					int tileY = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);

					if ((NPC.velocity.X < 0f && NPC.direction == -1) || (NPC.velocity.X > 0f && NPC.direction == 1))
					{
						//I believe this gets the tile coordinate at some point near the npc (unsure where tho)
						//Adapted from AI_003
						//Seems to have different checks for different jump heights
						//Main.tile[tileX, tileY + 1].halfBrick();
						int dir = 0 * NPC.direction; //npc.height >= 32 && 
						if (Main.tile[tileX + dir, tileY - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY - 2].TileType])
						{

							if (Main.tile[tileX + dir, tileY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY - 3].TileType])
							{
								NPC.velocity.Y = -8;
								NPC.netUpdate = true;
							}
							else
							{
								NPC.velocity.Y = -7;
								NPC.netUpdate = true;
							}
						}
						else if (Main.tile[tileX + dir, tileY - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY - 1].TileType])
						{
							NPC.velocity.Y = -6;
							NPC.netUpdate = true;
						}
						else if (NPC.position.Y + (float)NPC.height - (float)(tileY * 16) > 20f && Main.tile[tileX + dir, tileY].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY].TileType] && !Main.tile[tileX + dir, tileY].TopSlope)
						{
							NPC.velocity.Y = -5;
							NPC.netUpdate = true;
						}
						else if ((!Main.tile[tileX + dir, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX + dir, tileY + 1].TileType]) && (!Main.tile[tileX + NPC.direction, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX + NPC.direction, tileY + 1].TileType]))
						{
							NPC.velocity.Y = -8; //npc.directionY < 0 && 
												 //npc.velocity.X = npc.velocity.X * 1.5f;
							NPC.netUpdate = true;
						}
					}
				}
			}

			if (NPC.velocity.Y >= 0f)
			{
				//Bottom corner of hitbox depending on NPC direction
				int tileX2 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * NPC.direction)) / 16f);
				int tileY2 = (int)((NPC.position.Y + (float)NPC.height - 1f) / 16f);

				if (Main.tile[tileX2, tileY2].HasUnactuatedTile && !Main.tile[tileX2, tileY2].TopSlope && !Main.tile[tileX2, tileY2 - 1].TopSlope && Main.tileSolid[Main.tile[tileX2, tileY2].TileType] && !Main.tileSolidTop[Main.tile[tileX2, tileY2].TileType])
				{

					if ((!Main.tile[tileX2, tileY2 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 1].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 1].TileType] || (Main.tile[tileX2, tileY2 - 1].IsHalfBlock && (!Main.tile[tileX2, tileY2 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 4].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 4].TileType]))) && (!Main.tile[tileX2, tileY2 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 2].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 2].TileType]) && (!Main.tile[tileX2, tileY2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 3].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 3].TileType]) && (!Main.tile[tileX2 - NPC.direction, tileY2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2 - NPC.direction, tileY2 - 3].TileType]))
					{
						float num74 = (float)(tileY2 * 16);
						if (Main.tile[tileX2, tileY2].IsHalfBlock)
						{
							num74 += 8f;
						}
						if (Main.tile[tileX2, tileY2 - 1].IsHalfBlock)
						{
							num74 -= 8f;
						}
						if (num74 < NPC.position.Y + (float)NPC.height)
						{
							float num75 = NPC.position.Y + (float)NPC.height - num74;
							float num76 = 16.1f;
							if (num75 <= num76)
							{
								NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num74;
								NPC.position.Y = num74 - (float)NPC.height;
								if (num75 < 9f)
								{
									NPC.stepSpeed = 1f;
								}
								else
								{
									NPC.stepSpeed = 2f;
								}
							}
						}
					}
				}

				if (Main.tile[tileX2, tileY2 - 1].IsHalfBlock && Main.tile[tileX2, tileY2 - 1].HasUnactuatedTile)
				{
					if ((!Main.tile[tileX2, tileY2 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 1].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 1].TileType] || (Main.tile[tileX2, tileY2 - 1].IsHalfBlock && (!Main.tile[tileX2, tileY2 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 4].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 4].TileType]))) && (!Main.tile[tileX2, tileY2 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 2].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 2].TileType]) && (!Main.tile[tileX2, tileY2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2, tileY2 - 3].TileType] || Main.tileSolidTop[Main.tile[tileX2, tileY2 - 3].TileType]) && (!Main.tile[tileX2 - NPC.direction, tileY2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX2 - NPC.direction, tileY2 - 3].TileType]))
					{
						float num74 = (float)(tileY2 * 16);
						if (Main.tile[tileX2, tileY2].IsHalfBlock)
						{
							num74 += 8f;
						}
						if (Main.tile[tileX2, tileY2 - 1].IsHalfBlock)
						{
							num74 -= 8f;
						}
						if (num74 < NPC.position.Y + (float)NPC.height)
						{
							float num75 = NPC.position.Y + (float)NPC.height - num74;
							float num76 = 16.1f;
							if (num75 <= num76)
							{
								NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num74;
								NPC.position.Y = num74 - (float)NPC.height;
								if (num75 < 9f)
								{
									NPC.stepSpeed = 1f;
								}
								else
								{
									NPC.stepSpeed = 2f;
								}
							}
						}
					}
				}
			}
		}
	}
}
/**if (Main.tile[tileX - 1, tileY].nactive() && Main.tileSolid[Main.tile[tileX - 1, tileY].type] && !Main.tile[tileX - 1, tileY].halfBrick() && !Main.tile[tileX - 1, tileY].topSlope())
					{
						npc.velocity.Y = -6;
					}
					else if(Main.tile[tileX + 1, tileY].nactive() && Main.tileSolid[Main.tile[tileX + 1, tileY].type] && !Main.tile[tileX - 1, tileY].halfBrick() && !Main.tile[tileX - 1, tileY].topSlope())
					{
						npc.velocity.Y = -6;
					}**/

/**if (Main.tile[tileX - 1, tileY + 1] == null)
{
	npc.velocity.Y = -7;
}
else if (Main.tile[tileX + 1, tileY + 1] == null)
{
	npc.velocity.Y = -7;
}**/

/**if (npc.collideX)
{
	npc.velocity.Y = -7;
}**/

/**if (!npc.collideY && npc.velocity.Y == 0)
{
	npc.velocity.Y = -7;
}**/
/**
					npc.scale = 1;
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					float speed = 2f;
					//npc.velocity = new Vector2(npc.direction * speed, 0);
					npc.velocity.X = npc.direction * speed;
					npc.velocity.Y += 1f;
					//npc.velocity.Y = 0;
					//npc.velocity.Y = npc.direction * speed;
					//if (!Collision.SolidCollision(npc.position, npc.width, npc.height))

	/**float num1472 = 5f;
					Vector2 vector256 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float speedX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector256.X;
					float speedY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector256.Y;
					float num1475 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
					num1475 = num1472 / num1475;
					speedX *= num1475;
					speedY *= num1475;
					int damage = 1; //damage
									//int projID = ProjectileType<Projectiles.TestProjectile>(); //changes what projectile is used basde on id
					int projID = 300; //300
					Projectile.NewProjectile(vector256.X, vector256.Y, speedX / 2, speedY / 2, projID, damage, -1f, Main.myPlayer, 0f, 0f);**/
