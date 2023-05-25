using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using System.Numerics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;



namespace Hh1.NPCH
{
    public class ExampleGlobalNPC : /*Global*/ModNPC
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Chest;

        // Here we define an enum we will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
        private enum ActionState
        {

            Crawl,
            Notice,
            Jump,
            Hover,
            Fall,
            Fly

        }

        // Our texture is 36x36 with 2 pixels of padding vertically, so 38 is the vertical spacing.
        // These are for our benefit and the numbers could easily be used directly in the code below, but this is how we keep code organized.
        private enum Frame
        {
            Crawl,
            Notice,
            Falling,
            Flutter1,
            Flutter2,
            Flutter3
        }
        // These are reference properties. One, for example, lets us write AI_State as if it's NPC.ai[0], essentially giving the index zero our own name.
        // Here they help to keep our AI code clear of clutter. Without them, every instance of "AI_State" in the AI code below would be "npc.ai[0]", which is quite hard to read.
        // This is all to just make beautiful, manageable, and clean code.
        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_FlutterTime => ref NPC.ai[2];

        public override void AI()
        {
            foreach (Player p in Main.player)
            {
                Rectangle hitbox = NPC.Hitbox;

                if (hitbox.Intersects(p.Hitbox))
                {

                    NPC.velocity = new Vector2(NPC.direction * 2 * -1, -2f);


                }
            }

            // The npc starts in the asleep state, waiting for a player to enter range
            switch (AI_State)
            {
                case (float)ActionState.Crawl:
                    FallCrawl();
                    break;
                case (float)ActionState.Notice:
                    Notice();
                    break;
                case (float)ActionState.Jump:
                    Jump();
                    break;
                case (float)ActionState.Hover:
                    Hover();
                    break;
                case (float)ActionState.Fall:
                    if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 0;
                        AI_State = (float)ActionState.Crawl;
                        AI_Timer = 0;
                    }

                    break;
                case (float)ActionState.Fly:
                    Fly();
                    break;

            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flutter Slime"); // Automatic from localization files
            Main.npcFrameCount[NPC.type] = 6; // make sure to set this for your modnpcs.

            // Specify the debuffs it is immune to
            NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned // This NPC will be immune to the Poisoned debuff.
				}
            });
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 30;
            NPC.aiStyle = -1;
            //NPC.friendly = false;
            NPC.damage = 5;
            NPC.defense = 8;
            NPC.lifeMax = 50502;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 0f;
            NPC.value = 1000f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            //NPC.dontTakeDamage = true;
            NPC.rarity = 1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            // Spawn this NPC with something like Cheat Sheet or Hero's Mod
            return Terraria.ModLoader.Utilities.SpawnCondition.Overworld.Chance * 0.5f;

            if (/*Main.eclipse || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0&&*/ spawnInfo.Player.ZoneSkyHeight)
            {
                return Terraria.ModLoader.Utilities.SpawnCondition.Overworld.Chance * 0.5f;
            }

            //CanSpawnNow();
        }

        // Here, because we use custom AI (aiStyle not set to a suitable vanilla value), we should manually decide when Flutter Slime can fall through platforms
        public override bool? CanFallThroughPlatforms()
        {
            if (AI_State == (float)ActionState.Fall && NPC.HasValidTarget && Main.player[NPC.target].Top.Y > NPC.Bottom.Y)
            {
                // If Flutter Slime is currently falling, we want it to keep falling through platforms as long as it's above the player
                return true;
            }

            return false;
            // You could also return null here to apply vanilla behavior (which is the same as false for custom AI)
        }

        private void FallCrawl()
        {
            // TargetClosest sets npc.target to the player.whoAmI of the closest player.
            // The faceTarget parameter means that npc.direction will automatically be 1 or -1 if the targeted player is to the right or left.
            // This is also automatically flipped if npc.confused.
            NPC.TargetClosest(true);

            // Now we check the make sure the target is still valid and within our specified notice range (500)
            if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 500f)
            {
                // Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
                AI_State = (float)ActionState.Notice;
                AI_Timer = 0;
            }
        }
        private void Fly()
        {
            // If the targeted player is in attack range (250).
            if (Main.player[NPC.target].Distance(NPC.Center) < 1000f)
            {
                AI_Timer++; //NPC.velocity = new Vector2(0, +5f);

                float speed = 10f;
                float inertia = 5f;

                Vector2 RelPosPlayer/*abovePlayer*/ = Main.player[NPC.target].position;

                Vector2 /*toAbovePlayer*/toRelPosPlayer = RelPosPlayer/*abovePlayer*/ - NPC.Center;

                Vector2 toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ = toRelPosPlayer.SafeNormalize(Vector2.UnitY);
                Vector2 moveTo = toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ * speed;
                //NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
                NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
            }


            // Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
            //    if (AI_Timer == 1)
            //    {
            //        NPC.velocity = new Vector2(NPC.direction * 2, -2f);

            //    }


            //    if (AI_Timer >= 20)
            //    {
            //        AI_State = (float)ActionState.Jump;
            //        AI_Timer = 0;
            //    }
            ////}
            //else
            //{
            //    NPC.TargetClosest(true);

            //    if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 500fMain.player[NPC.target])
            //    {
            //        //Out targeted player seems to have left our range, so we'll go back to sleep.
            //        AI_State = (float)ActionState.Crawl;
            //        AI_Timer = 0;
            //    }
            //}
        }

        private void Notice()
        {
            // If the targeted player is in attack range (250).
            if (Main.player[NPC.target].Distance(NPC.Center) < 500f)
            {
                AI_Timer++;
                // Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
                if (AI_Timer == 1)
                {
                    NPC.velocity = new Vector2(NPC.direction * 2, -2f);

                }


                if (AI_Timer >= 20)
                {
                    AI_State = (float)ActionState.Jump;
                    AI_Timer = 0;
                }
            }
            else
            {
                NPC.TargetClosest(true);

                if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 500f)
                {
                    // Out targeted player seems to have left our range, so we'll go back to sleep.
                    AI_State = (float)ActionState.Crawl;
                    AI_Timer = 0;
                }
            }
        }

        private void Jump()
        {
            AI_Timer++;

            if (AI_Timer % 10 == 0 && Main.player[NPC.target].Distance(NPC.Center) <= 250f && Main.player[NPC.target].Distance(NPC.Center) >= 64f)
            {
                // We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.
                if (Main.player[NPC.target].position.X > NPC.position.X)
                {
                    NPC.velocity = new Vector2(NPC.direction * 5, -5f);
                }
                else
                {
                    NPC.velocity = new Vector2(NPC.direction * 5, -5f);
                }

            }
            if (AI_Timer % 11 == 0)
            {
                NPC.velocity = new Vector2(NPC.direction * 2, +50f);
            }
            if (Main.player[NPC.target].Distance(NPC.Center) > 250f)
            {
                AI_State = (float)ActionState.Fall;
                AI_Timer = 0;
            }


            if (Main.player[NPC.target].Distance(NPC.Center) < 64f)
            {
                // after .66 seconds, we go to the hover state. //TODO, gravity?
                AI_State = (float)ActionState.Hover;
                AI_Timer = 0;
            }
        }
        int conter = 0;
        private void Hover()
        {
            AI_Timer++;
            if (Main.player[NPC.target].Distance(NPC.Center) <= 64f)
            {

                // after .66 seconds, we go to the hover state. //TODO, gravity?

                if (AI_Timer == 1)// Just in one tick, it will rise, then, in half a second (30 ticks) 
                {
                    NPC.velocity = new Vector2(0, -5f);
                }
                if (AI_Timer == 5 /*&& Main.player[NPC.target].Distance(NPC.Center) < 64f*/)
                {
                    NPC.velocity = new Vector2(NPC.direction * 7, 2f);
                }
                if (NPC.collideY && AI_Timer > 5)
                {

                    AI_Timer = 0;

                }
            }
            else if (Main.player[NPC.target].Distance(NPC.Center) > 64f)
            {
                AI_State = (float)ActionState.Fall;
                AI_Timer = 0;
            }
            Console.WriteLine(AI_Timer);


            // Here we make a decision on how long this flutter will last. We check netmode != 1 to prevent Multiplayer Clients from running this code. (similarly, spawning projectiles should also be wrapped like this)
            // netMode == 0 is SP, netMode == 1 is MP Client, netMode == 2 is MP Server.
            // Typically in MP, Client and Server maintain the same state by running deterministic code individually. When we want to do something random, we must do that on the server and then inform MP Clients.
            //if (AI_Timer == 1 && Main.netMode != NetmodeID.MultiplayerClient)
            //{ 
            //    AI_FlutterTime = Main.rand.NextBool() ? 100 : 50;

            //    // Informing MP Clients is done automatically by syncing the npc.ai array over the network whenever npc.netUpdate is set.
            //    // Don't set netUpdate unless you do something non-deterministic ("random")
            //    NPC.netUpdate = true;
            //}

            // Here we add a tiny bit of upward velocity to our npc.
            //NPC.velocity += new Vector2(0, -.35f);

            //// ... and some additional X velocity when traveling slow.
            //if (Math.Abs(NPC.velocity.X) < 2)
            //{
            //    NPC.velocity += new Vector2(NPC.direction * .05f, 0);
            //}

            // after fluttering for 100 ticks (1.66 seconds), our Flutter Slime is tired, so he decides to go into the Fall state.

        }
    }
}

