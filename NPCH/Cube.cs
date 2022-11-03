//using ExampleMod.Common.Systems;
//using ExampleMod.Content.BossBars;
//using ExampleMod.Content.Items;
//using ExampleMod.Content.Items.Armor.Vanity;
//using ExampleMod.Content.Items.Consumables;
//using ExampleMod.Content.Pets.MinionBossPet;
//using ExampleMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;



namespace Hh1.BulletsorProjectiles
{
    public class Cube : ModNPC
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Boulder;
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Dust Cloud");
        //} // This boss has a second phase and we want to give it a second boss head icon, this variable keeps track of the registered texture from Load().
        //  // It is applied in the BossHeadSlot hook when the boss is in its second stage

        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;

        public const float RotationTimerMax = 360;
        public ref float RotationTimer => ref NPC.ai[2];

        // Helper method to determine the body type
        public static int BodyType()
        {
            return ModContent.NPCType<HallowedHypostasis>();
        }

        // This code here is called a property: It acts like a variable, but can modify other things. In this case it uses the NPC.ai[] array that has four entries.
        // We use properties because it makes code more readable ("if (SecondStage)" vs "if (NPC.ai[0] == 1f)").
        // We use NPC.ai[] because in combination with NPC.netUpdate we can make it multiplayer compatible. Otherwise (making our own fields) we would have to write extra code to make it work (not covered here)
        public bool SecondStage
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : 0f;
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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hypokube");
            Main.npcFrameCount[Type] = 1;

            // By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
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
        }
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 50;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Pixie;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.dontTakeDamage = true;
            // Take up open spawn slots, preventing random NPCs from spawning during the fight

            // Don't set immunities like this as of 1.4:
            // NPC.buffImmune[BuffID.Confused] = true;
            // immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

            // Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
            NPC.aiStyle = -1;

            // Custom boss bar
            //NPC.BossBar = ModContent.GetInstance<MinionBossBossBar>();


        }
        public override void HitEffect(int hitDirection, double damage)
        {
            // If the NPC dies, spawn gore and play a sound
            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"
                //int backGoreType = Mod.Find<ModGore>("MinionBossBody_Back").Type;
                //int frontGoreType = Mod.Find<ModGore>("MinionBossBody_Front").Type;

                //var entitySource = NPC.GetSource_Death();

                //for (int i = 0; i < 2; i++)
                //{
                //    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), backGoreType);
                //    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), frontGoreType);
                //}

                //SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];

            if (Despawn())
            {
                return;
            }

            FadeIn();

            MoveInFormation();



            


            switch (HallowedHypostasis.OrderByBossCasting())
            {
                case 0:
                    Crush(player);
                    break;
            }

        }


        private void FadeIn()
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
        }

        private void MoveInFormation()
        {
            NPC parentNPC = Main.npc[ParentIndex];

            // This basically turns the NPCs PositionIndex into a number between 0f and TwoPi to determine where around
            // the main body it is positioned at
            float rad = (float)PositionIndex / HallowedHypostasis.MinionCount() * MathHelper.TwoPi;

            // Add some slight uniform rotation to make the eyes move, giving a chance to touch the player and thus helping melee players
            RotationTimer += 0.5f;
            if (RotationTimer > RotationTimerMax)
            {
                RotationTimer = 0;
            }

            // Since RotationTimer is in degrees (0..360) we can convert it to radians (0..TwoPi) easily
            float continuousRotation = MathHelper.ToRadians(RotationTimer);
            rad += continuousRotation;
            if (rad > MathHelper.TwoPi)
            {
                rad -= MathHelper.TwoPi;
            }
            else if (rad < 0)
            {
                rad += MathHelper.TwoPi;
            }

            float distanceFromBody = parentNPC.width + NPC.width;

            // offset is now a vector that will determine the position of the NPC based on its index
            Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

            Vector2 destination = parentNPC.Center + offset;
            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);

            float speed = 21f;
            float inertia = 1;

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return true;
        }

        //private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        //{

        //    Vector2 idlePosition = owner.Center;
        //    idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

        //    // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
        //    // The index is projectile.minionPos
        //    //float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
        //    //idlePosition.X += minionPositionOffsetX; // Go behind the player

        //    // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

        //    // Teleport to player if distance is too big
        //    vectorToIdlePosition = idlePosition - Projectile.Center;
        //    distanceToIdlePosition = vectorToIdlePosition.Length();

        //    if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
        //    {
        //        // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
        //        // and then set netUpdate to true
        //        Projectile.position = idlePosition;
        //        Projectile.velocity *= 0.1f;
        //        Projectile.netUpdate = true;
        //    }



        // If your minion is flying, you want to do this independently of any conditions
        //float overlapVelocity = 0.04f;

        // Fix overlap with other minions
        //for (int i = 0; i < Main.maxProjectiles; i++)
        //{
        //    Projectile other = Main.projectile[i];

        //    if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
        //    {
        //        if (Projectile.position.X < other.position.X)
        //        {
        //            Projectile.velocity.X -= overlapVelocity;
        //        }

        //        else
        //        {
        //            Projectile.velocity.X += overlapVelocity;
        //        }

        //        if (Projectile.position.Y < other.position.Y)
        //        {
        //            Projectile.velocity.Y -= overlapVelocity;
        //        }
        //        else
        //        {
        //            Projectile.velocity.Y += overlapVelocity;
        //        }
        //    }
        //}
        private void Crush(Player target)
        {

            FirstStageTimer++;
            // Default movement parameters (here for attacking)
            float speed = 8f;
            float inertia = 20f;
            float offsetX = 200f;



            if (FirstStageTimer >= 120&& FirstStageTimer <360)
            {
                Vector2 formation = target.Top * -1f;
                float rad = (float)PositionIndex / HallowedHypostasis.MinionCount() * MathHelper.TwoPi;


                // Minion has a target: attack (here, fly towards the enemy)


                float distanceFromBody = target.width + NPC.width;

                // offset is now a vector that will determine the position of the NPC based on its index
                Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

                //    Vector2 destination = target.Center + offset;
                //    Vector2 toDestination = destination - NPC.Center;
                //    Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);
                //    Vector2 moveTo = toDestinationNormalized * speed;
                //    NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;


                NPC.position= target.Center + offset;
                
            }
            if (FirstStageTimer>360) {
                FirstStageTimer = 0;
            }
        }

        //}
        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient &&
                (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType()))
            {
                // * Not spawned by the boss body (didn't assign a position and parent) or
                // * Parent isn't active or
                // * Parent isn't the body
                // => invalid, kill itself without dropping any items
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                return true;
            }
            return false;
        }


    }
    //public override void SetDefaults()
    //{
    //	Projectile.damage = 0;
    //	Projectile.width = 10;
    //	Projectile.height = 10;
    //	//Projectile.friendly = false;
    //	Projectile.penetrate = 1;
    //	Projectile.tileCollide = true;
    //	Projectile.DamageType = DamageClass.Ranged;
    //	Projectile.timeLeft = 300;
    //	Projectile.hostile = true;
    //	Projectile.ignoreWater = true;         
    //	Projectile.tileCollide = true;

    //}

    //public override void AI()
    //{
    //	Projectile.ai[0]++;



    //	Projectile.velocity.Y -= -1;


    //	NPC npc = Main.npc[Projectile.owner];
    //	if(Projectile.ai[0]== 60)
    //          {

    //		Projectile.Kill();


    //          }


    //}
    //public override bool OnTileCollide(Vector2 oldVelocity)
    //{
    //	//If collide with tile, reduce the penetrate.
    //	//So the Projectile can reflect at most 5 times
    //	Projectile.Kill();
    //	//Projectile.penetrate--;
    //	//if (Projectile.penetrate <= 0)
    //	//{
    //	//	Projectile.Kill();
    //	//}
    //	return false;
    //}
    //      public override void Kill(int timeLeft)      
    //      {
    //	if (Projectile.penetrate == 1) {
    //		// Makes the Projectile hit all enemies as it circunvents the penetrate limit.
    //		Projectile.maxPenetrate = -1;
    //		Projectile.penetrate = -1;
    //		Projectile.maxPenetrate = -1;
    //	//Projectile.penetrate = -1;
    //		//Projectile.width = 70;
    //		//Projectile.height = 70;
    //		Projectile.tileCollide = false;
    //	//Projectile.velocity *= 0.01f;
    //	Projectile.damage = 20;
    //	//Projectile.Damage();


    //	int explosionArea = 30;
    //	Vector2 oldSize = Projectile.Size;
    //	// Resize the Projectile hitbox to be bigger.
    //	Projectile.position = Projectile.Center;
    //	Projectile.Size += new Vector2(explosionArea);
    //	Projectile.Center = Projectile.position;
    //	Projectile.tileCollide = false;
    //	Projectile.velocity *= 0.01f;
    //	// Damage enemies inside the hitbox area

    //	Projectile.Damage();
    //	Projectile.scale = 0.01f;
    //	}



    //        for (int g = 0; g < 2; g++)
    //{
    //	int goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)0 , Projectile.position.Y + (float)0), default(Vector2), Main.rand.Next(61, 64), 1f);
    //	Main.gore[goreIndex].scale = 1.5f;
    //	Main.gore[goreIndex].velocity.X = 0.1f;
    //	Main.gore[goreIndex].velocity.Y = 0.1f;
    //            //goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
    //            //Main.gore[goreIndex].scale = 1.5f;
    //            //Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
    //            //Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
    //            //goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
    //            //Main.gore[goreIndex].scale = 1.5f;
    //            //Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
    //            //Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
    //            //goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
    //            //Main.gore[goreIndex].scale = 1.5f;
    //            //Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
    //            //Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
    //        }



}