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
			if (Projectile.type == 324)
			{
				num = 120;
			}
			if (Projectile.type == 112)
			{
				num = 100;
			}
			if (Projectile.type == 127)
			{
				num = 50;
			}
			switch (Projectile.type)
			{
				case 816:
				case 821:
				case 825:
				case 854:
				case 858:
				case 859:
				case 860:
				case 885:
				case 889:
				case 891:
				case 897:
				case 899:
				case 900:
				case 934:
					num = 95;
					break;
				case 884:
				case 890:
					num = 80;
					break;
				case 881:
					num = 95;
					if (Main.player[Projectile.owner].ownedProjectileCounts[881] > 0)
					{
						num = 120;
					}
					break;
			}
			if (Projectile.type == 960)
			{
				Main.CurrentFrameFlags.HadAnActiveInteractibleProjectile = true;
				flag = true;
			}
			if (flag6)
			{
				if (Projectile.lavaWet)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
				}
				num = 60 + 30 * Projectile.minionPos;
			}
			else if (Projectile.type == 266)
			{
				num = 60 + 30 * Projectile.minionPos;
			}
			bool flag7 = Projectile.ai[0] == -1f || Projectile.ai[0] == -2f;
			bool num2 = Projectile.ai[0] == -1f;
			bool flag8 = Projectile.ai[0] == -2f;
			if (Projectile.type == 111)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].bunny = false;
				}
				if (Main.player[Projectile.owner].bunny)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 112)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].penguin = false;
				}
				if (Main.player[Projectile.owner].penguin)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 334)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].puppy = false;
				}
				if (Main.player[Projectile.owner].puppy)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 353)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].grinch = false;
				}
				if (Main.player[Projectile.owner].grinch)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 127)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].turtle = false;
				}
				if (Main.player[Projectile.owner].turtle)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 175)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].eater = false;
				}
				if (Main.player[Projectile.owner].eater)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 197)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].skeletron = false;
				}
				if (Main.player[Projectile.owner].skeletron)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 198)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].hornet = false;
				}
				if (Main.player[Projectile.owner].hornet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 199)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].tiki = false;
				}
				if (Main.player[Projectile.owner].tiki)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 200)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].lizard = false;
				}
				if (Main.player[Projectile.owner].lizard)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 208)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].parrot = false;
				}
				if (Main.player[Projectile.owner].parrot)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 209)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].truffle = false;
				}
				if (Main.player[Projectile.owner].truffle)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 210)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].sapling = false;
				}
				if (Main.player[Projectile.owner].sapling)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 324)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].cSapling = false;
				}
				if (Main.player[Projectile.owner].cSapling)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 313)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].spider = false;
				}
				if (Main.player[Projectile.owner].spider)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 314)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].squashling = false;
				}
				if (Main.player[Projectile.owner].squashling)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 211)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].wisp = false;
				}
				if (Main.player[Projectile.owner].wisp)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 236)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].dino = false;
				}
				if (Main.player[Projectile.owner].dino)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 499)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].babyFaceMonster = false;
				}
				if (Main.player[Projectile.owner].babyFaceMonster)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 765)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagSugarGlider = false;
				}
				if (Main.player[Projectile.owner].petFlagSugarGlider)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 266)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].slime = false;
				}
				if (Main.player[Projectile.owner].slime)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 268)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].eyeSpring = false;
				}
				if (Main.player[Projectile.owner].eyeSpring)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 269)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].snowman = false;
				}
				if (Main.player[Projectile.owner].snowman)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 319)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].blackCat = false;
				}
				if (Main.player[Projectile.owner].blackCat)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 380)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].zephyrfish = false;
				}
				if (Main.player[Projectile.owner].zephyrfish)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 774)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBabyShark = false;
				}
				if (Main.player[Projectile.owner].petFlagBabyShark)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 815)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagLilHarpy = false;
				}
				if (Main.player[Projectile.owner].petFlagLilHarpy)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 816)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagFennecFox = false;
				}
				if (Main.player[Projectile.owner].petFlagFennecFox)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 817)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagGlitteryButterfly = false;
				}
				if (Main.player[Projectile.owner].petFlagGlitteryButterfly)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 821)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBabyImp = false;
				}
				if (Main.player[Projectile.owner].petFlagBabyImp)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 825)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBabyRedPanda = false;
				}
				if (Main.player[Projectile.owner].petFlagBabyRedPanda)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 854)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagPlantero = false;
				}
				if (Main.player[Projectile.owner].petFlagPlantero)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 858)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagDynamiteKitten = false;
				}
				if (Main.player[Projectile.owner].petFlagDynamiteKitten)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 859)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBabyWerewolf = false;
				}
				if (Main.player[Projectile.owner].petFlagBabyWerewolf)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 860)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagShadowMimic = false;
				}
				if (Main.player[Projectile.owner].petFlagShadowMimic)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 875)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagVoltBunny = false;
				}
				if (Main.player[Projectile.owner].petFlagVoltBunny)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 881)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagKingSlimePet = false;
				}
				if (Main.player[Projectile.owner].petFlagKingSlimePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 884)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBrainOfCthulhuPet = false;
				}
				if (Main.player[Projectile.owner].petFlagBrainOfCthulhuPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 885)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagSkeletronPet = false;
				}
				if (Main.player[Projectile.owner].petFlagSkeletronPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 886)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagQueenBeePet = false;
				}
				if (Main.player[Projectile.owner].petFlagQueenBeePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 889)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagSkeletronPrimePet = false;
				}
				if (Main.player[Projectile.owner].petFlagSkeletronPrimePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 890)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagPlanteraPet = false;
				}
				if (Main.player[Projectile.owner].petFlagPlanteraPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 891)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagGolemPet = false;
				}
				if (Main.player[Projectile.owner].petFlagGolemPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 892)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagDukeFishronPet = false;
				}
				if (Main.player[Projectile.owner].petFlagDukeFishronPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 894)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagMoonLordPet = false;
				}
				if (Main.player[Projectile.owner].petFlagMoonLordPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 897)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagEverscreamPet = false;
				}
				if (Main.player[Projectile.owner].petFlagEverscreamPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 899)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagMartianPet = false;
				}
				if (Main.player[Projectile.owner].petFlagMartianPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 900)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagDD2OgrePet = false;
				}
				if (Main.player[Projectile.owner].petFlagDD2OgrePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 901)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagDD2BetsyPet = false;
				}
				if (Main.player[Projectile.owner].petFlagDD2BetsyPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 934)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagQueenSlimePet = false;
				}
				if (Main.player[Projectile.owner].petFlagQueenSlimePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 956)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagBerniePet = false;
				}
				if (Main.player[Projectile.owner].petFlagBerniePet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 958)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagDeerclopsPet = false;
				}
				if (Main.player[Projectile.owner].petFlagDeerclopsPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 959)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagPigPet = false;
				}
				if (Main.player[Projectile.owner].petFlagPigPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 960)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].petFlagChesterPet = false;
				}
				if (Main.player[Projectile.owner].petFlagChesterPet)
				{
					Projectile.timeLeft = 2;
				}
			}
			//if (Projectile.type == 994)
			//{
			//	if (Main.player[Projectile.owner].dead)
			//	{
			//		Main.player[Projectile.owner].petFlagJunimoPet = false;
			//	}
			//	if (Main.player[Projectile.owner].petFlagJunimoPet)
			//	{
			//		Projectile.timeLeft = 2;
			//	}
			//}
			//if (Projectile.type == 998)
			//{
			//	if (Main.player[Projectile.owner].dead)
			//	{
			//		Main.player[Projectile.owner].petFlagBlueChickenPet = false;
			//	}
			//	if (Main.player[Projectile.owner].petFlagBlueChickenPet)
			//	{
			//		Projectile.timeLeft = 2;
			//	}
			//}
			//if (Projectile.type == 1003)
			//{
			//	if (Main.player[Projectile.owner].dead)
			//	{
			//		Main.player[Projectile.owner].petFlagSpiffo = false;
			//	}
			//	if (Main.player[Projectile.owner].petFlagSpiffo)
			//	{
			//		Projectile.timeLeft = 2;
			//	}
			//}
			//if (Projectile.type == 1004)
			//{
			//	if (Main.player[Projectile.owner].dead)
			//	{
			//		Main.player[Projectile.owner].petFlagCaveling = false;
			//	}
			//	if (Main.player[Projectile.owner].petFlagCaveling)
			//	{
			//		Projectile.timeLeft = 2;
			//	}
			//}
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
			if (Projectile.type >= 390 && Projectile.type <= 392)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].spiderMinion = false;
				}
				if (Main.player[Projectile.owner].spiderMinion)
				{
					Projectile.timeLeft = 2;
				}
			}
			if (Projectile.type == 398)
			{
				if (Main.player[Projectile.owner].dead)
				{
					Main.player[Projectile.owner].miniMinotaur = false;
				}
				if (Main.player[Projectile.owner].miniMinotaur)
				{
					Projectile.timeLeft = 2;
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
			else if (Projectile.type == 891)
			{
				num = 30;
				float num4 = -50 * -Main.player[Projectile.owner].direction;
				float num5 = Main.player[Projectile.owner].Center.X + num4;
				if (num5 < Projectile.position.X + (float)(Projectile.width / 2) - (float)num)
				{
					flag2 = true;
				}
				else if (num5 > Projectile.position.X + (float)(Projectile.width / 2) + (float)num)
				{
					flag3 = true;
				}
			}
			else if (Projectile.type == 960 && !flag7)
			{
				num = 10;
				Player player = Main.player[Projectile.owner];
				int num6 = ((player.Center.X - Projectile.Center.X > 0f) ? 1 : (-1));
				if (player.velocity.X != 0f)
				{
					num6 = player.direction;
				}
				float num7 = -70 * num6;
				float num8 = player.Center.X + num7;
				if (num8 < Projectile.Center.X - (float)num)
				{
					flag2 = true;
				}
				else if (num8 > Projectile.Center.X + (float)num)
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
			if (Projectile.type == 175)
			{
				float num9 = 0.1f;
				Projectile.tileCollide = false;
				int num10 = 300;
				Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num11 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector.X;
				float num12 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector.Y;
				if (Projectile.type == 127)
				{
					num12 = Main.player[Projectile.owner].position.Y - vector.Y;
				}
				float num13 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
				float num14 = 7f;
				float num15 = 2000f;
				bool num16 = num13 > num15;
				if (num13 < (float)num10 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num13 < 150f)
				{
					if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
					{
						Projectile.velocity *= 0.99f;
					}
					num9 = 0.01f;
					if (num11 < -2f)
					{
						num11 = -2f;
					}
					if (num11 > 2f)
					{
						num11 = 2f;
					}
					if (num12 < -2f)
					{
						num12 = -2f;
					}
					if (num12 > 2f)
					{
						num12 = 2f;
					}
				}
				else
				{
					if (num13 > 300f)
					{
						num9 = 0.2f;
					}
					num13 = num14 / num13;
					num11 *= num13;
					num12 *= num13;
				}
				if (num16)
				{
					int num17 = 17;
					for (int i = 0; i < 12; i++)
					{
						float speedX = 1f - Main.rand.NextFloat() * 2f;
						float speedY = 1f - Main.rand.NextFloat() * 2f;
						int num18 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num17, speedX, speedY);
						Main.dust[num18].noLightEmittence = true;
						Main.dust[num18].noGravity = true;
					}
					Projectile.Center = Main.player[Projectile.owner].Center;
					Projectile.velocity = Vector2.Zero;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.netUpdate = true;
					}
				}
				if (Math.Abs(num11) > Math.Abs(num12) || num9 == 0.05f)
				{
					if (Projectile.velocity.X < num11)
					{
						Projectile.velocity.X += num9;
						if (num9 > 0.05f && Projectile.velocity.X < 0f)
						{
							Projectile.velocity.X += num9;
						}
					}
					if (Projectile.velocity.X > num11)
					{
						Projectile.velocity.X -= num9;
						if (num9 > 0.05f && Projectile.velocity.X > 0f)
						{
							Projectile.velocity.X -= num9;
						}
					}
				}
				if (Math.Abs(num11) <= Math.Abs(num12) || num9 == 0.05f)
				{
					if (Projectile.velocity.Y < num12)
					{
						Projectile.velocity.Y += num9;
						if (num9 > 0.05f && Projectile.velocity.Y < 0f)
						{
							Projectile.velocity.Y += num9;
						}
					}
					if (Projectile.velocity.Y > num12)
					{
						Projectile.velocity.Y -= num9;
						if (num9 > 0.05f && Projectile.velocity.Y > 0f)
						{
							Projectile.velocity.Y -= num9;
						}
					}
				}
				Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 6)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 1)
				{
					Projectile.frame = 0;
				}
				return;
			}
			if (Projectile.type == 197)
			{
				float num19 = 0.1f;
				Projectile.tileCollide = false;
				int num20 = 300;
				Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num21 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector2.X;
				float num22 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector2.Y;
				if (Projectile.type == 127)
				{
					num22 = Main.player[Projectile.owner].position.Y - vector2.Y;
				}
				float num23 = (float)Math.Sqrt(num21 * num21 + num22 * num22);
				float num24 = 3f;
				if (num23 > 500f)
				{
					Projectile.localAI[0] = 10000f;
				}
				if (Projectile.localAI[0] >= 10000f)
				{
					num24 = 14f;
				}
				float num25 = 2000f;
				bool num26 = num23 > num25;
				if (num23 < (float)num20 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num23 < 150f)
				{
					if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
					{
						Projectile.velocity *= 0.99f;
					}
					num19 = 0.01f;
					if (num21 < -2f)
					{
						num21 = -2f;
					}
					if (num21 > 2f)
					{
						num21 = 2f;
					}
					if (num22 < -2f)
					{
						num22 = -2f;
					}
					if (num22 > 2f)
					{
						num22 = 2f;
					}
				}
				else
				{
					if (num23 > 300f)
					{
						num19 = 0.2f;
					}
					num23 = num24 / num23;
					num21 *= num23;
					num22 *= num23;
				}
				if (num26)
				{
					int num27 = 26;
					for (int j = 0; j < 12; j++)
					{
						float speedX2 = 1f - Main.rand.NextFloat() * 2f;
						float speedY2 = 1f - Main.rand.NextFloat() * 2f;
						int num28 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num27, speedX2, speedY2);
						Main.dust[num28].noLightEmittence = true;
						Main.dust[num28].noGravity = true;
					}
					Projectile.Center = Main.player[Projectile.owner].Center;
					Projectile.velocity = Vector2.Zero;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.netUpdate = true;
					}
				}
				if (Projectile.velocity.X < num21)
				{
					Projectile.velocity.X += num19;
					if (num19 > 0.05f && Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X += num19;
					}
				}
				if (Projectile.velocity.X > num21)
				{
					Projectile.velocity.X -= num19;
					if (num19 > 0.05f && Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X -= num19;
					}
				}
				if (Projectile.velocity.Y < num22)
				{
					Projectile.velocity.Y += num19;
					if (num19 > 0.05f && Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y += num19;
					}
				}
				if (Projectile.velocity.Y > num22)
				{
					Projectile.velocity.Y -= num19;
					if (num19 > 0.05f && Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y -= num19;
					}
				}
				Projectile.localAI[0] += Main.rand.Next(10);
				if (Projectile.localAI[0] > 10000f)
				{
					if (Projectile.localAI[1] == 0f)
					{
						if (Projectile.velocity.X < 0f)
						{
							Projectile.localAI[1] = -1f;
						}
						else
						{
							Projectile.localAI[1] = 1f;
						}
					}
					Projectile.rotation += 0.25f * Projectile.localAI[1];
					if (Projectile.localAI[0] > 12000f)
					{
						Projectile.localAI[0] = 0f;
					}
				}
				else
				{
					Projectile.localAI[1] = 0f;
					float num29 = Projectile.velocity.X * 0.1f;
					if (Projectile.rotation > num29)
					{
						Projectile.rotation -= (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f;
						if (Projectile.rotation < num29)
						{
							Projectile.rotation = num29;
						}
					}
					if (Projectile.rotation < num29)
					{
						Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f;
						if (Projectile.rotation > num29)
						{
							Projectile.rotation = num29;
						}
					}
				}
				if ((double)Projectile.rotation > 6.28)
				{
					Projectile.rotation -= 6.28f;
				}
				if ((double)Projectile.rotation < -6.28)
				{
					Projectile.rotation += 6.28f;
				}
				return;
			}
			if (Projectile.type == 198 || Projectile.type == 380 || Projectile.type == 774 || Projectile.type == 815 || Projectile.type == 817 || Projectile.type == 886 || Projectile.type == 892 || Projectile.type == 894 || Projectile.type == 901)
			{
				float num30 = 0.4f;
				if (Projectile.type == 380)
				{
					num30 = 0.3f;
				}
				if (Projectile.type == 774)
				{
					num30 = 0.3f;
				}
				Projectile.tileCollide = false;
				int num31 = 100;
				float num32 = 50f;
				float num33 = 400f;
				float num34 = num33 / 2f;
				float num35 = 2000f;
				bool flag9 = false;
				Vector2 vector3 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num36 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector3.X;
				float num37 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector3.Y;
				if (Projectile.type == 774)
				{
					num32 = 2f;
					float num38 = (float)Math.Sin((float)Math.PI * 2f * ((float)Main.player[Projectile.owner].miscCounter / 60f));
					float num39 = Utils.GetLerpValue(0.5f, 1f, num38, clamped: true);
					if (new Vector2(num36 - (float)(70 * Main.player[Projectile.owner].direction), num37 - 60f).Length() > 50f)
					{
						num38 = 0f;
						num39 = 0f;
					}
					num37 += -60f + num39 * -8f + num38 * 8f;
					num36 += (float)(70 * -Main.player[Projectile.owner].direction);
				}
				else
				{
					if (Projectile.type != 892 && Projectile.type != 894)
					{
						num37 += (float)Main.rand.Next(-10, 21);
						num36 += (float)Main.rand.Next(-10, 21);
					}
					num36 += (float)(60 * -Main.player[Projectile.owner].direction);
					num37 -= 60f;
				}
				Vector2 vector4 = new Vector2(num36, num37);
				if (Projectile.type == 127)
				{
					num37 = Main.player[Projectile.owner].position.Y - vector3.Y;
				}
				float num40 = (float)Math.Sqrt(num36 * num36 + num37 * num37);
				float num41 = num40;
				float num42 = 14f;
				if (Projectile.type == 380)
				{
					num42 = 6f;
				}
				if (Projectile.type == 815 || Projectile.type == 817)
				{
					num42 = ((!(num40 < num33)) ? 10f : 6f);
				}
				if (Projectile.type == 892 || Projectile.type == 894 || Projectile.type == 901)
				{
					if (num40 < num34)
					{
						num42 = 6f;
					}
					num42 = ((!(num40 < num33)) ? 12f : 9f);
				}
				if (Projectile.type == 774)
				{
					num42 = 5f;
				}
				if (num40 < (float)num31 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num40 < num32)
				{
					if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
					{
						if (Projectile.type == 892 || Projectile.type == 892)
						{
							Projectile.velocity *= 0.95f;
						}
						else
						{
							Projectile.velocity *= 0.99f;
						}
					}
					num30 = 0.01f;
				}
				else
				{
					if (Projectile.type == 892 || Projectile.type == 894 || Projectile.type == 901)
					{
						if (num40 < 100f)
						{
							num30 = 0.1f;
						}
						if (num40 > num35)
						{
							flag9 = true;
						}
						else if (num40 > num33)
						{
							num30 = 0.7f;
						}
						else if (num40 > num34)
						{
							num30 = 0.5f;
						}
					}
					else if (Projectile.type == 815 || Projectile.type == 817)
					{
						if (num40 < 100f)
						{
							num30 = 0.1f;
						}
						if (num40 > num35)
						{
							flag9 = true;
						}
						else if (Projectile.type != 815 && num40 > num33)
						{
							num30 = 0.5f;
						}
					}
					else if (Projectile.type == 380)
					{
						if (num40 < 100f)
						{
							num30 = 0.1f;
						}
						if (num40 > 300f)
						{
							num30 = 0.4f;
						}
						if (num40 > num35)
						{
							flag9 = true;
						}
					}
					else if (Projectile.type == 198 || Projectile.type == 886)
					{
						if (num40 < 100f)
						{
							num30 = 0.1f;
						}
						if (num40 > 300f)
						{
							num30 = 0.6f;
						}
						if (num40 > num35)
						{
							flag9 = true;
						}
					}
					else if (Projectile.type == 774)
					{
						if (num40 < 40f)
						{
							num30 = 0.1f;
						}
						if (num40 > 300f)
						{
							num30 = 0.6f;
						}
						if (num40 > num35)
						{
							flag9 = true;
						}
					}
					num40 = num42 / num40;
					num36 *= num40;
					num37 *= num40;
				}
				if (Projectile.velocity.X < num36)
				{
					Projectile.velocity.X += num30;
					if (num30 > 0.05f && Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X += num30;
					}
				}
				if (Projectile.velocity.X > num36)
				{
					Projectile.velocity.X -= num30;
					if (num30 > 0.05f && Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X -= num30;
					}
				}
				if (Projectile.velocity.Y < num37)
				{
					Projectile.velocity.Y += num30;
					if (num30 > 0.05f && Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y += num30 * 2f;
					}
				}
				if (Projectile.velocity.Y > num37)
				{
					Projectile.velocity.Y -= num30;
					if (num30 > 0.05f && Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y -= num30 * 2f;
					}
				}
				if ((double)Projectile.velocity.X > 0.25)
				{
					Projectile.direction = -1;
				}
				else if ((double)Projectile.velocity.X < -0.25)
				{
					Projectile.direction = 1;
				}
				Projectile.spriteDirection = Projectile.direction;
				Projectile.rotation = Projectile.velocity.X * 0.05f;
				if (flag9)
				{
					int num43 = 33;
					if (Projectile.type == 198 || Projectile.type == 886)
					{
						num43 = 147;
					}
					if (Projectile.type == 815)
					{
						num43 = 31;
					}
					if (Projectile.type == 817)
					{
						num43 = 21;
					}
					for (int k = 0; k < 12; k++)
					{
						float speedX3 = 1f - Main.rand.NextFloat() * 2f;
						float speedY3 = 1f - Main.rand.NextFloat() * 2f;
						int num44 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num43, speedX3, speedY3);
						Main.dust[num44].noLightEmittence = true;
						Main.dust[num44].noGravity = true;
					}
					Projectile.Center = Main.player[Projectile.owner].Center;
					Projectile.velocity = Vector2.Zero;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.netUpdate = true;
					}
				}
				bool flag10 = false;
				int num45 = 3;
				int num46 = 0;
				int num47 = 3;
				if (Projectile.type == 380)
				{
					num45 = 6;
				}
				if (Projectile.type == 815)
				{
					num45 = 5;
					num46 = 0;
					num47 = 5;
					if (num41 > num33)
					{
						num46 = 6;
						num47 = 9;
					}
				}
				if (Projectile.type == 817)
				{
					num45 = 5;
					num46 = 0;
					num47 = 2;
					flag10 = true;
				}
				if (Projectile.type == 901)
				{
					num45 = 4;
					num46 = 0;
					num47 = 5;
					if (num41 > num33 / 2f)
					{
						num45 = 3;
						num46 = 6;
						num47 = 11;
					}
				}
				if (Projectile.type == 892)
				{
					num45 = 6;
					num46 = 0;
					num47 = Main.projFrames[Projectile.type] - 1;
				}
				if (Projectile.type == 886 || Projectile.type == 894)
				{
					num45 = 4;
					num46 = 0;
					num47 = Main.projFrames[Projectile.type] - 1;
				}
				if (Projectile.type == 774)
				{
					if (Main.player[Projectile.owner].velocity.Length() < 2f && vector4.Length() < 10f)
					{
						Projectile.direction = -Main.player[Projectile.owner].direction;
						Projectile.spriteDirection = Projectile.direction;
					}
					num45 = 6;
					if (!Projectile.wet)
					{
						num46 += 4;
						num47 += 4;
					}
					Projectile.rotation = Projectile.velocity.X * 0.05f + Math.Abs(Projectile.velocity.Y * -0.05f);
				}
				if (flag10)
				{
					int num48 = num45 * (num47 - num46) * 2;
					Projectile.frameCounter++;
					if (Projectile.frameCounter >= num48)
					{
						Projectile.frameCounter = 0;
					}
					Projectile.frame = Projectile.frameCounter / num45;
					if (Projectile.frame > num47)
					{
						Projectile.frame = num47 + (num47 - Projectile.frame);
					}
					Projectile.frame = (int)MathHelper.Clamp(Projectile.frame, num46, num47);
				}
				else if (++Projectile.frameCounter >= num45)
				{
					Projectile.frameCounter = 0;
					Projectile.frame++;
					if (Projectile.frame < num46 || Projectile.frame > num47)
					{
						Projectile.frame = num46;
					}
				}
				return;
			}
			if (Projectile.type == 211)
			{
				float num49 = 0.2f;
				float num50 = 5f;
				Projectile.tileCollide = false;
				Vector2 vector5 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num51 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector5.X;
				float num52 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].gfxOffY + (float)(Main.player[Projectile.owner].height / 2) - vector5.Y;
				if (Main.player[Projectile.owner].controlLeft)
				{
					num51 -= 120f;
				}
				else if (Main.player[Projectile.owner].controlRight)
				{
					num51 += 120f;
				}
				if (Main.player[Projectile.owner].controlDown)
				{
					num52 += 120f;
				}
				else
				{
					if (Main.player[Projectile.owner].controlUp)
					{
						num52 -= 120f;
					}
					num52 -= 60f;
				}
				float num53 = (float)Math.Sqrt(num51 * num51 + num52 * num52);
				if (num53 > 1000f)
				{
					Projectile.position.X += num51;
					Projectile.position.Y += num52;
				}
				if (Projectile.localAI[0] == 1f)
				{
					if (num53 < 10f)
					{
						Player player2 = Main.player[Projectile.owner];
						if (Math.Abs(player2.velocity.X) + Math.Abs(player2.velocity.Y) < num50 && (player2.velocity.Y == 0f || (player2.mount.Active && player2.mount.CanFly())))
						{
							Projectile.localAI[0] = 0f;
						}
					}
					num50 = 12f;
					if (num53 < num50)
					{
						Projectile.velocity.X = num51;
						Projectile.velocity.Y = num52;
					}
					else
					{
						num53 = num50 / num53;
						Projectile.velocity.X = num51 * num53;
						Projectile.velocity.Y = num52 * num53;
					}
					if ((double)Projectile.velocity.X > 0.5)
					{
						Projectile.direction = -1;
					}
					else if ((double)Projectile.velocity.X < -0.5)
					{
						Projectile.direction = 1;
					}
					Projectile.spriteDirection = Projectile.direction;
					Projectile.rotation -= (0.2f + Math.Abs(Projectile.velocity.X) * 0.025f) * (float)Projectile.direction;
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 3)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 5)
					{
						Projectile.frame = 5;
					}
					if (Projectile.frame > 9)
					{
						Projectile.frame = 5;
					}
					for (int l = 0; l < 2; l++)
					{
						int num54 = Dust.NewDust(new Vector2(Projectile.position.X + 3f, Projectile.position.Y + 4f), 14, 14, 156);
						Main.dust[num54].velocity *= 0.2f;
						Main.dust[num54].noGravity = true;
						Main.dust[num54].scale = 1.25f;
						Main.dust[num54].shader = GameShaders.Armor.GetSecondaryShader(Main.player[Projectile.owner].cLight, Main.player[Projectile.owner]);
					}
					return;
				}
				if (num53 > 200f)
				{
					Projectile.localAI[0] = 1f;
				}
				if ((double)Projectile.velocity.X > 0.5)
				{
					Projectile.direction = -1;
				}
				else if ((double)Projectile.velocity.X < -0.5)
				{
					Projectile.direction = 1;
				}
				Projectile.spriteDirection = Projectile.direction;
				if (num53 < 10f)
				{
					Projectile.velocity.X = num51;
					Projectile.velocity.Y = num52;
					Projectile.rotation = Projectile.velocity.X * 0.05f;
					if (num53 < num50)
					{
						Projectile.position += Projectile.velocity;
						Projectile.velocity *= 0f;
						num49 = 0f;
					}
					Projectile.direction = -Main.player[Projectile.owner].direction;
				}
				num53 = num50 / num53;
				num51 *= num53;
				num52 *= num53;
				if (Projectile.velocity.X < num51)
				{
					Projectile.velocity.X += num49;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X *= 0.99f;
					}
				}
				if (Projectile.velocity.X > num51)
				{
					Projectile.velocity.X -= num49;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X *= 0.99f;
					}
				}
				if (Projectile.velocity.Y < num52)
				{
					Projectile.velocity.Y += num49;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y *= 0.99f;
					}
				}
				if (Projectile.velocity.Y > num52)
				{
					Projectile.velocity.Y -= num49;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y *= 0.99f;
					}
				}
				if (Projectile.velocity.X != 0f || Projectile.velocity.Y != 0f)
				{
					Projectile.rotation = Projectile.velocity.X * 0.05f;
				}
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 3)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 4)
				{
					Projectile.frame = 0;
				}
				return;
			}
			if (Projectile.type == 199)
			{
				float num55 = 0.1f;
				Projectile.tileCollide = false;
				int num56 = 200;
				Vector2 vector6 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num57 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector6.X;
				float num58 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector6.Y;
				num58 -= 60f;
				num57 -= 2f;
				if (Projectile.type == 127)
				{
					num58 = Main.player[Projectile.owner].position.Y - vector6.Y;
				}
				float num59 = (float)Math.Sqrt(num57 * num57 + num58 * num58);
				float num60 = 4f;
				float num61 = num59;
				float num62 = 2000f;
				bool num63 = num59 > num62;
				if (num59 < (float)num56 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					if (Projectile.velocity.Y < -6f)
					{
						Projectile.velocity.Y = -6f;
					}
				}
				if (num59 < 4f)
				{
					Projectile.velocity.X = num57;
					Projectile.velocity.Y = num58;
					num55 = 0f;
				}
				else
				{
					if (num59 > 350f)
					{
						num55 = 0.2f;
						num60 = 10f;
					}
					num59 = num60 / num59;
					num57 *= num59;
					num58 *= num59;
				}
				if (num63)
				{
					int num64 = 2;
					for (int m = 0; m < 12; m++)
					{
						float speedX4 = 1f - Main.rand.NextFloat() * 2f;
						float speedY4 = 1f - Main.rand.NextFloat() * 2f;
						int num65 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num64, speedX4, speedY4);
						Main.dust[num65].noLightEmittence = true;
						Main.dust[num65].noGravity = true;
					}
					Projectile.Center = Main.player[Projectile.owner].Center;
					Projectile.velocity = Vector2.Zero;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.netUpdate = true;
					}
				}
				if (Projectile.velocity.X < num57)
				{
					Projectile.velocity.X += num55;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X += num55;
					}
				}
				if (Projectile.velocity.X > num57)
				{
					Projectile.velocity.X -= num55;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X -= num55;
					}
				}
				if (Projectile.velocity.Y < num58)
				{
					Projectile.velocity.Y += num55;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y += num55;
					}
				}
				if (Projectile.velocity.Y > num58)
				{
					Projectile.velocity.Y -= num55;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y -= num55;
					}
				}
				Projectile.direction = -Main.player[Projectile.owner].direction;
				Projectile.spriteDirection = 1;
				Projectile.rotation = Projectile.velocity.Y * 0.05f * (float)(-Projectile.direction);
				if (num61 >= 50f)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter <= 6)
					{
						return;
					}
					Projectile.frameCounter = 0;
					if (Projectile.velocity.X < 0f)
					{
						if (Projectile.frame < 2)
						{
							Projectile.frame++;
						}
						if (Projectile.frame > 2)
						{
							Projectile.frame--;
						}
					}
					else
					{
						if (Projectile.frame < 6)
						{
							Projectile.frame++;
						}
						if (Projectile.frame > 6)
						{
							Projectile.frame--;
						}
					}
				}
				else
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 6)
					{
						Projectile.frame += Projectile.direction;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 7)
					{
						Projectile.frame = 0;
					}
					if (Projectile.frame < 0)
					{
						Projectile.frame = 7;
					}
				}
				return;
			}
			if (Projectile.type == 885 || Projectile.type == 889)
			{
				Player player3 = Main.player[Projectile.owner];
				float num66 = 0.15f;
				Projectile.tileCollide = false;
				int num67 = 150;
				Vector2 center = Projectile.Center;
				float num68 = player3.Center.X - center.X;
				float num69 = player3.Center.Y - center.Y;
				num69 -= 65f;
				num68 -= (float)(30 * player3.direction);
				float num70 = (float)Math.Sqrt(num68 * num68 + num69 * num69);
				float num71 = 8f;
				float num72 = num70;
				float num73 = 2000f;
				bool num74 = num70 > num73;
				if (num70 < (float)num67 && player3.velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= player3.position.Y + (float)player3.height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) && Projectile.velocity.Y < -6f)
				{
					Projectile.velocity.Y = -6f;
				}
				if (num70 < 10f)
				{
					Projectile.velocity *= 0.9f;
					if (Projectile.velocity.Length() < 0.5f)
					{
						Projectile.velocity = Vector2.Zero;
					}
					num66 = 0f;
				}
				else
				{
					if (num70 > (float)num67)
					{
						num66 = 0.2f;
						num71 = 12f;
					}
					num70 = num71 / num70;
					num68 *= num70;
					num69 *= num70;
				}
				if (num74)
				{
					int num75 = 234;
					if (Projectile.type == 889)
					{
						num75 = 60;
					}
					for (int n = 0; n < 12; n++)
					{
						float speedX5 = 1f - Main.rand.NextFloat() * 2f;
						float speedY5 = 1f - Main.rand.NextFloat() * 2f;
						int num76 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num75, speedX5, speedY5);
						Main.dust[num76].noLightEmittence = true;
						Main.dust[num76].noGravity = true;
					}
					Projectile.Center = player3.Center;
					Projectile.velocity = Vector2.Zero;
					if (Main.myPlayer == Projectile.owner)
					{
						Projectile.netUpdate = true;
					}
				}
				if (Projectile.velocity.X < num68)
				{
					Projectile.velocity.X += num66;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X += num66;
					}
				}
				if (Projectile.velocity.X > num68)
				{
					Projectile.velocity.X -= num66;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X -= num66;
					}
				}
				if (Projectile.velocity.Y < num69)
				{
					Projectile.velocity.Y += num66;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y += num66;
					}
				}
				if (Projectile.velocity.Y > num69)
				{
					Projectile.velocity.Y -= num66;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y -= num66;
					}
				}
				Projectile.direction = -player3.direction;
				Projectile.spriteDirection = -Projectile.direction;
				int num77 = 100;
				if (num72 >= (float)num67)
				{
					Projectile.rotation += 0.5f;
					if (Projectile.rotation > (float)Math.PI * 2f)
					{
						Projectile.rotation -= (float)Math.PI * 2f;
					}
					Projectile.frame = 6;
					Projectile.frameCounter = 0;
					if (Projectile.type == 885)
					{
						Projectile.localAI[0] = 0f;
					}
					if (Projectile.type == 889)
					{
						Projectile.localAI[0] += 3f;
						if (Projectile.localAI[0] > (float)num77)
						{
							Projectile.localAI[0] = num77;
						}
					}
					return;
				}
				Projectile.rotation *= 0.95f;
				if (Projectile.rotation < 0.05f)
				{
					Projectile.rotation = 0f;
				}
				Projectile.frameCounter++;
				if (Projectile.type == 885)
				{
					switch (Projectile.frameCounter)
					{
						case 10:
							Projectile.localAI[0] = 0f;
							break;
						case 20:
							Projectile.localAI[0] = 1f;
							break;
						case 30:
							Projectile.localAI[0] = 2f;
							break;
						case 40:
							Projectile.localAI[0] = 1f;
							break;
					}
				}
				if (Projectile.type == 889)
				{
					Projectile.localAI[0] -= 3f;
					if (Projectile.localAI[0] < 0f)
					{
						Projectile.localAI[0] = 0f;
					}
				}
				if (Projectile.frameCounter % 5 == 0)
				{
					Projectile.frame++;
					if (Projectile.frame > 5)
					{
						Projectile.frame = 0;
					}
				}
				if (Projectile.frameCounter >= 40)
				{
					Projectile.frameCounter = 0;
				}
				return;
			}
			bool flag11 = Projectile.ai[1] == 0f;
			if (flag)
			{
				flag11 = true;
			}
			if (flag11)
			{
				int num78 = 500;
				if (Projectile.type == 127)
				{
					num78 = 200;
				}
				if (Projectile.type == 208)
				{
					num78 = 300;
				}
				switch (Projectile.type)
				{
					case 816:
					case 825:
					case 854:
					case 858:
					case 859:
					case 860:
					case 881:
					case 884:
					case 890:
					case 891:
					case 897:
					case 900:
					case 934:
						num78 = 400;
						break;
					case 821:
					case 899:
						num78 = 500;
						break;
				}
				if (flag6 || Projectile.type == 266 || (Projectile.type >= 390 && Projectile.type <= 392))
				{
					num78 += 40 * Projectile.minionPos;
					if (Projectile.localAI[0] > 0f)
					{
						num78 += 500;
					}
					if (Projectile.type == 266 && Projectile.localAI[0] > 0f)
					{
						num78 += 100;
					}
					if (Projectile.type >= 390 && Projectile.type <= 392 && Projectile.localAI[0] > 0f)
					{
						num78 += 400;
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
			if ((Projectile.type == 209 || Projectile.type == 956) && Projectile.ai[0] != 0f)
			{
				if (Main.player[Projectile.owner].velocity.Y == 0f && Projectile.alpha >= 100)
				{
					Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height - (float)Projectile.height;
					Projectile.ai[0] = 0f;
				}
				else
				{
					Projectile.velocity.X = 0f;
					Projectile.velocity.Y = 0f;
					if (Projectile.type == 956 && Projectile.alpha < 100)
					{
						int num82 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1.2f);
						Main.dust[num82].velocity.X += Main.rand.NextFloat() - 0.5f;
						Main.dust[num82].velocity.Y += (Main.rand.NextFloat() + 0.5f) * -1f;
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num82].noGravity = true;
						}
					}
					Projectile.alpha += 5;
					if (Projectile.alpha > 255)
					{
						Projectile.alpha = 255;
					}
				}
			}
			else if (Projectile.ai[0] != 0f && !flag7)
			{
				float num83 = 0.2f;
				int num84 = 200;
				if (Projectile.type == 127)
				{
					num84 = 100;
				}
				if (flag6 || Projectile.type == 816 || Projectile.type == 821 || Projectile.type == 825 || Projectile.type == 854 || Projectile.type == 858 || Projectile.type == 859 || Projectile.type == 860)
				{
					num83 = 0.5f;
					num84 = 100;
				}
				if (Projectile.type == 875)
				{
					num83 = 2f;
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
				if (Projectile.type == 127)
				{
					num91 = Main.player[Projectile.owner].position.Y - vector8.Y;
				}
				float num92 = (float)Math.Sqrt(num85 * num85 + num91 * num91);
				float num93 = num92;
				float num94 = 10f;
				float num95 = num92;
				if (Projectile.type == 111)
				{
					num94 = 11f;
				}
				if (Projectile.type == 127)
				{
					num94 = 9f;
				}
				if (Projectile.type == 875)
				{
					num83 = 1.8f;
					num94 = 16f;
				}
				if (Projectile.type == 324)
				{
					num94 = 20f;
				}
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
				if (Projectile.type == 208 && Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y) > 4f)
				{
					num84 = -1;
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
				if (Projectile.type == 324)
				{
					if (num95 > 1000f)
					{
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) < (double)num94 - 1.25)
						{
							Projectile.velocity *= 1.025f;
						}
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) > (double)num94 + 1.25)
						{
							Projectile.velocity *= 0.975f;
						}
					}
					else if (num95 > 600f)
					{
						if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < num94 - 1f)
						{
							Projectile.velocity *= 1.05f;
						}
						if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > num94 + 1f)
						{
							Projectile.velocity *= 0.95f;
						}
					}
					else if (num95 > 400f)
					{
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) < (double)num94 - 0.5)
						{
							Projectile.velocity *= 1.075f;
						}
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) > (double)num94 + 0.5)
						{
							Projectile.velocity *= 0.925f;
						}
					}
					else
					{
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) < (double)num94 - 0.25)
						{
							Projectile.velocity *= 1.1f;
						}
						if ((double)(Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) > (double)num94 + 0.25)
						{
							Projectile.velocity *= 0.9f;
						}
					}
					Projectile.velocity.X = (Projectile.velocity.X * 34f + num85) / 35f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 34f + num91) / 35f;
				}
				else if (Projectile.type == 875)
				{
					if (num93 < (float)num84)
					{
						if (Projectile.velocity.X < num85)
						{
							Projectile.velocity.X += num83;
						}
						else if (Projectile.velocity.X > num85)
						{
							Projectile.velocity.X -= num83;
						}
						if (Projectile.velocity.Y < num91)
						{
							Projectile.velocity.Y += num83;
						}
						else if (Projectile.velocity.Y > num91)
						{
							Projectile.velocity.Y -= num83;
						}
					}
					else
					{
						Projectile.velocity = Vector2.Lerp(Projectile.velocity, new Vector2(num85, num91), 0.75f);
					}
				}
				else
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
				if (Projectile.type == 111)
				{
					Projectile.frame = 7;
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
				if (Projectile.type == 112)
				{
					Projectile.frameCounter++;
					if (Projectile.frame < 3)
					{
						Projectile.frame = 3;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 3;
						}
					}
					Projectile.rotation = Projectile.velocity.X * 0.125f;
				}
				else if (Projectile.type == 900)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frameCounter++;
					if (Projectile.frame < 10)
					{
						Projectile.frame = 10;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 10;
						}
					}
					Projectile.rotation = Projectile.velocity.X * 0.125f;
				}
				else if (Projectile.type == 899)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frameCounter++;
					if (Projectile.frame < 10)
					{
						Projectile.frame = 10;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 10;
						}
					}
					Vector2 v = Projectile.velocity;
					v.Normalize();
					Projectile.rotation = v.ToRotation();
					if (Projectile.velocity.X < 0f)
					{
						Projectile.rotation += (float)Math.PI;
					}
				}
				else if (Projectile.type == 897)
				{
					Projectile.spriteDirection = 1;
					Projectile.frameCounter++;
					if (Projectile.frame < 8)
					{
						Projectile.frame = 8;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 8;
						}
					}
					Vector2 v2 = Projectile.velocity;
					v2.Normalize();
					Projectile.rotation = v2.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 891)
				{
					Projectile.spriteDirection = 1;
					Projectile.frameCounter++;
					if (Projectile.frame < 9)
					{
						Projectile.frame = 9;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 9;
						}
					}
					Vector2 v3 = Projectile.velocity;
					v3.Normalize();
					Projectile.rotation = v3.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 890)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frameCounter++;
					if (Projectile.frame < 9)
					{
						Projectile.frame = 9;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 9;
						}
					}
					Projectile.rotation = Projectile.velocity.X * 0.025f;
				}
				else if (Projectile.type == 884)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frameCounter++;
					if (Projectile.frame < 9)
					{
						Projectile.frame = 9;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frameCounter > 3)
					{
						Projectile.frameCounter = 0;
						Projectile.frame++;
						if (Projectile.frame >= Main.projFrames[Projectile.type])
						{
							Projectile.frame = 9;
						}
					}
					Vector2 v4 = Projectile.velocity;
					v4.Normalize();
					Projectile.rotation = v4.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 881 || Projectile.type == 934)
				{
					int num96 = 1226;
					if (Projectile.type == 934)
					{
						num96 = 1261;
					}
					if (Projectile.frame < 6 || Projectile.frame > 11)
					{
						Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.position.Y), Projectile.velocity * 0.5f, num96);
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 6 || Projectile.frame > 11)
					{
						Projectile.frame = 6;
					}
					Vector2 v5 = Projectile.velocity;
					v5.Normalize();
					Projectile.rotation = v5.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 875)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 7 || Projectile.frame > 10)
					{
						Projectile.frame = 7;
					}
					Vector2 v6 = Projectile.velocity;
					v6.Normalize();
					Projectile.rotation = v6.ToRotation() + ((Projectile.spriteDirection == -1) ? 0f : ((float)Math.PI));
				}
				else if (Projectile.type == 825)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 21 || Projectile.frame > 25)
					{
						Projectile.frame = 21;
					}
					Projectile.rotation = Projectile.velocity.X * 0.025f;
				}
				else if (Projectile.type == 854)
				{
					if (Projectile.frame < 13)
					{
						Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.position.Y), Projectile.velocity * 0.5f, 1269);
					}
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 13 || Projectile.frame > 18)
					{
						Projectile.frame = 13;
					}
					Vector2 v7 = Projectile.velocity;
					v7.Normalize();
					Projectile.rotation = v7.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 858)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 10 || Projectile.frame > 13)
					{
						Projectile.frame = 10;
					}
					Vector2 v8 = Projectile.velocity;
					v8.Normalize();
					Projectile.rotation = v8.ToRotation() + ((Projectile.spriteDirection == -1) ? 0f : ((float)Math.PI));
				}
				else if (Projectile.type == 859)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 18 || Projectile.frame > 23)
					{
						Projectile.frame = 18;
					}
					Projectile.rotation = Projectile.velocity.X * 0.025f;
				}
				else if (Projectile.type == 860)
				{
					if (Projectile.frame < 6)
					{
						Projectile.frame = 8;
						Projectile.frameCounter = 0;
					}
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 13)
					{
						Projectile.frame = 6;
					}
					Vector2 v9 = Projectile.velocity;
					v9.Normalize();
					Projectile.rotation = v9.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 816)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 11 || Projectile.frame > 16)
					{
						Projectile.frame = 11;
					}
					Projectile.rotation = Projectile.velocity.X * 0.025f;
				}
				else if (Projectile.type == 821)
				{
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 19 || Projectile.frame > 22)
					{
						Projectile.frame = 19;
					}
					Projectile.rotation = Projectile.velocity.X * 0.025f;
				}
				else if (Projectile.type == 958)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					if (Projectile.frame < 13)
					{
						Projectile.frame = 13;
						Projectile.frameCounter = 0;
					}
					if (++Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 16)
					{
						Projectile.frame = 13;
					}
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.4f, 0.4f);
				}
				else if (Projectile.type == 960)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frame = 4;
					Projectile.frameCounter = 0;
					Vector2 v10 = Projectile.velocity;
					v10.Normalize();
					Projectile.rotation = v10.ToRotation() + (float)Math.PI / 2f;
				}
				else if (Projectile.type == 959)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frame = 11;
					Projectile.frameCounter = 0;
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.4f, 0.4f);
					float num97 = Vector2.Dot(Projectile.velocity.SafeNormalize(Vector2.UnitX), new Vector2(0f, -1f));
					if (num97 > 0f && Main.rand.NextFloat() < 0.3f + num97 * 0.3f)
					{
						Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 16, Projectile.velocity.X * 0.7f, Projectile.velocity.Y * 1.2f, 150, default(Color), 0.6f);
						dust.noGravity = true;
						dust.fadeIn = 1f;
						dust.velocity.X = Projectile.velocity.X * 0.3f;
						dust.position = Projectile.Top + new Vector2(0f, -10f) + new Vector2(Main.rand.NextFloatDirection() * 30f, Main.rand.NextFloatDirection() * 10f);
						dust.velocity *= 0.7f;
						dust.position += dust.velocity * 2f;
					}
				}
				else if (Projectile.type == 994)
				{
					Projectile.spriteDirection = -1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = 1;
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 5)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 13 || Projectile.frame > 15)
					{
						Projectile.frame = 13;
					}
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.4f, 0.4f);
				}
				else if (Projectile.type == 998)
				{
					Projectile.spriteDirection = 1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = -1;
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 6 || Projectile.frame > 9)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.4f, 0.4f);
				}
				else if (Projectile.type == 1003)
				{
					Projectile.spriteDirection = 1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = -1;
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 3)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 12 || Projectile.frame > 15)
					{
						Projectile.frame = 12;
					}
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.35f, 0.35f);
				}
				else if (Projectile.type == 1004)
				{
					Projectile.spriteDirection = 1;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.spriteDirection = -1;
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 3)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 10 || Projectile.frame > 14)
					{
						Projectile.frame = 10;
					}
					Projectile.rotation = MathHelper.Clamp(Projectile.velocity.X * 0.025f, -0.35f, 0.35f);
				}
				else if (Projectile.type == 112)
				{
					if (Projectile.spriteDirection == -1)
					{
						Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
					}
					else
					{
						Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
					}
				}
				else if (Projectile.type >= 390 && Projectile.type <= 392)
				{
					int num98 = (int)(Projectile.Center.X / 16f);
					int num99 = (int)(Projectile.Center.Y / 16f);
					if (Main.tile[num98, num99] != null && Main.tile[num98, num99].WallType > 0)
					{
						Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
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
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 2)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 8 || Projectile.frame > 10)
						{
							Projectile.frame = 8;
						}
						Projectile.rotation = Projectile.velocity.X * 0.1f;
					}
				}
				else if (Projectile.type == 334)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 1)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 7 || Projectile.frame > 10)
					{
						Projectile.frame = 7;
					}
					Projectile.rotation = Projectile.velocity.X * 0.1f;
				}
				else if (Projectile.type == 353)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 10 || Projectile.frame > 13)
					{
						Projectile.frame = 10;
					}
					Projectile.rotation = Projectile.velocity.X * 0.05f;
				}
				else if (Projectile.type == 127)
				{
					Projectile.frameCounter += 3;
					if (Projectile.frameCounter > 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame <= 5 || Projectile.frame > 15)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = Projectile.velocity.X * 0.1f;
				}
				else if (Projectile.type == 269)
				{
					if (Projectile.frame == 6)
					{
						Projectile.frameCounter = 0;
					}
					else if (Projectile.frame < 4 || Projectile.frame > 6)
					{
						Projectile.frameCounter = 0;
						Projectile.frame = 4;
					}
					else
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
					}
					Projectile.rotation = Projectile.velocity.X * 0.05f;
				}
				else if (Projectile.type == 266)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 2 || Projectile.frame > 5)
					{
						Projectile.frame = 2;
					}
					Projectile.rotation = Projectile.velocity.X * 0.1f;
				}
				else if (Projectile.type == 324)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 1)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 6 || Projectile.frame > 9)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.58f;
					Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.9f, 0.6f, 0.2f);
					for (int num100 = 0; num100 < 2; num100++)
					{
						int num101 = 4;
						int num102 = Dust.NewDust(new Vector2(Projectile.Center.X - (float)num101, Projectile.Center.Y - (float)num101) - Projectile.velocity * 0f, num101 * 2, num101 * 2, 6, 0f, 0f, 100);
						Main.dust[num102].scale *= 1.8f + (float)Main.rand.Next(10) * 0.1f;
						Main.dust[num102].velocity *= 0.2f;
						if (num100 == 1)
						{
							Main.dust[num102].position -= Projectile.velocity * 0.5f;
						}
						Main.dust[num102].noGravity = true;
						num102 = Dust.NewDust(new Vector2(Projectile.Center.X - (float)num101, Projectile.Center.Y - (float)num101) - Projectile.velocity * 0f, num101 * 2, num101 * 2, 31, 0f, 0f, 100, default(Color), 0.5f);
						Main.dust[num102].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
						Main.dust[num102].velocity *= 0.05f;
						if (num100 == 1)
						{
							Main.dust[num102].position -= Projectile.velocity * 0.5f;
						}
					}
				}
				else if (Projectile.type == 268)
				{
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 4)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 6 || Projectile.frame > 7)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.58f;
				}
				else if (Projectile.type == 200)
				{
					Projectile.frameCounter += 3;
					if (Projectile.frameCounter > 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame <= 5 || Projectile.frame > 9)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = Projectile.velocity.X * 0.1f;
				}
				else if (Projectile.type == 208)
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
				else if (Projectile.type == 236)
				{
					Projectile.rotation = Projectile.velocity.Y * 0.05f * (float)Projectile.direction;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.frameCounter += 2;
					}
					else
					{
						Projectile.frameCounter++;
					}
					if (Projectile.frameCounter >= 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 12)
					{
						Projectile.frame = 9;
					}
					if (Projectile.frame < 9)
					{
						Projectile.frame = 9;
					}
				}
				else if (Projectile.type == 499)
				{
					Projectile.rotation = Projectile.velocity.Y * 0.05f * (float)Projectile.direction;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.frameCounter += 2;
					}
					else
					{
						Projectile.frameCounter++;
					}
					if (Projectile.frameCounter >= 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame >= 12)
					{
						Projectile.frame = 8;
					}
					if (Projectile.frame < 8)
					{
						Projectile.frame = 8;
					}
				}
				else if (Projectile.type == 765)
				{
					Projectile.rotation = Projectile.velocity.Y * 0.05f * (float)Projectile.direction;
					Projectile.frameCounter++;
					if (Projectile.frameCounter >= 8)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame >= 10)
					{
						Projectile.frame = 8;
					}
					if (Projectile.frame < 6)
					{
						Projectile.frame = 6;
					}
				}
				else if (Projectile.type == 314)
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.58f;
					Projectile.frameCounter++;
					if (Projectile.frameCounter >= 3)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 12)
					{
						Projectile.frame = 7;
					}
					if (Projectile.frame < 7)
					{
						Projectile.frame = 7;
					}
				}
				else if (Projectile.type == 319)
				{
					Projectile.rotation = Projectile.velocity.X * 0.05f;
					Projectile.frameCounter++;
					if (Projectile.frameCounter >= 6)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame > 10)
					{
						Projectile.frame = 6;
					}
					if (Projectile.frame < 6)
					{
						Projectile.frame = 6;
					}
				}
				else if (Projectile.type == 210)
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.58f;
					Projectile.frameCounter += 3;
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
				else if (Projectile.type == 313)
				{
					Projectile.position.Y += Projectile.height;
					Projectile.height = 54;
					Projectile.position.Y -= Projectile.height;
					Projectile.position.X += Projectile.width / 2;
					Projectile.width = 54;
					Projectile.position.X -= Projectile.width / 2;
					Projectile.rotation += Projectile.velocity.X * 0.01f;
					Projectile.frameCounter = 0;
					Projectile.frame = 11;
				}
				else if (Projectile.type == 398)
				{
					if ((double)Projectile.velocity.X > 0.5)
					{
						Projectile.spriteDirection = 1;
					}
					else if ((double)Projectile.velocity.X < -0.5)
					{
						Projectile.spriteDirection = -1;
					}
					Projectile.frameCounter++;
					if (Projectile.frameCounter > 1)
					{
						Projectile.frame++;
						Projectile.frameCounter = 0;
					}
					if (Projectile.frame < 6 || Projectile.frame > 9)
					{
						Projectile.frame = 6;
					}
					Projectile.rotation = Projectile.velocity.X * 0.1f;
				}
				else if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
				}
				else
				{
					Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
				}
				if (!flag6 && Projectile.type != 499 && Projectile.type != 765 && Projectile.type != 816 && Projectile.type != 821 && Projectile.type != 825 && Projectile.type != 859 && Projectile.type != 881 && Projectile.type != 884 && Projectile.type != 890 && Projectile.type != 891 && Projectile.type != 900 && Projectile.type != 934 && Projectile.type != 958 && Projectile.type != 959 && Projectile.type != 960 && Projectile.type != 994 && Projectile.type != 998 && Projectile.type != 1003 && Projectile.type != 1004)
				{
					if (Projectile.type == 899)
					{
						int num103 = 6;
						if (Main.rand.Next(4) == 0)
						{
							num103 = 31;
						}
						int num104 = Dust.NewDust(Projectile.Center + new Vector2(-8f, 0f) - Projectile.velocity * 0.25f, 15, 15, num103, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f, 0, default(Color), 1.3f);
						Main.dust[num104].velocity.X = Main.dust[num104].velocity.X * 0.2f;
						Main.dust[num104].velocity.Y = Main.dust[num104].velocity.Y * 0.2f - 0.2f;
						Main.dust[num104].noGravity = true;
					}
					else if (Projectile.type == 897)
					{
						int num105 = 6;
						if (Main.rand.Next(4) == 0)
						{
							num105 = 31;
						}
						int num106 = Dust.NewDust(Projectile.Center + new Vector2(-8f, -8f) - Projectile.velocity * 0.25f, 15, 15, num105, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f, 0, default(Color), 1.3f);
						Main.dust[num106].velocity.X = Main.dust[num106].velocity.X * 0.2f;
						Main.dust[num106].velocity.Y = Main.dust[num106].velocity.Y * 0.2f - 0.2f;
						Main.dust[num106].noGravity = true;
					}
					else if (Projectile.type == 875)
					{
						if (Main.rand.Next(3) == 0)
						{
							Gore.NewGorePerfect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(-10f + (float)Main.rand.Next(-20, 20) * 0.5f, -10f + (float)Main.rand.Next(-20, 20) * 0.5f), Projectile.velocity * 0.1f, 1225, 0.5f + Main.rand.NextFloat() * 1f);
							Vector2 vector9 = Main.rand.NextVector2CircularEdge(2f, 2f) + Projectile.velocity * -0.5f;
							vector9 *= 0.5f;
							int num107 = Dust.NewDust(Projectile.Center - Projectile.velocity * 1.5f - new Vector2(7f, 7f), 15, 15, 226, vector9.X, vector9.Y, 0, default(Color), 0.65f);
							Main.dust[num107].noGravity = true;
						}
					}
					else if (Projectile.type == 860)
					{
						int num108 = Dust.NewDust(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 4f, Projectile.position.Y + (float)(Projectile.height / 2) - 4f) + Projectile.velocity, 8, 8, 27, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f);
						Main.dust[num108].velocity.X = Main.dust[num108].velocity.X * 0.2f;
						Main.dust[num108].velocity.Y = Main.dust[num108].velocity.Y * 0.2f - 0.2f;
						Main.dust[num108].velocity += new Vector2((float)Main.rand.Next(-10, 10) * 0.4f, (float)Main.rand.Next(-10, 10) * 0.4f) * 0.5f;
						Main.dust[num108].noGravity = true;
					}
					else if (Projectile.type == 858)
					{
						int num109 = 6;
						if (Main.rand.Next(4) == 0)
						{
							num109 = 31;
						}
						int num110 = Dust.NewDust(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 4f, Projectile.position.Y + (float)(Projectile.height / 2) - 4f) - Projectile.velocity, 10, 10, num109, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f, 0, default(Color), 1.3f);
						Main.dust[num110].velocity.X = Main.dust[num110].velocity.X * 0.2f;
						Main.dust[num110].velocity.Y = Main.dust[num110].velocity.Y * 0.2f - 0.2f;
						Main.dust[num110].noGravity = true;
					}
					else if (Projectile.type == 112)
					{
						int num111 = 6;
						if (Main.rand.Next(4) == 0)
						{
							num111 = 31;
						}
						int num112 = Dust.NewDust(Projectile.Center + new Vector2(12 * Projectile.spriteDirection, 4f).RotatedBy(Projectile.rotation) + new Vector2(-5f, -5f), 10, 10, num111, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f, 0, default(Color), 1.3f);
						Main.dust[num112].velocity.X = Main.dust[num112].velocity.X * 0.2f;
						Main.dust[num112].velocity.Y = Main.dust[num112].velocity.Y * 0.2f - 0.2f;
						Main.dust[num112].noGravity = true;
					}
					else if (Projectile.type == 854)
					{
						if (Main.rand.Next(6) == 0)
						{
							Vector2 vector10 = Projectile.Center / 16f;
							int i2 = (int)vector10.X;
							int num113 = (int)vector10.Y;
							Tile tileSafely = Framing.GetTileSafely(i2, num113 + 1);
							if (!WorldGen.SolidTile(tileSafely) && tileSafely.LiquidType == LiquidID.Water)
							{
								Gore gore = Gore.NewGorePerfect(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2((float)Main.rand.Next(-30, 30) * 0.5f, (float)Main.rand.Next(-30, 30) * 0.5f), Projectile.velocity * -0.2f, 910);
								gore.Frame.CurrentColumn = 0;
								gore.timeLeft = 1;
							}
						}
					}
					else if (Projectile.type != 398 && Projectile.type != 390 && Projectile.type != 391 && Projectile.type != 392 && Projectile.type != 127 && Projectile.type != 200 && Projectile.type != 208 && Projectile.type != 210 && Projectile.type != 236 && Projectile.type != 266 && Projectile.type != 268 && Projectile.type != 269 && Projectile.type != 313 && Projectile.type != 314 && Projectile.type != 319 && Projectile.type != 324 && Projectile.type != 334 && Projectile.type != 353)
					{
						int num114 = Dust.NewDust(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 4f, Projectile.position.Y + (float)(Projectile.height / 2) - 4f) - Projectile.velocity, 8, 8, 16, (0f - Projectile.velocity.X) * 0.5f, Projectile.velocity.Y * 0.5f, 50, default(Color), 1.7f);
						Main.dust[num114].velocity.X = Main.dust[num114].velocity.X * 0.2f;
						Main.dust[num114].velocity.Y = Main.dust[num114].velocity.Y * 0.2f;
						Main.dust[num114].noGravity = true;
					}
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
						if (Projectile.type >= 390 && Projectile.type <= 392)
						{
							num158 = 500f;
							if ((double)Projectile.position.Y > Main.worldSurface * 16.0)
							{
								num158 = 250f;
							}
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
						if (Projectile.type >= 390 && Projectile.type <= 392 && Projectile.localAI[1] > 0f)
						{
							flag15 = true;
							Projectile.localAI[1] -= 1f;
						}
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
				else if (Projectile.type >= 390 && Projectile.type <= 392)
				{
					int num162 = (int)(Projectile.Center.X / 16f);
					int num163 = (int)(Projectile.Center.Y / 16f);
					if (Main.tile[num162, num163] != null && Main.tile[num162, num163].WallType > 0)
					{
						flag2 = (flag3 = false);
					}
				}
				if (Projectile.type == 127)
				{
					if ((double)Projectile.rotation > -0.1 && (double)Projectile.rotation < 0.1)
					{
						Projectile.rotation = 0f;
					}
					else if (Projectile.rotation < 0f)
					{
						Projectile.rotation += 0.1f;
					}
					else
					{
						Projectile.rotation -= 0.1f;
					}
				}
				else if (Projectile.type != 313 && !flag14)
				{
					Projectile.rotation = 0f;
				}
				if (Projectile.type < 390 || Projectile.type > 392)
				{
					Projectile.tileCollide = true;
				}
				float num164 = 0.08f;
				float num165 = 6.5f;
				if (Projectile.type == 127)
				{
					num165 = 2f;
					num164 = 0.04f;
				}
				if (Projectile.type == 112)
				{
					num165 = 6f;
					num164 = 0.06f;
				}
				if (Projectile.type == 334)
				{
					num165 = 8f;
					num164 = 0.08f;
				}
				if (Projectile.type == 268)
				{
					num165 = 8f;
					num164 = 0.4f;
				}
				if (Projectile.type == 324)
				{
					num164 = 0.1f;
					num165 = 3f;
				}
				if (Projectile.type == 858)
				{
					num164 = 0.3f;
					num165 = 7f;
				}
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
				if (Projectile.type == 875)
				{
					num165 = 7f;
					num164 = 0.25f;
					if (num165 < Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y))
					{
						num165 = Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y);
						num164 = 0.35f;
					}
				}
				if (Projectile.type >= 390 && Projectile.type <= 392)
				{
					num164 *= 2f;
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
				if (Projectile.type == 208)
				{
					Projectile.velocity.X *= 0.95f;
					if ((double)Projectile.velocity.X > -0.1 && (double)Projectile.velocity.X < 0.1)
					{
						Projectile.velocity.X = 0f;
					}
					flag2 = false;
					flag3 = false;
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
				if (Projectile.type == 268 && Projectile.frameCounter < 10)
				{
					flag5 = false;
				}
				if (Projectile.type == 860 && Projectile.velocity.X != 0f)
				{
					flag5 = true;
				}
				if ((Projectile.type == 881 || Projectile.type == 934) && Projectile.velocity.X != 0f)
				{
					flag5 = true;
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
							if (Projectile.type == 200)
							{
								Projectile.velocity.Y = -3.1f;
							}
							else
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
					else if (Projectile.type == 266 && (flag2 || flag3))
					{
						Projectile.velocity.Y -= 6f;
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
				if (Projectile.type == 398 || Projectile.type == 958 || Projectile.type == 960 || Projectile.type == 956 || Projectile.type == 959 || Projectile.type == 994)
				{
					Projectile.spriteDirection = Projectile.direction;
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
