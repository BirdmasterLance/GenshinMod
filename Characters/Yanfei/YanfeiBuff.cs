using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Yanfei
{
    // For usage with Yanfei's attacks
    // TODO: visual indicator on the player like the Beetle Buff
    // TODO: At character ascension 1, each seal increases pyro damage by 5%

    internal class SignedEdictCooldown : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; // Probably should make the icons look like the characters
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skill Cooldown");
            // Description.SetDefault("Yanfei's Signed Edict is on cooldown");
        }
    }

    internal class DoneDealCooldown : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; // Probably should make the icons look like the characters
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Burst Cooldown");
            // Description.SetDefault("Yanfei's Done Deal is on cooldown");
        }
    }

    internal class ScarletSealBuff1 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Seals");
            // Description.SetDefault("Charged attack damage and knockback increase by 18%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int flameDust = Dust.NewDust(new Vector2(player.position.X + 10, player.position.Y), player.width, player.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust].noGravity = true;
            Main.dust[flameDust].noLight = true;
            Main.dust[flameDust].scale = 1;
            Main.dust[flameDust].velocity = Vector2.Zero;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int flameDust = Dust.NewDust(new Vector2(npc.position.X + 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust].noGravity = true;
            Main.dust[flameDust].noLight = true;
            Main.dust[flameDust].scale = 1;
            Main.dust[flameDust].velocity = Vector2.Zero;
        }
    }

    internal class ScarletSealBuff2 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Seals");
            // Description.SetDefault("Charged attack damage and knockback increase by 36%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int flameDust = Dust.NewDust(new Vector2(npc.position.X + 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust].noGravity = true;
            Main.dust[flameDust].noLight = true;
            Main.dust[flameDust].scale = 1;
            Main.dust[flameDust].velocity = Vector2.Zero;

            int flameDust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 10), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust2].noGravity = true;
            Main.dust[flameDust2].noLight = true;
            Main.dust[flameDust2].scale = 1;
            Main.dust[flameDust2].velocity = Vector2.Zero;
        }
    }

    internal class ScarletSealBuff3 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Seals");
            // Description.SetDefault("Charged attack damage and knockback increase by 54%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int flameDust = Dust.NewDust(new Vector2(npc.position.X + 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust].noGravity = true;
            Main.dust[flameDust].noLight = true;
            Main.dust[flameDust].scale = 1;
            Main.dust[flameDust].velocity = Vector2.Zero;

            int flameDust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 10), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust2].noGravity = true;
            Main.dust[flameDust2].noLight = true;
            Main.dust[flameDust2].scale = 1;
            Main.dust[flameDust2].velocity = Vector2.Zero;

            int flameDust3 = Dust.NewDust(new Vector2(npc.position.X - 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust3].noGravity = true;
            Main.dust[flameDust3].noLight = true;
            Main.dust[flameDust3].scale = 1;
            Main.dust[flameDust3].velocity = Vector2.Zero;
        }
    }

    internal class ScarletSealBuff4 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; 
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scarlet Seals");
            // Description.SetDefault("Charged attack damage and knockback increase by 72%");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int flameDust = Dust.NewDust(new Vector2(npc.position.X + 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust].noGravity = true;
            Main.dust[flameDust].noLight = true;
            Main.dust[flameDust].scale = 1;
            Main.dust[flameDust].velocity = Vector2.Zero;

            int flameDust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 10), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust2].noGravity = true;
            Main.dust[flameDust2].noLight = true;
            Main.dust[flameDust2].scale = 1;
            Main.dust[flameDust2].velocity = Vector2.Zero;

            int flameDust3 = Dust.NewDust(new Vector2(npc.position.X - 10, npc.position.Y), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust3].noGravity = true;
            Main.dust[flameDust3].noLight = true;
            Main.dust[flameDust3].scale = 1;
            Main.dust[flameDust3].velocity = Vector2.Zero;

            int flameDust4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y - 10), npc.width, npc.height / 2, DustID.RedTorch, 0f, 0f, 150, default(Color), 8f);
            Main.dust[flameDust4].noGravity = true;
            Main.dust[flameDust4].noLight = true;
            Main.dust[flameDust4].scale = 1;
            Main.dust[flameDust4].velocity = Vector2.Zero;
        }
    }

    // For Yanfei's ultimate
    // TODO: The shield that comes with it at higher levels

    internal class BrillianceBuff : ModBuff
    {
        private int Timer = 0;

        public override string Texture => "Terraria/Images/Buff_" + BuffID.WeaponImbueFire;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brilliance");
            // Description.SetDefault("Gain a Scarlet Seal every second");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Timer++;
            if (Timer % 60 == 0)
            {
                if (player.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
                {
                    player.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
                }
                else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
                {
                    player.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
                    player.ClearBuff(ModContent.BuffType<ScarletSealBuff3>());
                }
                else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
                {
                    player.AddBuff(ModContent.BuffType<ScarletSealBuff3>(), 600);
                    player.ClearBuff(ModContent.BuffType<ScarletSealBuff2>());
                }
                else if (player.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
                {
                    player.AddBuff(ModContent.BuffType<ScarletSealBuff2>(), 600);
                    player.ClearBuff(ModContent.BuffType<ScarletSealBuff1>());
                }
                else
                {
                    player.AddBuff(ModContent.BuffType<ScarletSealBuff1>(), 600);
                }
            }       
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Timer++;
            if (Timer % 60 == 0)
            {
                if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
                {
                    npc.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
                }
                else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
                {
                    npc.AddBuff(ModContent.BuffType<ScarletSealBuff4>(), 600);
                    npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff3>()));
                }
                else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
                {
                    npc.AddBuff(ModContent.BuffType<ScarletSealBuff3>(), 600);
                    npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff2>()));
                }
                else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
                {
                    npc.AddBuff(ModContent.BuffType<ScarletSealBuff2>(), 600);
                    npc.DelBuff(npc.FindBuffIndex(ModContent.BuffType<ScarletSealBuff1>()));
                }
                else
                {
                    npc.AddBuff(ModContent.BuffType<ScarletSealBuff1>(), 600);
                }
            }
        }
    }

    internal class YanfeiGlobalNPC : GlobalNPC
    {


        // Use PostDraw to draw sprites on top of NPCs
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D scarletSealTexture = ModContent.Request<Texture2D>("GenshinMod/Characters/Yanfei/ScarletSeal", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff1>()))
            {
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(0f, 10f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
            }
            else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff2>()))
            {
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(0f, 10f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(10f, 0f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
            }
            else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff3>()))
            {
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(0f, 10f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(10f, 0f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(-10f, 0f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
            }
            else if (npc.HasBuff(ModContent.BuffType<ScarletSealBuff4>()))
            {
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(0f, 10f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(10f, 0f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(-10f, 0f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
                spriteBatch.Draw(scarletSealTexture, npc.Center - screenPos + new Vector2(0f, -10f), null, drawColor, 0, scarletSealTexture.Size() * 0.5f, 0.5f, SpriteEffects.None, 1f);
            }

            if(npc.HasBuff(ModContent.BuffType<BrillianceBuff>()))
            {
                Texture2D brillianceShieldTexture = ModContent.Request<Texture2D>("GenshinMod/Characters/Yanfei/BrillianceShield", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw(brillianceShieldTexture, npc.position - screenPos, drawColor);
            }
        }
    }

}
