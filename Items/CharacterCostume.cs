//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;

//namespace GenshinTest.Items
//{
//    class CharacterCostume : ModItem
//    {
//		public override void Load()
//		{
//			// The code below runs only if we're not loading on a server
//			if (Main.netMode == NetmodeID.Server)
//				return;

//			// Add equip textures
//			EquipLoader.AddEquipTexture(Mod, $"GenshinTest/Items/CosmicGarouCostume_Head", EquipType.Head, this);
//			EquipLoader.AddEquipTexture(Mod, $"GenshinTest/Items/CosmicGarouCostume_Body", EquipType.Body, this);
//			EquipLoader.AddEquipTexture(Mod, $"GenshinTest/Items/CosmicGarouCostume_Legs", EquipType.Legs, this);
//		}

//		// Called in SetStaticDefaults
//		private void SetupDrawing()
//		{
//			// Since the equipment textures weren't loaded on the server, we can't have this code running server-side
//			if (Main.netMode == NetmodeID.Server)
//				return;

//			int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
//			int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
//			int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);

//			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
//			ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
//			ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
//			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
//		}

//		public override void SetStaticDefaults()
//		{
//			DisplayName.SetDefault("Character Costume");
//			Tooltip.SetDefault("Replaces the player with their active character.");
//			SetupDrawing();
//		}

//		public override void SetDefaults()
//		{
//			Item.width = 24;
//			Item.height = 28;
//			// Item.accessory = true;
//			Item.value = 150000;
//			Item.rare = ItemRarityID.Expert;
//		}

//		public override void UpdateAccessory(Player player, bool hideVisual)
//		{
//			//PlayerCode p = player.GetModPlayer<PlayerCode>();
//			//p.characterAccessory = true; // Let the ModPlayer class we made know that the accessory is on
//			//p.replaceTexture = true; // Let the ModPlayer class we made know that we want to replace the texture
//		}

//		// Doing this for fun
//		Color[] itemNameCycleColors = new Color[]{
//			new Color(50, 255, 153),
//			new Color(0, 140, 255),
//			new Color(0, 255, 140),
//			new Color(255, 0, 115)
//		};
//		public override void ModifyTooltips(List<TooltipLine> tooltips)
//		{
//			// This code shows using Color.Lerp,  Main.GameUpdateCount, and the modulo operator (%) to do a neat effect cycling between 4 custom colors.
//			foreach (TooltipLine line2 in tooltips)
//			{
//				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
//				{
//					float fade = Main.GameUpdateCount % 60 / 60f;
//					int index = (int)(Main.GameUpdateCount / 60 % 4);
//					line2.OverrideColor = Color.Lerp(itemNameCycleColors[index], itemNameCycleColors[(index + 1) % 4], fade);
//				}
//			}
//		}
//	}
//}
