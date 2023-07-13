using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Characters.Yanfei
{
    internal class YanfeiNPC : ModNPC
    {

		public override string Texture => "Terraria/Images/NPC_" + NPCID.Stylist;

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Stylist];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				// Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 14;
			NPC.defense = 6;
			NPC.lifeMax = 200;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 7;
			NPC.friendly = true;
			AnimationType = NPCID.Stylist;
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

        public override void AI()
        {
			// Might not work in multiplayer?;
			PlayerCharacterCode modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
			for(int i = 0; i < modPlayer.partyCharacters.Count; i++)
            {
				if (NPC.whoAmI == modPlayer.partyCharacters[i].GetNPCID())
                {
					modPlayer.partyCharacters[i].life = NPC.life;
					int flameDust = Dust.NewDust(NPC.position, NPC.width, NPC.height / 2, DustID.CorruptTorch, 0f, 0f, 150, default(Color), 8f);
					Main.dust[flameDust].noGravity = true;
					Main.dust[flameDust].noLight = true;
					Main.dust[flameDust].scale = Main.rand.NextFloat() * 3f;
					Main.dust[flameDust].fadeIn = Main.rand.NextFloat() * 1f;
					Main.dust[flameDust].velocity *= Main.rand.NextFloat() * 40f;
				}
            }
        }
    }
}
