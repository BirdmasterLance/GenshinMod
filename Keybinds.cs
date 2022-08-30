using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace GenshinMod
{
    class Keybinds : ModSystem
    {
        public static ModKeybind CharacterUIHotKey;

		public override void Load()
		{
			CharacterUIHotKey = KeybindLoader.RegisterKeybind(Mod, "Character Menu", "G");
		}

		public override void Unload()
		{
			CharacterUIHotKey = null;
		}
	}

	class KeybindPlayer : ModPlayer
    {
		// Handles whenever a specific keybind is pressed
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Keybinds.CharacterUIHotKey.JustPressed && !Main.playerInventory && !Main.inFancyUI && !Main.InReforgeMenu && !Main.InGuideCraftMenu && !Main.hairWindow && !Main.ingameOptionsWindow && Main.LocalPlayer.talkNPC == -1)
			{
				if(UISystem.Instance.IsCharacerUIActive())
                {
					UISystem.Instance.HideCharacterUI();
				}
				else
                {
					UISystem.Instance.ShowCharacterUI();
				}
			}
		}
	}
}
