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
        DragableUIPanel mainWindow;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new DragableUIPanel();
            mainWindow.Width.Set(740, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(44, 44, 110);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f;
            mainWindow.PaddingRight = mainWindow.PaddingLeft = 20;

            // Header of the Main Window
            UIText mainWindowHeader = new UIText("Character Banner");
            mainWindowHeader.HAlign = 0.5f; 
            mainWindowHeader.Top.Set(15, 0); 
            mainWindow.Append(mainWindowHeader);

            // Button for pulling
            UIPanel pullButton = new UIPanel();
            pullButton.Width.Set(100, 0);
            pullButton.Height.Set(50, 0);
            pullButton.HAlign = 0.5f;
            pullButton.Top.Set(0, 0.87f);
            pullButton.BackgroundColor = new Color(44, 44, 176);
            pullButton.OnClick += WishClick;
            mainWindow.Append(pullButton);

            // Text for remove button
            UIText text = new UIText("Wish");
            text.HAlign = text.VAlign = 0.5f;
            pullButton.Append(text);

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
            List<string> fourStars = Character.GetAll4Stars();
            List<string> fiveStars = Character.GetAll5Stars();

            var rand = new Random();
            int chance = rand.Next(100);
            string character;
            if (chance < 5)
            {
                character = fiveStars[rand.Next(fiveStars.Count)];
            }
            else
            {
                character = fourStars[rand.Next(fourStars.Count)];
            }
            Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().AddCharacter(character);
            Main.NewText($"You got: {character}");

            // TODO: check if we have enough primo gems and reduce our number of primo gems as we do this
            // TODO: maybe pity system
        }

    }
}
