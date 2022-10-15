using GenshinMod.Buffs;
using GenshinMod.Items;
using GenshinMod.UI;
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

		public override void Load()
		{
			CharacterUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Character Menu", Microsoft.Xna.Framework.Input.Keys.C);
			PartyUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Party Menu", Microsoft.Xna.Framework.Input.Keys.L);
			GachaUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Gacha Menu", Microsoft.Xna.Framework.Input.Keys.F3);
			ElementalSkill = KeybindLoader.RegisterKeybind(Mod, "Elemental Skill", Microsoft.Xna.Framework.Input.Keys.E);
			ElementalBurst = KeybindLoader.RegisterKeybind(Mod, "Elemental Burst", Microsoft.Xna.Framework.Input.Keys.Q);
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
		// Handles whenever a specific keybind is pressed
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Keybinds.CharacterUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null)
				{
					UISystem.Instance.ShowCharacterUI();
				}
				else
				{
					UISystem.Instance.HideUIs();
				}
			}

			if (Keybinds.PartyUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null)
				{
					UISystem.Instance.ShowPartyUI();
				}
				else
				{
					UISystem.Instance.HideUIs();
				}
			}

			if (Keybinds.GachaUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if (UISystem.Instance.GenshinInterface.CurrentState == null)
				{
					UISystem.Instance.ShowGachaUI();
				}
				else
				{
					UISystem.Instance.HideUIs();
				}
			}


			if (Main.myPlayer == Player.whoAmI)
            {
				if (Keybinds.ElementalSkill.JustPressed && Main.player[Main.myPlayer].GetModPlayer<PlayerCharacterCode>().activeCharacter != null && Collision.CanHitLine(Main.player[Main.myPlayer].position, 0, 0, Main.MouseWorld, 0, 0))
				{
					if(Main.player[Main.myPlayer].GetModPlayer<PlayerCharacterCode>().activeCharacter.Name == "Yanfei")
                    {
						Projectile proj = Main.projectile[ModContent.ProjectileType<YanfeiSkill>()];
						Projectile.NewProjectile(Projectile.InheritSource(proj), Main.MouseWorld, Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<YanfeiSkill>(), 50, proj.knockBack, Main.myPlayer);
					}
				}

				if (Keybinds.ElementalBurst.JustPressed)
				{
					if (Main.player[Main.myPlayer].GetModPlayer<PlayerCharacterCode>().activeCharacter.Name == "Yanfei")
					{
						Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<BrillianceBuff>(), 900);
						Projectile proj = Main.projectile[ModContent.ProjectileType<YanfeiBurst>()];
						Projectile.NewProjectile(Projectile.InheritSource(proj), Main.player[Main.myPlayer].position, Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<YanfeiBurst>(), 70, proj.knockBack, Main.myPlayer);
					}
				}
			}      
		}
	}
}
