using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace GenshinMod.UI
{
    class GachaUI : UIState
    {
        UIPanel mainWindow;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new UIPanel();
            mainWindow.Width.Set(740, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(181, 225, 255);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f;
            mainWindow.PaddingRight = mainWindow.PaddingLeft = 20;

            // Button for pulling
            UIPanel pullButton = new UIPanel();
            pullButton.Width.Set(150, 0);
            pullButton.Height.Set(40, 0);
            pullButton.Top.Set(330, 0f);
            pullButton.Left.Set(370, 0f);
            pullButton.BackgroundColor = new Color(255, 255, 255);
            pullButton.BorderColor = new Color(194, 180, 105);
            pullButton.OnLeftClick += WishClick;
            pullButton.SetPadding(0);
            mainWindow.Append(pullButton);

            // Text for remove button
            UIText text = new UIText("Wish x1", 0.8f);
            text.HAlign = 0.5f;
            text.Top.Set(5, 0);
            pullButton.Append(text);

            UIPanel pullButton10 = new UIPanel();
            pullButton10.Width.Set(150, 0);
            pullButton10.Height.Set(40, 0);
            pullButton10.Top.Set(330, 0f);
            pullButton10.Left.Set(540, 0);
            pullButton10.BackgroundColor = new Color(255, 255, 255);
            pullButton10.BorderColor = new Color(194, 180, 105);
            pullButton10.SetPadding(0);
            pullButton10.OnLeftClick += WishClick10;
            mainWindow.Append(pullButton10);

            // Text for remove button
            UIText text10 = new UIText("Wish x10", 0.8f);
            text10.HAlign = 0.5f;
            text10.Top.Set(5, 0);
            pullButton10.Append(text10);

            Append(mainWindow);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (mainWindow.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (Main.playerInventory || Main.inFancyUI || Main.InReforgeMenu || Main.InGuideCraftMenu || Main.hairWindow || Main.ingameOptionsWindow || Main.LocalPlayer.talkNPC != -1)
            {
                UISystem.Instance.HideUIs();
            }
        }

        private void WishClick(UIMouseEvent evt, UIElement listeningElement)
        {
            GetRandomCharacter(1);
        }

        private void WishClick10(UIMouseEvent evt, UIElement listeningElement)
        {
            GetRandomCharacter(10);
        }

        private void GetRandomCharacter(int times)
        {
            List<string> fourStars = CharacterLists.FourStarCharacters;
            List<string> fiveStars = CharacterLists.FiveStarCharacters;

            // List for testing purposes only
            List<string> addedChars = CharacterLists.AddedCharacters;

            for (int i = 0; i < times; i++)
            {
                // Uncomment the below when we have more characters added
                var rand = new Random();
                //int chance = rand.Next(100);
                //string character;
                //if (chance < 5)
                //{
                //    character = fiveStars[rand.Next(fiveStars.Count)];
                //}
                //else
                //{
                //    character = fourStars[rand.Next(fourStars.Count)];
                //}
                string character = addedChars[rand.Next(addedChars.Count)];
                Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().AddCharacter(character);
                Main.NewText($"You got: {character}");

                // TODO: check if we have enough primo gems and reduce our number of primo gems as we do this
                // TODO: maybe pity system
            }
        }
    }
}
