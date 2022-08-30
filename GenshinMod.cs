using GenshinMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace GenshinMod
{
	public class GenshinMod : Mod
	{
        public static GenshinMod Instance;

		public override void Load()
		{
            if(Instance == null)
            {
                Instance = this;
            }

			if (!Main.dedServ)
			{
                // We need to load ALL of the textures for each character here first

                //EquipLoader.AddEquipTexture(this, $"GenshinTest/Items/[character here]_Head", EquipType.Head, name: "[character here]");
                //EquipLoader.AddEquipTexture(this, $"GenshinTest/Items/[character here]_Body", EquipType.Body, name: "[character here]");
                //EquipLoader.AddEquipTexture(this, $"GenshinTest/Items/[character here]_Legs", EquipType.Legs, name: "[character here]");

            }
        }

		public override void Unload()
		{
        }
    }
}