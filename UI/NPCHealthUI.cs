using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.Localization;

namespace GenshinMod.UI
{
    internal class NPCHealthBar : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		private UIText text, characterName;
		private UIElement area;
		private UIImage barFrame;
		private UIPanel characterPfp;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 300, 1f); // Place the resource bar to the left of the hearts.
			area.VAlign = 0.9f;
			area.Width.Set(360, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(90, 0f);

			barFrame = new UIImage(ModContent.Request<Texture2D>("GenshinMod/UI/ExampleResourceFrame")); // Frame of our resource bar
			barFrame.Left.Set(47, 0f);
			barFrame.Top.Set(10, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			characterPfp = new();
			characterPfp.Width.Set(50, 0);
			characterPfp.Height.Set(50, 0);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(50, 0f);
			text.Left.Set(0, 0f);

			characterName = new UIText("Character Name", 0.8f);
			characterName.Width.Set(138, 0f);
			characterName.Height.Set(34, 0f);
			characterName.Top.Set(0, 0f);
			characterName.Left.Set(35, 0f);

			gradientA = new Color(123, 25, 138); // A dark purple
			gradientB = new Color(187, 91, 201); // A light purple

			area.Append(text);
			area.Append(barFrame);
			area.Append(characterName);
			area.Append(characterPfp);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// This prevents drawing unless we are using an ExampleCustomResourceWeapon
			//if (Main.LocalPlayer.HeldItem.ModItem is not ExampleCustomResourceWeapon)
			//	return;

			base.Draw(spriteBatch);
		}

		// Here we draw our UI
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
			var characterNPC = modPlayer.activeCharacters[0].GetNPC();
			if (characterNPC == null) return;
			// Calculate quotient
			float quotient = (float)characterNPC.life / characterNPC.lifeMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				// float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}
		}

		public override void Update(GameTime gameTime)
		{
			//if (Main.LocalPlayer.HeldItem.ModItem is not ExampleCustomResourceWeapon)
			//	return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
			var characterNPC = modPlayer.activeCharacters[0].GetNPC();
			if (characterNPC == null) return;
			// Setting the text per tick to update and show our resource values.
			text.SetText(string.Format("{0} / {1}", characterNPC.life, characterNPC.lifeMax));
			characterName.SetText(modPlayer.activeCharacters[0].Name);
			base.Update(gameTime);
		}
	}

	// This class will only be autoloaded/registered if we're not loading on a server
	[Autoload(Side = ModSide.Client)]
	internal class ExampleResourceUISystem : ModSystem
	{
		private UserInterface ExampleResourceBarUserInterface;

		internal NPCHealthBar NPCHealthBar;

		public static LocalizedText ExampleResourceText { get; private set; }

		public override void Load()
		{
			NPCHealthBar = new();
			ExampleResourceBarUserInterface = new();
			ExampleResourceBarUserInterface.SetState(NPCHealthBar);

			string category = "UI";
			ExampleResourceText ??= Language.GetText("waa");
			//.GetOrRegister(Mod.GetLocalizationKey($"{category}.ExampleResource"));
		}

		public override void UpdateUI(GameTime gameTime)
		{
			ExampleResourceBarUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1)
			{
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Resource Bar",
					delegate {
						ExampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
