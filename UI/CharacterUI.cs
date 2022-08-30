using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;

namespace GenshinMod.UI
{
    class CharacterListUI : UIState
    {
        DragableUIPanel mainWindow;
        UIList list;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new DragableUIPanel();
            mainWindow.Width.Set(740, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(44, 44, 110);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f; // Center the main window
            mainWindow.PaddingRight = mainWindow.PaddingLeft = 20;

            // Header of the Main Window
            UIText mainWindowHeader = new UIText("Characters");
            mainWindowHeader.HAlign = 0.5f; // Centers the element horizontally based on its parent
            mainWindowHeader.Top.Set(15, 0); // Sets the element to 15 pixels down from the top of the parent
            mainWindow.Append(mainWindowHeader); // Add the element to its parent

            // Panel to put the list of characters in
            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(220, 0f);
            listPanel.Height.Set(0, 0.7f);
            listPanel.Left.Set(0, 0.01f);
            listPanel.Top.Set(0, 0.15f);
            listPanel.BackgroundColor = new Color(47, 50, 102);
            listPanel.OverflowHidden = false;
            listPanel.PaddingLeft = listPanel.PaddingRight = 10;
            mainWindow.Append(listPanel);

            // The element that will store the buttons for all the characters the player has
            list = new();
            list.SetPadding(0);
            list.Width.Set(0, 0.9f);
            list.Height.Set(0, 0.9f);
            list.Left.Set(20, 0);
            list.Top.Set(0, 0.05f);
            listPanel.Append(list);

            // Scrollbar
            UIScrollbar scroll = new();
            scroll.Width.Set(20, 0);
            scroll.Height.Set(0, 0.825f);
            scroll.Left.Set(0, 0.95f);
            scroll.Top.Set(0, 0.1f);

            list.SetScrollbar(scroll);
            list.Append(scroll);
            list.ListPadding = 10;

            // Button at the bottom that removes the active character
            UIPanel removeCharButton = new UIPanel();
            removeCharButton.Width.Set(100, 0);
            removeCharButton.Height.Set(50, 0);
            removeCharButton.Left.Set(0, 0.1f);
            removeCharButton.Top.Set(0, 0.87f);
            removeCharButton.BackgroundColor = new Color(44, 44, 176);
            removeCharButton.OnClick += RemoveCharacterClick; 
            mainWindow.Append(removeCharButton);

            // Text for remove button
            UIText text = new UIText("Remove Character");
            text.HAlign = text.VAlign = 0.5f;
            removeCharButton.Append(text);

            // Panel to show what the character looks like
            UIPanel charPanel = new UIPanel();
            charPanel.Width.Set(220, 0f);
            charPanel.Height.Set(0, 0.7f);
            charPanel.Left.Set(0, 0.345f);
            charPanel.Top.Set(0, 0.15f);
            charPanel.BackgroundColor = new Color(47, 50, 102);
            mainWindow.Append(charPanel);

            // Panel to show the character's skills
            UIPanel skillsPanel = new UIPanel();
            skillsPanel.Width.Set(220, 0f);
            skillsPanel.Height.Set(0, 0.7f);
            skillsPanel.Left.Set(0, 0.68f);
            skillsPanel.Top.Set(0, 0.15f);
            skillsPanel.BackgroundColor = new Color(47, 50, 102);
            skillsPanel.PaddingLeft = skillsPanel.PaddingRight = 20;
            mainWindow.Append(skillsPanel);

            Append(mainWindow);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Prevent the player from using any items while the menu is open
            // if (ContainsPoint(Main.MouseScreen))
            if(mainWindow.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            // Automatically closes the menu if any of these conditions are true
            // (These conditions usually refer to opening some other menu, so it'll auto close when we open them)
            if(Main.playerInventory || Main.inFancyUI || Main.InReforgeMenu || Main.InGuideCraftMenu || Main.hairWindow || Main.ingameOptionsWindow || Main.LocalPlayer.talkNPC != -1)
            {
                UISystem.Instance.HideCharacterUI();
            }
        }

        private void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();

            // For now, let's determine which character the player picked based on the text on the button
            // We have to go through all the children of the element that called this event
            // Thankfully theres only one child in the panel, so it's ok
            foreach(UIElement element in listeningElement.Children)
            {
                UIText text = (UIText) element;
                modPlayer.ChangeActiveCharacter(text.Text);
            }
        }

        private void RemoveCharacterClick(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.RemoveActiveCharacter();
        }

        // Call this method when you open the menu, so we can populate the list with all the player's characters
        // This has to be its own method because tryign to do this in OnInitialize will crash the mod
        // as there is no LocalPlayer loaded at that point.
        public void OpenMenu()
        {
            list.Clear();

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            foreach (string characterName in modPlayer.GetCharacters())
            {
                UIPanel button = new UIPanel();
                button.Width.Set(150, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(44, 44, 176);
                button.OnClick += OnButtonClick;
                list.Add(button);

                UIText text = new UIText(characterName);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);
            }
        }

    }
}
