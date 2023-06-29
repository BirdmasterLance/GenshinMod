using GenshinMod.Buffs;
using GenshinMod.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace GenshinMod
{
    class Keybinds : ModSystem
    {
        public static ModKeybind CharacterUIHotKey;
		public static ModKeybind PartyUIHotKey;
		public static ModKeybind GachaUIHotKey;
		public static ModKeybind ElementalSkill;
		public static ModKeybind ElementalBurst;
		public static ModKeybind SwapCharacter;

		public override void Load()
		{
			CharacterUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Character Menu", "C");
			PartyUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Party Menu", "L");
			GachaUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Gacha Menu", "F3");
			ElementalSkill = KeybindLoader.RegisterKeybind(Mod, "Elemental Skill", "E");
			ElementalBurst = KeybindLoader.RegisterKeybind(Mod, "Elemental Burst", "Q");
			SwapCharacter = KeybindLoader.RegisterKeybind(Mod, "Swap Character", "Z");
		}

		public override void Unload()
		{
			CharacterUIHotKey = null;
			PartyUIHotKey = null;
			GachaUIHotKey = null;
			ElementalSkill= null;
			ElementalBurst = null;
		}
	}

	class KeybindPlayer : ModPlayer
    {
		public int partyCharacterIndex = 0;

		// Handles whenever a specific keybind is pressed
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Keybinds.CharacterUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null) UISystem.Instance.ShowCharacterUI();
				else UISystem.Instance.HideUIs();
			}

			if (Keybinds.PartyUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null) UISystem.Instance.ShowPartyUI();
				else UISystem.Instance.HideUIs();
			}

			if (Keybinds.GachaUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null) UISystem.Instance.ShowGachaUI();
				else UISystem.Instance.HideUIs();
			}


			if (Main.myPlayer == Player.whoAmI)
            {
				Player player = Main.player[Main.myPlayer];
				PlayerCharacterCode modPlayer = Main.player[Main.myPlayer].GetModPlayer<PlayerCharacterCode>();
				
				if (Keybinds.SwapCharacter.JustPressed)
                {
					CycleThroughPartyCharacters(modPlayer);
                }

				//if (Keybinds.ElementalSkill.JustPressed && modPlayer.activeCharacter != null && Collision.CanHitLine(player.position, 0, 0, Main.MouseWorld, 0, 0))
				//{
				//	if (modPlayer.activeCharacter == null) return;
				//	if(modPlayer.activeCharacter.Name == "Yanfei" && !player.HasBuff<Characters.Yanfei.YanfeiSkillCooldown>())
    //                {
				//		Projectile proj = Main.projectile[ModContent.ProjectileType<Characters.Yanfei.YanfeiSkill>()];
				//		Vector2 projSpawnPos = new Vector2(Main.MouseWorld.X - proj.width, Main.MouseWorld.Y - proj.height);
				//		Projectile.NewProjectile(Projectile.InheritSource(proj), projSpawnPos, Vector2.Zero, ModContent.ProjectileType<Characters.Yanfei.YanfeiSkill>(), proj.damage, proj.knockBack, Main.myPlayer);
				//		player.AddBuff(ModContent.BuffType<Characters.Yanfei.YanfeiSkillCooldown>(), 540);
				//	}
				//}

				//if (Keybinds.ElementalBurst.JustPressed)
				//{
				//	if (modPlayer.activeCharacter == null) return;
				//	if (modPlayer.activeCharacter.Name == "Yanfei" && !player.HasBuff<Characters.Yanfei.YanfeiBurstCooldown>())
				//	{
				//		Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<BrillianceBuff>(), 900);
				//		Projectile proj = Main.projectile[ModContent.ProjectileType<Characters.Yanfei.YanfeiBurst>()];
				//		Projectile.NewProjectile(Projectile.InheritSource(proj), Main.player[Main.myPlayer].position, Vector2.Zero, ModContent.ProjectileType<Characters.Yanfei.YanfeiBurst>(), proj.damage, proj.knockBack, Main.myPlayer);
				//		player.AddBuff(ModContent.BuffType<Characters.Yanfei.YanfeiBurstCooldown>(), 1200);
				//	}
				//}
			}      
		}

		// Used to cycle through the party characters one by one
		// Might be deleted in favor of using a specific key to pick specific characters
		private void CycleThroughPartyCharacters(PlayerCharacterCode modPlayer)
        {
			int lastValidCharacter = 0;
			for (int i = 0; i < 4; i++)
			{
				if (modPlayer.partyCharacters[i].Name == "None") continue;
				lastValidCharacter = i;
			}

			if (partyCharacterIndex == lastValidCharacter)
			{
				for (int i = 0; i < 3; i++)
				{
					if (modPlayer.partyCharacters[i].Name == "None") continue;
					//modPlayer.ChangeActiveCharacter(modPlayer.partyCharacters[i].Name);
					partyCharacterIndex = i;
					break;
				}
			}
			else
            {
				for (int i = 0; i < 4; i++)
				{
					if (modPlayer.partyCharacters[i].Name == "None") continue;
					if (i <= partyCharacterIndex) continue;
					//modPlayer.ChangeActiveCharacter(modPlayer.partyCharacters[i].Name);
					partyCharacterIndex = i;
					break;
				}
			}
		}
	}
}
