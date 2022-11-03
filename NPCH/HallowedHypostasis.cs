using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Hh1.BulletsorProjectiles
{        // The main part of the boss, usually refered to as "body"
         //[AutoloadBossHead] // This attribute looks for a texture called "ClassName_Head_Boss" and automatically registers it as the NPC boss head icon

    public class HallowedHypostasis : ModNPC
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MagnetSphereBall /*ToxicBubble */;

        //public override void SetStaticDefaults()
        //{
        //	DisplayName.SetDefault("ElectricalBall");
        //}

        // This boss has a second phase and we want to give it a second boss head icon, this variable keeps track of the registered texture from Load().
        // It is applied in the BossHeadSlot hook when the boss is in its second stage
        public static int secondStageHeadSlot = -1;

        // This code here is called a property: It acts like a variable, but can modify other things. In this case it uses the NPC.ai[] array that has four entries.
        // We use properties because it makes code more readable ("if (SecondStage)" vs "if (NPC.ai[0] == 1f)").
        // We use NPC.ai[] because in combination with NPC.netUpdate we can make it multiplayer compatible. Otherwise (making our own fields) we would have to write extra code to make it work (not covered here)
        //public bool SecondStage
        //{
        //    get => NPC.ai[0] == 1f;
        //    set => NPC.ai[0] = value ? 1f : 0f;
        //}
        public ref float AI_State => ref NPC.ai[0];

        private enum ActionState
        {
            Crawl,
            Notice,
            Jump,
            Hover,
            Fall
        }

        // If your boss has more than two stages, and since this is a boolean and can only be two things (true, false), concider using an integer or enum

        // More advanced usage of a property, used to wrap around to floats to act as a Vector2
        public Vector2 FirstStageDestination
        {
            get => new Vector2(NPC.ai[1], NPC.ai[2]);
            set
            {
                NPC.ai[1] = value.X;
                NPC.ai[2] = value.Y;
            }
        }
        // Auto-implemented property, acts exactly like a variable by using a hidden backing field
        public Vector2 LastFirstStageDestination { get; set; } = Vector2.Zero;

        // This property uses NPC.localAI[] instead which doesn't get synced, but because SpawnedMinions is only used on spawn as a flag, this will get set by all parties to true.
        // Knowing what side (client, server, all) is in charge of a variable is important as NPC.ai[] only has four entries, so choose wisely which things you need synced and not synced
        public bool SpawnedMinions
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        private const int FirstStageTimerMax = 90;
        // This is a reference property. It lets us write FirstStageTimer as if it's NPC.localAI[1], essentially giving it our own name
        // *2 = We could also repurpose FirstStageTimer since it's unused in the second stage, or write "=> ref FirstStageTimer", but then we have to reset the timer when the state switch happens

        public ref float FirstStageTimer => ref NPC.localAI[1];
        //*1
        public ref float RemainingShields => ref NPC.localAI[2];
        //*2 
        public ref float SecondStageTimer_SpawnEyes => ref NPC.localAI[3];

        // Do NOT try to use NPC.ai[4]/NPC.localAI[4] or higher indexes, it only accepts 0, 1, 2 and 3!
        // If you choose to go the route of "wrapping properties" for NPC.ai[], make sure they don't overlap (two properties using the same variable in different ways), and that you don't accidently use NPC.ai[] directly

        // Helper method to determine the minion type
        //public static int MinionType()
        //{
        //    //return ModContent.NPCType<MinionBossMinion>();
        //}

        // Helper method to determine the amount of minions summoned
        public static int MinionCount()
        {
            int count = 4;

            //if (Main.expertMode)
            //{
            //    count += 5; // Increase by 5 if expert or master mode
            //}

            //if (Main.getGoodWorld)
            //{
            //    count += 5; // Increase by 5 if using the "For The Worthy" seed
            //}

            return count;
        }
        public enum OrderByBoss
        {

            crush,
            MagicalIlusion,


        }


        private static int orderGiven=0;
        public static int OrderByBossCasting()
        {
            return orderGiven;


        }
        public static int OrderByBossCasting1(int order)
        {

            return order;

        }
        //public override void Load()
        //{
        //    // We want to give it a second boss head icon, so we register one
        //    //string texture = BossHeadTexture + "_SecondStage"; // Our texture is called "ClassName_Head_Boss_SecondStage"
        //    secondStageHeadSlot = Mod.AddBossHeadTexture(texture, -1); // -1 because we already have one registered via the [AutoloadBossHead] attribute, it would overwrite it otherwise
        //}

        //public override void BossHeadSlot(ref int index)
        //{
        //    int slot = secondStageHeadSlot;
        //    if (SecondStage && slot != -1)
        //    {
        //        // If the boss is in its second stage, display the other head icon instead
        //        index = slot;
        //    }
        //}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Hypostasis.");
            Main.npcFrameCount[Type] = 6;

            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            // Influences how the NPC looks in the Bestiary
            //NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            //{
            //    CustomTexturePath = "ExampleMod/Assets/Textures/Bestiary/MinionBoss_Preview",
            //    PortraitScale = 0.6f, // Portrait refers to the full picture when clicking on the icon in the bestiary
            //    PortraitPositionYOverride = 0f,
            //};
            //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 48;
            NPC.damage = 12;
            NPC.defense = 50;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 50);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f; // Take up open spawn slots, preventing random NPCs from spawning during the fight

            // Don't set immunities like this as of 1.4:
            // NPC.buffImmune[BuffID.Confused] = true;
            // immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

            // Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
            NPC.aiStyle = -1;

            // Custom boss bar (TODO)
            //NPC.BossBar = ModContent.GetInstance<MinionBossBossBar>();

            // The following code assigns a music track to the boss in a simple way.
            //if (!Main.dedServ)
            //{
            //    Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/??");
            //}
        }

        //public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        //{
        //    // Sets the description of this NPC that is listed in the bestiary
        //    //        bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
        //    //            new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
        //    //new FlavorTextBestiaryInfoElement("Example Minion Boss that spawns minions on spawn, summoned with a spawn item. Showcases boss minion handling, multiplayer conciderations, and custom boss bar.")
        //    //        });
        //}

        public override void AI()
        {
            // Fade in (we have NPC.alpha = 255 in SetDefaults which means it spawns transparent)
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 10;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
            // Default movement parameters (here for attacking)

            // Default movement parameters (here for attacking)

            Idle(player);
            SpawnMinions();


            switch (AI_State)
            {
                case (float)OrderByBoss.crush:
                    orderGiven = (int)OrderByBoss.crush;
                    FirstStageTimer++;

                    float speed = 21f;
                    float inertia = 1f;



                    if (FirstStageTimer >= 360 && FirstStageTimer <= 420)
                    {
                        //NPC.velocity.Y += 1f;


                        Console.WriteLine("!!");
                        //NPC.velocity = new Vector2(0, +5f);

                        Vector2 RelPosPlayer/*abovePlayer*/ = player.oldPosition + new Vector2(NPC.direction  /** offsetX*/, -NPC.height * -8);

                        Vector2 /*toAbovePlayer*/toRelPosPlayer = RelPosPlayer/*abovePlayer*/ - NPC.Center;

                        Vector2 toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ = toRelPosPlayer.SafeNormalize(Vector2.UnitY);
                        Vector2 moveTo = toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ * speed;
                        //NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
                        NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;

                        if (NPC.alpha < 255)
                        {
                            NPC.alpha += 21;

                            if (NPC.alpha > 255)
                            {
                                NPC.alpha = 255;
                            }
                        }


                    }
                    if (FirstStageTimer == 420)
                    {

                        NPC.position = player.position + new Vector2(NPC.direction *200f  /** offsetX*/, -NPC.height);
                    }
                    if (FirstStageTimer > 420)
                    {
                        //    NPC.alpha = 255;

                        Idle(player);
                        FirstStageTimer = 0;
                    }
                    break;


                case (float)OrderByBoss.MagicalIlusion:
                    orderGiven = (int)OrderByBoss.MagicalIlusion;



                    break;


                    ;

            }

            //CheckSecondStage();

            //// Be invulnerable during the first stage
            //NPC.dontTakeDamage = !SecondStage;

            //if (SecondStage)
            //{
            //    DoSecondStage(player);
            //}
            //else
            //{
            //    DoFirstStage(player);
            //}
            //if (FirstStageTimer == 1f)
            //{
            //    OrderByBoss(1);
            //}

        }

        private void Idle(Player player)
        {

            float speed = 17f;
            float inertia = 1f;

            float cubeRange = 64f;
            //float cubeRange = 64f;
            if (NPC.Top.Y > player.Bottom.Y)
            {
                speed = 12f;
            }



            Vector2 RelPosPlayer/*abovePlayer*/ = player.Top + new Vector2(NPC.direction  /** offsetX*/, -NPC.height * 5);

            Vector2 /*toAbovePlayer*/toRelPosPlayer = RelPosPlayer/*abovePlayer*/ - NPC.Center;

            Vector2 toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ = toRelPosPlayer.SafeNormalize(Vector2.UnitY);
            Vector2 moveTo = toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ * speed;
            //NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;


            if (NPC.direction == -1 && NPC.Center.X/* - changeDirOffset */< RelPosPlayer.X ||
                 NPC.direction == 1 && NPC.Center.X/* + changeDirOffset */> RelPosPlayer.X)
            {
                NPC.direction *= -1;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npcMinion = Main.npc[i];
                //if ()
                if ((npcMinion.ModNPC is Cube minion) && Vector2.Distance(npcMinion.Center, NPC.Center) < cubeRange)
                {
                    NPC.dontTakeDamage = true;
                    // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables


                }
                else
                {
                    NPC.dontTakeDamage = false;
                }


            }
        }
        private void MagicalIllusion(Player player)
        {


            float speed = 17f;
            float inertia = 1f;

            Vector2 RelPosPlayer/*abovePlayer*/ = player.Right + new Vector2(NPC.direction  /** offsetX*/, -NPC.height * 5);

            Vector2 /*toAbovePlayer*/toRelPosPlayer = RelPosPlayer/*abovePlayer*/ - NPC.Center;

            Vector2 toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ = toRelPosPlayer.SafeNormalize(Vector2.UnitY);
            Vector2 moveTo = toRelPosPlayerNormalized/*toAbovePlayerNormalized*/ * speed;
            //NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;



        }
        private void SpawnMinions()
        {
            if (SpawnedMinions)
            {
                // No point executing the code in this method again
                return;
            }

            SpawnedMinions = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // Because we want to spawn minions, and minions are NPCs, we have to do this on the server (or singleplayer, "!= NetmodeID.MultiplayerClient" covers both)
                // This means we also have to sync it after we spawned and set up the minion
                return;
            }

            int count = MinionCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Cube>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                // Now that the minion is spawned, we need to prepare it with data that is necessary for it to work
                // This is not required usually if you simply spawn NPCs, but because the minion is tied to the body, we need to pass this information to it

                if (minionNPC.ModNPC is Cube minion)
                {
                    // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                    minion.ParentIndex = NPC.whoAmI; // Let the minion know who the "parent" is
                    minion.PositionIndex = i; // Give it the iteration index so each minion has a separate one, used for movement
                }

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
            //public override void ModifyNPCLoot(NPCLoot npcLoot)
            //{
            //    // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            //    // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            //    npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<MinionBossBag>()));

            //    // Trophies are spawned with 1/10 chance
            //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.Furniture.MinionBossTrophy>(), 10));

            //    // ItemDropRule.MasterModeCommonDrop for the relic
            //    npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.Furniture.MinionBossRelic>()));

            //    // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //    npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            //    // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            //    LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            //    // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            //    // Boss masks are spawned with 1/7 chance
            //    notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

            //    // This part is not required for a boss and is just showcasing some advanced stuff you can do with drop rules to control how items spawn
            //    // We make 12-15 ExampleItems spawn randomly in all directions, like the lunar pillar fragments. Hereby we need the DropOneByOne rule,
            //    // which requires these parameters to be defined
            //    int itemType = ModContent.ItemType<ExampleItem>();
            //    var parameters = new DropOneByOne.Parameters()
            //    {
            //        ChanceNumerator = 1,
            //        ChanceDenominator = 1,
            //        MinimumStackPerChunkBase = 1,
            //        MaximumStackPerChunkBase = 1,
            //        MinimumItemDropsCount = 12,
            //        MaximumItemDropsCount = 15,
            //    };

            //    notExpertRule.OnSuccess(new DropOneByOne(itemType, parameters));

            //    // Finally add the leading rule
            //    npcLoot.Add(notExpertRule);
            //}

        }

        //public override void SetDefaults()
        //{
        //	Projectile.width = 10;
        //	Projectile.height = 10;
        //	//Projectile.friendly = false;
        //	Projectile.penetrate = 1;
        //	Projectile.tileCollide = true;
        //	Projectile.DamageType = DamageClass.Ranged;
        //	Projectile.timeLeft = 300;
        //	Projectile.hostile = true;
        //}

        //          public override void AI()
        //{
        //	NPC npc = Main.npc[Projectile.owner];
        //	float distanceFromTarget = 250f;
        //	Vector2 targetCenter = Projectile.position;
        //	float speed = 7f;
        //	float inertia = 0.5f;


        //	if (npc.target < 0 || !Main.player[npc.target].active || Main.player[npc.target].dead)
        //		{
        //		npc.TargetClosest(true);
        //	}
        //	if (npc.velocity.X < 0f)
        //	{
        //		npc.direction = -1;
        //	}
        //	else if (npc.velocity.X > 0f)
        //	{
        //		npc.direction = 1;
        //	}
        //	Projectile.ai[0]++;

        //	Projectile.velocity.X = Projectile.velocity.X;
        //	Projectile.velocity.Y = Projectile.velocity.Y;

        //          //Vector2 distance = Main.player[npc.target].Center - Projectile.Center;

        //          //float speed = 5f + distance.Length() / 100f;
        //          //float innercia = 25f;

        //          if (Projectile.ai[0] <=1f){

        //		//distance.Normalize();
        //		////distance *= speed;
        //		//Projectile.velocity = (Projectile.velocity * (inertia - 1f) + distance) / inertia;
        //		//Projectile.rotation = (Projectile.rotation * 9f + Projectile.velocity.X * 0.08f) / 10f;
        //		Vector2 direction = Main.player[npc.target].Center - Projectile.Center;
        //		//Vector2 direction = targetCenter - Projectile.Center;
        //		float distance = direction.Length(); //direction.Length()
        //											 //float speed
        //		speed += distance / 200f;
        //		//inertia
        //		direction.Normalize(); //direction.Normalize();
        //		direction *= speed; //direction *= speed;
        //		Projectile.velocity = (Projectile.velocity * (float)(inertia - 1) + direction) / (float)inertia;
        //		Projectile.spriteDirection = Projectile.direction;
        //		if (direction.Length() >= 250f)
        //		{
        //			Projectile.Kill();

        //		}

        //	}




        //       }
        //}
    }
}