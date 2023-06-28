using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
//using System.Numerics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace GenshinMod.NPCH
{

    public class Paimon : ModNPC
    {
        int OwnerWhois = 0;
        bool checkedOwner = false;
        Player Owner = null;
        private int NumberOfTimesTalkedTo = 0;
        //private int NumberOfTimesFeed = 0;
        private int AfkTime = 0;
        //Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
        private enum ActionState
        {

            Idle,
            Curious,
            Jump,
            Hover,
            Fall,
            Fly

        }
        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_FlutterTime => ref NPC.ai[2];

        public override string Texture => "Terraria/Images/NPC_" + NPCID.FairyCritterBlue;

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Party Zombie");

            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.FairyCritterBlue];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value); NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<CrimsonBiome>(AffectionLevel.Dislike) //
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate)

            ; // < Mind the semicolon!

        }

        public override void SetDefaults()
        {
            //// Sets NPC to be a Town NPC
            ///
            AnimationType = NPCID.FairyCritterBlue;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 20;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.5f;

            //AnimationType = NPCID.Guide;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
        // Sets the preferred biomes of this town NPC listed in the bestiary.
        // With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
        BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

        // Sets your NPC's flavor text in the bestiary.
        new FlavorTextBestiaryInfoElement("From another dimention. She's here to give you a hand!"),

        // You can add multiple elements if you really wanted to
        // You can also use localization keys (see Localization/en-US.lang)
        //new FlavorTextBestiaryInfoElement("Mods.ExampleMod.Bestiary.ExamplePerson")
                });
        }
        // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
        // Returning false will allow you to manually draw your NPC
        //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    // This code slowly rotates the NPC in the bestiary
        //    // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
        //    if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
        //    {
        //        drawModifiers.Rotation += 0.001f;

        //        // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
        //        NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
        //        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        //    }

        //    return true;
        //}
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        { // Requirements for the town NPC to spawn.
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }
                return true;
                // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
                //if (player.inventory.Any(item => item.type == ModContent.ItemType<ExampleItem>() || item.type == ModContent.ItemType<Items.Placeable.ExampleBlock>()))
                //{
                //    return true;
                //}
            }

            return true;
        }
        public override bool CanChat()
        {
            return true;
        }//hh

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int Merchant = NPC.FindFirstNPC(NPCID.Merchant);
            if (Merchant >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("That " +/*"Mods.ExampleMod.Dialogue.Paimon.MerchantDialogue",*/ Main.npc[Merchant].GivenName + " does not like money as I do."));
            }
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Hey, make sure to complete the Daily Quests! With that, you get cool rewards!"/*"Mods.ExampleMod.Dialogue.ExamplePerson.StandardDialogue1"*/));
            chat.Add(Language.GetTextValue("There are many unknown foods here, THAT I HAVE NOT EVEN TASTED!!"/*"Mods.ExampleMod.Dialogue.Paimon.StandardDialogue2"*/));
            chat.Add(Language.GetTextValue("I'm hungry. Feed me something!"/*"Mods.ExampleMod.Dialogue.Paimon.StandardDialogue3"*/));
            chat.Add(Language.GetTextValue("Hello there, Traveler! Anything I could help you with?"/*"Mods.ExampleMod.Dialogue.Paimon.CommonDialogue"*/), 5.0);
            chat.Add(Language.GetTextValue($"Look at my {Lang.GetItemNameValue(ItemID.CratePotion)}! Isn't it something you'd like?!"/*"Mods.ExampleMod.Dialogue.Paimon.RareDialogue"*/), 0.1);
            if (NPC.loveStruck)
            {
                chat.Add(Language.GetTextValue("I-I.. feel at ease when I'm with you~"));

                chat.Add(Language.GetTextValue("Your eyes.. are so beautiful, do you know?"));

            }

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
                chat.Add(Language.GetTextValue("What, WHAAT?!"/*"Mods.ExampleMod.Dialogue.ExamplePerson.TalkALot"*/));
            }

            return chat; // chat is implicitly cast to a string.
        }
        private List<int> PaimonsFaveTreats()
        {
            return new List<int>(){
            ItemID.Mango,
            ItemID.Lemon,
            ItemID.Nachos,
            ItemID.FruitSalad,
            ItemID.Pizza,
            ItemID.FriedEgg,
            ItemID.FruitJuice,
            ItemID.Spaghetti,
            };


        }

        int item = 0;
        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("Shop"/*LegacyInterface.28*/);
            button2 = "Talk";
            for (int i = 0; i < PaimonsFaveTreats().Count; i++)
            {
                if (Main.LocalPlayer.HasItem(PaimonsFaveTreats()[i]))
                {
                    button2 = "Feed " + Lang.GetItemNameValue((PaimonsFaveTreats()[i]));
                    for (int k = 0; k < /*num*/5; k++)
                    {
                        Dust.NewDust(NPC.Center, NPC.width, (NPC.height * 100) / 30, DustID.Honey);
                    }

                    item = (PaimonsFaveTreats()[i]);

                }

            }
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                // We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.



                shop = true;
            }
            if (!firstButton && (Owner.HasItem(item)))
            {
                //if 
                //{
                SoundEngine.PlaySound(SoundID.Item29); // 37= Reforge/Anvil sound
                CombatText.NewText(NPC.getRect(), CombatText.HealLife, "+20 ");



                Main.npcChatText = $"Yum!~ Thanks for the {Lang.GetItemNameValue(item)}!";
                for (int k = 0; k < /*num*/5; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, (NPC.height * 100) / 30, DustID.PinkCrystalShard);

                    //Gore.NewGore(this, NPC.Center, NPC.width, NPC.height, GoreID);
                }
                int LemonItemIndex = Owner.FindItem(item);
                //var entitySource = NPC.GetSource_GiftOrReward();

                Owner.inventory[LemonItemIndex].TurnToAir();
                //Main.LocalPlayer.QuickSpawnItem(entitySource, ModContent.ItemType<WaspNest>());

                return;
                //}
            }
        }


        public void FondPoints()
        {



        }
        // Not  finished, but below is what the NPC shop contains

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot++].SetDefaults(ItemID.OceanCrateHard/*ItemType < ???? ITEM FROM GENSHIN > ()*/);
            shop.item[nextSlot].SetDefaults(ItemID.CratePotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.JungleFishingCrate);
            nextSlot++;
            shop.item[nextSlot++].SetDefaults(ItemID.OasisCrate);/*ItemType<Items.Placeable.Furniture.ExampleWorkbench>());*/
            shop.item[nextSlot++].SetDefaults(ItemID.FrozenCrate);/*ItemType<Items.Placeable.Furniture.ExampleChair>());*/
            shop.item[nextSlot++].SetDefaults(ItemID.WoodenCrateHard);/*ItemType<Items.Placeable.Furniture.ExampleDoor>());*/
            shop.item[nextSlot++].SetDefaults(ItemID.FloatingIslandFishingCrate);/*ItemType<Items.Placeable.Furniture.ExampleBed>());*/
            shop.item[nextSlot++].SetDefaults(ItemID.Lemon);/*ItemType<Items.Placeable.Furniture.ExampleChest>());*/
            shop.item[nextSlot++].SetDefaults();/*ItemType<ExamplePickaxe>());*/
            shop.item[nextSlot++].SetDefaults();/*ItemType<ExampleHamaxe>());*/

            if (Main.LocalPlayer.HasBuff(BuffID.Lifeforce))
            {
                shop.item[nextSlot++].SetDefaults(ItemID.HeartLantern);
            }
        }
        int ChatTime = 0;
        public override void AI()
        {
            //ChatTime++;

            switch (AI_State)
            {
                case (float)ActionState.Idle:
                    Idle();
                    break;
                case (float)ActionState.Curious:

                    Curious();
                    break;
                    //case (float)ActionState.Jump:
                    //    Jump();
                    //    break;
                    //case (float)ActionState.Hover:
                    //    Hover();
                    //    break;
                    //case (float)ActionState.Fall:
                    //    if (NPC.velocity.Y == 0)
                    //    {
                    //        NPC.velocity.X = 0;
                    //        AI_State = (float)ActionState.Crawl;
                    //        AI_Timer = 0;
                    //    }

                    //    break;
                    //case (float)ActionState.Fly:
                    //    Fly();
                    //    break;

            }
            //NPC.spriteDirection = NPC.direction;

            //AI_Timer++;
            //AfkTime++;
            //int num = 0;



            //Player owner = Main.player[NPC.target];
            //foreach (Player player in Main.player)
            //{

            //    if (player.getRect().Intersects(NPC.Hitbox))
            //    {
            //        owner = player;
            //    }
            //}
            //NPC target;

            //if (!CheckActive(owner))
            //{
            //    return;
            //}


            Visuals();




        }
        private void Idle()
        {
            ChatTime++;


            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player owner = Main.player[NPC.target];
            if (!checkedOwner)
            {
                Owner = owner;
                //OwnerWhoisM(owner.whoAmI);
                OwnerWhois = owner.whoAmI;
                checkedOwner = true;
            }


            if (owner.afkCounter >= 3600)
            {
                CombatText.NewText(NPC.getRect(), new Color(255, 50, 0), "I'm bored.. What's that?");
                if (Owner.afkCounter == 3600)
                {
                    ChatTime = 0;
                }
                AI_State = (float)ActionState.Curious;
            }


            if (Owner.afkCounter == 3770 && Main.rand.Next(0, 6) == 1)
            {

                CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Woaw, so this world is called " + Main.worldName + "+? It's pretty.", true);


            }
            GeneralBehavior(Owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);//hh

            SearchForOwner(Owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            movement2(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition, Owner);
            //Console.WriteLine(owner.afkCounter);
        }
        private void SearchForOwner(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {

            // Starting search distance
            distanceFromTarget = 231f;
            targetCenter = NPC.position;
            foundTarget = false;



            float between = Vector2.Distance(owner.Center, NPC.oldPosition);
            bool closest = Vector2.Distance(NPC.Center, owner.Center) > between;
            bool inRange = between < distanceFromTarget;
            bool lineOfSight = Collision.CanHitLine(NPC.position, NPC.width, NPC.height, owner.position, owner.width, owner.height);
            // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
            // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
            bool closeThroughWall = between < 200f;

            if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall) && distanceFromTarget <= 400f)
            {
                distanceFromTarget = between;
                targetCenter = owner.Top + new Vector2(NPC.direction * -0.5f /** offsetX*/, -NPC.height * 2);
                foundTarget = true;
                NPC.TargetClosest(true);
            }
            else
            {
                NPC.TargetClosest(false);
            }




        }
        List<string> PaimonUGText()
        {

            return new List<string> {

            "I don't like this place much..",
            "Woah.. Some monsters here glow.. Insane!",
            "It's Getting hot in here..",
            "So many.. Stone..",
            "I think we should loot something.."

            };

        }
        List<string> PaimonUGCrimson_And_CrimsonText()
        {
            return new List<string> {

                    "GROSS! Is this all FLESH?! EWW!!",
            " Let's get out of here, please..",

            };
        }
        List<string> PaimonUGCorruptionText()
        {
            return new List<string> {
            "I don't remember ever seeing such a place..",
                "Eww.. Smells like rancid fish. Why you had to choose this place out of them all",

            };
        }
        List<string> PaimonUGHallowText()
        {

            return new List<string> {

            "1",
            "This place has Rainbows!",
            "I LOVE this place! So beautiful! Shinny!",

            };
        }
        List<string> PaimonNightTimeText()
        {

            return new List<string> {


            "I'm tired..",
            "Why do you insist on not sleeping? Gosh.",

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
        private void movement2(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition, Player owner)
        {
            // Default movement parameters (here for attacking)
            float speed = owner.accRunSpeed * 0.7f;
            float inertia = 8f;
            //Vector2 toAboveNpc = 
            AI_Timer++;
            //int random = Main.rand.Next();
            if (foundTarget)
            {


                NPC.noTileCollide = true;
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceToIdlePosition > 40f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    NPC.velocity = (NPC.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                    //Vector2 direction = targetCenter - NPC.Center;
                    //direction.SafeNormalize(Vector2.UnitY);
                    //direction *= speed;

                    //NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
                }
                //if ((owner.equippedWings.type == /*>= 639 && target.type <=650*/ NPCID.GemBunnySapphire || target.type == NPCID.GemBunnyAmber || target.type == NPCID.GemBunnyTopaz || target.type == NPCID.GemSquirrelSapphire) && owner.afkCounter == 420 /*&&Main.rand.Next(1, 21) == 1*/)
                //{
                //    CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Woaah! A " + .TypeName + "?! Incredible..");
                //}

                //else
                //{
                //if (AI_Timer % 60 == 0)
                //{
                //    CombatText.NewText(NPC.getRect(), new Color(20, 20, 0), target.TypeName + " Found target?: " + foundTarget);
                //    Console.WriteLine("Target Name: " + target.TypeName + " Found target?: " + foundTarget + " Distance: " + distanceFromTarget);

                //}
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
                    inertia = 8f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement hh
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    NPC.velocity = (NPC.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (NPC.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    //NPC.velocity.X = -0.15f;
                    NPC.velocity.Y = -0.05f;
                }
            }


            //}
            if (ChatTime == 900 /*&& AI_State == (float)ActionState.Idle*/)
            {
                switch (Main.rand.Next(0, 4))
                {
                    case 0:
                        CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Now, where are we going?!");

                        //{
                        SoundEngine.PlaySound(SoundID.Meowmere); // 37= Reforge/Anvil sound
                        NPC.velocity = new Vector2(NPC.direction * 2);
                        ChatTime = 0;


                        break;

                    case 1:
                        //NPC.velocity = new Vector2(NPC.direction * 2f, 0f);
                        QuickPopUpText("Lets find a Treasure!!", Color.White, 120, new Vector2(1f, 0f));
                        ChatTime = 0;

                        break;
                    case 2:
                        QuickPopUpText($"Hey! Why not going for a {Lang.GetItemNameValue(Main.rand.NextFromList(ItemID.FriedEgg, ItemID.Bacon, ItemID.BBQRibs, ItemID.Nachos, ItemID.Pineapple, ItemID.Pizza, ItemID.SugarCookie, ItemID.SeafoodDinner))}.... And feed me it?", Color.White, 120, new Vector2(1f, 0f));

                        ChatTime = 0;

                        break;
                    case 3:
                        if (owner.ZoneNormalSpace)
                        {

                            QuickPopUpText($"Brr.. Soo cold.. I'd love a Warmth {Lang.GetItemNameValue(ItemID.CoffeeCup)}....", Color.White, 120, new Vector2(1f, 0f));

                            ChatTime = 0;
                            break;


                        }
                        else if (owner.ZoneNormalUnderground)
                        {
                            QuickPopUpText(PaimonUGText()[Main.rand.Next(0, PaimonUGText().Count)], Color.White, 120, new Vector2(1f, 0f));

                            ChatTime = 0;
                            break;
                        }
                        else if (owner.ZoneCorrupt)
                        {
                            QuickPopUpText(PaimonUGCorruptionText()[Main.rand.Next(0, PaimonUGCorruptionText().Count)], Color.White, 120, new Vector2(1f, 0f));

                            ChatTime = 0;
                            break;


                        }
                        else if (owner.ZoneHallow)
                        {
                            QuickPopUpText(PaimonUGHallowText()[Main.rand.Next(1, PaimonUGHallowText().Count)], Color.White, 120, new Vector2(1f, 0f));

                            ChatTime = 0;
                            break;


                        }
                        else if (!Main.dayTime)
                        {
                            QuickPopUpText(PaimonNightTimeText()[Main.rand.Next(1, PaimonNightTimeText().Count)], Color.White, 120, new Vector2(1f, 0f));

                            ChatTime = 0;
                            break;


                        }

                        break;
                }
            }
        }
        void Curious()
        {
            AI_Timer++;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player owner = Main.player[NPC.target];
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);//hh



            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition, owner, target);
            if (AI_Timer >= 7200)
            {
                AI_State = (float)ActionState.Idle;
                AI_Timer = 0;
            }
            //Console.WriteLine(checkedNPC + " " + AI_Timer);
        }
        private bool CheckActive(Player owner)
        {
            //if (owner.dead || !owner.active)
            //{
            //    NPC.life = 0;
            //    NPC.active = false;
            //    NPC.netSkip = -1;

            //    //owner.ClearBuff(ModContent.BuffType<ExampleSimpleMinionBuff>());

            //    return false;
            //}

            //if (owner.HasBuff(ModContent.BuffType<ExampleSimpleMinionBuff>()))h
            //{
            //    NPC.timeLeft = 2;
            //}

            return true;
        }
        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 32f;
            /*idlePosition.X -= owner.direction;*/// Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is NPC.minionPos
            //float minionPositionOffsetX = (10 + 0 * 40) * -owner.direction;
            //idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            vectorToIdlePosition = idlePosition - NPC.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                NPC.position = idlePosition;
                NPC.velocity *= 0.1f;
                NPC.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            //float overlapVelocity = 0.04f;

            //// Fix overlap with other minions
            //for (int i = 0; i < Main.maxProjectiles; i++)
            //{
            //    Projectile other = Main.projectile[i];

            //    if (i != NPC.whoAmI && other.active && other.owner == NPC.target && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
            //    {
            //        if (NPC.position.X < other.position.X)
            //        {
            //            NPC.velocity.X -= overlapVelocity;
            //        }
            //        else
            //        {
            //            NPC.velocity.X += overlapVelocity;
            //        }

            //        if (NPC.position.Y < other.position.Y)
            //        {
            //            NPC.velocity.Y -= overlapVelocity;
            //        }
            //        else
            //        {
            //            NPC.velocity.Y += overlapVelocity;
            //        }
            //    }
            //}
        }
        private bool IsSSomethingOnscreen(Vector2 center, Player owner)
        {
            float speed = 8f;
            float inertia = 5f;
            //Vector2 toAboveNpc = 
            int w = NPC.sWidth + NPC.safeRangeX * 2;
            int h = NPC.sHeight + NPC.safeRangeY * 2;
            Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
            bool checkFlowers = false;

            if (owner.active && owner.getRect().Intersects(npcScreenRect))
            {



                for (int x = npcScreenRect.Left; x <= npcScreenRect.Right; x++)
                {
                    for (int y = npcScreenRect.Top; y <= npcScreenRect.Bottom; y++)
                    {
                        int type = Main.tile[x, y].TileType;
                        if (type == TileID.Sapphire)
                        {
                            Vector2 block = new Vector2(x, y);

                            if (Vector2.Distance(block, center) > 75f)
                            {
                                // The immediate range around the target (so it doesn't latch onto it when close)
                                Vector2 direction = block - center;
                                direction.Normalize();
                                direction *= speed;

                                NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
                            }
                            else
                            {

                                if ((type == TileID.MatureHerbs || type == TileID.VineFlowers) && checkFlowers == false && AI_Timer >= 120)
                                {

                                    QuickPopUpText($"In Monstadt we got pretty flowers, too.", Color.White, 120, new Vector2(1f, 0f));
                                    checkFlowers = true;
                                    if (AI_Timer >= 120)
                                    {

                                    }

                                }
                                if (type == TileID.VanityTreeSakura || type == TileID.VanityTreeWillowSaplings)
                                {


                                    CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), $"What a pretty {Lang.GetItemNameValue(type)} I'd love having one..");


                                }



                            }
                        }

                        //if (Main.tile[x, y].WallType == TileID..)h
                        //{

                        //}
                        if ((type == TileID.MatureHerbs || type == TileID.VineFlowers) && checkFlowers == false)
                        {

                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), $"Ooh! {Lang.GetItemNameValue(type)}, so pretty..");
                            //checkFlowers = true;
                            if (AI_Timer >= 120)
                            {

                            }

                        }
                        if (type == TileID.VanityTreeSakura || type == TileID.VanityTreeWillowSaplings)
                        {


                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), $"What a pretty {Lang.GetItemNameValue(type)} I'd love having one..");


                        }
                    }
                }
            }
            //return score >= ((right - left) * (bottom - top)) / 2;

            return false;
        }
        //public override bool CheckConditions(int left, int right, int top, int bottom)
        //{
        //    int score = 0;
        //    for (int x = left; x <= right; x++)
        //    {
        //        for (int y = top; y <= bottom; y++)
        //        {
        //            int type = Main.tile[x, y].TileType;
        //            if (type == TileID.Sapphire || type == ModContent.TileType<ExampleChair>() || type == ModContent.TileType<ExampleWorkbench>() || type == ModContent.TileType<ExampleBed>() || type == ModContent.TileType<ExampleDoorOpen>() || type == ModContent.TileType<ExampleDoorClosed>())
        //            {



        //                score++;
        //            }

        //            if (Main.tile[x, y].WallType == ModContent.WallType<ExampleWall>())
        //            {
        //                score++;
        //            }
        //        }
        //    }
        //    return score >= ((right - left) * (bottom - top)) / 2;
        //}
        NPC lastNPC;
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target)
        {
            // Starting search distance
            distanceFromTarget = 700f;
            targetCenter = NPC.position;
            foundTarget = false;

            target = NPC;


            if (!foundTarget /*&& AI_Timer <= 1800*/)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CountsAsACritter && npc.active && npc.life > 0)
                    {
                        float between = Vector2.Distance(npc.Center, NPC.Center);
                        //if (between < 2000f)
                        //{
                        //    distanceFromTarget = between;
                        //    targetCenter = npc.Center;
                        //    foundTarget = true;
                        //}

                        bool closest = Vector2.Distance(NPC.Center, npc.Center) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(NPC.position, NPC.width, NPC.height, owner.position, owner.width, owner.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)/* && distanceFromTarget <= 400f*/)
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            targetCenter.Y -= 32f;
                            foundTarget = true;
                            target = npc;
                            lastNPC = target;
                        }
                    }

                }
            }

        }
        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition, Player owner, NPC target)
        {
            ChatTime++;
            // Default movement parameters (here for attacking)
            float speed = 5f;
            float inertia = 5f;

            if (foundTarget /*&& target.life < 0 && target.active*/ /*&& AI_Timer <= 1800*/)
            {
                if (AI_Timer % 60 == 0)
                {
                    CombatText.NewText(NPC.getRect(), new Color(255, 50, 0), "Found: " + target.GivenOrTypeName);
                    Console.WriteLine("Target Name: " + target.GivenOrTypeName + " Found target?: " + foundTarget + " Distance: " + distanceFromTarget);

                }
                NPC.noTileCollide = true;
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget > 40f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - NPC.Center;
                    direction.Normalize();  /*SafeNormalize(Vector2.UnitY);*/
                    direction *= speed;

                    NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
                }
                if (ChatTime == 240 /*&& AI_State == (float)ActionState.Idle*/)
                {

                    switch (target.type)
                    {
                        case NPCID.Bunny:
                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Cuuuuute " + target.GivenOrTypeName + "! So fluffy!");
                            NPC.velocity = new Vector2(NPC.direction * 2, 0f);
                            ChatTime = 0;


                            break;

                        case NPCID.GemBunnySapphire:
                            //NPC.velocity = new Vector2(NPC.direction * 2f, 0f);
                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Ohh! Look at that! It shiny and cute!!");
                            ChatTime = 0;

                            break;
                        case NPCID.Duck:

                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Oh! A " + target.TypeName + " If I had some bread..");

                            ChatTime = 0;

                            break;
                        default:

                            CombatText.NewText(NPC.getRect(), new Color(255, 255, 0), "Oh, a " + target.GivenOrTypeName);
                            ChatTime = 0;
                            break;
                    }
                }
                if (ChatTime > 240)
                {

                    ChatTime = 0;

                }
                Console.WriteLine("Target active?: " + target.active + " ChatTime: " + ChatTime);

            }
            else
            {
                if (AI_Timer % 60 == 0)
                {
                    CombatText.NewText(NPC.getRect(), new Color(100, 50, 0), "Found: " + target.GivenOrTypeName);
                    Console.WriteLine("Target Name: " + target.GivenOrTypeName + " Found target?: " + foundTarget + " Distance: " + distanceFromTarget);

                }
                //AI_Timer = 0;
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
                    inertia = 8f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement hh
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    NPC.velocity = (NPC.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (NPC.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    //NPC.velocity.X = -0.15f;
                    NPC.velocity.Y = -0.05f;
                }

            }
            if (AI_Timer >= 9000)
            {
                Curious();
            }

        }

        private bool IsAfk(Player owner)

        {

            if (owner.velocity == Vector2.Zero)
            {
                AfkTime++;



                if (AfkTime > 3600) { return true; }

            }
            return false;

        }


        private void Visuals()
        {
            // So it will lean slightly towards the direction it's moving
            NPC.rotation = NPC.velocity.X * 0.05f;



            // This is a simple "loop through all frames from top to bottom" animation
            //int frameSpeed = 5;

            //NPC.frameCounter++;


            Dust.NewDust(NPC.Center, NPC.width, (NPC.height * 100) / 30, DustID.SilverCoin);

            //if (NPC.frameCounter >= frameSpeed)
            //{
            //    NPC.frameCounter = 0;
            //    NPC.frame++;

            //    if (NPC.frame >= Main.npcFrameCount[NPC.type])
            //    {
            //        NPC.frame = 0;
            //    }
            //}

            // Some visuals here
            Lighting.AddLight(NPC.Center, Color.White.ToVector3() * 0.78f);


        }

    }
}
