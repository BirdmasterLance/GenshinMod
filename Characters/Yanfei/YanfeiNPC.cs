using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Yanfei
{
    internal class YanfeiNPC : ModNPC
    {

        private bool checkedOwner = false;
        private Player Owner = null;
        private int timer = 0;

        // Energy Variables
        public ref float Energy => ref NPC.ai[2]; // Using ai[2] to store NPC energy
        public int BurstCost = 80; // Yanfei requires 80 energy to use her burst

        // Using this to keep track of what attack Yanfei should be doing
        private enum ActionState
        {
            None, // This one is for moving or idling
            NormalAttack,
            ChargedAttack,
            Skill,
            Burst
        }
        private ActionState state;

        // Like ExampleMod, we are going to use an enum to keep track of Frames in an enum
        // so that the code is much easier to read.
        private enum Frame
        {
            Idle,
            Walking1,
            Walking2,
            Walking3,
            Walking4,
            Jumping,
            Falling,
            NormalAttack1,
            NormalAttack2,
            NormalAttack3
        }

        public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 10;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				// Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
            AnimationType = NPCID.Stylist;
            NPC.width = 40;
            NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.friendly = true;
            NPC.damage = 37;
            NPC.defense = 200;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.knockBackResist = 0f;
            NPC.value = 1000f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.dontTakeDamage = false;
            NPC.homeless = true;
            TownNPCStayingHomeless = true;

            NPC.dontCountMe = true;
            //NPC.rarity = 1;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Yanfei from Genshin Impact!!"),
			});
		}

        float frameCounter = 0; // I cannot get NPC.frameCounter to work, so we'll make a variable for it instead
        public override void FindFrame(int frameHeight)
        {
            // This makes the sprite flip horizontally in conjunction with the npc.direction.
            NPC.spriteDirection = NPC.direction;

            //For the most part, our animation matches up with our states.
            switch (state)
            {
                case ActionState.NormalAttack:
                    frameCounter++;

                    if (frameCounter < 8)
                    {
                        NPC.frame.Y = (int)Frame.NormalAttack1 * frameHeight;
                    }
                    else if (frameCounter < 16)
                    {
                        NPC.frame.Y = (int)Frame.NormalAttack2 * frameHeight;
                    }
                    else if (frameCounter < 24)
                    {
                        NPC.frame.Y = (int)Frame.NormalAttack3 * frameHeight;
                    }
                    else
                    {
                        frameCounter = 0;
                    }
                    break;
                case ActionState.None:
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.frame.Y = (int)Frame.Jumping * frameHeight;
                    }
                    else if (NPC.velocity.Y < 0)
                    {
                        NPC.frame.Y = (int)Frame.Falling * frameHeight;
                    }
                    else
                    {
                        if (NPC.velocity.X == 0)
                        {
                            NPC.frame.Y = (int)Frame.Idle * frameHeight;
                            frameCounter = 0;
                        }
                        else
                        {
                            frameCounter++;
                            if (frameCounter < 10)
                            {
                                NPC.frame.Y = (int)Frame.Walking1 * frameHeight;
                            }
                            else if (frameCounter < 20)
                            {
                                NPC.frame.Y = (int)Frame.Walking2 * frameHeight;
                            }
                            else if (frameCounter < 30)
                            {
                                NPC.frame.Y = (int)Frame.Walking3 * frameHeight;
                            }
                            else if (frameCounter < 40)
                            {
                                NPC.frame.Y = (int)Frame.Walking4 * frameHeight;
                            }
                            else
                            {
                                frameCounter = 0;
                            }
                        }
                    }
                    break;
            }
        }

        public override void AI()
        {
			// Might not work in multiplayer?;
			PlayerCharacterCode modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
			for(int i = 0; i < modPlayer.GetActiveCharacters().Count; i++)
            {
				if (NPC.whoAmI == modPlayer.GetActiveCharacters()[i].GetNPCID())
                {
					modPlayer.GetActiveCharacters()[i].life = NPC.life;
                    NPC.damage = modPlayer.GetActiveCharacters()[i].damage;
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Owner.dead || !Owner.active)
            {
                NPC.TargetClosest();
            }
            Player owner = Main.player[NPC.target];
            if (!checkedOwner)
            {
                Owner = owner;
                checkedOwner = true;
            }
            AI_026();
            if (NPC.Distance(Owner.Center) > 1000f)
            {
                NPC.position = Owner.position;
                for (int k = 0; k < /*num*/21; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.ManaRegeneration);
                    SoundEngine.PlaySound(SoundID.Item154);
                }

            }

            if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) && Owner.Bottom.Y < NPC.Bottom.Y)
            {
                NPC.velocity.Y -= 0.4f;
            }

            Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num36 = Owner.position.X + (float)(Owner.width / 2) - vector3.X;
            float num37 = Owner.position.Y + (float)(Owner.height / 2) - vector3.Y;
            //if (Projectile.type != 892 && Projectile.type != 894)
            //{
            num37 += (float)Main.rand.Next(-10, 21);
            num36 += (float)Main.rand.Next(-10, 21);
            //}
            num36 += (float)(60 * -Owner.direction);
            num37 -= 60f;
            if (0 < (float)100 && Owner.velocity.Y == 0f && NPC.position.Y + (float)NPC.height <= Owner.position.Y + (float)Owner.height && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.ai[0] = 0f;
                if (NPC.velocity.Y < -6f)
                {
                    NPC.velocity.Y = -6f;
                }
            }
        }

        private void AI_026()
        {
            if (!Owner.active/*Owner*/)
            {
                NPC.active = false;
                return;
            }

            // Not entirely sure if AI_26 always targets closest NPC, so we will save the NPC it does target separately
            NPC target = null;

            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            int num = 85;
            //bool flag6 = NPC.type >= 191 && NPC.type <= 194;

            if (NPC.lavaWet)
            {
                NPC.ai[0] = 1f;
                NPC.ai[1] = 0f;
            }
            //num = 60 + 30 * 0;

            bool flag7 = NPC.ai[0] == -1f || NPC.ai[0] == -2f;
            bool num2 = NPC.ai[0] == -1f;
            bool flag8 = NPC.ai[0] == -2f;

            //if (Owner.dead)
            //{
            //    Owner.pygmy = false;
            //}
            //if (Owner.pygmy)
            //{
            //    NPC.timeLeft = Main.rand.Next(2, 10);
            //}

            //if (flag7)
            //{
            //    NPC.timeLeft = 2;
            //}

            num = 10;
            int num3 = 40 * (0 + 1) * Owner.direction;
            if (Owner.position.X + (float)(Owner.width / 2) < NPC.position.X + (float)(NPC.width / 2) - (float)num + (float)num3)
            {
                flag2 = true;
            }
            else if (Owner.position.X + (float)(Owner.width / 2) > NPC.position.X + (float)(NPC.width / 2) + (float)num + (float)num3)
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

            bool flag11 = NPC.ai[1] == 0f;
            if (flag)
            {
                flag11 = true;
            }
            if (flag11)
            {
                int num78 = 500;

                num78 += 40 * 0;
                if (NPC.localAI[0] > 0f)
                {
                    num78 += 500;
                }

                if (Owner.rocketDelay2 > 0)
                {
                    NPC.ai[0] = 1f;
                }
                Vector2 vector7 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num79 = Owner.position.X + (float)(Owner.width / 2) - vector7.X;
                float num80 = Owner.position.Y + (float)(Owner.height / 2) - vector7.Y;
                float num81 = (float)Math.Sqrt(num79 * num79 + num80 * num80);
                if (!flag7)
                {
                    if (num81 > 2000f)
                    {
                        NPC.position.X = Owner.position.X + (float)(Owner.width / 2) - (float)(NPC.width / 2);
                        NPC.position.Y = Owner.position.Y + (float)(Owner.height / 2) - (float)(NPC.height / 2);
                    }
                }
            }
            if (NPC.ai[0] != 0f && !flag7)
            {
                //Main.NewText("returning to player");
                float num83 = 0.2f;
                int num84 = 200;

                num83 = 0.5f;
                num84 = 100;

                NPC.noTileCollide = true;
                Vector2 vector8 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num85 = Owner.position.X + (float)(Owner.width / 2) - vector8.X;

                num85 -= (float)(40 * Owner.direction);
                float num86 = 700f;
                num86 += 100f;
                //Console.WriteLine("NOT An opposition");
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
                    if (Math.Abs(Owner.position.X + (float)(Owner.width / 2) - num89) + Math.Abs(Owner.position.Y + (float)(Owner.height / 2) - num90) < num86)
                    {
                        if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.npc[num88].position, Main.npc[num88].width, Main.npc[num88].height))
                        {
                            num87 = num88;
                        }
                        flag12 = true;
                        break;
                    }
                }


                if (!flag12)
                {
                    num85 -= (float)(40 * 0 * Owner.direction);
                }
                if (flag12 && num87 >= 0)
                {
                    NPC.ai[0] = 0f;
                }

                float num91 = Owner.position.Y + (float)(Owner.height / 2) - vector8.Y;
                float num92 = (float)Math.Sqrt(num85 * num85 + num91 * num91);
                float num93 = num92;
                float num94 = 10f;
                float num95 = num92;

                num83 = 0.4f;
                num94 = 12f;

                if (num94 < Math.Abs(Owner.velocity.X) + Math.Abs(Owner.velocity.Y))
                {
                    num94 = Math.Abs(Owner.velocity.X) + Math.Abs(Owner.velocity.Y);
                }

                if (num92 < (float)num84 && Owner.velocity.Y == 0f && NPC.position.Y + (float)NPC.height <= Owner.position.Y + (float)Owner.height && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[0] = 0f;
                    if (NPC.velocity.Y < -6f)
                    {
                        NPC.velocity.Y = -6f;
                    }
                }
                if (num92 < 60f)
                {
                    num85 = NPC.velocity.X;
                    num91 = NPC.velocity.Y;
                }
                else
                {
                    num92 = num94 / num92;
                    num85 *= num92;
                    num91 *= num92;
                }

                if (NPC.velocity.X < num85)
                {
                    NPC.velocity.X += num83;
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X += num83 * 1.5f;
                    }
                }
                if (NPC.velocity.X > num85)
                {
                    NPC.velocity.X -= num83;
                    if (NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X -= num83 * 1.5f;
                    }
                }
                if (NPC.velocity.Y < num91)
                {
                    NPC.velocity.Y += num83;
                    if (NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y += num83 * 1.5f;
                    }
                }
                if (NPC.velocity.Y > num91)
                {
                    NPC.velocity.Y -= num83;
                    if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y -= num83 * 1.5f;
                    }
                }

                //if (NPC.frame < 12)
                //{
                //    NPC.frame = Main.rand.Next(12, 18);
                //    NPC.frameCounter = 0;
                //}

                if ((double)NPC.velocity.X > 0.5)
                {
                    NPC.spriteDirection = -1;
                }
                else if ((double)NPC.velocity.X < -0.5)
                {
                    NPC.spriteDirection = 1;
                }

                if (NPC.spriteDirection == -1)
                {
                    NPC.rotation = NPC.velocity.X * -0.05f;/*(float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);*/
                }
                else
                {
                    NPC.rotation = NPC.velocity.X * 0.05f; /*(float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 3.14f;*/
                }
            }
            else // Code for moving and attacking
            {
                float num115 = 0;
                int num116 = 30;
                int num117 = 60;
                NPC.localAI[0] -= 1f;

                if (NPC.localAI[0] < 0f)
                {
                    NPC.localAI[0] = 0f;
                }
                if (NPC.ai[1] > 0f)
                {
                    NPC.ai[1] -= 1f;
                }
                else
                {
                    float targetX = NPC.position.X;
                    float targetY = NPC.position.Y;
                    float num120 = 100000f;
                    float num121 = num120;
                    int targetID = -1;
                    float aimOffset = 20f; // I think this is used to predict where the target will be
                    //NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
                    //if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this))
                    //{
                    //    float num124 = ownerMinionAttackTargetNPC.position.X + (float)(ownerMinionAttackTargetNPC.width / 2);
                    //    float num125 = ownerMinionAttackTargetNPC.position.Y + (float)(ownerMinionAttackTargetNPC.height / 2);
                    //    float num126 = Math.Abs(NPC.position.X + (float)(NPC.width / 2) - num124) + Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - num125);
                    //    if (num126 < num120)
                    //    {
                    //        if (num122 == -1 && num126 <= num121)
                    //        {
                    //            num121 = num126;
                    //            num118 = num124;
                    //            num119 = num125;
                    //        }
                    //        if (Collision.CanHit(NPC.position, NPC.width, NPC.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                    //        {
                    //            num120 = num126;
                    //            num118 = num124;
                    //            num119 = num125;
                    //            num122 = ownerMinionAttackTargetNPC.whoAmI;
                    //        }
                    //    }
                    //}

                    if (targetID == -1)
                    {
                        // Loop for finding the nearest target
                        for (int num127 = 0; num127 < 200; num127++)
                        {
                            if (!Main.npc[num127].CanBeChasedBy(this))
                            {
                                continue;
                            }

                            float targetCenterX = Main.npc[num127].position.X + (Main.npc[num127].width / 2);
                            float targetCenterY = Main.npc[num127].position.Y + (Main.npc[num127].height / 2);
                            float distanceToTarget = Math.Abs(NPC.position.X + (NPC.width / 2) - targetCenterX) + Math.Abs(NPC.position.Y + (NPC.height / 2) - targetCenterY);
                            if (distanceToTarget < num120)
                            {
                                // I think it multiplies the target's velocity with an offset
                                // to predict where the target will be and aim accordingly
                                if (targetID == -1 && distanceToTarget <= num121)
                                {
                                    num121 = distanceToTarget;
                                    targetX = targetCenterX + Main.npc[num127].velocity.X * aimOffset;
                                    targetY = targetCenterY + Main.npc[num127].velocity.Y * aimOffset;
                                }
                                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.npc[num127].position, Main.npc[num127].width, Main.npc[num127].height))
                                {
                                    num120 = distanceToTarget;
                                    targetX = targetCenterX + Main.npc[num127].velocity.X * aimOffset;
                                    targetY = targetCenterY + Main.npc[num127].velocity.Y * aimOffset;
                                    targetID = num127;
                                }
                            }
                        }
                    }

                    if (targetID == -1 && num121 < num120)
                    {
                        num120 = num121;
                    }
                    float num131 = 400f;
                    if (NPC.position.Y > Main.worldSurface * 16.0)
                    {
                        num131 = 200f;
                    }
                    if (num120 < num131 + num115 && targetID == -1)
                    {
                        float horizontalDistanceToTarget = targetX - (NPC.position.X + (float)(NPC.width / 2));
                        if (horizontalDistanceToTarget < -5f)
                        {
                            flag2 = true;
                            flag3 = false;
                        }
                        else if (horizontalDistanceToTarget > 5f)
                        {
                            flag3 = true;
                            flag2 = false;
                        }
                    }
                    else if (targetID >= 0 && num120 < 800f + num115)
                    {
                        NPC.localAI[0] = num117;
                        float horizontalDistanceToTarget = targetX - (NPC.position.X + (NPC.width / 2));
                        if (horizontalDistanceToTarget > 450f || horizontalDistanceToTarget < -450f)
                        {
                            if (horizontalDistanceToTarget < -50f)
                            {
                                flag2 = true;
                                flag3 = false;
                            }
                            else if (horizontalDistanceToTarget > 50f)
                            {
                                flag3 = true;
                                flag2 = false;
                            }
                        }
                        else if (Owner.whoAmI == Main.myPlayer)
                        {
                            NPC.ai[1] = num116;
                            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + (NPC.height / 2) - 8f);
                            int flameDust = Dust.NewDust(npcCenter, NPC.width, NPC.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
                            Main.dust[flameDust].noGravity = true;

                            float num134 = targetX - npcCenter.X + Main.rand.Next(-20, 21);
                            float num135 = Math.Abs(num134) * 0.1f;
                            num135 = num135 * Main.rand.Next(0, 100) * 0.001f;
                            float num136 = targetY - npcCenter.Y + Main.rand.Next(-20, 21) - num135;
                            float num137 = (float)Math.Sqrt(num134 * num134 + num136 * num136);
                            num137 = 11f / num137;
                            num134 *= num137;
                            num136 *= num137;

                            timer++;

                            // Using else cases because i only want one condition to be true at a time
                            // Uses buffs as cooldown
                            // Ordered from most important attack to least important
                            if (Energy >= BurstCost && !NPC.HasBuff(ModContent.BuffType<DoneDealCooldown>()))
                            {
                                int num138 = NPC.damage;
                                int num139 = ModContent.ProjectileType<YanfeiBurst>();
                                //int num140 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector11.X, vector11.Y, num134, num136, num139, num138, 0f, Main.myPlayer);
                                var Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), npcCenter, Vector2.Zero, num139, num138, 0f, Main.myPlayer, ai1: NPC.whoAmI);
                                NPC.AddBuff(ModContent.BuffType<BrillianceBuff>(), 900);
                                NPC.AddBuff(ModContent.BuffType<SignedEdictCooldown>(), 1200);
                                Energy = 0;
                                state = ActionState.Burst;
                            }

                            else if (!NPC.HasBuff(ModContent.BuffType<SignedEdictCooldown>()))
                            {
                                int num138 = NPC.damage;
                                int num139 = ModContent.ProjectileType<YanfeiSkill>();
                                var Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), Main.npc[targetID].position, Vector2.Zero, num139, num138, 0f, Main.myPlayer, ai1: NPC.whoAmI);
                                NPC.AddBuff(ModContent.BuffType<SignedEdictCooldown>(), 540);
                                state = ActionState.Skill;
                            }

                            else if(NPC.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
                            {
                                int num138 = NPC.damage;
                                int num139 = ModContent.ProjectileType<YanfeiCharged>();
                                //int num140 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector11.X, vector11.Y, num134, num136, num139, num138, 0f, Main.myPlayer);
                                var Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), Main.npc[targetID].position + new Vector2(0, -150), Vector2.Zero, num139, num138, 0f, Main.myPlayer, ai1: NPC.whoAmI);
                                state = ActionState.ChargedAttack;
                            }

                            else if (timer % 2 == 0)
                            {
                                int num138 = NPC.damage;
                                int num139 = ModContent.ProjectileType<YanfeiProjectile>();
                                //int num140 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector11.X, vector11.Y, num134, num136, num139, num138, 0f, Main.myPlayer);
                                var Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), npcCenter, Vector2.Zero, num139, num138, 0f, Main.myPlayer, ai1: NPC.whoAmI);
                                state = ActionState.NormalAttack;
                                //Proj.position = new Vector2(num118, num119);
                                timer = 0;
                            }

                            //Main.NPC[num140].timeLeft = 300;
                            if (num134 < 0f)
                            {
                                NPC.direction = -1;
                            }
                            if (num134 > 0f)
                            {
                                NPC.direction = 1;
                            }
                            NPC.netUpdate = true;
                        }
                    }
                }

                bool flag13 = false;
                Vector2 vector12 = Vector2.Zero;
                bool flag14 = false;

                if (NPC.ai[1] != 0f)
                {
                    flag2 = false;
                    flag3 = false;
                }
                else if (NPC.localAI[0] == 0f)
                {
                    NPC.direction = Owner.direction;
                }
                if (!flag14)
                {
                    NPC.rotation = 0f;
                }
                NPC.noTileCollide = false;

                float num164 = 6f;
                float num165 = 0.2f;

                if (num165 < Math.Abs(Owner.velocity.X) + Math.Abs(Owner.velocity.Y))
                {
                    num165 = Math.Abs(Owner.velocity.X) + Math.Abs(Owner.velocity.Y);
                    num164 = 0.3f;
                }
                num164 *= 2f;

                if (flag7)
                {
                    num165 = 6f;
                }
                if (flag2)
                {
                    if ((double)NPC.velocity.X > -3.5)
                    {
                        NPC.velocity.X -= num164;
                    }
                    else
                    {
                        NPC.velocity.X -= num164 * 0.25f;
                    }
                }
                else if (flag3)
                {
                    if ((double)NPC.velocity.X < 3.5)
                    {
                        NPC.velocity.X += num164;
                    }
                    else
                    {
                        NPC.velocity.X += num164 * 0.25f;
                    }
                }
                else
                {
                    NPC.velocity.X *= 0.9f;
                    if (NPC.velocity.X >= 0f - num164 && NPC.velocity.X <= num164)
                    {
                        NPC.velocity.X = 0f;
                    }
                }
                if (flag2 || flag3)
                {
                    int num166 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
                    int j2 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
                    if (NPC.type == 236)
                    {
                        num166 += NPC.direction;
                    }
                    if (flag2)
                    {
                        num166--;
                    }
                    if (flag3)
                    {
                        num166++;
                    }
                    num166 += (int)NPC.velocity.X;
                    if (WorldGen.SolidTile(num166, j2))
                    {
                        flag5 = true;
                    }
                }
                if (Owner.position.Y + (float)Owner.height - 8f > NPC.position.Y + (float)NPC.height)
                {
                    flag4 = true;
                }
                Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
                if (NPC.velocity.Y == 0f)
                {
                    if (!flag4 && (NPC.velocity.X < 0f || NPC.velocity.X > 0f))
                    {
                        int num167 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
                        int j3 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16 + 1;
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
                        int num168 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
                        int num169 = (int)(NPC.position.Y + (float)NPC.height) / 16;
                        if (WorldGen.SolidTileAllowBottomSlope(num168, num169) || Main.tile[num168, num169].IsHalfBlock || Main.tile[num168, num169].Slope > 0 || NPC.type == 200)
                        {

                            try
                            {
                                num168 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
                                num169 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
                                if (flag2)
                                {
                                    num168--;
                                }
                                if (flag3)
                                {
                                    num168++;
                                }
                                num168 += (int)NPC.velocity.X;
                                if (!WorldGen.SolidTile(num168, num169 - 1) && !WorldGen.SolidTile(num168, num169 - 2))
                                {
                                    NPC.velocity.Y = -5.1f;
                                }
                                else if (!WorldGen.SolidTile(num168, num169 - 2))
                                {
                                    NPC.velocity.Y = -7.1f;
                                }
                                else if (WorldGen.SolidTile(num168, num169 - 5))
                                {
                                    NPC.velocity.Y = -11.1f;
                                }
                                else if (WorldGen.SolidTile(num168, num169 - 4))
                                {
                                    NPC.velocity.Y = -10.1f;
                                }
                                else
                                {
                                    NPC.velocity.Y = -9.1f;
                                }
                            }
                            catch
                            {
                                NPC.velocity.Y = -9.1f;
                            }

                        }
                    }
                }
                if (NPC.velocity.X > num165)
                {
                    NPC.velocity.X = num165;
                }
                if (NPC.velocity.X < 0f - num165)
                {
                    NPC.velocity.X = 0f - num165;
                }
                if (NPC.velocity.X < 0f)
                {
                    NPC.direction = -1;
                }
                if (NPC.velocity.X > 0f)
                {
                    NPC.direction = 1;
                }
                if (NPC.velocity.X > num164 && flag3)
                {
                    NPC.direction = 1;
                }
                if (NPC.velocity.X < 0f - num164 && flag2)
                {
                    NPC.direction = -1;
                }

                if (NPC.direction == -1)
                {
                    NPC.spriteDirection = 1;
                }
                if (NPC.direction == 1)
                {
                    NPC.spriteDirection = -1;
                }

                bool flag16 = NPC.position.X - NPC.oldPosition.X == 0f;

                if (NPC.ai[1] > 0f)
                {
                    if (NPC.localAI[1] == 0f)
                    {
                        NPC.localAI[1] = 1f;
                        //NPC.frame = 1;
                    }
                    //        if (NPC.frame != 0)
                    //        {
                    //            NPC.frameCounter++;
                    //            if (NPC.frameCounter > 4)
                    //            {
                    //                NPC.frame++;
                    //                NPC.frameCounter = 0;
                    //            }
                    //            if (NPC.frame >= 4)
                    //            {
                    //                NPC.frame = 0;
                    //            }
                    //        }
                }
                else if (NPC.velocity.Y == 0f)
                {
                    NPC.localAI[1] = 0f;
                    //        if (flag16)
                    //        {
                    //            NPC.frame = 0;
                    //            NPC.frameCounter = 0;
                    //        }
                    //        else if ((double)NPC.velocity.X < -0.8 || (double)NPC.velocity.X > 0.8)
                    //        {
                    //            NPC.frameCounter += (int)Math.Abs(NPC.velocity.X);
                    //            NPC.frameCounter++;
                    //            if (NPC.frameCounter > 6)
                    //            {
                    //                NPC.frame++;
                    //                NPC.frameCounter = 0;
                    //            }
                    //            if (NPC.frame < 5)
                    //            {
                    //                NPC.frame = 5;
                    //            }
                    //            if (NPC.frame >= 11)
                    //            {
                    //                NPC.frame = 5;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Projectile.frame = 0;
                    //            NPC.frameCounter = 0;
                    //        }
                    //}
                    //    else if (NPC.velocity.Y < 0f)
                    //    {
                    //        NPC.frameCounter = 0;
                    //        NPC.frame = 4;
                    //    }
                    //    else if (NPC.velocity.Y > 0f)
                    //    {
                    //        NPC.frameCounter = 0;
                    //        NPC.frame = 4;
                    //    }
                    NPC.velocity.Y += 0.4f;
                    if (NPC.velocity.Y > 10f)
                    {
                        NPC.velocity.Y = 10f;
                    }
                    _ = NPC.velocity;

                }
            }
        }

    }
}
