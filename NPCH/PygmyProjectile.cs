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
			AI_026();
		}

		private void AI_026()
		{
			if (!Main.player[Projectile.owner].active)
			{
				Projectile.active = false;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int num = 85;
			//bool flag6 = Projectile.type >= 191 && Projectile.type <= 194;

			if (Projectile.lavaWet)
			{
				Projectile.ai[0] = 1f;
				Projectile.ai[1] = 0f;
			}
			num = 60 + 30 * Projectile.minionPos;

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

			num = 10;
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
			if (flag)
			{
				flag11 = true;
			}
			if (flag11)
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
			if (Projectile.ai[0] != 0f && !flag7)
			{
				//Main.NewText("returning to player");
				float num83 = 0.2f;
				int num84 = 200;

				num83 = 0.5f;
				num84 = 100;

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
				float num93 = num92;
				float num94 = 10f;
				float num95 = num92;

				num83 = 0.4f;
				num94 = 12f;

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
			else
			{
				//Main.NewText("else");
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

				bool flag13 = false;
				Vector2 vector12 = Vector2.Zero;
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

				float num164 = 0.08f;
				float num165 = 6.5f;

				num165 = 6f;
				num164 = 0.2f;
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
	}
}