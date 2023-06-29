using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Commands
{
	internal class PartyCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "party";

		public override string Usage
			=> "/party <add/remove/change/list/clear/spawn> [character name] [slot]";

		public override string Description
			=> "Modify information about what the player's party";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			var modPlayer = caller.Player.GetModPlayer<PlayerCharacterCode>();

			if (args.Length == 0)
			{
				Main.NewText(Usage);
			}
			else if(args.Length == 1)
            {
				switch (args[0])
                {
					case "list":
						string output = "";
						foreach (Character c in modPlayer.GetPartyCharacters())
                        {
							output += c.Name + " ";
                        }
						Main.NewText("You have: " + output);
						break;
					case "clear":
						modPlayer.partyCharacters.Clear();
						Main.NewText("Cleared party");
						break;
				}
            }
			else if(args.Length == 2)
            {
				Character selectedCharacter = modPlayer.partyCharacters[int.Parse(args[1])];
				if(selectedCharacter == null)
                {
					Main.NewText("Not a valid character!");
					return;
                }
				switch (args[0])
                {
					case "add":
						modPlayer.AddPartyCharacter(args[1]);
						Main.NewText("Added " + args[1]);
						break;
					case "remove":
						selectedCharacter.GetNPC().life = 0;
						modPlayer.RemovePartyCharacter(args[1]);
						modPlayer.RemoveActiveCharacter(args[1]);
						Main.NewText("Removed " + args[1]);
						break;
					case "spawn":
						selectedCharacter.SpawnCharacter(modPlayer.Player);
						//if(!modPlayer.IsActiveCharacter(args[1]))
						//                  {
						//	selectedCharacter.SpawnCharacter(modPlayer.Player);
						//	modPlayer.AddActiveCharacter(args[1]);
						//	Main.NewText("Spawned " + selectedCharacter.Name);
						//}
						//else
						//                  {
						//	Main.NewText(args[1] + " is already on the field!");
						//                  }
						break;
					case "teleport":
						selectedCharacter.GetNPC().Teleport(Main.player[selectedCharacter.GetPlayerID()].position);
						Main.NewText("Spawned " + selectedCharacter.Name + " to player");
						break;
                }
            }
			else
            {
				Main.NewText(Usage);
			}
		}
	}
}
