using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;
using System.Collections.Generic;

namespace GenshinMod.UI
{
    class CharacterListUI : UIState
    {
        Character selectedCharacter;

        DragableUIPanel mainWindow;
        UIList list;
        UIText charName;
        UIText attributeText, artifactsText, constellationText, talentText;

        // Artifact Elements
        UIPanel artifactSlot1, artifactSlot2;
        UIText artifactDesc1, artifactDesc2;

        // Constellation Elements
        UIPanel constellationPanel, constellationInfoPanel, constellationInfoActivateButton;
        UIPanel constellation1, constellation2, constellation3, constellation4, constellation5, constellation6;
        UIText constellationText1, constellationText2, constellationText3, constellationText4, constellationText5, constellationText6;
        UIText constellationInfoName, constellationInfoLevel, constellationInfoDesc, constellationInfoActivate;
        int constellationSelected;

        // Talent Elements
        UIPanel talentPanel, talentInfoPanel;        
        UIText talent1Text, talent1LVL, talent2Text, talent2LVL, talent3Text, talent3LVL, talent4Text, talent4LVL, talent5Text;
        UIText talentInfoCatergory, talentInfoName, talentInfoLVL;
        int talentSelected;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new DragableUIPanel();
            mainWindow.Width.Set(900, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(44, 44, 110);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f; // Center the main window

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
            list.ListPadding = 10;
            listPanel.Append(list);

            // Character's Name & Element
            charName = new UIText("[c/bcc6cf:element / name]", 0.85f);
            charName.Top.Set(15, 0);
            charName.Left.Set(80, 0);
            mainWindow.Append(charName);

            // Character's Attributes (unknown if needed rn)
            attributeText = new UIText("Attributes", 1.2f);
            attributeText.Top.Set(60, 0);
            attributeText.Left.Set(100, 0);
            attributeText.OnClick += OnAttributeClick;
            mainWindow.Append(attributeText);

            artifactsText = new UIText("Artifacts", 1.2f);
            artifactsText.TextColor = new Color(188, 198, 207);
            artifactsText.Top.Set(110, 0);
            artifactsText.Left.Set(100, 0);
            artifactsText.OnClick += OnArtifactsClick;
            mainWindow.Append(artifactsText);

            constellationText = new UIText("Constellation", 1.2f);
            constellationText.TextColor = new Color(188, 198, 207);
            constellationText.Top.Set(160, 0);
            constellationText.Left.Set(100, 0);
            constellationText.OnClick += OnConstellationClick;
            mainWindow.Append(constellationText);
          
            UIPanel attributePanel = new UIPanel();
            attributePanel.Width.Set(250, 0f);
            attributePanel.Height.Set(350, 0f);
            attributePanel.Left.Set(610, 0f);
            attributePanel.Top.Set(10, 0f);
            attributePanel.BackgroundColor = new Color(99, 98, 112);
            attributePanel.BorderColor = new Color(166, 165, 181);
            attributePanel.OverflowHidden = false;
            attributePanel.PaddingLeft = attributePanel.PaddingRight = 10;
            // mainWindow.Append(attributePanel);

            #region Talent Elements

            talentText = new UIText("Talents", 1.2f);
            talentText.TextColor = new Color(188, 198, 207);
            talentText.Top.Set(210, 0);
            talentText.Left.Set(100, 0);
            talentText.OnClick += OnTalentClick;
            mainWindow.Append(talentText);

            talentPanel = new UIPanel();
            talentPanel.Width.Set(250, 0f);
            talentPanel.Height.Set(350, 0f);
            talentPanel.Left.Set(610, 0f);
            talentPanel.Top.Set(10, 0f);
            talentPanel.BackgroundColor = new Color(99, 98, 112, 10);
            // talentPanel.BorderColor = new Color(166, 165, 181);
            talentPanel.OverflowHidden = false;
            talentPanel.PaddingLeft = talentPanel.PaddingRight = 10;

            talentInfoPanel = new UIPanel();
            talentInfoPanel.Width.Set(250, 0);
            talentInfoPanel.Height.Set(380, 0);
            talentInfoPanel.BackgroundColor = new Color(38, 58, 69);
            //talentInfoPanel.PaddingLeft = talentInfoPanel.PaddingRight = 10;
            //mainWindow.Append(talentInfoPanel);

            // The level up button at the bottom of the attribute panel
            UIPanel talentLevelUpButton = new UIPanel();
            talentLevelUpButton.BackgroundColor = new Color(240, 238, 223);
            talentLevelUpButton.Width.Set(230, 0f);
            talentLevelUpButton.Height.Set(40, 0f);
            talentLevelUpButton.Top.Set(315, 0);
            talentLevelUpButton.HAlign = 0.5f;
            talentInfoPanel.Append(talentLevelUpButton);
            UIText talentLevelUpText = new UIText("Level Up");
            // talentLevelUpText.TextColor = new Color(0, 0, 0);
            talentLevelUpText.HAlign = talentLevelUpText.VAlign = 0.5f; // To center the text
            talentLevelUpButton.Append(talentLevelUpText);

            // The info button at the top of the attribute panel
            UIPanel talentInfoButton = new UIPanel();
            talentInfoButton.BackgroundColor = new Color(72, 76, 101);
            talentInfoButton.Width.Set(110, 0f);
            talentInfoButton.Height.Set(30, 0f);
            talentInfoButton.Top.Set(90, 0);
            talentInfoButton.Left.Set(0, 0);
            talentInfoPanel.Append(talentInfoButton);
            UIText talentInfoText = new UIText("Info");
            talentInfoText.HAlign = talentInfoText.VAlign = 0.5f;
            talentInfoButton.Append(talentInfoText);

            // The attribute button at the top of the attribute panel
            UIPanel talentAttrButton = new UIPanel();
            talentAttrButton.BackgroundColor = new Color(240, 238, 223);
            talentAttrButton.Width.Set(110, 0f);
            talentAttrButton.Height.Set(30, 0f);
            talentAttrButton.Top.Set(90, 0);
            talentAttrButton.Left.Set(115, 0);
            talentInfoPanel.Append(talentAttrButton);
            UIText talentAttrText = new UIText("Attributes");
            talentAttrText.HAlign = talentAttrText.VAlign = 0.5f;
            talentAttrButton.Append(talentAttrText);

            // For the description of the talent or the stats of it
            UIPanel talentInfoPanel2 = new UIPanel();
            //talentInfoPanel2.BackgroundColor = new Color(72, 76, 101, 255);
            talentInfoPanel2.Width.Set(230, 0f);
            talentInfoPanel2.Height.Set(175, 0f);
            talentInfoPanel2.Top.Set(130, 0);
            talentInfoPanel2.Left.Set(0, 0);
            talentInfoPanel.Append(talentInfoPanel2);

            talentInfoCatergory = new UIText("Combat Talent", 0.75f);
            talentInfoCatergory.HAlign = 0.5f;
            talentInfoPanel.Append(talentInfoCatergory);

            talentInfoName = new UIText("Talent Name", 0.75f);
            talentInfoName.Top.Set(60, 0);
            talentInfoName.HAlign = 0.5f;
            talentInfoPanel.Append(talentInfoName);

            talentInfoLVL = new UIText("Lv. ", 0.75f);
            talentInfoLVL.Top.Set(75, 0);
            talentInfoLVL.HAlign = 0.5f;
            talentInfoPanel.Append(talentInfoLVL);

            UIPanel talentInfoExitButton = new UIPanel();
            talentInfoExitButton.Left.Set(205, 0);
            talentInfoExitButton.Width.Set(20, 0);
            talentInfoExitButton.Height.Set(20, 0);
            talentInfoExitButton.OnClick += (UIMouseEvent evt, UIElement listeningElement) => { talentInfoPanel.Remove(); };
            talentInfoPanel.Append(talentInfoExitButton);

            #endregion

            #region Constellation Elements



            #endregion

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

        // Call this method when you open the menu, so we can populate the list with all the player's characters
        // This has to be its own method because tryign to do this in OnInitialize will crash the mod
        // as there is no LocalPlayer loaded at that point.
        public void OpenMenu()
        {
            list.Clear();

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> playerCharacters = modPlayer.GetCharacters();
            foreach (Character character in playerCharacters)
            {
                UIPanel button = new UIPanel();
                button.Width.Set(50, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnClick += OnCharacterListSelect;
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

            if (playerCharacters.Count > 0)
            {
                if (modPlayer.activeCharacterName != null)
                {
                    selectedCharacter = modPlayer.activeCharacter;
                    SetCharUI(modPlayer.activeCharacterName);
                }
                else
                {
                    selectedCharacter = playerCharacters[0];
                    SetCharUI(playerCharacters[0].Name);
                }
            }
            else
            {
                SetCharUI("None");
            }
        }

        public void CloseMenu()
        {
            talentPanel.RemoveAllChildren();
            talentPanel.Remove();
            attributeText.TextColor = new Color(255, 255, 255);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
        }

        private void SetCharUI(string name)
        {
            string elementName = CharacterLists.GetElement(name);
            charName.SetText(elementName + " / " + name);

            // Set the window color based on their element
            if (elementName == "Anemo")
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
            else
            {
                mainWindow.BackgroundColor = new Color(125, 125, 125);
            }
        }

        private void OnCharacterListSelect(UIMouseEvent evt, UIElement listeningElement)
        {

            CloseMenu();

            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();

            // For now, let's determine which character the player picked based on the text on the button
            // We have to go through all the children of the element that called this event
            // Thankfully theres only one child in the panel, so it's ok
            foreach (UIElement element in listeningElement.Children)
            {
                UIText text = (UIText) element;
                selectedCharacter = modPlayer.GetCharacter(text.Text);
                //modPlayer.ChangeActiveCharacter(text.Text);
                SetCharUI(text.Text);
            }
        }

        private void RemoveCharacterClick(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.RemoveActiveCharacter();
        }

        #region Attribute UI Methods

        private void OnAttributeClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(255, 255, 255);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            talentPanel.Remove();
        }

        #endregion

        #region Artifacts UI Methods

        private void OnArtifactsClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(255, 255, 255);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            talentPanel.Remove();
        }

        #endregion

        #region Constellation UI Methods

        private void OnConstellationClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(255, 255, 255);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            talentPanel.Remove();
        }

        #endregion

        #region Talent UI Methods

        private void OnTalentClick(UIMouseEvent evt, UIElement listeningElement)
        {
            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(255, 255, 255);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            mainWindow.Append(talentPanel);

            talent1Text = new UIText("Normal Attack: ", 1.1f);
            talent1Text.TextColor = new Color(255, 255, 255);
            talent1Text.Top.Set(0, 0);
            talent1Text.Left.Set(0, 0);
            talent1Text.OnClick += OnTalentNormalClick;
            talentPanel.Append(talent1Text);
            talent1LVL = new UIText("Lv. ", 1.1f);
            talent1LVL.TextColor = new Color(188, 198, 207);
            talent1LVL.Top.Set(20, 0);
            talent1LVL.Left.Set(0, 0);
            talentPanel.Append(talent1LVL);

            talent2Text = new UIText("Skill", 1.1f);
            talent2Text.TextColor = new Color(188, 198, 207);
            talent2Text.Top.Set(60, 0);
            talent2Text.Left.Set(0, 0);
            talent2Text.OnClick += OnTalentSkillClick;
            talentPanel.Append(talent2Text);
            talent2LVL = new UIText("Lv. ", 1.1f);
            talent2LVL.TextColor = new Color(188, 198, 207);
            talent2LVL.Top.Set(80, 0);
            talent2LVL.Left.Set(0, 0);
            talentPanel.Append(talent2LVL);

            talent3Text = new UIText("Burst", 1.1f);
            talent3Text.TextColor = new Color(188, 198, 207);
            talent3Text.Top.Set(120, 0);
            talent3Text.Left.Set(0, 0);
            talent3Text.OnClick += OnTalentBurstClick;
            talentPanel.Append(talent3Text);
            talent3LVL = new UIText("Lv. ", 1.1f);
            talent3LVL.TextColor = new Color(188, 198, 207);
            talent3LVL.Top.Set(140, 0);
            talent3LVL.Left.Set(0, 0);
            talentPanel.Append(talent3LVL);

            talent4Text = new UIText("Ascension 1", 1.1f);
            talent4Text.TextColor = new Color(188, 198, 207);
            talent4Text.Top.Set(180, 0);
            talent4Text.Left.Set(0, 0);
            talent4Text.OnClick += OnTalentAscension1Click;
            talentPanel.Append(talent4Text);
            talent4LVL = new UIText("Lv. ", 1.1f);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent4LVL.Top.Set(200, 0);
            talent4LVL.Left.Set(0, 0);
            talentPanel.Append(talent4LVL);

            talent5Text = new UIText("Ascension 2", 1.1f);
            talent5Text.TextColor = new Color(188, 198, 207);
            talent5Text.Top.Set(240, 0);
            talent5Text.Left.Set(0, 0);
            talent5Text.OnClick += OnTalentAscension2Click;
            talentPanel.Append(talent5Text);

            talent1Text.SetText($"{selectedCharacter.NormalAttackName}");
            talent2Text.SetText($"{selectedCharacter.SkillName}");
            talent3Text.SetText($"{selectedCharacter.BurstName}");
            talent4Text.SetText($"{selectedCharacter.AscensionTalent1Name}");
            talent5Text.SetText($"{selectedCharacter.AscensionTalent2Name}");

            talent1LVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            talent2LVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            talent3LVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
            talent4LVL.SetText($"Lv. {selectedCharacter.AscenstionTalentLevel}");
        }

        private void OnTalentNormalClick(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.NormalAttackName);
            talentInfoLVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            mainWindow.Append(talentInfoPanel);
            talent1Text.TextColor = new Color(255, 255, 255);
            talent1LVL.TextColor = new Color(255, 255, 255);
            talent2Text.TextColor = new Color(188, 198, 207);
            talent2LVL.TextColor = new Color(188, 198, 207);
            talent3Text.TextColor = new Color(188, 198, 207);
            talent3LVL.TextColor = new Color(188, 198, 207);
            talent4Text.TextColor = new Color(188, 198, 207);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent5Text.TextColor = new Color(188, 198, 207);

            talentSelected = 1;
        }

        private void OnTalentSkillClick(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.SkillName);
            talentInfoLVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            mainWindow.Append(talentInfoPanel);
            talent2Text.TextColor = new Color(255, 255, 255);
            talent2LVL.TextColor = new Color(255, 255, 255);
            talent1Text.TextColor = new Color(188, 198, 207);
            talent1LVL.TextColor = new Color(188, 198, 207);
            talent3Text.TextColor = new Color(188, 198, 207);
            talent3LVL.TextColor = new Color(188, 198, 207);
            talent4Text.TextColor = new Color(188, 198, 207);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent5Text.TextColor = new Color(188, 198, 207);

            talentSelected = 2;
        }

        private void OnTalentBurstClick(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.BurstName);
            talentInfoLVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
            mainWindow.Append(talentInfoPanel);
            talent3Text.TextColor = new Color(255, 255, 255);
            talent3LVL.TextColor = new Color(255, 255, 255);
            talent2Text.TextColor = new Color(188, 198, 207);
            talent2LVL.TextColor = new Color(188, 198, 207);
            talent1Text.TextColor = new Color(188, 198, 207);
            talent1LVL.TextColor = new Color(188, 198, 207);
            talent4Text.TextColor = new Color(188, 198, 207);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent5Text.TextColor = new Color(188, 198, 207);

            talentSelected = 3;
        }

        private void OnTalentAscension1Click(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.AscensionTalent1Name);
            talentInfoLVL.SetText($"Lv. {selectedCharacter.AscenstionTalentLevel}");
            mainWindow.Append(talentInfoPanel);
            talent4Text.TextColor = new Color(255, 255, 255);
            talent4LVL.TextColor = new Color(255, 255, 255);
            talent2Text.TextColor = new Color(188, 198, 207);
            talent2LVL.TextColor = new Color(188, 198, 207);
            talent3Text.TextColor = new Color(188, 198, 207);
            talent3LVL.TextColor = new Color(188, 198, 207);
            talent1Text.TextColor = new Color(188, 198, 207);
            talent1LVL.TextColor = new Color(188, 198, 207);
            talent5Text.TextColor = new Color(188, 198, 207);

            talentSelected = 4;
        }

        private void OnTalentAscension2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.AscensionTalent2Name);
            talentInfoLVL.SetText($"");
            mainWindow.Append(talentInfoPanel);
            talent5Text.TextColor = new Color(255, 255, 255);
            talent2Text.TextColor = new Color(188, 198, 207);
            talent2LVL.TextColor = new Color(188, 198, 207);
            talent3Text.TextColor = new Color(188, 198, 207);
            talent3LVL.TextColor = new Color(188, 198, 207);
            talent4Text.TextColor = new Color(188, 198, 207);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent1Text.TextColor = new Color(188, 198, 207);
            talent1LVL.TextColor = new Color(255, 255, 255);

            talentSelected = 5;
        }

        #endregion

    }
}
