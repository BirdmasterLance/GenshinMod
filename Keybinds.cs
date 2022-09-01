using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace GenshinMod
{
    class Keybinds : ModSystem
    {
        public static ModKeybind CharacterUIHotKey;
		public static ModKeybind GachaUIHotKey;

		public override void Load()
		{
			CharacterUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Character Menu", "G");
			GachaUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Gacha Menu", "H");
		}

		public override void Unload()
		{
			CharacterUIHotKey = null;
			GachaUIHotKey = null;
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
		}
	}
}
