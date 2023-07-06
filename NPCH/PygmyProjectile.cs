using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.NPCH
{
	internal class PygmyItem : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.ChlorophytePartisan;

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<PygmyProjectile>();
			Item.shootSpeed = 0f;
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

	internal class PygmyProjectile : ModProjectile
	{

		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BouncyBomb;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pygmy 2");
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void AI()
		{
			AI_066();
		}

		private void AI_026()
		{
			if (!Main.player[Projectile.owner].active)
			{
				Projectile.active = false;
				return;
			}
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;

			if (Projectile.lavaWet)
			{
				Projectile.ai[0] = 1f;
				Projectile.ai[1] = 0f;
			}

			bool flag7 = Projectile.ai[0] == -1f || Projectile.ai[0] == -2f;
			bool num2 = Projectile.ai[0] == -1f;
			bool flag8 = Projectile.ai[0] == -2f;

			if (Main.player[Projectile.owner].dead)
			{
				Main.player[Projectile.owner].pygmy = false;
			}
			if (Main.player[Projectile.owner].pygmy)
			{
				Projectile.timeLeft = Main.rand.Next(2, 10);
			}

			if (flag7)
			{
				Projectile.timeLeft = 2;
			}

			int num = 10;
			int num3 = 40 * (Projectile.minionPos + 1) * Main.player[Projectile.owner].direction;
			if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) < Projectile.position.X + (float)(Projectile.width / 2) - (float)num + (float)num3)
			{
				flag2 = true;
			}
			else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) > Projectile.position.X + (float)(Projectile.width / 2) + (float)num + (float)num3)
			{
				flag3 = true;
			}

			if (num2)
			{
				flag2 = false;
				flag3 = true;
				num = 30;
			}
			if (flag8)
			{
				flag2 = false;
				flag3 = false;
			}

			bool flag11 = Projectile.ai[1] == 0f;
			if (flag11) // When the Projectile is not attacking
			{
				int num78 = 500;

				num78 += 40 * Projectile.minionPos;
				if (Projectile.localAI[0] > 0f)
				{
					num78 += 500;
				}

				if (Main.player[Projectile.owner].rocketDelay2 > 0)
				{
					Projectile.ai[0] = 1f;
				}
				Vector2 vector7 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num79 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector7.X;
				float num80 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector7.Y;
				float num81 = (float)Math.Sqrt(num79 * num79 + num80 * num80);
				if (!flag7)
				{
					if (num81 > 2000f)
					{
						Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
						Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - (float)(Projectile.height / 2);
					}
				}
			}
			if (Projectile.ai[0] != 0f && !flag7) // When the Projectile is returning to the player
			{
				int num84 = 100;

				Projectile.tileCollide = false;
				Vector2 vector8 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num85 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector8.X;

				num85 -= (float)(40 * Main.player[Projectile.owner].direction);
				float num86 = 700f;
				num86 += 100f;

				bool flag12 = false;
				int num87 = -1;
				for (int num88 = 0; num88 < 200; num88++)
				{
					if (!Main.npc[num88].CanBeChasedBy(this))
					{
						continue;
					}
					float num89 = Main.npc[num88].position.X + (float)(Main.npc[num88].width / 2);
					float num90 = Main.npc[num88].position.Y + (float)(Main.npc[num88].height / 2);
					if (Math.Abs(Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - num89) + Math.Abs(Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - num90) < num86)
					{
						if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num88].position, Main.npc[num88].width, Main.npc[num88].height))
						{
							num87 = num88;
						}
						flag12 = true;
						break;
					}
				}
				if (!flag12)
				{
					num85 -= (float)(40 * Projectile.minionPos * Main.player[Projectile.owner].direction);
				}
				if (flag12 && num87 >= 0)
				{
					Projectile.ai[0] = 0f;
				}

				float num91 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector8.Y;
				float num92 = (float)Math.Sqrt(num85 * num85 + num91 * num91);

				float num83 = 0.4f;
				float num94 = 12f;

				if (num94 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
				{
					num94 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
				}

				if (num92 < (float)num84 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num92 < 60f)
				{
					num85 = Projectile.velocity.X;
					num91 = Projectile.velocity.Y;
				}
				else
				{
					num92 = num94 / num92;
					num85 *= num92;
					num91 *= num92;
				}

				if (Projectile.velocity.X < num85)
				{
					Projectile.velocity.X += num83;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X += num83 * 1.5f;
					}
				}
				if (Projectile.velocity.X > num85)
				{
					Projectile.velocity.X -= num83;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X -= num83 * 1.5f;
					}
				}
				if (Projectile.velocity.Y < num91)
				{
					Projectile.velocity.Y += num83;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y += num83 * 1.5f;
					}
				}
				if (Projectile.velocity.Y > num91)
				{
					Projectile.velocity.Y -= num83;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y -= num83 * 1.5f;
					}
				}

				if (Projectile.frame < 12)
				{
					Projectile.frame = Main.rand.Next(12, 18);
					Projectile.frameCounter = 0;
				}

				if ((double)Projectile.velocity.X > 0.5)
				{
					Projectile.spriteDirection = -1;
				}
				else if ((double)Projectile.velocity.X < -0.5)
				{
					Projectile.spriteDirection = 1;
				}

				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
				}
				else
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
				}
			}
			else // When the Projectile moves around on the ground
			{
				float num115 = 40 * Projectile.minionPos;
				int num116 = 30;
				int num117 = 60;
				Projectile.localAI[0] -= 1f;
				if (Projectile.localAI[0] < 0f)
				{
					Projectile.localAI[0] = 0f;
				}
				if (Projectile.ai[1] > 0f)
				{
					Projectile.ai[1] -= 1f;
				}
				else
				{
					float num118 = Projectile.position.X;
					float num119 = Projectile.position.Y;
					float num120 = 100000f;
					float num121 = num120;
					int num122 = -1;
					float num123 = 20f;
					NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this))
					{
						float num124 = ownerMinionAttackTargetNPC.position.X + (float)(ownerMinionAttackTargetNPC.width / 2);
						float num125 = ownerMinionAttackTargetNPC.position.Y + (float)(ownerMinionAttackTargetNPC.height / 2);
						float num126 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num124) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num125);
						if (num126 < num120)
						{
							if (num122 == -1 && num126 <= num121)
							{
								num121 = num126;
								num118 = num124;
								num119 = num125;
							}
							if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
							{
								num120 = num126;
								num118 = num124;
								num119 = num125;
								num122 = ownerMinionAttackTargetNPC.whoAmI;
							}
						}
					}
					if (num122 == -1)
					{
						for (int num127 = 0; num127 < 200; num127++)
						{
							if (!Main.npc[num127].CanBeChasedBy(this))
							{
								continue;
							}
							float num128 = Main.npc[num127].position.X + (float)(Main.npc[num127].width / 2);
							float num129 = Main.npc[num127].position.Y + (float)(Main.npc[num127].height / 2);
							float num130 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num128) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num129);
							if (num130 < num120)
							{
								if (num122 == -1 && num130 <= num121)
								{
									num121 = num130;
									num118 = num128 + Main.npc[num127].velocity.X * num123;
									num119 = num129 + Main.npc[num127].velocity.Y * num123;
								}
								if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num127].position, Main.npc[num127].width, Main.npc[num127].height))
								{
									num120 = num130;
									num118 = num128 + Main.npc[num127].velocity.X * num123;
									num119 = num129 + Main.npc[num127].velocity.Y * num123;
									num122 = num127;
								}
							}
						}
					}
					if (num122 == -1 && num121 < num120)
					{
						num120 = num121;
					}
					float num131 = 400f;
					if ((double)Projectile.position.Y > Main.worldSurface * 16.0)
					{
						num131 = 200f;
					}
					if (num120 < num131 + num115 && num122 == -1)
					{
						float num132 = num118 - (Projectile.position.X + (float)(Projectile.width / 2));
						if (num132 < -5f)
						{
							flag2 = true;
							flag3 = false;
						}
						else if (num132 > 5f)
						{
							flag3 = true;
							flag2 = false;
						}
					}
					else if (num122 >= 0 && num120 < 800f + num115)
					{
						Projectile.localAI[0] = num117;
						float num133 = num118 - (Projectile.position.X + (float)(Projectile.width / 2));
						if (num133 > 450f || num133 < -450f)
						{
							if (num133 < -50f)
							{
								flag2 = true;
								flag3 = false;
							}
							else if (num133 > 50f)
							{
								flag3 = true;
								flag2 = false;
							}
						}
						else if (Projectile.owner == Main.myPlayer)
						{
							Projectile.ai[1] = num116;
							Vector2 vector11 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)(Projectile.height / 2) - 8f);
							float num134 = num118 - vector11.X + (float)Main.rand.Next(-20, 21);
							float num135 = Math.Abs(num134) * 0.1f;
							num135 = num135 * (float)Main.rand.Next(0, 100) * 0.001f;
							float num136 = num119 - vector11.Y + (float)Main.rand.Next(-20, 21) - num135;
							float num137 = (float)Math.Sqrt(num134 * num134 + num136 * num136);
							num137 = 11f / num137;
							num134 *= num137;
							num136 *= num137;
							int num138 = Projectile.damage;
							int num139 = ModContent.ProjectileType<Characters.Yanfei.YanfeiProjectile>();
							int num140 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector11.X, vector11.Y, num134, num136, num139, num138, Projectile.knockBack, Main.myPlayer);
							Main.projectile[num140].timeLeft = 300;
							if (num134 < 0f)
							{
								Projectile.direction = -1;
							}
							if (num134 > 0f)
							{
								Projectile.direction = 1;
							}
							Projectile.netUpdate = true;
						}
					}
				}

				bool flag14 = false;

				if (Projectile.ai[1] != 0f)
				{
					flag2 = false;
					flag3 = false;
				}
				else if (Projectile.localAI[0] == 0f)
				{
					Projectile.direction = Main.player[Projectile.owner].direction;
				}
				if (!flag14)
				{
					Projectile.rotation = 0f;
				}
				Projectile.tileCollide = true;

				float num165 = 6f;
				float num164 = 0.2f;
				if (num165 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
				{
					num165 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
					num164 = 0.3f;
				}
				num164 *= 2f;

				if (flag7)
				{
					num165 = 6f;
				}
				if (flag2)
				{
					if ((double)Projectile.velocity.X > -3.5)
					{
						Projectile.velocity.X -= num164;
					}
					else
					{
						Projectile.velocity.X -= num164 * 0.25f;
					}
				}
				else if (flag3)
				{
					if ((double)Projectile.velocity.X < 3.5)
					{
						Projectile.velocity.X += num164;
					}
					else
					{
						Projectile.velocity.X += num164 * 0.25f;
					}
				}
				else
				{
					Projectile.velocity.X *= 0.9f;
					if (Projectile.velocity.X >= 0f - num164 && Projectile.velocity.X <= num164)
					{
						Projectile.velocity.X = 0f;
					}
				}
				if (flag2 || flag3)
				{
					int num166 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
					int j2 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
					if (Projectile.type == 236)
					{
						num166 += Projectile.direction;
					}
					if (flag2)
					{
						num166--;
					}
					if (flag3)
					{
						num166++;
					}
					num166 += (int)Projectile.velocity.X;
					if (WorldGen.SolidTile(num166, j2))
					{
						flag5 = true;
					}
				}
				if (Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height - 8f > Projectile.position.Y + (float)Projectile.height)
				{
					flag4 = true;
				}
				Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
				if (Projectile.velocity.Y == 0f)
				{
					if (!flag4 && (Projectile.velocity.X < 0f || Projectile.velocity.X > 0f))
					{
						int num167 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
						int j3 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16 + 1;
						if (flag2)
						{
							num167--;
						}
						if (flag3)
						{
							num167++;
						}
						WorldGen.SolidTile(num167, j3);
					}
					if (flag5)
					{
						int num168 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
						int num169 = (int)(Projectile.position.Y + (float)Projectile.height) / 16;
						if (WorldGen.SolidTileAllowBottomSlope(num168, num169) || Main.tile[num168, num169].IsHalfBlock || Main.tile[num168, num169].Slope > 0 || Projectile.type == 200)
						{

							try
							{
								num168 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
								num169 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
								if (flag2)
								{
									num168--;
								}
								if (flag3)
								{
									num168++;
								}
								num168 += (int)Projectile.velocity.X;
								if (!WorldGen.SolidTile(num168, num169 - 1) && !WorldGen.SolidTile(num168, num169 - 2))
								{
									Projectile.velocity.Y = -5.1f;
								}
								else if (!WorldGen.SolidTile(num168, num169 - 2))
								{
									Projectile.velocity.Y = -7.1f;
								}
								else if (WorldGen.SolidTile(num168, num169 - 5))
								{
									Projectile.velocity.Y = -11.1f;
								}
								else if (WorldGen.SolidTile(num168, num169 - 4))
								{
									Projectile.velocity.Y = -10.1f;
								}
								else
								{
									Projectile.velocity.Y = -9.1f;
								}
							}
							catch
							{
								Projectile.velocity.Y = -9.1f;
							}

						}
					}
				}
				if (Projectile.velocity.X > num165)
				{
					Projectile.velocity.X = num165;
				}
				if (Projectile.velocity.X < 0f - num165)
				{
					Projectile.velocity.X = 0f - num165;
				}
				if (Projectile.velocity.X < 0f)
				{
					Projectile.direction = -1;
				}
				if (Projectile.velocity.X > 0f)
				{
					Projectile.direction = 1;
				}
				if (Projectile.velocity.X > num164 && flag3)
				{
					Projectile.direction = 1;
				}
				if (Projectile.velocity.X < 0f - num164 && flag2)
				{
					Projectile.direction = -1;
				}

				if (Projectile.direction == -1)
				{
					Projectile.spriteDirection = 1;
				}
				if (Projectile.direction == 1)
				{
					Projectile.spriteDirection = -1;
				}

				bool flag16 = Projectile.position.X - Projectile.oldPosition.X == 0f;

				if (Projectile.ai[1] > 0f)
				{
					if (Projectile.localAI[1] == 0f)
					{
						Projectile.localAI[1] = 1f;
						Projectile.frame = 1;
					}
					if (Projectile.frame != 0)
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame >= 4)
						{
							Projectile.frame = 0;
						}
					}
				}
				else if (Projectile.velocity.Y == 0f)
				{
					Projectile.localAI[1] = 0f;
					if (flag16)
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
					{
						Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5)
						{
							Projectile.frame = 5;
						}
						if (Projectile.frame >= 11)
						{
							Projectile.frame = 5;
						}
					}
					else
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
				}
				else if (Projectile.velocity.Y < 0f)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = 4;
				}
				else if (Projectile.velocity.Y > 0f)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = 4;
				}
				Projectile.velocity.Y += 0.4f;
				if (Projectile.velocity.Y > 10f)
				{
					Projectile.velocity.Y = 10f;
				}
				_ = Projectile.velocity;

			}
		}

		private void AI_066()
        {
			float num555 = 0f;
			float num556 = 0f;
			float num557 = 0f;
			float num558 = 0f;
			bool flag26 = true;
			if (flag26)
			{
				num555 = 2000f;
				num556 = 800f;
				num557 = 1200f;
				num558 = 150f;
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].twinsMinion = false;
				}
				if (Main.player[Projectile.owner].twinsMinion)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 533)
			{
				num555 = 2000f;
				num556 = 900f;
				num557 = 1500f;
				num558 = 450f;
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].DeadlySphereMinion = false;
				}
				if (Main.player[Projectile.owner].DeadlySphereMinion)
				{
					Projectile.timeLeft = 2;
				}
				Projectile.localAI[2] = Utils.Clamp(Projectile.localAI[2] - 1f, 0f, 60f);
			}
			float num559 = 0.05f;
			for (int num560 = 0; num560 < 1000; num560++)
			{
				bool flag27 = (Main.projectile[num560].type == 387 || Main.projectile[num560].type == 388) && (Projectile.type == 387 || Projectile.type == 388);
				if (!flag27)
				{
					flag27 = Projectile.type == 533 && Main.projectile[num560].type == 533;
				}
				if (num560 != Projectile.whoAmI && Main.projectile[num560].active && Main.projectile[num560].owner == Projectile.owner && flag27 && Math.Abs(Projectile.position.X - Main.projectile[num560].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num560].position.Y) < (float)Projectile.width)
				{
					if (Projectile.position.X < Main.projectile[num560].position.X)
					{
						Projectile.velocity.X -= num559;
					}
					else
					{
						Projectile.velocity.X += num559;
					}
					if (Projectile.position.Y < Main.projectile[num560].position.Y)
					{
						Projectile.velocity.Y -= num559;
					}
					else
					{
						Projectile.velocity.Y += num559;
					}
				}
			}
			if (Projectile.type == 533)
			{
				if ((int)Projectile.ai[0] % 3 != 2)
				{
					Lighting.AddLight(Projectile.Center, 0.8f, 0.3f, 0.1f);
				}
				else
				{
					Lighting.AddLight(Projectile.Center, 0.3f, 0.5f, 0.7f);
				}
			}
			bool flag28 = false;
			if (Projectile.ai[0] == 2f && Projectile.type == 388)
			{
				Projectile.ai[1]++;
				Projectile.extraUpdates = 1;
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI;
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 1)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 2)
				{
					Projectile.frame = 0;
				}
				if (Projectile.ai[1] > 40f)
				{
					Projectile.ai[1] = 1f;
					Projectile.ai[0] = 0f;
					Projectile.extraUpdates = 0;
					Projectile.numUpdates = 0;
					Projectile.netUpdate = true;
				}
				else
				{
					flag28 = true;
				}
			}
			if (Projectile.type == 533 && Projectile.ai[0] >= 3f && Projectile.ai[0] <= 5f)
			{
				int num561 = 2;
				flag28 = true;
				Projectile.velocity *= 0.9f;
				Projectile.ai[1]++;
				int num562 = (int)Projectile.ai[1] / num561 + (int)(Projectile.ai[0] - 3f) * 8;
				if (num562 < 4)
				{
					Projectile.frame = 17 + num562;
				}
				else if (num562 < 5)
				{
					Projectile.frame = 0;
				}
				else if (num562 < 8)
				{
					Projectile.frame = 1 + num562 - 5;
				}
				else if (num562 < 11)
				{
					Projectile.frame = 11 - num562;
				}
				else if (num562 < 12)
				{
					Projectile.frame = 0;
				}
				else if (num562 < 16)
				{
					Projectile.frame = num562 - 2;
				}
				else if (num562 < 20)
				{
					Projectile.frame = 29 - num562;
				}
				else if (num562 < 21)
				{
					Projectile.frame = 0;
				}
				else
				{
					Projectile.frame = num562 - 4;
				}
				if (Projectile.ai[1] > (float)(num561 * 8))
				{
					Projectile.ai[0] -= 3f;
					Projectile.ai[1] = 0f;
				}
			}
			if (Projectile.type == 533 && Projectile.ai[0] >= 6f && Projectile.ai[0] <= 8f)
			{
				Projectile.ai[1]++;
				Projectile.MaxUpdates = 2;
				if (Projectile.ai[0] == 7f)
				{
					Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI;
				}
				else
				{
					Projectile.rotation += (float)Math.PI / 6f;
				}
				int num563 = 0;
				switch ((int)Projectile.ai[0])
				{
					case 6:
						Projectile.frame = 5;
						num563 = 40;
						break;
					case 7:
						Projectile.frame = 13;
						num563 = 30;
						break;
					case 8:
						Projectile.frame = 17;
						num563 = 30;
						break;
				}
				if (Projectile.ai[1] > (float)num563)
				{
					Projectile.ai[1] = 1f;
					Projectile.ai[0] -= 6f;
					Projectile.localAI[0]++;
					Projectile.extraUpdates = 0;
					Projectile.numUpdates = 0;
					Projectile.netUpdate = true;
				}
				else
				{
					flag28 = true;
				}
				if (Projectile.ai[0] == 8f)
				{
					for (int num564 = 0; num564 < 4; num564++)
					{
						int num565 = Utils.SelectRandom<int>(Main.rand, 226, 228, 75);
						int num566 = Dust.NewDust(Projectile.Center, 0, 0, num565);
						Dust dust14 = Main.dust[num566];
						Vector2 vector43 = Vector2.One.RotatedBy((float)num564 * ((float)Math.PI / 2f)).RotatedBy(Projectile.rotation);
						dust14.position = Projectile.Center + vector43 * 10f;
						dust14.velocity = vector43 * 1f;
						dust14.scale = 0.6f + Main.rand.NextFloat() * 0.5f;
						dust14.noGravity = true;
					}
				}
			}
			if (flag28)
			{
				return;
			}
			Vector2 center5 = Projectile.position;
			Vector2 zero = Vector2.Zero;
			bool flag29 = false;
			if (Projectile.ai[0] != 1f && flag26)
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.type == 533 && Projectile.ai[0] < 9f)
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
			{
				Projectile.tileCollide = false;
			}
			NPC ownerMinionAttackTargetNPC3 = Projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC3 != null && ownerMinionAttackTargetNPC3.CanBeChasedBy(this))
			{
				float num567 = Vector2.Distance(ownerMinionAttackTargetNPC3.Center, Projectile.Center);
				float num568 = num555 * 3f;
				if (num567 < num568 && !flag29 && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC3.position, ownerMinionAttackTargetNPC3.width, ownerMinionAttackTargetNPC3.height))
				{
					num555 = num567;
					center5 = ownerMinionAttackTargetNPC3.Center;
					flag29 = true;
				}
			}
			if (!flag29)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					NPC nPC5 = Main.npc[num569];
					if (nPC5.CanBeChasedBy(this))
					{
						float num570 = Vector2.Distance(nPC5.Center, Projectile.Center);
						if (!(num570 >= num555) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC5.position, nPC5.width, nPC5.height))
						{
							num555 = num570;
							center5 = nPC5.Center;
							zero = nPC5.velocity;
							flag29 = true;
						}
					}
				}
			}
			float num571 = num556;
			if (flag29)
			{
				num571 = num557;
			}
			Player player4 = Main.player[Projectile.owner];
			if (Vector2.Distance(player4.Center, Projectile.Center) > num571)
			{
				if (flag26)
				{
					Projectile.ai[0] = 1f;
				}
				if (Projectile.type == 533 && Projectile.ai[0] < 9f)
				{
					Projectile.ai[0] += 3 * (3 - (int)(Projectile.ai[0] / 3f));
				}
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			if (flag26 && flag29 && Projectile.ai[0] == 0f)
			{
				Vector2 vector44 = center5 - Projectile.Center;
				float num572 = vector44.Length();
				vector44.Normalize();
				if (num572 > 200f)
				{
					float num573 = 6f;
					if (Projectile.type == 388)
					{
						num573 = 14f;
					}
					vector44 *= num573;
					Projectile.velocity = (Projectile.velocity * 40f + vector44) / 41f;
				}
				else
				{
					float num574 = 4f;
					vector44 *= 0f - num574;
					Projectile.velocity = (Projectile.velocity * 40f + vector44) / 41f;
				}
			}
			else
			{
				bool flag30 = false;
				if (!flag30 && flag26)
				{
					flag30 = Projectile.ai[0] == 1f;
				}
				if (!flag30 && Projectile.type == 533)
				{
					flag30 = Projectile.ai[0] >= 9f;
				}
				float num575 = 6f;
				float num576 = 40f;
				if (Projectile.type == 533)
				{
					num575 = 12f;
				}
				if (flag30)
				{
					num575 = 15f;
				}
				Vector2 center6 = Projectile.Center;
				Vector2 vector45 = player4.Center - center6 + new Vector2(0f, -60f);
				float num577 = vector45.Length();
				float num578 = num577;
				if (num577 > 200f && num575 < 8f)
				{
					num575 = 8f;
				}
				if (num575 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
				{
					num576 = 30f;
					num575 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
					if (num577 > 200f)
					{
						num576 = 20f;
						num575 += 4f;
					}
					else if (num577 > 100f)
					{
						num575 += 3f;
					}
				}
				if (flag30 && num577 > 300f)
				{
					num575 += 6f;
					num576 -= 10f;
				}
				if (num577 < num558 && flag30 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					if (Projectile.type == 387 || Projectile.type == 388)
					{
						Projectile.ai[0] = 0f;
					}
					if (Projectile.type == 533)
					{
						Projectile.ai[0] -= 9f;
					}
					Projectile.netUpdate = true;
				}
				if (num577 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
					Projectile.netUpdate = true;
				}
				if (num577 > 70f)
				{
					Vector2 vector46 = vector45;
					vector45.Normalize();
					vector45 *= num575;
					Projectile.velocity = (Projectile.velocity * num576 + vector45) / (num576 + 1f);
				}
				else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
				{
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
				if (Projectile.velocity.Length() > num575)
				{
					Projectile.velocity *= 0.95f;
				}
			}
			if (Projectile.type == 388)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI;
			}
			if (Projectile.type == 387)
			{
				if (Projectile.ai[0] != 1f && flag29)
				{
					Projectile.rotation = (center5 - Projectile.Center).ToRotation() + (float)Math.PI;
				}
				else
				{
					Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI;
				}
			}
			if (Projectile.type == 533 && (Projectile.ai[0] < 3f || Projectile.ai[0] >= 9f))
			{
				Projectile.rotation += Projectile.velocity.X * 0.04f;
			}
			if (Projectile.type == 388 || Projectile.type == 387)
			{
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 3)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 2)
				{
					Projectile.frame = 0;
				}
			}
			else if (Projectile.type == 533)
			{
				if (Projectile.ai[0] < 3f || Projectile.ai[0] >= 9f)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter >= 24)
					{
						Projectile.frameCounter = 0;
					}
					int num579 = Projectile.frameCounter / 4;
					Projectile.frame = 4 + num579;
					switch ((int)Projectile.ai[0])
					{
						case 0:
						case 9:
							Projectile.frame = 4 + num579;
							break;
						case 1:
						case 10:
							num579 = Projectile.frameCounter / 8;
							Projectile.frame = 14 + num579;
							break;
						case 2:
						case 11:
							num579 = Projectile.frameCounter / 3;
							if (num579 >= 4)
							{
								num579 -= 4;
							}
							Projectile.frame = 17 + num579;
							break;
					}
				}
				if (Projectile.ai[0] == 2f && Main.rand.Next(2) == 0)
				{
					for (int num580 = 0; num580 < 4; num580++)
					{
						if (Main.rand.Next(2) != 0)
						{
							int num581 = Utils.SelectRandom<int>(Main.rand, 226, 228, 75);
							int num582 = Dust.NewDust(Projectile.Center, 0, 0, num581);
							Dust dust15 = Main.dust[num582];
							Vector2 vector47 = Vector2.One.RotatedBy((float)num580 * ((float)Math.PI / 2f)).RotatedBy(Projectile.rotation);
							dust15.position = Projectile.Center + vector47 * 10f;
							dust15.velocity = vector47 * 1f;
							dust15.scale = 0.3f + Main.rand.NextFloat() * 0.5f;
							dust15.noGravity = true;
							dust15.customData = this;
							dust15.noLight = true;
						}
					}
				}
			}
			if (Projectile.ai[1] > 0f && flag26)
			{
				Projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 90f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] > 40f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] > 0f && Projectile.type == 533)
			{
				Projectile.ai[1]++;
				int num583 = 10;
				if (Projectile.ai[1] > (float)num583)
				{
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
			}
			if (Projectile.ai[0] == 0f && flag26)
			{
				if (true)
				{
					float num584 = 8f;
					int num585 = 389;
					if (flag29 && Projectile.ai[1] == 0f)
					{
						Projectile.ai[1]++;
						if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, center5, 0, 0))
						{
							Vector2 vector48 = center5 - Projectile.Center;
							vector48.Normalize();
							vector48 *= num584;
							int num586 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector48.X, vector48.Y, num585, (int)((float)Projectile.damage * 1.15f), 0f, Main.myPlayer);
							Main.projectile[num586].timeLeft = 300;
							Projectile.netUpdate = true;
						}
					}
				}
				if (Projectile.ai[1] == 0f && flag29 && num555 < 500f)
				{
					Projectile.ai[1]++;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.ai[0] = 2f;
						Vector2 v4 = center5 - Projectile.Center;
						v4 = v4.SafeNormalize(Projectile.velocity);
						float num587 = 8f;
						Projectile.velocity = v4 * num587;
						AI_066_TryInterceptingTarget(center5, zero, num587);
						Projectile.netUpdate = true;
					}
				}
			}
			else
			{
				if (Projectile.type != 533 || !(Projectile.ai[0] < 9f))
				{
					return;
				}
				int num588 = 0;
				num588 = 800;
				if (!(Projectile.ai[1] == 0f && flag29) || !(num555 < (float)num588))
				{
					return;
				}
				Projectile.ai[1]++;
				if (Main.myPlayer != Projectile.owner)
				{
					return;
				}
				if (Projectile.localAI[0] >= 3f)
				{
					Projectile.ai[0] += 4f;
					if (Projectile.ai[0] == 6f)
					{
						Projectile.ai[0] = 3f;
					}
					Projectile.localAI[0] = 0f;
				}
				else
				{
					Projectile.ai[0] += 6f;
					Vector2 v5 = center5 - Projectile.Center;
					v5 = v5.SafeNormalize(Vector2.Zero);
					float num589 = ((Projectile.ai[0] == 8f) ? 12f : 10f);
					Projectile.velocity = v5 * num589;
					AI_066_TryInterceptingTarget(center5, zero, num589);
					Projectile.netUpdate = true;
				}
			}
		}

		private void AI_066_TryInterceptingTarget(Vector2 targetDir, Vector2 targetVelocity, float speed)
		{
			float num = 5f;
			float num2 = 30f;
			float num3 = num2 + num;
			int num4 = 1;
			int num5 = 4;
			int num6 = 2;
			bool flag = false;
			if (Projectile.type == 533)
			{
				num4 = 2;
			}
			if (Projectile.type == 388)
			{
				num4 = 2;
				num2 = 40f;
			}
			targetVelocity /= (float)num4;
			for (float num7 = 1f; num7 <= 1.5f; num7 += 0.1f)
			{
				ChaseResults chaseResults = GetChaseResults(Projectile.Center, speed, targetDir, targetVelocity);
				if (chaseResults.InterceptionHappens && chaseResults.InterceptionTime <= num3)
				{
					Projectile.velocity = chaseResults.ChaserVelocity;
					if (flag)
					{
						int num8 = (int)Utils.Clamp((float)Math.Ceiling(chaseResults.InterceptionTime) + (float)num6, num5, num2 - 1f) / num4;
						float num9 = num2 / (float)num4 - (float)num8;
						Projectile.ai[1] += num9 * (float)num4;
					}
					break;
				}
			}
		}

		public struct ChaseResults
		{
			public bool InterceptionHappens;

			public Vector2 InterceptionPosition;

			public float InterceptionTime;

			public Vector2 ChaserVelocity;
		}

		public ChaseResults GetChaseResults(Vector2 chaserPosition, float chaserSpeed, Vector2 runnerPosition, Vector2 runnerVelocity)
		{
			ChaseResults result = default(ChaseResults);
			if (chaserPosition == runnerPosition)
			{
				ChaseResults result2 = default(ChaseResults);
				result2.InterceptionHappens = true;
				result2.InterceptionPosition = chaserPosition;
				result2.InterceptionTime = 0f;
				result2.ChaserVelocity = Vector2.Zero;
				return result2;
			}
			if (chaserSpeed <= 0f)
			{
				return default(ChaseResults);
			}
			Vector2 value = chaserPosition - runnerPosition;
			float num = value.Length();
			float num2 = runnerVelocity.Length();
			if (num2 == 0f)
			{
				result.InterceptionTime = num / chaserSpeed;
				result.InterceptionPosition = runnerPosition;
			}
			else
			{
				float a = chaserSpeed * chaserSpeed - num2 * num2;
				float b = 2f * Vector2.Dot(value, runnerVelocity);
				float c = (0f - num) * num;
				if (!SolveQuadratic(a, b, c, out var result3, out var result4))
				{
					return default(ChaseResults);
				}
				if (result3 < 0f && result4 < 0f)
				{
					return default(ChaseResults);
				}
				if (result3 > 0f && result4 > 0f)
				{
					result.InterceptionTime = Math.Min(result3, result4);
				}
				else
				{
					result.InterceptionTime = Math.Max(result3, result4);
				}
				result.InterceptionPosition = runnerPosition + runnerVelocity * result.InterceptionTime;
			}
			result.ChaserVelocity = (result.InterceptionPosition - chaserPosition) / result.InterceptionTime;
			result.InterceptionHappens = true;
			return result;
		}

		public bool SolveQuadratic(float a, float b, float c, out float result1, out float result2)
		{
			float num = b * b - 4f * a * c;
			result1 = 0f;
			result2 = 0f;
			if (num > 0f)
			{
				result1 = (0f - b + (float)Math.Sqrt(num)) / (2f * a);
				result2 = (0f - b - (float)Math.Sqrt(num)) / (2f * a);
				return true;
			}
			if (num < 0f)
			{
				return false;
			}
			result1 = (result2 = (0f - b + (float)Math.Sqrt(num)) / (2f * a));
			return true;
		}
	}
}