using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using Terraria.Graphics.Effects;

using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Hh1.NPCH
{
    //[AutoloadBossHead]
    class Chest : ModNPC
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.GoldChest;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regular Chest");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.friendly = true;
            NPC.damage = 0;
            NPC.defense = 8;
            NPC.lifeMax = 50502;
            NPC.HitSound = SoundID.NPCHit4;
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
            NPC.dontTakeDamage = true;
            NPC.rarity = 1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Spawn this NPC with something like Cheat Sheet or Hero's Mod
            return Terraria.ModLoader.Utilities.SpawnCondition.Overworld.Chance * 0.1f;
        }
        //test
        private static bool IsNpcOnscreen(Vector2 center)
        {
            int w = NPC.sWidth + NPC.safeRangeX * 2;
            int h = NPC.sHeight + NPC.safeRangeY * 2;
            Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
            foreach (Player player in Main.player)
            {
                // If any player is close enough to the chest, it will prevent the npc from despawning
                if (player.active && player.getRect().Intersects(npcScreenRect)) return true;
            }
            return false;
        }
        private static bool CanSpawnNow()
        {
            // can't spawn if any events are running
            if (Main.eclipse || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
                return false;

            // can't spawn if the sundial is active
            if (Main.fastForwardTime)
                return false;

            // can spawn if daytime, and between the spawn and despawn times
            return Main.dayTime ;
        }

        
        public override bool PreAI()
        {
            Player p = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (!IsNpcOnscreen(NPC.Center)) // If it's past the despawn time and the NPC isn't onscreen
            {
                NPC.active = false;
                NPC.netSkip = -1;
                NPC.life = 0;
                return false;
            }

            return true;
        }

    
        public override void AI()
        {

           
            NPC.homeless = true;
            // death drama
            //if (NPC.ai[3] > 0f)
            //{
            //    NPC.dontTakeDamage = true;
            //    NPC.ai[3] += 1f; // increase our death timer.
            //    if (NPC.ai[3] >= 180f)
            //    {
            //        NPC.life = 0;
            //        NPC.HitEffect(0, 0);
            //        NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
            //    }
            //    return;
            //}

            // Now we check the make sure the target is still valid and within our specified notice range (500)
            //if (/*NPC.HasValidTarget &&*/ Main.player[NPC.target].Distance(NPC.Center) <= 50f)
            //{
            //    Item.NewItem(NPC.getRect(), ItemID.Heart, 10);
            //    NPC.life = 0;

            //}


            //if (Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1) && Main.player[NPC.target].Distance(NPC.Center) <= 10f)
            //{
            //	NPC.life = 0;
            //	NPC.active = false;
            //}
        }
        //public override void NPCLoot()
        //{



        //}
        public override bool CanChat()
        {
            return true;
        }

        public override string GetChat()
        {
            // NPC.SpawnedFromStatue value is kept when the NPC is transformed.
            
                
               
                    return "Regular Chest";
            
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Open";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ItemID.Heart, 10);
                NPC.life = 0;


            }
        }
    // PreDraw and PostDraw are responsible for applying and then removing the shader. If you omit PostDraw, the following NPC to be drawn will inherit the shader, so don't do that.
    // Basically, we need to End the previous spriteBatch, start it again, apply our shader, draw the NPC, and finally End and Start a fresh spriteBatch.
    //public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
    //{
    //    Main.spriteBatch.End();
    //    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

    //    // Retrieve reference to shader
    //    var deathShader = GameShaders.Misc["Test:Chest"];

    //    // Reset back to default value.
    //    deathShader.UseOpacity(1f);
    //    // We use NPC.ai[3] as a counter since the real death.
    //    if (NPC.ai[3] > 30f)
    //    {
    //        // Our shader uses the Opacity register to drive the effect. See ExampleEffectDeath.fx to see how the Opacity parameter factors into the shader math. 
    //        deathShader.UseOpacity(1f - (NPC.ai[3] - 30f) / 150f);
    //    }
    //    // Call Apply to apply the shader to the SpriteBatch. Only 1 shader can be active at a time.
    //    deathShader.Apply(null);
    //    return true;
    //}
    //    public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
    //    {
    //        // As mentioned above, be sure not to forget this step.
    //        Main.spriteBatch.End();
    //        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
    //    }
    //    public override bool CheckDead()
    //    {
    //        if (NPC.ai[3] == 0f)
    //        {
    //            NPC.ai[3] = 1f;
    //            NPC.damage = 0;
    //            NPC.life = NPC.lifeMax;
    //            NPC.dontTakeDamage = true;
    //            NPC.netUpdate = true;
    //            return false;
    //        }
    //        return true;
    //    }

    }
}
