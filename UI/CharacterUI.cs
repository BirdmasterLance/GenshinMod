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
        UIText charName;
        UIText attributeText;
        UIText artifactsText;
        UIText constellationText;
        UIText talentText;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new DragableUIPanel();
            mainWindow.Width.Set(900, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(44, 44, 110);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f; // Center the main window
            mainWindow.PaddingRight = mainWindow.PaddingLeft = 20;



            //// Header of the Main Window
            //UIText mainWindowHeader = new UIText("Characters");
            //mainWindowHeader.HAlign = 0.5f; // Centers the element horizontally based on its parent
            //mainWindowHeader.Top.Set(15, 0); // Sets the element to 15 pixels down from the top of the parent
            //mainWindow.Append(mainWindowHeader); // Add the element to its parent

            // Panel to put the list of characters in
            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(70, 0f);
            listPanel.Height.Set(370, 0f);
            listPanel.Left.Set(0, 0f);
            listPanel.Top.Set(0, 0f);
            listPanel.BackgroundColor = new Color(99, 98, 112);
            listPanel.BorderColor = new Color(166, 165, 181);
            listPanel.OverflowHidden = false;
            listPanel.PaddingLeft = listPanel.PaddingRight = 10;
            mainWindow.Append(listPanel);

            // The element that will store the buttons for all the characters the player has
            list = new();
            list.SetPadding(0);
            list.Width.Set(70, 0f);
            list.Height.Set(370, 0f);
            list.Left.Set(0, 0);
            list.Top.Set(0, 0);
            listPanel.Append(list);

            list.ListPadding = 10;

            // Character's Name & Element
            charName = new UIText("[c/bcc6cf:element / name]", 0.85f);
            charName.Top.Set(15, 0);
            charName.Left.Set(80, 0);
            mainWindow.Append(charName);

            // Character's Attributes (unknown if needed rn)
            attributeText = new UIText("Attributes", 1.2f);
            attributeText.Top.Set(60, 0);
            attributeText.Left.Set(100, 0);
            attributeText.OnClick += AttributeClick;
            mainWindow.Append(attributeText);

            artifactsText = new UIText("Artifacts", 1.2f);
            artifactsText.TextColor = new Color(188, 198, 207);
            artifactsText.Top.Set(110, 0);
            artifactsText.Left.Set(100, 0);
            artifactsText.OnClick += ArtifactsClick;
            mainWindow.Append(artifactsText);

            constellationText = new UIText("Constellation", 1.2f);
            constellationText.TextColor = new Color(188, 198, 207);
            constellationText.Top.Set(160, 0);
            constellationText.Left.Set(100, 0);
            constellationText.OnClick += ConstellationClick;
            mainWindow.Append(constellationText);

            talentText = new UIText("Talents", 1.2f);
            talentText.TextColor = new Color(188, 198, 207);
            talentText.Top.Set(210, 0);
            talentText.Left.Set(100, 0);
            talentText.OnClick += TalentClick;
            mainWindow.Append(talentText);

            UIPanel attributePanel = new UIPanel();
            attributePanel.Width.Set(250, 0f);
            attributePanel.Height.Set(350, 0f);
            attributePanel.Left.Set(610, 0f);
            attributePanel.Top.Set(10, 0f);
            attributePanel.BackgroundColor = new Color(99, 98, 112);
            attributePanel.BorderColor = new Color(166, 165, 181);
            attributePanel.OverflowHidden = false;
            attributePanel.PaddingLeft = attributePanel.PaddingRight = 10;
            mainWindow.Append(attributePanel);

            //// Button at the bottom that removes the active character
            //UIPanel removeCharButton = new UIPanel();
            //removeCharButton.Width.Set(100, 0);
            //removeCharButton.Height.Set(50, 0);
            //removeCharButton.Left.Set(0, 0.1f);
            //removeCharButton.Top.Set(0, 0.87f);
            //removeCharButton.BackgroundColor = new Color(44, 44, 176);
            //removeCharButton.OnClick += RemoveCharacterClick; 
            //mainWindow.Append(removeCharButton);

            //// Text for remove button
            //UIText text = new UIText("Remove Character");
            //text.HAlign = text.VAlign = 0.5f;
            //removeCharButton.Append(text);

            //// Panel to show what the character looks like
            //UIPanel charPanel = new UIPanel();
            //charPanel.Width.Set(220, 0f);
            //charPanel.Height.Set(0, 0.7f);
            //charPanel.Left.Set(0, 0.345f);
            //charPanel.Top.Set(0, 0.15f);
            //charPanel.BackgroundColor = new Color(47, 50, 102);
            //mainWindow.Append(charPanel);

            //// Panel to show the character's skills
            //UIPanel skillsPanel = new UIPanel();
            //skillsPanel.Width.Set(220, 0f);
            //skillsPanel.Height.Set(0, 0.7f);
            //skillsPanel.Left.Set(0, 0.68f);
            //skillsPanel.Top.Set(0, 0.15f);
            //skillsPanel.BackgroundColor = new Color(47, 50, 102);
            //skillsPanel.PaddingLeft = skillsPanel.PaddingRight = 20;
            //mainWindow.Append(skillsPanel);

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
            // Main.playerInventory || 
            if (Main.inFancyUI || Main.InReforgeMenu || Main.InGuideCraftMenu || Main.hairWindow || Main.ingameOptionsWindow || Main.LocalPlayer.talkNPC != -1)
            {
                UISystem.Instance.HideUIs();
            }
        }

        private void SetCharUI(string name)
        {
            string elementName = CharacterLists.GetElement(name);
            charName.SetText(elementName + " / " + name);

            // Set the window color based on their element
            if(elementName == "Anemo")
            {
                mainWindow.BackgroundColor = new Color(80, 230, 205);
            }
            else if(elementName == "Cryo")
            {
                mainWindow.BackgroundColor = new Color(115, 222, 255);
            }
            else if (elementName == "Dendro")
            {
                mainWindow.BackgroundColor = new Color(111, 219, 61);
            }
            else if (elementName == "Electro")
            {
                mainWindow.BackgroundColor = new Color(143, 72, 224);
            }
            else if (elementName == "Geo")
            {
                mainWindow.BackgroundColor = new Color(227, 199, 73);
            }
            else if (elementName == "Hydro")
            {
                mainWindow.BackgroundColor = new Color(37, 112, 232);
            }
            else if (elementName == "Pyro")
            {
                mainWindow.BackgroundColor = new Color(217, 93, 85);
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
                SetCharUI(text.Text);
            }
        }

        private void RemoveCharacterClick(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.RemoveActiveCharacter();
        }

        private void AttributeClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(255, 255, 255);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
        }

        private void ArtifactsClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(255, 255, 255);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
        }

        private void ConstellationClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(255, 255, 255);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
        }

        private void TalentClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(255, 255, 255);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
        }

        // Call this method when you open the menu, so we can populate the list with all the player's characters
        // This has to be its own method because tryign to do this in OnInitialize will crash the mod
        // as there is no LocalPlayer loaded at that point.
        public void OpenMenu()
        {
            list.Clear();

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            foreach (Character character in modPlayer.GetCharacters())
            {
                UIPanel button = new UIPanel();
                button.Width.Set(50, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnClick += OnButtonClick;
                list.Add(button);

                UIText text = new UIText(character.Name);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);
            }

            //// Scrollbar
            //UIScrollbar scroll = new();
            //scroll.Width.Set(20, 0);
            //scroll.Height.Set(0, 0.825f);
            //scroll.Left.Set(0, 0.95f);
            //scroll.Top.Set(0, 0.1f);

            //list.SetScrollbar(scroll);
            //list.Append(scroll);

            SetCharUI(Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>().activeCharacter.Name);
        }

    }
}
