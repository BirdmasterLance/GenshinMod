using GenshinMod.Characters.Barbara;
using GenshinMod.Characters.Klee;
using GenshinMod.Elements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace GenshinMod.Characters.Barbara
{
    internal class BarbaraNPC : ModNPC
    {
        int OwnerWhois = 0;
        bool checkedOwner = false;
        Player Owner = null;
        private enum ActionState
        {
            Idle,
            Hostile,
            Jump,
            Hover,
            Fall
        }

        // Our texture is 36x36 with 2 pixels of padding vertically, so 38 is the vertical spacing.
        // These are for our benefit and the numbers could easily be used directly in the code below, but this is how we keep code organized.
        private enum Frame
        {
            Idle,
            Hostile,
            Falling,
            Flutter1,
            Flutter2,
            Flutter3
        }
        public static int ProjType()
        {


            return ModContent.ProjectileType<BarbaraWater>();


        }
        public static int OwnerWhoisM(int OwnerWhois)
        {


            return OwnerWhois;

        }
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Dryad;

        private Vector2 GlobTargetCenter;
        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_TextTime => ref NPC.ai[2];
        private float detectionRange = 300f; //60

        private float speed = 4f; //1.5
        private float inertia = 0.7f; //0.07
        private int timer = 0;
        private int Altr_Timer = 0;
        public override void SetStaticDefaults()
        {
            //NPCID.Sets.ActsLikeTownNPC[Type] = true;h
            DisplayName.SetDefault("Barbara");
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Dryad];
            //NPCID.Sets.ActsLikeTownNPC[Type] = true;


            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Staff Credits - Kirbys Dream Land  Super Smash Bros. Ultimate ");
        }
        public override void SetDefaults()
        {
            AnimationType = NPCID.Dryad;
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
            //NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.aiStyle = -1;
            //NPC.buffImmune[BuffID.Venom] = true;
            //NPC.buffImmune[BuffID.OnFire] = true;
            //NPC.buffImmune[BuffID.Confused] = true;
            NPC.dontTakeDamage = false;
            NPC.homeless = true;
            TownNPCStayingHomeless = true;

            NPC.dontCountMe = true;
            //NPC.rarity = 1;

        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Talk to " + NPC.TypeName;
            button2 = "Misc";
        }

        int item = 0;

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {

            if (!firstButton)
            {

                SoundEngine.PlaySound(SoundID.Item29); // Reforge/Anvil sound

                Main.npcChatText = $"Oh, hey. Here, found these as we explore. You can keep them.";

                var entitySource = NPC.GetSource_GiftOrReward();

                Main.LocalPlayer.QuickSpawnItem(entitySource, ItemID.ReleaseDoves);


            }

            if (firstButton)
            {
                switch (Main.rand.Next(0, NPCHallowText().Count))
                {
                    case 0:
                        Main.npcChatText = $"Oh. So.. this is not Monstadt?!";

                        //CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Yawn");
                        break;
                    case 1:
                        //if (Main.Main.rand.Next(0, 100)) { }
                        Main.npcChatText = $"Umm, sorry.. But what is a {Lang.GetItemNameValue(Main.rand.NextFromList(ItemID.SpaceGun, ItemID.BrainScrambler))}?";
                        break;
                    case 2:

                        Main.npcChatText = $"Yes? " + Owner.name + "?";
                        break;
                    case 3:
                        Main.npcChatText = $"I like singing..";
                        break;
                }


            }
            if (!firstButton && (Main.LocalPlayer.HasItem(item)))
            {
                SoundEngine.PlaySound(SoundID.Item3); // 37= Reforge/Anvil sound

                Main.npcChatText = $"Umm.. Thanks for the {Lang.GetItemNameValue(item)}.";
                NPC.life += 20;
                if (NPC.life > NPC.lifeMax)
                {
                    NPC.life = NPC.lifeMax;
                }
                for (int k = 0; k < /*num*/5; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.ManaRegeneration);
                }
                int LemonItemIndex = Main.LocalPlayer.FindItem(item);

                Main.LocalPlayer.inventory[LemonItemIndex].TurnToAir();

                return;
            }


        }

        public override bool CanChat()
        {
            return true;
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int partyGirl = NPC.FindFirstNPC(NPCID.Princess);
            if (partyGirl >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("I like " + Main.npc[partyGirl].GivenName) + ". She's so lovely.");
            }
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Oh, hey. How is it going?"));
            chat.Add(Language.GetTextValue("Ta~ ta~ ta~ ta~"));
            chat.Add(Language.GetTextValue("Could you, please, talk me about this world?"));
            chat.Add(Language.GetTextValue($"The {Lang.GetItemNameValue(ItemID.BunnyStew)} is very tasty! Wonder what if added some spicy ingredients.."));
            chat.Add(Language.GetTextValue("Could you, please, talk me about this world?"));


            //if (NPC.life < NPC.lifeMax / 50)
            //{
            //    chat.Add(Language.GetTextValue("I'm tiiiiired."));
            //}
            if (!Main.dayTime)
            {
                chat.Add(Language.GetTextValue("Night time. Would not be a good idea to be out."));
            }
            //chat.Add(Language.GetTextValue("hh"), 5.0);
            //chat.Add(Language.GetTextValue("Woah!"), 0.1);

            //NumberOfTimesTalkedTo++;
            //if (NumberOfTimesTalkedTo >= 10)
            //{
            //    //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
            //    chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.TalkALot"));
            //}

            return chat; // chat is implicitly cast to a string.
        }

        public List<string> NightTimeText()
        {
            return new List<string>() {
                "Dear "+ Owner.name+". It's not good being up this late.",
                "Being up this late is bad for my mood.",
                "Yawn",
                "Yawn2"
            };

        }
        public List<string> NPCCrimsonText()
        {
            return new List<string>() {
            "This place is gross.",
            " Mhpm! I steped on something.....",
            "My goodness..",
            "Ew."


            };
        }
        public List<string> NPCHallowText()
        {
            return new List<string>() {
                "Oh, pretty place.",
                "It's so good.",
                "My, my.. Lovely place..!",
                "I like how it seems that everything glitters."
            };

        }
        int QuickPopUpText(string text, Color color, int DurationInFrames, Vector2 Velocity)
        {
            AdvancedPopupRequest popupText = new AdvancedPopupRequest();

            //if(color == "white")

            popupText.Velocity = Velocity;
            popupText.Color = color;
            popupText.DurationInFrames = DurationInFrames;
            popupText.Text = text;


            return PopupText.NewText(popupText, NPC.Center);



        }
        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //    if (NPC.HasBuff(ModContent.BuffType<BarbaraCharm>()))
        //    {

        //        HealTargets(NPC);

        //    }
        //}
        bool BuffUsed = false;
        public override void AI()
        {
            PlayerCharacterCode modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            for (int i = 0; i < modPlayer.partyCharacters.Count; i++)
            {
                if (NPC.whoAmI == modPlayer.partyCharacters[i].GetNPCID())
                {
                    modPlayer.partyCharacters[i].life = NPC.life;
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
                OwnerWhoisM(owner.whoAmI);
                OwnerWhois = owner.whoAmI;
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
            //if (NPC.Distance(Owner.Center) < 54f)
            //{
            //    NPC.noTileCollide = true;
            //}
            //if (Owner.Bottom.Y < NPC.Bottom.Y) {
            //    //NPC.noTileCollide = false;
            //    

            if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) &&
            Owner.Bottom.Y < NPC.Bottom.Y)
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


            //if (NPC.Distance(Owner.Center) > 203f/*&& NPC.Distance(OwnerCenter) <100f*/)
            //{
            //    CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), " " + NPC.velocity);

            //    //if (Owner.position.X > NPC.position.X)
            //    //{

            //    //    NPC.direction = 1;
            //    //    //  if ((NPC.Center.X - Owner.Center.X)*-1 > 46)
            //    //    //{
            //    //    //speed = 20f;
            //    //    //speed *= NPC.direction;
            //    //    //if (speed > 5.5f)
            //    //    //{
            //    //    //    speed = 4f;
            //    //    //}


            //    //    //else
            //    //    //{
            //    //    //    NPC.velocity.X += 1f;
            //    //    //}

            //    //    //}

            //    //}
            //    //else
            //    //{
            //    //    NPC.direction = -1;
            //    //    //speed = 5f;
            //    //    //speed *= -1 * NPC.direction;
            //    //    //if (speed > 5.5f)
            //    //    //{
            //    //    //    speed = NPC.direction * -3.5f;
            //    //    //}
            //    //}

            //    AI_State = (float)ActionState.Jump;
            //}

            BuffUsage();
            //if (NPC.HasBuff(ModContent.BuffType<BarbaraCharm>()))
            //{
            //    AI_Timer++;
            //}
            //switch (AI_State)
            //{
            //    case (float)ActionState.Idle:

            //        searchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC npcTargetted);
            //        if (foundTarget && NPC.noTileCollide)
            //        {
            //            AI_State = (float)ActionState.Hostile;
            //            GlobTargetCenter = targetCenter;
            //        }
            //        else
            //        {
            //            NPCIdle();
            //        }
            //        break;
            //    case (float)ActionState.Hostile:
            //        NPCHostile();
            //        break;
            //    case (float)ActionState.Jump:
            //        NPC.noTileCollide = true;
            //        if (Owner.Top.Y < NPC.Top.Y)
            //        {

            //            NPC.velocity.Y -= 2f;

            //        }

            //        SearchForOwner(out bool foundOwner, out float distanceFromOwner, out Vector2 OwnerCenter, out Vector2 idlePosition, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);

            //        Movement(foundOwner, distanceFromOwner, OwnerCenter, distanceToIdlePosition, vectorToIdlePosition);

            //        if (NPC.Distance(OwnerCenter) < 203) { AI_State = (float)ActionState.Idle; }

            //        break;
            //    case (float)ActionState.Hover:
            //        //Hover();
            //        break;
            //    case (float)ActionState.Fall:
            //        if (NPC.velocity.Y == 0)
            //        {
            //            //NPC.velocity.X = 0;
            //            //AI_State = (float)ActionState.Asleep;
            //            //AI_Timer = 0;
            //        }

            //        break;
            //}



        }
        private void SearchForOwner(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out Vector2 idlePosition, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            // Starting search distance
            distanceFromTarget = 2000f;
            targetCenter = NPC.position;
            foundTarget = false;
            vectorToIdlePosition = NPC.Center;
            distanceToIdlePosition = 0f;
            //for (int i = 0; i < Main.maxPlayers; i++)
            //{
            //    Player owner = Main.player[i];
            idlePosition = NPC.Center;
            if (Main.myPlayer == Owner.whoAmI)
            {


                float between = Vector2.Distance(Owner.Center, NPC.Center);
                bool closest = Vector2.Distance(NPC.Center, Owner.Center) > between;
                bool inRange = between < distanceFromTarget;
                bool lineOfSight = Collision.CanHitLine(NPC.position, NPC.width, NPC.height, Owner.position, Owner.width, Owner.height);
                // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                bool closeThroughWall = between < 1000f;

                if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                {
                    distanceFromTarget = between;
                    targetCenter = Owner.Center;
                    idlePosition = Owner.Center;
                    idlePosition.X += 34f * -Owner.direction;
                    vectorToIdlePosition = idlePosition - NPC.Center;
                    distanceToIdlePosition = vectorToIdlePosition.Length();
                    //targetCenter.X += 32f;
                    foundTarget = true;
                    NPC.TargetClosest(true);
                    if (Owner != null)
                    {
                        Console.WriteLine("Owner!2" + Owner.name + "/" + Owner.whoAmI);
                    }
                }
            }
            else
            {
                NPC.TargetClosest(false);

            }
        }
        public void searchForTargets(/*Player owner,*/ out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC npcTargetted)
        {// Starting search distance
            distanceFromTarget = 200f;
            targetCenter = NPC.position;
            foundTarget = false;
            npcTargetted = NPC;
            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];


                    if (npc.CanBeChasedBy() && NPC.life > 0)
                    {

                        float between = Vector2.Distance(npc.Center, NPC.Center);
                        bool closest = Vector2.Distance(NPC.Center, npc.Center) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(NPC.position, NPC.width, NPC.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 200f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            //targetCenter.X += 32f;
                            foundTarget = true;
                            Console.WriteLine(inRange);
                            npcTargetted = npc;

                            AI_State = (float)ActionState.Hostile;


                        }

                    }
                }
            }

        }

        public void NPCIdle(/*Player owner*//*, out Vector2 targetCenter1*/)
        {
            Vector2 DistFromP = NPC.Center - Owner.Center;
            SearchForOwner(out bool foundOwner, out float distanceFromOwner, out Vector2 OwnerCenter, out Vector2 idlePosition, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);

            //////Console.WriteLine("Distance: " + NPC.Distance(owner.Center) + " Distance from Owner: " + distanceFromOwner);


            if (Owner.position.Y < NPC.position.Y)
            {
                if ((NPC.Center.Y - Owner.Center.Y) > Owner.height)
                {
                    if (NPC.velocity.Y > Owner.velocity.Y - 7f) { NPC.velocity.Y = Owner.velocity.Y - 7F; }


                    NPC.velocity.Y -= 1f;
                }
                //else
                //{

                //    if (NPC.velocity.Y > Owner.velocity.Y - 7f) { NPC.velocity.Y = Owner.velocity.Y - 7F; }


                //    NPC.velocity.Y -= 1f;

                //}
            }
            else if (Owner.position.Y > NPC.position.Y)
            {
                NPC.velocity.Y += Owner.maxFallSpeed + 1F;
                if (NPC.velocity.Y > Owner.maxFallSpeed) { NPC.velocity.Y = Owner.maxFallSpeed + 1F; }

            }

            if (NPC.Distance(OwnerCenter) <= 32f)
            {
                NPC.velocity.X = 0f;
                speed = 0;

            }
            else if (NPC.Distance(OwnerCenter) >= 32f && NPC.Distance(OwnerCenter) < 203)
            {
                //  else if((NPC.Distance(OwnerCenter) < 32f && NPC.Distance(OwnerCenter) < 100f))
                //{          

                speed = Owner.accRunSpeed;
                NPC.noTileCollide = false;

                AI_003(speed, inertia, 0.1f);
                //speed = 4f;
            }
            //}
            TalkingText(Owner);
        }
        public void TalkingText(Player owner)
        {
            AI_TextTime++;

            if (AI_TextTime == 600)
            {
                if (!Main.dayTime)
                {
                    switch (Main.rand.Next(0, NightTimeText().Count))
                    {
                        case 0:
                            QuickPopUpText(NightTimeText()[0], Color.Wheat, 150, new Vector2(0f, -10f));

                            //CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Yawn");
                            AI_TextTime = 0;
                            break;
                        case 1:
                            QuickPopUpText(NightTimeText()[1], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;
                            break;
                        case 2:

                            QuickPopUpText(NightTimeText()[2], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;

                            break;
                        case 3:
                            QuickPopUpText(NightTimeText()[3], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;
                            break;
                    }

                }

                if (owner.ZoneHallow)
                {

                    switch (Main.rand.Next(0, NPCHallowText().Count))
                    {
                        case 0:
                            QuickPopUpText(NPCHallowText()[0], Color.Wheat, 150, new Vector2(0f, -10f));

                            //CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Yawn");
                            AI_TextTime = 0;
                            break;
                        case 1:
                            QuickPopUpText(NPCHallowText()[1], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;
                            break;
                        case 2:

                            QuickPopUpText(NPCHallowText()[2], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;

                            break;
                        case 3:
                            QuickPopUpText(NPCHallowText()[3], Color.Wheat, 150, new Vector2(0f, -10f));
                            AI_TextTime = 0;
                            break;
                    }
                }
            }
            //AI_TextTime = 0;

        }
        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 21f;
            float inertia = 20f;

            if (foundTarget)
            {
                //Minion has a target: attack(here, fly towards the enemy)
                if (distanceFromTarget > 44f)
                {
                    //  The immediate range around the target(so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - NPC.Center;
                    direction.Normalize();
                    direction *= speed;

                    NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
                }
            }
            else
            {
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    // Slow down the minion if closer to the player
                    speed = 4f;
                    inertia = 80f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    NPC.velocity = (NPC.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (NPC.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    NPC.velocity.X = -0.15f;
                    NPC.velocity.Y = -0.05f;
                }
            }
        }

        public void NPCHostile()
        {

            //Player owner = Main.player[NPC.target];

            searchForTargets(out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC npcTargetted);
            if (Collision.CanHit(NPC.Center, 0, 0, targetCenter, 0, 0) && foundTarget)
            {
                Altr_Timer++;

                if (NPC.Distance(targetCenter) > 300f)
                {
                    //NPC.velocity.X = 0f;
                    if (targetCenter.X > NPC.position.X)
                    {
                        NPC.velocity.X += 15f;
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.velocity.X -= 15f;

                        NPC.direction = -1;
                    }
                }
                else if (NPC.Distance(targetCenter) < 100)
                {
                    if (targetCenter.X > NPC.position.X)
                    {
                        NPC.velocity.X -= 15f;
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.velocity.X += 15f;

                        NPC.direction = 1;
                    }


                }
                else if (NPC.Distance(targetCenter) >= 100f && NPC.Distance(targetCenter) <= 300f)
                {

                    NPC.velocity.X *= 0f;
                    //if (targetCenter1.X > NPC.position.X)
                    //{
                    //    //NPC.velocity.X -= 2f;
                    //    NPC.direction = 1;
                    //}
                    //else
                    //{
                    //    //NPC.velocity.X += 1f;

                    //    NPC.direction = -1;
                    //}
                }
                var entitySource = NPC.GetSource_FromAI();
                Vector2 towards = NPC.Center - targetCenter;
                towards.Normalize();
                if (Altr_Timer % 30 == 0)
                {
                    //CombatText.NewText(NPC.getRect(), CombatText.HealLife, "+ 50" );


                    if (NPC.life <= NPC.lifeMax * 0.5)
                    {
                        //CombatText.NewText(NPC.getRect(), new Color(0, 255, 50), "I'm at " + (int)NPC.Distance(targetCenter) + "My NPC.VELOCITY.X IS: " + (int)NPC.velocity.X);
                        if (targetCenter.X > NPC.position.X)
                        {


                            //NPC.velocity.X += 2f;
                            NPC.direction = 1;
                            Projectile.NewProjectile(entitySource, NPC.Center, Vector2.Zero, ModContent.ProjectileType<BarbaraWater>(), NPC.damage, 0f, Main.myPlayer, ai1:NPC.whoAmI);
                            //foreach (NPC npc in Main.npc)
                            //{
                            //    if (npc.Distance(npc.Center) > distanceFromTarget)
                            //    {
                            //        CombatText.NewText(npc.getRect(), CombatText.HealLife, "+" + npc);
                            //        for (int r = 0; r < 10; r++)
                            //        {
                            //            Dust.NewDust(npc.Center, npc.width, npc.height, DustID.ManaRegeneration);
                            //        }
                            //    }
                            //}
                            Altr_Timer = 0;
                        }
                        else
                        {
                            //NPC.velocity.X -= 1f;

                            NPC.direction = -1;
                            Projectile.NewProjectile(entitySource, NPC.Center, Vector2.Zero, ModContent.ProjectileType<BarbaraWater>(), NPC.damage, 0f, Main.myPlayer);

                            //for (int i = 0; i < Main.maxNPCs; i++)
                            //{
                            //    NPC npc = Main.npc[i];
                            //    if (npc.Distance(npc.Center) < distanceFromTarget && npc.friendly)
                            //    {
                            //        //CombatText.NewText(npc.getRect(), CombatText.HealLife, "+ " + 50);
                            //        for (int r = 0; r < 10; r++)
                            //        {
                            //            Dust.NewDust(npc.Center, npc.width, npc.height, DustID.ManaRegeneration);
                            //        }
                            //    }
                            //}
                            Altr_Timer = 0;
                        }
                    }
                    else
                    {
                        if (targetCenter.X > NPC.position.X)
                        {
                            //NPC.velocity.X += 2f;
                            NPC.direction = 1;
                            Projectile.NewProjectile(entitySource, NPC.Center, Vector2.Zero, ModContent.ProjectileType<Wind_Attack>(), NPC.damage, 0f, Main.myPlayer);
                            //foreach (NPC npc in Main.npc)
                            //{
                            //    if (npc.Distance(npc.Center) > distanceFromTarget)
                            //    {
                            //        CombatText.NewText(npc.getRect(), CombatText.HealLife, "+" + npc);
                            //        for (int r = 0; r < 10; r++)
                            //        {
                            //            Dust.NewDust(npc.Center, npc.width, npc.height, DustID.ManaRegeneration);
                            //        }
                            //    }
                            //}

                            Altr_Timer = 0;
                        }
                        else
                        {
                            //NPC.velocity.X -= 1f;

                            NPC.direction = -1;
                            Projectile.NewProjectile(entitySource, NPC.Center, Vector2.Zero, ModContent.ProjectileType<Wind_Attack>(), NPC.damage, 0f, Main.myPlayer);

                            //for (int i = 0; i < Main.maxNPCs; i++)
                            //{
                            //    NPC npc = Main.npc[i];
                            //    if (npc.Distance(npc.Center) < distanceFromTarget && npc.friendly)
                            //    {
                            //        //CombatText.NewText(npc.getRect(), CombatText.HealLife, "+ " + 50);
                            //        for (int r = 0; r < 10; r++)
                            //        {
                            //            Dust.NewDust(npc.Center, npc.width, npc.height, DustID.ManaRegeneration);
                            //        }
                            //    }
                            //}
                            Altr_Timer = 0;
                        }
                    }
                }
                //else
                //{
                AI_003(speed, inertia, 0.3f);
                ////}
                //Console.WriteLine("CANHIT");
            }
            else
            {
                AI_State = (float)ActionState.Idle;

            }
        }
        public void AI_003(float speed, float inertia, float value)
        {
            //if (NPC.velocity.X == 0f)
            //{                        ////Console.WriteLine("Test5");

            //    if (NPC.velocity.Y == 0f)
            //    {
            //        AI_Timer += 1f;
            //        if (AI_Timer >= 2f)
            //        {
            //            NPC.direction *= -1;
            //            NPC.spriteDirection = NPC.direction;
            //            AI_Timer = 0f;
            //        }
            //    }
            //}
            //else
            //{
            //    AI_Timer = 0f;
            //}

            //Basic X-Movement for Enemies
            //Possessed Armor = 1.5 speed & 0.07 Inertia, Armored Skeleton = 2 speed & 0.07 Inertia
            //Bone Lee = 5 speed, & 0.2 inertia
            //if (NPC.velocity.X < -speed || NPC.velocity.X > speed)
            //{

            //    Console.WriteLine("testh");
            //    if (NPC.velocity.Y == 0f)
            //    {
            //        //maybe this slows it down?
            //        NPC.velocity *= value; //idk wat this does Bone Lee's is 0.7 & P. Armor is 0.8
            //    }
            //}
            if (NPC.velocity.X < speed && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + inertia;

            }
            if (NPC.velocity.X > speed)
            {
                NPC.velocity.X = speed;
                //////Console.WriteLine("11");
            }
            else if (NPC.velocity.X > -speed && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - inertia;

            }
            if (NPC.velocity.X < -speed)
            {
                NPC.velocity.X = -speed;
            }

            bool flag12 = false;
            if (NPC.velocity.Y == 0)
            {

                int num199 = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
                int num200 = (int)NPC.position.X / 16;
                int num201 = (int)(NPC.position.X + (float)NPC.width) / 16;
                for (int num202 = num200; num202 <= num201; num202++)
                {
                    ////Console.WriteLine("test" + 2);

                    if (Main.tile[num202, num199] == null)
                    {
                        ////Console.WriteLine("test" + 4);

                        return;
                    }
                    if (Main.tile[num202, num199].HasUnactuatedTile && Main.tileSolid[Main.tile[num202, num199].TileType])
                    {
                        flag12 = true;
                        break;
                    }
                }

                ////Console.WriteLine(flag12);

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
                            ////Console.WriteLine("Test5");

                            if (Main.tile[tileX + dir, tileY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY - 3].TileType])
                            {
                                NPC.velocity.Y = -8;
                                NPC.netUpdate = true;
                                ////Console.WriteLine("Test5.1");

                            }
                            else
                            {
                                ////Console.WriteLine("Test5.2");

                                NPC.velocity.Y = -7;
                                NPC.netUpdate = true;
                            }
                        }
                        else if (Main.tile[tileX + dir, tileY - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY - 1].TileType])
                        {
                            ////Console.WriteLine("Test5/2");

                            NPC.velocity.Y = -6;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.position.Y + (float)NPC.height - (float)(tileY * 16) > 20f && Main.tile[tileX + dir, tileY].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX + dir, tileY].TileType] && !Main.tile[tileX + dir, tileY].TopSlope)
                        {
                            ////Console.WriteLine("Test5/3");

                            NPC.velocity.Y = -5;
                            NPC.netUpdate = true;
                            //////Console.WriteLine("test");
                        }
                        else if ((!Main.tile[tileX + dir, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX + dir, tileY + 1].TileType]) && (!Main.tile[tileX + NPC.direction, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX + NPC.direction, tileY + 1].TileType]))
                        {
                            ////Console.WriteLine("Test5/4");

                            NPC.velocity.Y = -8; //npc.directionY < 0 && 
                                                 //npc.velocity.X = npc.velocity.X * 1.5f;
                            NPC.netUpdate = true;
                        }
                    }
                }
            }

            if (NPC.velocity.Y >= 0f)
            {
                ////Console.WriteLine("!Test y<=0");

                //Bottom corner of hitbox depending on NPC direction
                int tileX2 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * NPC.direction)) / 16f);
                int tileY2 = (int)((NPC.position.Y + (float)NPC.height - 1f) / 16f);

                if (Main.tile[tileX2, tileY2].HasUnactuatedTile && !Main.tile[tileX2, tileY2].TopSlope && !Main.tile[tileX2, tileY2 - 1].TopSlope && Main.tileSolid[Main.tile[tileX2, tileY2].TileType] && !Main.tileSolidTop[Main.tile[tileX2, tileY2].TileType])
                {
                    ////Console.WriteLine("Test2/2");

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
                                    ////Console.WriteLine("Test Step 1");

                                    NPC.stepSpeed = 1f;
                                }
                                else
                                {
                                    ////Console.WriteLine("Test Step 2");

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
        //internal class PygmyProjectile : ModProjectile
        //{

        //    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BouncyBomb;

        //    public override void SetStaticDefaults()
        //    {
        //        DisplayName.SetDefault("Pygmy 2");
        //    }

        //    public override void SetDefaults()
        //    {
        //        Projectile.netImportant = true;
        //        Projectile.width = 18;
        //        Projectile.height = 18;
        //        Projectile.penetrate = -1;
        //        Projectile.timeLeft *= 5;
        //    }

        //    public override bool OnTileCollide(Vector2 oldVelocity)
        //    {
        //        return false;
        //    }

        //    public override void AI()
        //    {
        //        AI_026();
        //    }

        private void AI_026()
        {
            if (!Owner.active/*Owner*/)
            {
                NPC.active = false;
                return;
            }
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
                Console.WriteLine("NOT An opposition");
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
            else
            {
                //Main.NewText("else");
                float num115 = 40 * 0;
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
                    float num118 = NPC.position.X;
                    float num119 = NPC.position.Y;
                    float num120 = 100000f;
                    float num121 = num120;
                    int num122 = -1;
                    float num123 = 20f;
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
                            float num130 = Math.Abs(NPC.position.X + (float)(NPC.width / 2) - num128) + Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - num129);
                            if (num130 < num120)
                            {
                                if (num122 == -1 && num130 <= num121)
                                {
                                    num121 = num130;
                                    num118 = num128 + Main.npc[num127].velocity.X * num123;
                                    num119 = num129 + Main.npc[num127].velocity.Y * num123;
                                }
                                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.npc[num127].position, Main.npc[num127].width, Main.npc[num127].height))
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
                    if ((double)NPC.position.Y > Main.worldSurface * 16.0)
                    {
                        num131 = 200f;
                    }
                    if (num120 < num131 + num115 && num122 == -1)
                    {
                        float num132 = num118 - (NPC.position.X + (float)(NPC.width / 2));
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
                        NPC.localAI[0] = num117;
                        float num133 = num118 - (NPC.position.X + (float)(NPC.width / 2));
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
                        else if (Owner.whoAmI == Main.myPlayer)
                        {
                            NPC.ai[1] = num116;
                            Vector2 vector11 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2) - 8f);
                            float num134 = num118 - vector11.X + (float)Main.rand.Next(-20, 21);
                            float num135 = Math.Abs(num134) * 0.1f;
                            num135 = num135 * (float)Main.rand.Next(0, 100) * 0.001f;
                            float num136 = num119 - vector11.Y + (float)Main.rand.Next(-20, 21) - num135;
                            float num137 = (float)Math.Sqrt(num134 * num134 + num136 * num136);
                            num137 = 11f / num137;
                            num134 *= num137;
                            num136 *= num137;
                            timer++;
                            if (timer % 30 == 0)
                            {
                                int num138 = NPC.damage;
                                int num139 = ModContent.ProjectileType<Wind_Attack>();
                                //int num140 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector11.X, vector11.Y, num134, num136, num139, num138, 0f, Main.myPlayer);
                                var Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), vector11, Vector2.Zero, num139, num138, 0f, Main.myPlayer);

                                Proj.position = new Vector2(num118, num119);
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

                float num164 = 0.08f;
                float num165 = 6.5f;

                num165 = 6f;
                num164 = 0.2f;
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

        int BuffCounter = 0;
        void BuffUsage()
        {
            if ((NPC.life < NPC.lifeMax * 0.5 || Owner.statLife * 0.5 < Owner.statLifeMax || NPC.life < NPC.lifeMax && Owner.statLife < Owner.statLifeMax) /*&& Main.rand.Next(0, 8) == 1 */&& !BuffUsed)
            {
                NPC.AddBuff(ModContent.BuffType<BarbaraCharm>(), 900);
                Owner.AddBuff(ModContent.BuffType<BarbaraCharm>(), 900);
                BuffUsed = true;
            }
            if ((!NPC.HasBuff(ModContent.BuffType<BarbaraCharm>()) || !Owner.HasBuff(ModContent.BuffType<BarbaraCharm>()) || (!NPC.HasBuff(ModContent.BuffType<BarbaraCharm>()) && !Owner.HasBuff(ModContent.BuffType<BarbaraCharm>()))) && BuffCounter < 182)
            {
                BuffCounter++;
            }
            if (BuffCounter >= 182)
            {
                BuffUsed = false;
                AI_Timer = 0;
            }
        }




    }
}

internal class BarbaraCharm : ModBuff

{
    int timer = 0;
    int WH = 1;
    public override string Texture => "Terraria/Images/Buff_" + BuffID.Lovestruck;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Barbara's Charm.");
        Description.SetDefault("Water Healing.");
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        timer++;
        if (timer % 60 == 0)
        {

            for (int k = 0; k < /*num*/1; k++)
            {
                Dust.NewDust(npc.Center, npc.width * 100 / 50 * -1, (npc.height * 100 / 50) * -1, DustID.ManaRegeneration);
            }
            var p = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, new Vector2(Main.rand.NextFloat(0.1f, 0.5f), Main.rand.NextFloat(-0.1f, -0.5f)), ProjectileID.EighthNote, 0, 0f, Main.myPlayer);
            p.timeLeft = 30;
            //p.width--;
            //p.height--;

        }
        if (timer % 150 == 0 && !(npc.life >= npc.lifeMax))
        {


            npc.life += 50;
            if (npc.life > npc.lifeMax)
            {
                npc.life = npc.lifeMax;
            }
            //for (int k = 0; k < /*num*/5; k++)
            //{
            //    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.ManaRegeneration);
            //}


            CombatText.NewText(npc.getRect(), new Color(0, 255, 124), "+ 50 ");



        }
        else if (timer < 600)
        {

            WH--;

        }
    }
    public override void Update(Player player, ref int buffIndex)
    {
        if (timer % 60 == 0)
        {

            for (int k = 0; k < /*num*/1; k++)
            {
                Dust.NewDust(player.Center, player.width * 100 / 50 * -1, (player.height * 100 / 50) * -1, DustID.ManaRegeneration);
            }
            var p = Projectile.NewProjectileDirect(player.GetSource_FromAI(), player.Center, new Vector2(Main.rand.NextFloat(0.1f, 0.5f), Main.rand.NextFloat(-0.1f, -0.5f)), ProjectileID.EighthNote, 0, 0f, Main.myPlayer);
            p.timeLeft = 30;
            //p.width--;
            //p.height--;

        }
        if (timer % 150 == 0 && !(player.statLife >= player.statLifeMax))
        {


            player.statLife += 20;
            if (player.statLife > player.statLifeMax)
            {
                player.statLife = player.statLifeMax;
            }  //}


            CombatText.NewText(player.getRect(), new Color(0, 255, 124), "+ 20 ");


        }
    }

    //public override 



}
internal class BarbaraWater : ModProjectile
{
    public override string Texture => "Terraria/Images/Item_" + ProjectileID.WaterBolt;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Water Attack");
    }
    public int ParentIndex
    {
        get => (int)Projectile.ai[0] - 1;
        set => Projectile.ai[0] = value + 1;


    }
    public bool HasParent => ParentIndex > -1;

    public static int ProjectileOwner()
    {

        return ModContent.NPCType<BarbaraNPC>();
    }
    public override void SetDefaults()
    {
        Projectile.damage = 100;
        Projectile.width = 50;
        Projectile.height = 50;
        //Projectile.friendly = false;
        Projectile.penetrate = 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 90;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.ignoreWater = true;
        //Projectile.scale = 0.01f;

        // We are going to use Projectile.ai[1] to store the ID of the NPC who shot this projectile
        Projectile.ai[1] = -1;
    }
    private void SearchForTargets(/*Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter*/)
    {
        //Player player = Owner;

        // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
        float distanceFromTarget = 100f;
        Vector2 targetCenter = Projectile.position;
        bool foundTarget = false;
        if (!foundTarget)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];



                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, Projectile.Center);
                    bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                    // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                    // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                    bool closeThroughWall = between < 5f;

                    if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                    {
                        distanceFromTarget = between;
                        targetCenter = npc.Center;
                        foundTarget = true;
                        Projectile.position = npc.position;


                    }
                }

            }
        }
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {

        for (int g = 0; g < 2; g++)
        {
            Dust.NewDust(target.position, target.width, target.height, DustID.Water, 0f, 0f, 0, Color.Aqua);

        }
        HealTargets();


        //base.OnHitNPC(target, damage, knockback, crit);
    }


    private void HealTargets()
    {
        //Player player = Main.player[Projectile.Projectile];

        // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
        float distanceFromTarget = 100f;
        Vector2 targetCenter = Projectile.position;
        bool foundTarget = false;
        int healthAmmount = 0;
        if (!foundTarget)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];



                if ((/*npc.type == ModContent.NPCType<KleeNPC>() ||*/ npc.type == ModContent.NPCType<BarbaraNPC>() || npc.friendly) && npc.life > 00)
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


                        if (npc.life < npc.lifeMax)
                        {


                            healthAmmount = Projectile.damage * 100 / 50;
                            if (npc.life + healthAmmount > npc.lifeMax)
                            {


                                npc.life = npc.lifeMax;
                            }
                            else
                            {

                                npc.life += Projectile.damage * 100 / 50;
                                CombatText.NewText(npc.getRect(), CombatText.LifeRegen, "+" + healthAmmount);

                            }
                        }
                        //CombatText.NewText(npc.getRect(), new Color(253, 255, 121), "Name: " +npc.TypeName +" Health: "+ healthAmmount);
                        //CombatText.NewText(Projectile.getRect(), new Color(0, 120, 121), "" +npc.GivenName+" "+ healthAmmount);

                    }

                }
            }
        }
    }

    public override void AI()
    {

        SearchForTargets();
    }

}
