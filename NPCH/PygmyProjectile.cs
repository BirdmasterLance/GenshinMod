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
			bool flag6 = true;
			if (flag6)
			{
				if (Projectile.lavaWet)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
				}
				num = 60 + 30 * Projectile.minionPos;
			}
			bool flag7 = Projectile.ai[0] == -1f || Projectile.ai[0] == -2f;
			bool num2 = Projectile.ai[0] == -1f;
			bool flag8 = Projectile.ai[0] == -2f;
			
			if (flag6)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].pygmy = false;
				}
				if (Main.player[Projectile.owner].pygmy)
				{
					Projectile.timeLeft = Main.rand.Next(2, 10);
				}
			}
			
			if (flag7)
			{
				Projectile.timeLeft = 2;
			}
			if (flag6 || Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392))
			{
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
			}
			else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) < Projectile.position.X + (float)(Projectile.width / 2) - (float)num)
			{
				flag2 = true;
			}
			else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) > Projectile.position.X + (float)(Projectile.width / 2) + (float)num)
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
				if (flag6 || Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392))
				{
					num78 += 40 * Projectile.minionPos;
					if (Projectile.localAI[0] > 0f)
					{
						num78 += 500;
					}
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
					else if (num81 > (float)num78 || (Math.Abs(num80) > 300f && ((!flag6 && Projectile.type != 266 && (Projectile.type < 390 || Projectile.type > 392)) || !(Projectile.localAI[0] > 0f))))
					{
						if (Projectile.type != 324)
						{
							if (num80 > 0f && Projectile.velocity.Y < 0f)
							{
								Projectile.velocity.Y = 0f;
							}
							if (num80 < 0f && Projectile.velocity.Y > 0f)
							{
								Projectile.velocity.Y = 0f;
							}
						}
						Projectile.ai[0] = 1f;
					}
				}
			}
			if (Projectile.ai[0] != 0f && !flag7)
			{
				Main.NewText("returning to player");
				float num83 = 0.2f;
				int num84 = 200;
				if (flag6 || Projectile.type == 816 || Projectile.type == 821 || Projectile.type == 825 || Projectile.type == 854 || Projectile.type == 858 || Projectile.type == 859 || Projectile.type == 860)
				{
					num83 = 0.5f;
					num84 = 100;
				}
				Projectile.tileCollide = false;
				Vector2 vector8 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num85 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector8.X;
				if (flag6 || Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392))
				{
					num85 -= (float)(40 * Main.player[Projectile.owner].direction);
					float num86 = 700f;
					if (flag6)
					{
						num86 += 100f;
					}
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
				}
				float num91 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector8.Y;
				float num92 = (float)Math.Sqrt(num85 * num85 + num91 * num91);
				float num93 = num92;
				float num94 = 10f;
				float num95 = num92;
				
				if (flag6 || Projectile.type == 816 || Projectile.type == 821 || Projectile.type == 825 || Projectile.type == 854 || Projectile.type == 858 || Projectile.type == 859 || Projectile.type == 860 || Projectile.type == 956 || Projectile.type == 958 || Projectile.type == 959 || Projectile.type == 960 || Projectile.type == 994 || Projectile.type == 998 || Projectile.type == 1003 || Projectile.type == 1004)
				{
					num83 = 0.4f;
					num94 = 12f;
					if (flag6)
					{
						num83 = 0.8f;
					}
					if (num94 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
					{
						num94 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
					}
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
				
				if(true)
				{
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
				}
				if (flag6 && Projectile.frame < 12)
				{
					Projectile.frame = Main.rand.Next(12, 18);
					Projectile.frameCounter = 0;
				}
				if (Projectile.type != 313)
				{
					if ((double)Projectile.velocity.X > 0.5)
					{
						Projectile.spriteDirection = -1;
					}
					else if ((double)Projectile.velocity.X < -0.5)
					{
						Projectile.spriteDirection = 1;
					}
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
				if (flag6)
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
				}
				bool flag13 = false;
				Vector2 vector12 = Vector2.Zero;
				bool flag14 = false;
				if (Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392))
				{
					float num141 = 40 * Projectile.minionPos;
					int num142 = 60;
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
						float num143 = Projectile.position.X;
						float num144 = Projectile.position.Y;
						float num145 = 100000f;
						float num146 = num145;
						int num147 = -1;
						NPC ownerMinionAttackTargetNPC2 = Projectile.OwnerMinionAttackTargetNPC;
						if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(this))
						{
							float x = ownerMinionAttackTargetNPC2.Center.X;
							float y = ownerMinionAttackTargetNPC2.Center.Y;
							float num148 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - x) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - y);
							if (num148 < num145)
							{
								if (num147 == -1 && num148 <= num146)
								{
									num146 = num148;
									num143 = x;
									num144 = y;
								}
								if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height))
								{
									num145 = num148;
									num143 = x;
									num144 = y;
									num147 = ownerMinionAttackTargetNPC2.whoAmI;
								}
							}
						}
						if (num147 == -1)
						{
							for (int num149 = 0; num149 < 200; num149++)
							{
								if (!Main.npc[num149].CanBeChasedBy(this))
								{
									continue;
								}
								float num150 = Main.npc[num149].position.X + (float)(Main.npc[num149].width / 2);
								float num151 = Main.npc[num149].position.Y + (float)(Main.npc[num149].height / 2);
								float num152 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num150) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num151);
								if (num152 < num145)
								{
									if (num147 == -1 && num152 <= num146)
									{
										num146 = num152;
										num143 = num150;
										num144 = num151;
									}
									if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num149].position, Main.npc[num149].width, Main.npc[num149].height))
									{
										num145 = num152;
										num143 = num150;
										num144 = num151;
										num147 = num149;
									}
								}
							}
						}
						if (Projectile.type >= 390 && Projectile.type <= 392 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
						{
							Projectile.tileCollide = true;
						}
						if (num147 == -1 && num146 < num145)
						{
							num145 = num146;
						}
						else if (num147 >= 0)
						{
							flag13 = true;
							vector12 = new Vector2(num143, num144) - Projectile.Center;
							if (Projectile.type >= 390 && Projectile.type <= 392)
							{
								if (Main.npc[num147].position.Y > Projectile.position.Y + (float)Projectile.height)
								{
									int num153 = (int)(Projectile.Center.X / 16f);
									int num154 = (int)((Projectile.position.Y + (float)Projectile.height + 1f) / 16f);
									if (Main.tile[num153, num154] != null && Main.tile[num153, num154].HasUnactuatedTile && TileID.Sets.Platforms[Main.tile[num153, num154].TileType])
									{
										Projectile.tileCollide = false;
									}
								}
								Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
								Rectangle value = new Rectangle((int)Main.npc[num147].position.X, (int)Main.npc[num147].position.Y, Main.npc[num147].width, Main.npc[num147].height);
								int num155 = 10;
								value.X -= num155;
								value.Y -= num155;
								value.Width += num155 * 2;
								value.Height += num155 * 2;
								if (rectangle.Intersects(value))
								{
									flag14 = true;
									Vector2 vector13 = Main.npc[num147].Center - Projectile.Center;
									if (Projectile.velocity.Y > 0f && vector13.Y < 0f)
									{
										Projectile.velocity.Y *= 0.5f;
									}
									if (Projectile.velocity.Y < 0f && vector13.Y > 0f)
									{
										Projectile.velocity.Y *= 0.5f;
									}
									if (Projectile.velocity.X > 0f && vector13.X < 0f)
									{
										Projectile.velocity.X *= 0.5f;
									}
									if (Projectile.velocity.X < 0f && vector13.X > 0f)
									{
										Projectile.velocity.X *= 0.5f;
									}
									if (vector13.Length() > 14f)
									{
										vector13.Normalize();
										vector13 *= 14f;
									}
									Projectile.rotation = (Projectile.rotation * 5f + vector13.ToRotation() + (float)Math.PI / 2f) / 6f;
									Projectile.velocity = (Projectile.velocity * 9f + vector13) / 10f;
									for (int num156 = 0; num156 < 1000; num156++)
									{
										if (Projectile.whoAmI != num156 && Projectile.owner == Main.projectile[num156].owner && Main.projectile[num156].type >= 390 && Main.projectile[num156].type <= 392 && (Main.projectile[num156].Center - Projectile.Center).Length() < 15f)
										{
											float num157 = 0.5f;
											if (Projectile.Center.Y > Main.projectile[num156].Center.Y)
											{
												Main.projectile[num156].velocity.Y -= num157;
												Projectile.velocity.Y += num157;
											}
											else
											{
												Main.projectile[num156].velocity.Y += num157;
												Projectile.velocity.Y -= num157;
											}
											if (Projectile.Center.X > Main.projectile[num156].Center.X)
											{
												Projectile.velocity.X += num157;
												Main.projectile[num156].velocity.X -= num157;
											}
											else
											{
												Projectile.velocity.X -= num157;
												Main.projectile[num156].velocity.Y += num157;
											}
										}
									}
								}
							}
						}
						float num158 = 300f;
						if ((double)Projectile.position.Y > Main.worldSurface * 16.0)
						{
							num158 = 150f;
						}
						if (num145 < num158 + num141 && num147 == -1)
						{
							float num159 = num143 - (Projectile.position.X + (float)(Projectile.width / 2));
							if (num159 < -5f)
							{
								flag2 = true;
								flag3 = false;
							}
							else if (num159 > 5f)
							{
								flag3 = true;
								flag2 = false;
							}
						}
						bool flag15 = false;
						if (num147 >= 0 && num145 < 800f + num141)
						{
							Projectile.friendly = true;
							Projectile.localAI[0] = num142;
							float num160 = num143 - (Projectile.position.X + (float)(Projectile.width / 2));
							if (num160 < -10f)
							{
								flag2 = true;
								flag3 = false;
							}
							else if (num160 > 10f)
							{
								flag3 = true;
								flag2 = false;
							}
							if (num144 < Projectile.Center.Y - 100f && num160 > -50f && num160 < 50f && Projectile.velocity.Y == 0f)
							{
								float num161 = Math.Abs(num144 - Projectile.Center.Y);
								if (num161 < 120f)
								{
									Projectile.velocity.Y = -10f;
								}
								else if (num161 < 210f)
								{
									Projectile.velocity.Y = -13f;
								}
								else if (num161 < 270f)
								{
									Projectile.velocity.Y = -15f;
								}
								else if (num161 < 310f)
								{
									Projectile.velocity.Y = -17f;
								}
								else if (num161 < 380f)
								{
									Projectile.velocity.Y = -18f;
								}
							}
							if (flag15)
							{
								Projectile.friendly = false;
								if (Projectile.velocity.X < 0f)
								{
									flag2 = true;
								}
								else if (Projectile.velocity.X > 0f)
								{
									flag3 = true;
								}
							}
						}
						else
						{
							Projectile.friendly = false;
						}
					}
				}
				if (Projectile.ai[1] != 0f)
				{
					flag2 = false;
					flag3 = false;
				}
				else if (flag6 && Projectile.localAI[0] == 0f)
				{
					Projectile.direction = Main.player[Projectile.owner].direction;
				}
				if (Projectile.type != 313 && !flag14)
				{
					Projectile.rotation = 0f;
				}
				if (Projectile.type < 390 || Projectile.type > 392)
				{
					Projectile.tileCollide = true;
				}
				float num164 = 0.08f;
				float num165 = 6.5f;
				if (flag6 || Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392) || Projectile.type == 816 || Projectile.type == 821 || Projectile.type == 825 || Projectile.type == 859 || Projectile.type == 860 || Projectile.type == 881 || Projectile.type == 884 || Projectile.type == 890 || Projectile.type == 891 || Projectile.type == 897 || Projectile.type == 899 || Projectile.type == 900 || Projectile.type == 934 || Projectile.type == 956 || Projectile.type == 958 || Projectile.type == 959 || Projectile.type == 960 || Projectile.type == 994 || Projectile.type == 998 || Projectile.type == 1003 || Projectile.type == 1004)
				{
					num165 = 6f;
					num164 = 0.2f;
					if (num165 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
					{
						num165 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
						num164 = 0.3f;
					}
					if (flag6)
					{
						num164 *= 2f;
					}
				}
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
				if (Projectile.velocity.Y == 0f || Projectile.type == 200)
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
							if(true)
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
							if (Projectile.type == 127)
							{
								Projectile.ai[0] = 1f;
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
				if (Projectile.type != 313)
				{
					if (Projectile.direction == -1)
					{
						Projectile.spriteDirection = 1;
					}
					if (Projectile.direction == 1)
					{
						Projectile.spriteDirection = -1;
					}
				}
				bool flag16 = Projectile.position.X - Projectile.oldPosition.X == 0f;
				if (Projectile.type == 956)
				{
					if (Projectile.alpha > 0)
					{
						int num170 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1.2f);
						Main.dust[num170].velocity.X += Main.rand.NextFloat() - 0.5f;
						Main.dust[num170].velocity.Y += (Main.rand.NextFloat() + 0.5f) * -1f;
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num170].noGravity = true;
						}
						Projectile.alpha -= 5;
						if (Projectile.alpha < 0)
						{
							Projectile.alpha = 0;
						}
					}
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 10;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num171 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num171;
						if (Projectile.frameCounter > 7)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 1 || Projectile.frame > 9)
						{
							Projectile.frame = 1;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 958)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.localAI[0] = 0f;
						Projectile.frame = 4;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.localAI[0] += 1f;
						if (Projectile.localAI[0] > 200f)
						{
							Projectile.frame = 1 + (int)(Projectile.localAI[0] - 200f) / 6;
							if (Projectile.localAI[0] >= 218f)
							{
								Projectile.frame = 0;
								Projectile.localAI[0] = Main.rand.Next(100);
							}
						}
						else
						{
							Projectile.frame = 0;
						}
					}
					else
					{
						Projectile.localAI[0] = 0f;
						float num172 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num172;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5 || Projectile.frame > 12)
						{
							Projectile.frame = 5;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 959)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = ((Projectile.velocity.Y > 0f) ? 10 : 9);
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num173 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num173;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 1 || Projectile.frame > 8)
						{
							Projectile.frame = 1;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 998)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num174 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num174;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 0 || Projectile.frame > 5)
						{
							Projectile.frame = 0;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 1003)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num175 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num175;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 2 || Projectile.frame > 11)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 1004)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num176 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num176;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 2 || Projectile.frame > 9)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 994)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 4;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 5)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 3)
						{
							Projectile.frame = 0;
						}
					}
					else
					{
						float num177 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num177;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5 || Projectile.frame > 12)
						{
							Projectile.frame = 5;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 960)
				{
					_ = Main.player[Projectile.owner];
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.localAI[0] = 0f;
						Projectile.localAI[1] = 0f;
						Projectile.frameCounter = 0;
						Projectile.frame = 4;
					}
					else if (flag16)
					{
						if (!flag7)
						{
							Projectile.spriteDirection = 1;
							if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
							{
								Projectile.spriteDirection = -1;
							}
						}
						if (Projectile.frame >= 5 && Projectile.frame < 12)
						{
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 3)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 12)
							{
								Projectile.frame = 0;
							}
						}
						else if (Chester_IsAnyPlayerTrackingThisProjectile())
						{
							if (Projectile.localAI[0] == 0f)
							{
								if (Projectile.localAI[1] == 0f)
								{
									Projectile.localAI[1] = 1f;
									Projectile.frameCounter = 0;
								}
								Projectile.frame = 13;
								Projectile.frameCounter++;
								if (Projectile.frameCounter > 6)
								{
									Projectile.localAI[0] = 1f;
									Projectile.frame = 14;
									Projectile.frameCounter = 0;
								}
							}
							else
							{
								Projectile.frameCounter++;
								if (Projectile.frameCounter > 6)
								{
									Projectile.frame++;
									if (Projectile.frame > 18)
									{
										Projectile.frame = 14;
									}
									Projectile.frameCounter = 0;
								}
							}
						}
						else
						{
							Projectile.localAI[0] = 0f;
							if (Projectile.localAI[1] == 1f)
							{
								Projectile.localAI[1] = 0f;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 12 && Projectile.frame <= 19)
							{
								Projectile.frame = 19;
								Projectile.frameCounter++;
								if (Projectile.frameCounter > 6)
								{
									Projectile.frame = 0;
									Projectile.frameCounter = 0;
								}
							}
							else
							{
								Projectile.frameCounter++;
								if (Projectile.frameCounter >= 24)
								{
									Projectile.frameCounter = 0;
								}
								Projectile.frame = Projectile.frameCounter / 6;
							}
						}
					}
					else
					{
						Projectile.localAI[0] = 0f;
						Projectile.localAI[1] = 0f;
						float val = Projectile.velocity.Length();
						Projectile.frameCounter += (int)Math.Max(2f, val);
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5 || Projectile.frame > 12)
						{
							Projectile.frame = 5;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 816)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 4;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						if (++Projectile.frameCounter > 5)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 0 || Projectile.frame > 3)
						{
							Projectile.frame = 0;
						}
					}
					else
					{
						int num178 = 5;
						float num179 = Projectile.velocity.Length();
						if (num179 > 4f)
						{
							num178 = 3;
						}
						else if (num179 > 2f)
						{
							num178 = 4;
						}
						if (++Projectile.frameCounter > num178)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 4 || Projectile.frame > 10)
						{
							Projectile.frame = 4;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				if (Projectile.type == 860)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.localAI[0] = 0f;
						if (Projectile.frame >= 5)
						{
							Projectile.frame = 5;
							Projectile.frameCounter = 0;
						}
						else if (++Projectile.frameCounter > 5)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
					}
					else if (Math.Abs(Projectile.velocity.X) < 1f)
					{
						if (Projectile.localAI[0] > 800f)
						{
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 3)
							{
								Projectile.frameCounter = 0;
								Projectile.frame++;
								if (Projectile.frame > 3)
								{
									Projectile.frame = 3;
								}
							}
							Projectile.localAI[0] += 1f;
							if (Projectile.localAI[0] > 850f)
							{
								Projectile.localAI[0] = 0f;
							}
							if (Projectile.frame == 3 && Projectile.localAI[0] == 820f)
							{
								for (int num180 = 0; num180 < 3 + Main.rand.Next(3); num180++)
								{
									int num181 = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X, Projectile.Center.Y - 10f), Vector2.Zero, 1218);
									Main.gore[num181].velocity = new Vector2((float)Main.rand.Next(1, 10) * 0.3f * (float)(-Projectile.spriteDirection), 0f - (2f + (float)Main.rand.Next(4) * 0.3f));
								}
							}
						}
						else if (Projectile.frame == 0)
						{
							Projectile.localAI[0] += 1f;
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else
						{
							Projectile.localAI[0] = 0f;
							if (Projectile.frame > 5)
							{
								Projectile.frame = 5;
								Projectile.frameCounter = 0;
							}
							if (++Projectile.frameCounter > 4)
							{
								Projectile.frame--;
								Projectile.frameCounter = 0;
							}
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				if (Projectile.type == 859)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 4;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						if (Projectile.frame == 6)
						{
							if (++Projectile.frameCounter > 5)
							{
								Projectile.frame = 0;
								Projectile.frameCounter = 0;
							}
						}
						else if (Projectile.frame > 3)
						{
							Projectile.frame = 6;
							Projectile.frameCounter = 0;
						}
						else
						{
							if (++Projectile.frameCounter > 5)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame < 0 || Projectile.frame > 3)
							{
								Projectile.frame = 0;
							}
						}
					}
					else
					{
						float num182 = Projectile.velocity.Length();
						int num183 = 8;
						if (num182 < 3f)
						{
							num183 = 4;
						}
						if (num182 < 1f)
						{
							num183 = 2;
						}
						Projectile.frameCounter += (int)num182;
						if (Projectile.frameCounter > num183)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5 || Projectile.frame > 17)
						{
							Projectile.frame = 5;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 858)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.frame = 0;
					}
					else
					{
						float num184 = Projectile.velocity.Length();
						Projectile.frameCounter += (int)num184;
						if (Projectile.frameCounter > 3)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 2 || Projectile.frame > 9)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 900)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.3f);
						if (Projectile.frame < 2)
						{
							Projectile.frame = 2;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 9)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 899)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.3f);
						if (Projectile.frame < 2)
						{
							Projectile.frame = 2;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 9)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 897)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.3f);
						if (Projectile.frame < 2)
						{
							Projectile.frame = 2;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 7)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 891)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = Main.player[Projectile.owner].direction;
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.3f);
						if (Projectile.frame < 2)
						{
							Projectile.frame = 2;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 8)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 890)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.3f);
						if (Projectile.frame < 2)
						{
							Projectile.frame = 2;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 7)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 884)
				{
					Projectile.spriteDirection = Projectile.direction;
					if (Projectile.velocity.Y != 0f)
					{
						if (Projectile.velocity.Y < 0f)
						{
							Projectile.frame = 9;
						}
						else
						{
							Projectile.frame = 1;
						}
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = 1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.5f);
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 8)
						{
							Projectile.frame = 2;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 881 || Projectile.type == 934)
				{
					Projectile.spriteDirection = 1;
					if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
					{
						Projectile.spriteDirection = -1;
					}
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 2)
						{
							Projectile.frame++;
							if (Projectile.frame >= 2)
							{
								Projectile.frame = 2;
							}
							Projectile.frameCounter = 0;
						}
					}
					else if (Projectile.velocity.Y < 0f)
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 2)
						{
							Projectile.frame++;
							if (Projectile.frame >= 5)
							{
								Projectile.frame = 0;
							}
							Projectile.frameCounter = 0;
						}
					}
					else if (Projectile.frame == 0)
					{
						Projectile.frame = 0;
					}
					else if (++Projectile.frameCounter > 3)
					{
						Projectile.frame++;
						if (Projectile.frame >= 6)
						{
							Projectile.frame = 0;
						}
						Projectile.frameCounter = 0;
					}
					if (Projectile.wet && Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height < Projectile.position.Y + (float)Projectile.height && Projectile.localAI[0] == 0f)
					{
						if (Projectile.velocity.Y > -4f)
						{
							Projectile.velocity.Y -= 0.2f;
						}
						if (Projectile.velocity.Y > 0f)
						{
							Projectile.velocity.Y *= 0.95f;
						}
					}
					else
					{
						Projectile.velocity.Y += 0.4f;
					}
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 875)
				{
					if (Projectile.velocity.Y != 0f)
					{
						if (Projectile.velocity.Y < 0f)
						{
							Projectile.frame = 3;
						}
						else
						{
							Projectile.frame = 6;
						}
						Projectile.frameCounter = 0;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else
					{
						Projectile.frameCounter += 1 + (int)Math.Abs(Projectile.velocity.X * 0.75f);
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 6)
						{
							Projectile.frame = 0;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 854)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.frame = 7;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						if (++Projectile.frameCounter > 5)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 0 || Projectile.frame > 3)
						{
							Projectile.frame = 0;
						}
					}
					else
					{
						int num185 = 3;
						float num186 = Projectile.velocity.Length();
						if (num186 > 4f)
						{
							num185 = 1;
						}
						else if (num186 > 2f)
						{
							num185 = 2;
						}
						if (++Projectile.frameCounter > num185)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 4 || Projectile.frame > 12)
						{
							Projectile.frame = 4;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 825)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.localAI[0] = 0f;
						Projectile.frame = 12;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						if (Projectile.frame >= 1 && Projectile.frame <= 2)
						{
							Projectile.localAI[0] = 0f;
							if (++Projectile.frameCounter > 5)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 2)
							{
								Projectile.frame = 0;
							}
						}
						else if (Projectile.frame >= 3 && Projectile.frame <= 11)
						{
							Projectile.localAI[0] = 0f;
							if (++Projectile.frameCounter > 5)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 11)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							if (Projectile.frame == 13)
							{
								if (++Projectile.frameCounter > 8)
								{
									Projectile.frame++;
									Projectile.frameCounter = 0;
								}
								if (Projectile.frame == 14)
								{
									Projectile.frame = 0;
								}
							}
							if (Projectile.frame != 0)
							{
								Projectile.frame = 13;
							}
							else
							{
								Projectile.frame = 0;
							}
							if (Projectile.frame == 0)
							{
								Projectile.localAI[0] += 1f;
								if (Projectile.localAI[0] > 300f && Main.rand.Next(50) == 0)
								{
									switch (Main.rand.Next(2))
									{
										case 0:
											Projectile.frame = 1;
											break;
										case 1:
											Projectile.frame = 3;
											break;
									}
								}
							}
						}
					}
					else
					{
						Projectile.localAI[0] = 0f;
						int num187 = 3;
						float num188 = Projectile.velocity.Length();
						if (num188 > 4f)
						{
							num187 = 2;
						}
						else if (num188 > 2f)
						{
							num187 = 1;
						}
						if (++Projectile.frameCounter > num187)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 13)
						{
							Projectile.frame = 13;
						}
						if (Projectile.frame > 19)
						{
							Projectile.frame = 14;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 821)
				{
					if (Projectile.velocity.Y != 0f)
					{
						Projectile.localAI[0] = 0f;
						Projectile.frame = 12;
					}
					else if (flag16)
					{
						Projectile.spriteDirection = -1;
						if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
						{
							Projectile.spriteDirection = 1;
						}
						Projectile.localAI[0] += 1f;
						if (Projectile.localAI[0] > 400f)
						{
							int num189 = 7;
							if (Projectile.frame == 9)
							{
								num189 = 25;
							}
							if (++Projectile.frameCounter > num189)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame < 5)
							{
								Projectile.frame = 5;
							}
							if (Projectile.frame > 11)
							{
								Projectile.localAI[0] = 0f;
								Projectile.frame = 0;
							}
						}
						else
						{
							if (++Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame < 0 || Projectile.frame > 4)
							{
								Projectile.frame = 0;
							}
						}
					}
					else
					{
						Projectile.localAI[0] = 0f;
						int num190 = 4;
						float num191 = Projectile.velocity.Length();
						if (num191 > 3f)
						{
							num190 = 3;
						}
						if (num191 > 5f)
						{
							num190 = 2;
						}
						if (++Projectile.frameCounter > num190)
						{
							Projectile.frame++;
							if (num190 == 0)
							{
								Projectile.frame++;
							}
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 13 || Projectile.frame > 18)
						{
							Projectile.frame = 13;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (flag6)
				{
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
				else if (Projectile.type == 268)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if (Projectile.frame > 5)
						{
							Projectile.frameCounter = 0;
						}
						if (flag16)
						{
							int num192 = 3;
							Projectile.frameCounter++;
							if (Projectile.frameCounter < num192)
							{
								Projectile.frame = 0;
							}
							else if (Projectile.frameCounter < num192 * 2)
							{
								Projectile.frame = 1;
							}
							else if (Projectile.frameCounter < num192 * 3)
							{
								Projectile.frame = 2;
							}
							else if (Projectile.frameCounter < num192 * 4)
							{
								Projectile.frame = 3;
							}
							else
							{
								Projectile.frameCounter = num192 * 4;
							}
						}
						else
						{
							Projectile.velocity.X *= 0.8f;
							Projectile.frameCounter++;
							int num193 = 3;
							if (Projectile.frameCounter < num193)
							{
								Projectile.frame = 0;
							}
							else if (Projectile.frameCounter < num193 * 2)
							{
								Projectile.frame = 1;
							}
							else if (Projectile.frameCounter < num193 * 3)
							{
								Projectile.frame = 2;
							}
							else if (Projectile.frameCounter < num193 * 4)
							{
								Projectile.frame = 3;
							}
							else if (flag2 || flag3)
							{
								Projectile.velocity.X *= 2f;
								Projectile.frame = 4;
								Projectile.velocity.Y = -6.1f;
								Projectile.frameCounter = 0;
								for (int num194 = 0; num194 < 4; num194++)
								{
									int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + (float)Projectile.height - 2f), Projectile.width, 4, 5);
									Main.dust[num195].velocity += Projectile.velocity;
									Main.dust[num195].velocity *= 0.4f;
								}
							}
							else
							{
								Projectile.frameCounter = num193 * 4;
							}
						}
					}
					else if (Projectile.velocity.Y < 0f)
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 5;
					}
					else
					{
						Projectile.frame = 4;
						Projectile.frameCounter = 3;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 269)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							int num196 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + (float)Projectile.height - 2f), Projectile.width, 6, 76);
							Main.dust[num196].noGravity = true;
							Main.dust[num196].velocity *= 0.3f;
							Main.dust[num196].noLight = true;
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 3)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 2;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 313)
				{
					int i3 = (int)(Projectile.Center.X / 16f);
					int num197 = (int)(Projectile.Center.Y / 16f);
					int num198 = 0;
					Tile tileSafely2 = Framing.GetTileSafely(i3, num197);
					Tile tileSafely3 = Framing.GetTileSafely(i3, num197 - 1);
					Tile tileSafely4 = Framing.GetTileSafely(i3, num197 + 1);
					if (tileSafely2.WallType > 0)
					{
						num198++;
					}
					if (tileSafely3.WallType > 0)
					{
						num198++;
					}
					if (tileSafely4.WallType > 0)
					{
						num198++;
					}
					if (num198 > 1)
					{
						Projectile.position.Y += Projectile.height;
						Projectile.height = 34;
						Projectile.position.Y -= Projectile.height;
						Projectile.position.X += Projectile.width / 2;
						Projectile.width = 34;
						Projectile.position.X -= Projectile.width / 2;
						Vector2 vector14 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
						float num199 = Main.player[Projectile.owner].Center.X - vector14.X;
						float num200 = Main.player[Projectile.owner].Center.Y - vector14.Y;
						float num201 = (float)Math.Sqrt(num199 * num199 + num200 * num200);
						float num202 = 4f / num201;
						num199 *= num202;
						num200 *= num202;
						if (num201 < 120f)
						{
							Projectile.velocity.X *= 0.9f;
							Projectile.velocity.Y *= 0.9f;
							if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) < 0.1)
							{
								Projectile.velocity *= 0f;
							}
						}
						else
						{
							Projectile.velocity.X = (Projectile.velocity.X * 9f + num199) / 10f;
							Projectile.velocity.Y = (Projectile.velocity.Y * 9f + num200) / 10f;
						}
						if (num201 >= 120f)
						{
							Projectile.spriteDirection = Projectile.direction;
							Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y * (float)(-Projectile.direction), Projectile.velocity.X * (float)(-Projectile.direction));
						}
						Projectile.frameCounter += (int)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y));
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 10)
						{
							Projectile.frame = 5;
						}
						if (Projectile.frame < 5)
						{
							Projectile.frame = 10;
						}
					}
					else
					{
						Projectile.rotation = 0f;
						if (Projectile.direction == -1)
						{
							Projectile.spriteDirection = 1;
						}
						if (Projectile.direction == 1)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.position.Y += Projectile.height;
						Projectile.height = 30;
						Projectile.position.Y -= Projectile.height;
						Projectile.position.X += Projectile.width / 2;
						Projectile.width = 30;
						Projectile.position.X -= Projectile.width / 2;
						if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
						{
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
								if (Projectile.frame > 3)
								{
									Projectile.frame = 0;
								}
							}
							else
							{
								Projectile.frame = 0;
								Projectile.frameCounter = 0;
							}
						}
						else
						{
							Projectile.frameCounter = 0;
							Projectile.frame = 4;
						}
						Projectile.velocity.Y += 0.4f;
						if (Projectile.velocity.Y > 10f)
						{
							Projectile.velocity.Y = 10f;
						}
					}
				}
				else if (Projectile.type >= 390 && Projectile.type <= 392)
				{
					int i4 = (int)(Projectile.Center.X / 16f);
					int num203 = (int)(Projectile.Center.Y / 16f);
					int num204 = 0;
					Tile tileSafely5 = Framing.GetTileSafely(i4, num203);
					Tile tileSafely6 = Framing.GetTileSafely(i4, num203 - 1);
					Tile tileSafely7 = Framing.GetTileSafely(i4, num203 + 1);
					if (tileSafely5.WallType > 0)
					{
						num204++;
					}
					if (tileSafely6.WallType > 0)
					{
						num204++;
					}
					if (tileSafely7.WallType > 0)
					{
						num204++;
					}
					if (num204 > 1)
					{
						Projectile.position.Y += Projectile.height;
						Projectile.height = 34;
						Projectile.position.Y -= Projectile.height;
						Projectile.position.X += Projectile.width / 2;
						Projectile.width = 34;
						Projectile.position.X -= Projectile.width / 2;
						float num205 = 9f;
						float num206 = 40 * (Projectile.minionPos + 1);
						Vector2 v11 = Main.player[Projectile.owner].Center - Projectile.Center;
						if (flag13)
						{
							v11 = vector12;
							num206 = 10f;
						}
						else if (!Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
						{
							Projectile.ai[0] = 1f;
						}
						if (v11.Length() < num206)
						{
							Projectile.velocity *= 0.9f;
							if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) < 0.1)
							{
								Projectile.velocity *= 0f;
							}
						}
						else if (v11.Length() < 800f || !flag13)
						{
							Projectile.velocity = (Projectile.velocity * 9f + v11.SafeNormalize(Vector2.Zero) * num205) / 10f;
						}
						if (v11.Length() >= num206)
						{
							Projectile.spriteDirection = Projectile.direction;
							Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
						}
						else
						{
							Projectile.rotation = v11.ToRotation() + (float)Math.PI / 2f;
						}
						Projectile.frameCounter += (int)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y));
						if (Projectile.frameCounter > 5)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 7)
						{
							Projectile.frame = 4;
						}
						if (Projectile.frame < 4)
						{
							Projectile.frame = 7;
						}
					}
					else
					{
						if (!flag14)
						{
							Projectile.rotation = 0f;
						}
						if (Projectile.direction == -1)
						{
							Projectile.spriteDirection = 1;
						}
						if (Projectile.direction == 1)
						{
							Projectile.spriteDirection = -1;
						}
						Projectile.position.Y += Projectile.height;
						Projectile.height = 30;
						Projectile.position.Y -= Projectile.height;
						Projectile.position.X += Projectile.width / 2;
						Projectile.width = 30;
						Projectile.position.X -= Projectile.width / 2;
						if (!flag13 && !Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
						{
							Projectile.ai[0] = 1f;
						}
						if (!flag14 && Projectile.frame >= 4 && Projectile.frame <= 7)
						{
							Vector2 vector15 = Main.player[Projectile.owner].Center - Projectile.Center;
							if (flag13)
							{
								vector15 = vector12;
							}
							float num207 = 0f - vector15.Y;
							if (!(vector15.Y > 0f))
							{
								if (num207 < 120f)
								{
									Projectile.velocity.Y = -10f;
								}
								else if (num207 < 210f)
								{
									Projectile.velocity.Y = -13f;
								}
								else if (num207 < 270f)
								{
									Projectile.velocity.Y = -15f;
								}
								else if (num207 < 310f)
								{
									Projectile.velocity.Y = -17f;
								}
								else if (num207 < 380f)
								{
									Projectile.velocity.Y = -18f;
								}
							}
						}
						if (flag14)
						{
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 3)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 8)
							{
								Projectile.frame = 4;
							}
							if (Projectile.frame <= 3)
							{
								Projectile.frame = 7;
							}
						}
						else if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
						{
							if (flag16)
							{
								Projectile.frame = 0;
								Projectile.frameCounter = 0;
							}
							else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
							{
								Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
								Projectile.frameCounter++;
								if (Projectile.frameCounter > 5)
								{
									Projectile.frame++;
									Projectile.frameCounter = 0;
								}
								if (Projectile.frame > 2)
								{
									Projectile.frame = 0;
								}
							}
							else
							{
								Projectile.frame = 0;
								Projectile.frameCounter = 0;
							}
						}
						else
						{
							Projectile.frameCounter = 0;
							Projectile.frame = 3;
						}
						Projectile.velocity.Y += 0.4f;
						if (Projectile.velocity.Y > 10f)
						{
							Projectile.velocity.Y = 10f;
						}
					}
				}
				else if (Projectile.type == 314)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
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
							if (Projectile.frame > 6)
							{
								Projectile.frame = 1;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 7;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 319)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 8)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 5)
							{
								Projectile.frame = 2;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 236)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							if (Projectile.frame < 2)
							{
								Projectile.frame = 2;
							}
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 8)
							{
								Projectile.frame = 2;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 499)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							if (Projectile.frame < 2)
							{
								Projectile.frame = 2;
							}
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 8)
							{
								Projectile.frame = 2;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 765)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							if (Projectile.frame < 1)
							{
								Projectile.frame = 1;
							}
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 6)
							{
								Projectile.frame = 1;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 266)
				{
					if (Projectile.velocity.Y >= 0f && (double)Projectile.velocity.Y <= 0.8)
					{
						if (flag16)
						{
							Projectile.frameCounter++;
						}
						else
						{
							Projectile.frameCounter += 3;
						}
					}
					else
					{
						Projectile.frameCounter += 5;
					}
					if (Projectile.frameCounter >= 20)
					{
						Projectile.frameCounter -= 20;
						Projectile.frame++;
					}
					if (Projectile.frame > 1)
					{
						Projectile.frame = 0;
					}
					if (Projectile.wet && Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height < Projectile.position.Y + (float)Projectile.height && Projectile.localAI[0] == 0f)
					{
						if (Projectile.velocity.Y > -4f)
						{
							Projectile.velocity.Y -= 0.2f;
						}
						if (Projectile.velocity.Y > 0f)
						{
							Projectile.velocity.Y *= 0.95f;
						}
					}
					else
					{
						Projectile.velocity.Y += 0.4f;
					}
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 334)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if (flag16)
						{
							if (Projectile.frame > 0)
							{
								Projectile.frameCounter += 2;
								if (Projectile.frameCounter > 6)
								{
									Projectile.frame++;
									Projectile.frameCounter = 0;
								}
								if (Projectile.frame >= 7)
								{
									Projectile.frame = 0;
								}
							}
							else
							{
								Projectile.frame = 0;
								Projectile.frameCounter = 0;
							}
						}
						else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
						{
							Projectile.frameCounter += (int)Math.Abs((double)Projectile.velocity.X * 0.75);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 7 || Projectile.frame < 1)
							{
								Projectile.frame = 1;
							}
						}
						else if (Projectile.frame > 0)
						{
							Projectile.frameCounter += 2;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame >= 7)
							{
								Projectile.frame = 0;
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
						Projectile.frame = 2;
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
				}
				else if (Projectile.type == 353)
				{
					if (Projectile.velocity.Y == 0f)
					{
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
							if (Projectile.frame > 9)
							{
								Projectile.frame = 2;
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
						Projectile.frame = 1;
					}
					else if (Projectile.velocity.Y > 0f)
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 111)
				{
					if (Projectile.velocity.Y == 0f)
					{
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
							if (Projectile.frame >= 7)
							{
								Projectile.frame = 0;
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
						Projectile.frame = 6;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 112)
				{
					if (Projectile.velocity.Y == 0f)
					{
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
							if (Projectile.frame >= 3)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else if (Projectile.velocity.Y != 0f)
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 127)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.1 || (double)Projectile.velocity.X > 0.1)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 5)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 200)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.1 || (double)Projectile.velocity.X > 0.1)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 5)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.rotation = Projectile.velocity.X * 0.1f;
						Projectile.frameCounter++;
						if (Projectile.velocity.Y < 0f)
						{
							Projectile.frameCounter += 2;
						}
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 9)
						{
							Projectile.frame = 6;
						}
						if (Projectile.frame < 6)
						{
							Projectile.frame = 6;
						}
					}
					Projectile.velocity.Y += 0.1f;
					if (Projectile.velocity.Y > 4f)
					{
						Projectile.velocity.Y = 4f;
					}
				}
				else if (Projectile.type == 208)
				{
					if (Projectile.velocity.Y == 0f && flag16)
					{
						if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) < Projectile.position.X + (float)(Projectile.width / 2))
						{
							Projectile.direction = -1;
						}
						else if (Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) > Projectile.position.X + (float)(Projectile.width / 2))
						{
							Projectile.direction = 1;
						}
						Projectile.rotation = 0f;
						Projectile.frame = 0;
					}
					else
					{
						Projectile.rotation = Projectile.velocity.X * 0.075f;
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 4)
						{
							Projectile.frame = 1;
						}
						if (Projectile.frame < 1)
						{
							Projectile.frame = 1;
						}
					}
					Projectile.velocity.Y += 0.1f;
					if (Projectile.velocity.Y > 4f)
					{
						Projectile.velocity.Y = 4f;
					}
				}
				else if (Projectile.type == 209)
				{
					if (Projectile.alpha > 0)
					{
						Projectile.alpha -= 5;
						if (Projectile.alpha < 0)
						{
							Projectile.alpha = 0;
						}
					}
					if (Projectile.velocity.Y == 0f)
					{
						if (flag16)
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
						else if ((double)Projectile.velocity.X < -0.1 || (double)Projectile.velocity.X > 0.1)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 11)
							{
								Projectile.frame = 2;
							}
							if (Projectile.frame < 2)
							{
								Projectile.frame = 2;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frame = 1;
						Projectile.frameCounter = 0;
						Projectile.rotation = 0f;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 324)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if ((double)Projectile.velocity.X < -0.1 || (double)Projectile.velocity.X > 0.1)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 5)
							{
								Projectile.frame = 2;
							}
							if (Projectile.frame < 2)
							{
								Projectile.frame = 2;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 1;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 14f)
					{
						Projectile.velocity.Y = 14f;
					}
				}
				else if (Projectile.type == 210)
				{
					if (Projectile.velocity.Y == 0f)
					{
						if ((double)Projectile.velocity.X < -0.1 || (double)Projectile.velocity.X > 0.1)
						{
							Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
							Projectile.frameCounter++;
							if (Projectile.frameCounter > 6)
							{
								Projectile.frame++;
								Projectile.frameCounter = 0;
							}
							if (Projectile.frame > 6)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else
					{
						Projectile.rotation = Projectile.velocity.X * 0.05f;
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame > 11)
						{
							Projectile.frame = 7;
						}
						if (Projectile.frame < 7)
						{
							Projectile.frame = 7;
						}
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				else if (Projectile.type == 398)
				{
					if (Projectile.velocity.Y == 0f)
					{
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
							if (Projectile.frame >= 5)
							{
								Projectile.frame = 0;
							}
						}
						else
						{
							Projectile.frame = 0;
							Projectile.frameCounter = 0;
						}
					}
					else if (Projectile.velocity.Y != 0f)
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 5;
					}
					Projectile.velocity.Y += 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
			}
			if (Projectile.type == 891)
			{
				_ = Main.player[Projectile.owner];
				DelegateMethods.v3_1 = new Vector3(1f, 0.61f, 0.16f) * 1.5f;
				Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
				Utils.PlotTileLine(Projectile.Left, Projectile.Right, 20f, DelegateMethods.CastLightOpen);
			}
		}

		private bool Chester_IsAnyPlayerTrackingThisProjectile()
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && player.piggyBankProjTracker.IsTracking(Projectile))
				{
					return true;
				}
			}
			return false;
		}
	}
}
