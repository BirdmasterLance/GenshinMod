using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Buffs
{
    // For usage with Yanfei's attacks
    // TODO: visual indicator on the player like the Beetle Buff
    // TODO: At character ascension 1, each seal increases pyro damage by 5%

    internal class SignedEdictCooldown : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; // Probably should make the icons look like the characters
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skill Cooldown");
            Description.SetDefault("Yanfei's Signed Edict is on cooldown");
        }
    }

    internal class DoneDealCooldown : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; // Probably should make the icons look like the characters
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burst Cooldown");
            Description.SetDefault("Yanfei's Done Deal is on cooldown");
        }
    }

    internal class ScarletSealBuff1 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Seals");
            Description.SetDefault("Charged attack damage and knockback increase by 18%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }
    }

    internal class ScarletSealBuff2 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Seals");
            Description.SetDefault("Charged attack damage and knockback increase by 36%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }
    }

    internal class ScarletSealBuff3 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Seals");
            Description.SetDefault("Charged attack damage and knockback increase by 54%");
            Main.buffNoSave[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }
    }

    internal class ScarletSealBuff4 : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_" + BuffID.OnFire; 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Seals");
            Description.SetDefault("Charged attack damage and knockback increase by 72%");
            Main.buffNoSave[Type] = true;
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
            DisplayName.SetDefault("Brilliance");
            Description.SetDefault("Gain a Scarlet Seal every second");
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
    }
}
