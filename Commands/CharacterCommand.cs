using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Commands
{
    class CharacterCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "character";

		public override string Usage
			=> "/character <add/remove/list/active> [character name]";

		public override string Description
			=> "Modify information about what characters the player has";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// ModPlayer version of the player who used the command
			var modPlayer = caller.Player.GetModPlayer<PlayerCharacterCode>();

			if(args.Length == 0)
            {
				Main.NewText(Usage);
            }
			else if(args.Length == 1)
            {
				if (args[0] == "list")
				{
					Main.NewText("You have: ");
					string output = "";
					foreach (string s in modPlayer.GetCharacters())
					{
						output += s + ", ";
					}
					Main.NewText(output);
				}
				else if(args[0] == "active")
                {
					Main.NewText("Your active character is: " + modPlayer.activeCharacter);
                }
			}
			else if(args.Length == 2)
            {
				// For later, make a global list of all characters?
				List<string> characters = modPlayer.GetCharacters();
				if (args[0] == "add")
                {
					if(characters.Contains(args[1]))
                    {
						Main.NewText("You already have that character!");
                    }
					else
                    {
						Main.NewText("Added " + args[1]);
						characters.Add(args[1]);
                    }
                }
				else if (args[0] == "remove")
				{
					if (!characters.Contains(args[1]))
					{
						Main.NewText("You don't have that character!");
					}
					else
					{
						Main.NewText("Removed " + args[1]);
						characters.Remove(args[1]);
					}
				}
				else if(args[0] == "active")
                {
					if (!characters.Contains(args[1]))
					{
						Main.NewText("You don't have that character!");
					}
					else
					{
						Main.NewText("Your active character is now: " + args[1]);
						modPlayer.ChangeActiveCharacter(args[1]);
					}
				}
			}
		}
    }
}
