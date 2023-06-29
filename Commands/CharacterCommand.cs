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
			=> "/character <add/remove/list/clear> [character name]";

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
					foreach(Character c in modPlayer.GetCharacters())
                    {
						output += c.Name + ", ";
                    }
					Main.NewText(output);
				}
				//else if(args[0] == "active")
    //            {
				//	Main.NewText("Your active character is: " + modPlayer.activeCharacter.Name);
    //            }
				else if(args[0] == "clear")
                {
					modPlayer.GetCharacters().Clear();
					//modPlayer.RemoveActiveCharacter();

					modPlayer.GetPartyCharacters().Clear();
					//for (int i = 0; i < 4; i++)
					//{
					//	modPlayer.partyCharacters.Add(new Character("None"));
					//}
					Main.NewText("Removed all characters");
                }
				else if(args[0] == "partyfill")
                {
					//for (int i = 0; i < 4; i++)
					//{
					//	modPlayer.partyCharacters.Add(new Character("None"));
					//}
				}
			}
			else if (args.Length > 1)
            {
				string character = "";
				for(int i = 1; i < args.Length; i++)
                {
					character += args[i] + " ";
                }
				character = character.Substring(0, character.Length - 1);
				if (args[0] == "add")
				{
					if (!modPlayer.AddCharacter(character))
					{
						Main.NewText("You already have that character!");
					}
					else
					{
						Main.NewText("Added " + character);
					}
				}
				else if (args[0] == "remove")
				{
					if (!modPlayer.RemoveCharacter(character))
					{
						Main.NewText("You don't have that character!");
					}
					else
					{
						Main.NewText("Removed " + character);
					}
				}
				else if (args[0] == "active")
				{
					//if (!modPlayer.ChangeActiveCharacter(character))
					//{
					//	Main.NewText("You don't have that character!");
					//}
					//else
					//{
					//	Main.NewText("Your active character is now: " + character);
					//}
				}

			}
		}
    }
}
