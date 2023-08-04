using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;
using System.Collections.Generic;
using GenshinMod.CharacterClasses;
using GenshinMod.Elements;

namespace GenshinMod.UI
{
    class CharacterListUI : UIState
    {
        Character selectedCharacter;

        UIPanel mainWindow;
        UIList list;
        UIText charName;
        UIText attributeText, artifactsText, constellationText, talentText;
        UIText lifeText, damageText, defenseText, critText, critDmgText, elementalMasteryText, energyRechargeText, healingBonusText;

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
        UIPanel talentPanel, talentInfoPanel, talentConfirmLvlUpPanel;        
        UIText talent1Text, talent1LVL, talent2Text, talent2LVL, talent3Text, talent3LVL, talent4Text, talent4LVL, talent5Text, talent5LVL;
        UIText talentInfoCatergory, talentInfoName, talentInfoLVL, talentDescription, talentConfirmChangesText, talentConfirmMatsText;
        int talentSelected;

        public override void OnInitialize()
        {
            // The Main Window
            mainWindow = new UIPanel();
            mainWindow.Width.Set(900, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(44, 44, 110);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f; // Center the main window

            //UIItemSlot itemSlot = new UIItemSlot(new Item[5], 0, ItemSlot.Context.ChestItem);
            //mainWindow.Append(itemSlot);

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
            charName.Top.Set(10, 0);
            charName.Left.Set(80, 0);
            mainWindow.Append(charName);

            // Character's Stats
            lifeText = new UIText("Health: ", 0.85f);
            lifeText.Top.Set(30, 0);
            lifeText.Left.Set(500, 0);
            mainWindow.Append(lifeText);

            damageText = new UIText("Damage: ", 0.85f);
            damageText.Top.Set(50, 0);
            damageText.Left.Set(500, 0);
            mainWindow.Append(damageText);

            defenseText = new UIText("Defense: ", 0.85f);
            defenseText.Top.Set(70, 0);
            defenseText.Left.Set(500, 0);
            mainWindow.Append(defenseText);

            critText = new UIText("Crit Chance: ", 0.85f);
            critText.Top.Set(90, 0);
            critText.Left.Set(500, 0);
            mainWindow.Append(critText);

            critDmgText = new UIText("Crit Damage: ", 0.85f);
            critDmgText.Top.Set(110, 0);
            critDmgText.Left.Set(500, 0);
            mainWindow.Append(critDmgText);

            elementalMasteryText = new UIText("Elemental Mastery: ", 0.85f);
            elementalMasteryText.Top.Set(130, 0);
            elementalMasteryText.Left.Set(500, 0);
            mainWindow.Append(elementalMasteryText);

            energyRechargeText = new UIText("Energy Recharge: ", 0.85f);
            energyRechargeText.Top.Set(150, 0);
            energyRechargeText.Left.Set(500, 0);
            mainWindow.Append(energyRechargeText);

            healingBonusText = new UIText("Healing Bonus: ", 0.85f);
            healingBonusText.Top.Set(170, 0);
            healingBonusText.Left.Set(500, 0);
            mainWindow.Append(healingBonusText);

            // Character's Attributes (unknown if needed rn)
            attributeText = new UIText("Attributes", 1.2f);
            attributeText.Top.Set(60, 0);
            attributeText.Left.Set(100, 0);
            attributeText.OnLeftClick += OnAttributeClick;
            mainWindow.Append(attributeText);

            artifactsText = new UIText("Artifacts", 1.2f);
            artifactsText.TextColor = new Color(188, 198, 207);
            artifactsText.Top.Set(110, 0);
            artifactsText.Left.Set(100, 0);
            artifactsText.OnLeftClick += OnArtifactsClick;
            mainWindow.Append(artifactsText);

            constellationText = new UIText("Constellation", 1.2f);
            constellationText.TextColor = new Color(188, 198, 207);
            constellationText.Top.Set(160, 0);
            constellationText.Left.Set(100, 0);
            constellationText.OnLeftClick += OnConstellationClick;
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

            talentText = new UIText("Talents", 1.2f);
            talentText.TextColor = new Color(188, 198, 207);
            talentText.Top.Set(210, 0);
            talentText.Left.Set(100, 0);
            talentText.OnLeftClick += OnTalentClick;
            mainWindow.Append(talentText);

            #region Artifact Elements



            #endregion

            #region Constellation Elements

            constellationPanel = new UIPanel();
            constellationPanel.Width.Set(250, 0);
            constellationPanel.Height.Set(380, 0);
            constellationPanel.Top.Set(0, 0);
            constellationPanel.Left.Set(625, 0);
            constellationPanel.BackgroundColor = new Color();
            constellationPanel.BorderColor = new Color();

            constellationInfoPanel = new UIPanel();
            constellationInfoPanel.Width.Set(250, 0);
            constellationInfoPanel.Height.Set(380, 0);
            constellationInfoPanel.BackgroundColor = new Color(38, 58, 69);

            constellationInfoName = new UIText("Constellation Name");
            constellationInfoName.TextColor = new Color(232, 190, 128);
            constellationInfoName.Top.Set(70, 0);
            constellationInfoName.HAlign = 0.5f;
            constellationInfoPanel.Append(constellationInfoName);

            constellationInfoLevel = new UIText("Constellation Lv. ", 0.85f);
            constellationInfoLevel.TextColor = new Color(255, 252, 237);
            constellationInfoLevel.Top.Set(95, 0);
            constellationInfoLevel.Left.Set(0, 0);
            constellationInfoPanel.Append(constellationInfoLevel);

            constellationInfoDesc = new UIText("Constellation Description", 0.85f);
            constellationInfoDesc.Top.Set(125, 0);
            constellationInfoDesc.HAlign = 0.5f;
            constellationInfoPanel.Append(constellationInfoDesc);

            UIPanel constellationInfoExitButton = new UIPanel();
            constellationInfoExitButton.Left.Set(205, 0);
            constellationInfoExitButton.Width.Set(20, 0);
            constellationInfoExitButton.Height.Set(20, 0);
            constellationInfoExitButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => { constellationInfoPanel.Remove(); };
            constellationInfoPanel.Append(constellationInfoExitButton);

            constellationInfoActivateButton = new UIPanel();
            constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
            constellationInfoActivateButton.Width.Set(150, 0);
            constellationInfoActivateButton.Height.Set(50, 0);
            constellationInfoActivateButton.Top.Set(300, 0);
            constellationInfoActivateButton.HAlign = 0.5f;
            constellationInfoActivateButton.OnLeftClick += OnConstellationActivateClick;
            constellationInfoPanel.Append(constellationInfoActivateButton);

            constellationInfoActivate = new UIText("Activate");
            constellationInfoActivate.HAlign = constellationInfoActivate.VAlign = 0.5f;
            constellationInfoActivateButton.Append(constellationInfoActivate);

            #endregion

            #region Talent Elements

            talentPanel = new UIPanel();
            talentPanel.Width.Set(250, 0f);
            talentPanel.Height.Set(350, 0f);
            talentPanel.Left.Set(610, 0f);
            talentPanel.Top.Set(10, 0f);
            talentPanel.BackgroundColor = new Color();
            talentPanel.BorderColor = new Color();
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
            talentLevelUpButton.OnLeftClick += OnTalentLevelUpClick;
            talentInfoPanel.Append(talentLevelUpButton);
            UIText talentLevelUpText = new UIText("Level Up");
            talentLevelUpText.TextColor = new Color(160, 160, 160);
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
            talentAttrText.TextColor = new Color(160, 160, 160);
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

            talentDescription = new UIText("Talent Description", 0.75f);
            talentDescription.HAlign = 0.5f;
            talentInfoPanel2.Append(talentDescription);

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
            talentInfoExitButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => { talentInfoPanel.Remove(); };
            talentInfoPanel.Append(talentInfoExitButton);

            talentConfirmLvlUpPanel = new UIPanel();
            talentConfirmLvlUpPanel.BackgroundColor = new Color(38, 58, 69);
            talentConfirmLvlUpPanel.HAlign = talentConfirmLvlUpPanel.VAlign = 0.5f;
            talentConfirmLvlUpPanel.Width.Set(400, 0);
            talentConfirmLvlUpPanel.Height.Set(300, 0);

            UIText talentConfirmHeader = new UIText("Level Up Talent", 1.1f);
            talentConfirmHeader.TextColor = new Color(232, 190, 128);
            talentConfirmHeader.HAlign = 0.5f;
            talentConfirmLvlUpPanel.Append(talentConfirmHeader);
            UIText talentConfirmAttrHeader = new UIText("Attribute Changes", 0.8f);
            talentConfirmAttrHeader.TextColor = new Color(255, 255, 255);
            talentConfirmAttrHeader.HAlign = 0.5f;
            talentConfirmAttrHeader.Top.Set(60, 0);
            talentConfirmLvlUpPanel.Append(talentConfirmAttrHeader);
            UIText talentConfirmMatsHeader = new UIText("Required Materials", 0.8f);
            talentConfirmMatsHeader.TextColor = new Color(255, 255, 255);
            talentConfirmMatsHeader.HAlign = 0.5f;
            talentConfirmMatsHeader.Top.Set(175, 0);
            talentConfirmLvlUpPanel.Append(talentConfirmMatsHeader);

            UIPanel talentConfirmConfirm = new UIPanel();
            talentConfirmConfirm.BackgroundColor = new Color(240, 238, 223);
            talentConfirmConfirm.Width.Set(180, 0);
            talentConfirmConfirm.Height.Set(35, 0);
            talentConfirmConfirm.Top.Set(235, 0);
            talentConfirmConfirm.Left.Set(200, 0);
            talentConfirmConfirm.OnLeftClick += OnTalentConfirmClick;
            talentConfirmLvlUpPanel.Append(talentConfirmConfirm);
            UIText talentConfirmConfirmText = new UIText("Confirm");
            talentConfirmConfirmText.TextColor = new Color(160, 160, 160);
            talentConfirmConfirmText.HAlign = talentConfirmConfirmText.VAlign = 0.5f;
            talentConfirmConfirm.Append(talentConfirmConfirmText);

            UIPanel talentConfirmExit = new UIPanel();
            talentConfirmExit.BackgroundColor = new Color(240, 238, 223);
            talentConfirmExit.Width.Set(180, 0);
            talentConfirmExit.Height.Set(35, 0);
            talentConfirmExit.Top.Set(235, 0);
            talentConfirmExit.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => { talentConfirmLvlUpPanel.Remove(); };
            talentConfirmLvlUpPanel.Append(talentConfirmExit);
            UIText talentConfirmExitText = new UIText("Exit");
            talentConfirmExitText.TextColor = new Color(160, 160, 160);
            talentConfirmExitText.HAlign = talentConfirmExitText.VAlign = 0.5f;
            talentConfirmExit.Append(talentConfirmExitText);

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

            UpdateCharacterStats();

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

            // Needed to easily keep track of what characters were added
            List<string> charactersAddedToList = new List<string>(); 

            // Add the Party characters to the top of the list first
            foreach(Character character in modPlayer.GetPartyCharacters())
            {
                if (character == null) continue;
                if (character.Name == "None") continue;

                UIPanel button = new UIPanel();
                button.Width.Set(50, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnLeftClick += OnCharacterListSelect;
                list.Add(button);

                UIText text = new UIText(character.Name);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);

                charactersAddedToList.Add(character.Name);
            }
            // Then add the remaining characters
            // TODO: Sort by strength
            foreach (Character character in playerCharacters)
            {
                if (character == null) continue;
                if (charactersAddedToList.Contains(character.Name)) continue;

                UIPanel button = new UIPanel();
                button.Width.Set(50, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnLeftClick += OnCharacterListSelect;
                list.Add(button);

                UIText text = new UIText(character.Name);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);
            }

            //// Scrollbar
            UIScrollbar scroll = new();
            scroll.Width.Set(20, 0);
            scroll.Height.Set(0, 0.825f);
            scroll.Left.Set(0, 0.95f);
            scroll.Top.Set(0, 0.1f);

            list.SetScrollbar(scroll);
            list.Append(scroll);

            if (playerCharacters.Count == 0)
            {
                selectedCharacter = null;
                SetCharUI("None");
            }
            if (modPlayer.activeCharacters.Count > 0)
            {
                selectedCharacter = modPlayer.activeCharacters[0];
                SetCharUI(modPlayer.activeCharacters[0].Name);
            }
        }

        public void CloseMenu()
        {
            talentPanel.RemoveAllChildren();
            talentPanel.Remove();
            constellationPanel.RemoveAllChildren();
            constellationPanel.Remove();
            attributeText.TextColor = new Color(255, 255, 255);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
        }

        // Set up data related to the character
        private void SetCharUI(string name)
        { 
            if(selectedCharacter == null)
            {
                charName.SetText("None / " + name);
                mainWindow.BackgroundColor = new Color(44, 44, 110);
                return;
            }
            // Set the window color based on their element
            // Set up Element related data
            switch (selectedCharacter.Element)
            {
                case Element.Anemo:
                    charName.SetText("Anemo / " + name);
                    mainWindow.BackgroundColor = new Color(80, 230, 205);
                    break;
                case Element.Geo:
                    charName.SetText("Geo / " + name);
                    mainWindow.BackgroundColor = new Color(227, 199, 73);
                    break;
                case Element.Electro:
                    charName.SetText("Electro / " + name);
                    mainWindow.BackgroundColor = new Color(143, 72, 224);
                    break;
                case Element.Dendro:
                    charName.SetText("Dendro / " + name);
                    mainWindow.BackgroundColor = new Color(111, 219, 61);
                    break;
                case Element.Hydro:
                    charName.SetText("Hydro / " + name);
                    mainWindow.BackgroundColor = new Color(37, 112, 232);
                    break;
                case Element.Pyro:
                    charName.SetText("Pyro / " + name);
                    mainWindow.BackgroundColor = new Color(217, 93, 85);
                    break;
                case Element.Cryo:
                    charName.SetText("Cryo / " + name);
                    mainWindow.BackgroundColor = new Color(115, 222, 255);
                    break;
                default:
                    charName.SetText("None / " + name);
                    mainWindow.BackgroundColor = new Color(125, 125, 125);
                    break;
            }
        }

        private void UpdateCharacterStats()
        {         
            if (selectedCharacter == null)
            {
                lifeText.SetText("");
                damageText.SetText("");
                defenseText.SetText("");
                critText.SetText("");
                critDmgText.SetText("");
                elementalMasteryText.SetText("");
                energyRechargeText.SetText("");
                healingBonusText.SetText("");
                return;
            }

            lifeText.SetText(string.Format("Life: {0} / {1}", selectedCharacter.life, selectedCharacter.lifeMax));
            damageText.SetText(string.Format("Damage: {0}", selectedCharacter.damage));
            defenseText.SetText(string.Format("Defense: {0}", selectedCharacter.defense));
            critText.SetText(string.Format("Crit Chance: {0}", selectedCharacter.crit));
            critDmgText.SetText(string.Format("Crit Damage: {0}", selectedCharacter.critDmg));
            elementalMasteryText.SetText(string.Format("Elemental Mastery: {0}", selectedCharacter.elementalMastery));
            energyRechargeText.SetText(string.Format("Energy Recharge: {0}", selectedCharacter.energyRecharge));
            healingBonusText.SetText(string.Format("Healing Bonus: {0}", selectedCharacter.healingBonus));
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
                selectedCharacter = modPlayer.GetCharacters().Find(chara => chara.Name == text.Text);
                //modPlayer.ChangeActiveCharacter(text.Text);
                SetCharUI(text.Text);
            }
        }

        private void RemoveCharacterClick(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            //modPlayer.RemoveActiveCharacter();
        }

        #region Attribute UI Methods

        private void OnAttributeClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (selectedCharacter == null) return;

            attributeText.TextColor = new Color(255, 255, 255);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            constellationPanel.Remove();

            talentPanel.Remove();
        }

        #endregion

        #region Artifacts UI Methods

        private void OnArtifactsClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (selectedCharacter == null) return;

            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(255, 255, 255);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            constellationPanel.Remove();

            talentPanel.Remove();
        }

        #endregion

        #region Constellation UI Methods

        private void OnConstellationClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (selectedCharacter == null) return;

            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(255, 255, 255);
            talentText.TextColor = new Color(188, 198, 207);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            talentPanel.Remove();

            constellation1 = new UIPanel();
            constellation1.Width.Set(40, 0);
            constellation1.Height.Set(40, 0);
            constellation1.Top.Set(30, 0);
            constellation1.Left.Set(0, 0);
            constellation1.OnLeftClick += OnConstellationClick1;
            constellationPanel.Append(constellation1);

            constellation2 = new UIPanel();
            constellation2.Width.Set(40, 0);
            constellation2.Height.Set(40, 0);
            constellation2.Top.Set(80, 0);
            constellation2.Left.Set(20, 0);
            constellation2.OnLeftClick += OnConstellationClick2;
            constellationPanel.Append(constellation2);

            constellation3 = new UIPanel();
            constellation3.Width.Set(40, 0);
            constellation3.Height.Set(40, 0);
            constellation3.Top.Set(130, 0);
            constellation3.Left.Set(40, 0);
            constellation3.OnLeftClick += OnConstellationClick3;
            constellationPanel.Append(constellation3);

            constellation4 = new UIPanel();
            constellation4.Width.Set(40, 0);
            constellation4.Height.Set(40, 0);
            constellation4.Top.Set(180, 0);
            constellation4.Left.Set(40, 0);
            constellation4.OnLeftClick += OnConstellationClick4;
            constellationPanel.Append(constellation4);

            constellation5 = new UIPanel();
            constellation5.Width.Set(40, 0);
            constellation5.Height.Set(40, 0);
            constellation5.Top.Set(230, 0);
            constellation5.Left.Set(20, 0);
            constellation5.OnLeftClick += OnConstellationClick5;
            constellationPanel.Append(constellation5);

            constellation6 = new UIPanel();
            constellation6.Width.Set(40, 0);
            constellation6.Height.Set(40, 0);
            constellation6.Top.Set(280, 0);
            constellation6.Left.Set(0, 0);
            constellation6.OnLeftClick += OnConstellationClick6;
            constellationPanel.Append(constellation6);

            constellationText1 = new UIText(selectedCharacter.Constellation1, 0.85f);
            constellationText1.Top.Set(35, 0);
            constellationText1.Left.Set(50, 0);
            constellationPanel.Append(constellationText1);

            constellationText2 = new UIText(selectedCharacter.Constellation2, 0.85f);
            constellationText2.Top.Set(85, 0);
            constellationText2.Left.Set(70, 0);
            constellationPanel.Append(constellationText2);

            constellationText3 = new UIText(selectedCharacter.Constellation3, 0.85f);
            constellationText3.Top.Set(135, 0);
            constellationText3.Left.Set(90, 0);
            constellationPanel.Append(constellationText3);

            constellationText4 = new UIText(selectedCharacter.Constellation4, 0.85f);
            constellationText4.Top.Set(185, 0);
            constellationText4.Left.Set(90, 0);
            constellationPanel.Append(constellationText4);

            constellationText5 = new UIText(selectedCharacter.Constellation5, 0.85f);
            constellationText5.Top.Set(235, 0);
            constellationText5.Left.Set(70, 0);
            constellationPanel.Append(constellationText5);

            constellationText6 = new UIText(selectedCharacter.Constellation6, 0.85f);
            constellationText6.Top.Set(285, 0);
            constellationText6.Left.Set(50, 0);
            constellationPanel.Append(constellationText6);

            ActivateConstellationUIButtons();

            mainWindow.Append(constellationPanel);
        }

        private void OnConstellationClick1(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 1;
            constellationInfoName.SetText(selectedCharacter.Constellation1);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:1]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation1Desc);

            if(selectedCharacter.Constellation >= 1)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if (selectedCharacter.ConstellationUpgrade < 1)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void OnConstellationClick2(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 2;
            constellationInfoName.SetText(selectedCharacter.Constellation2);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:2]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation2Desc);

            if (selectedCharacter.Constellation >= 2)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if (selectedCharacter.ConstellationUpgrade < 2)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void OnConstellationClick3(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 3;
            constellationInfoName.SetText(selectedCharacter.Constellation3);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:3]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation3Desc);

            if (selectedCharacter.Constellation >= 3)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if (selectedCharacter.ConstellationUpgrade < 3)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void OnConstellationClick4(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 4;
            constellationInfoName.SetText(selectedCharacter.Constellation4);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:4]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation4Desc);

            if (selectedCharacter.Constellation >= 4)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if(selectedCharacter.ConstellationUpgrade < 4)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void OnConstellationClick5(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 5;
            constellationInfoName.SetText(selectedCharacter.Constellation5);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:5]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation5Desc);

            if (selectedCharacter.Constellation >= 5)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if (selectedCharacter.ConstellationUpgrade < 5)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void OnConstellationClick6(UIMouseEvent evt, UIElement listeningElement)
        {
            constellationSelected = 6;
            constellationInfoName.SetText(selectedCharacter.Constellation6);
            constellationInfoLevel.SetText("Constellation Lv. [c/1ce1ff:6]");
            constellationInfoDesc.SetText(selectedCharacter.Constellation6Desc);

            if (selectedCharacter.Constellation >= 6)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            else if (selectedCharacter.ConstellationUpgrade < 6)
            {
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }
            else
            {
                constellationInfoActivateButton.BackgroundColor = new Color(240, 238, 223);
                constellationInfoActivate.SetText("Activate");
                constellationInfoActivate.TextColor = new Color(255, 255, 255);
            }

            mainWindow.Append(constellationInfoPanel);
        }

        private void ActivateConstellationUIButtons()
        {
            if (selectedCharacter.Constellation >= 1) constellation1.BorderColor = new Color(255, 255, 255);
            else if (selectedCharacter.Constellation >= 2) constellation2.BorderColor = new Color(255, 255, 255);
            else if (selectedCharacter.Constellation >= 3) constellation3.BorderColor = new Color(255, 255, 255);
            else if (selectedCharacter.Constellation >= 4) constellation4.BorderColor = new Color(255, 255, 255);
            else if (selectedCharacter.Constellation >= 5) constellation5.BorderColor = new Color(255, 255, 255);
            else if (selectedCharacter.Constellation >= 6) constellation6.BorderColor = new Color(255, 255, 255);
        }

        private void OnConstellationActivateClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (selectedCharacter.Constellation < constellationSelected)
            {
                selectedCharacter.Constellation = constellationSelected;
                constellationInfoActivateButton.BackgroundColor = new Color();
                constellationInfoActivate.SetText("Activated");
                constellationInfoActivate.TextColor = new Color(245, 200, 66);
            }
            ActivateConstellationUIButtons();
        }

        #endregion

        #region Talent UI Methods

        private void OnTalentClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (selectedCharacter == null) return;

            attributeText.TextColor = new Color(188, 198, 207);
            artifactsText.TextColor = new Color(188, 198, 207);
            constellationText.TextColor = new Color(188, 198, 207);
            talentText.TextColor = new Color(255, 255, 255);
            SoundEngine.PlaySound(SoundID.Frog, Main.LocalPlayer.position);

            constellationPanel.Remove();

            mainWindow.Append(talentPanel);

            talent1Text = new UIText("Normal Attack: ", 1.1f);
            talent1Text.TextColor = new Color(255, 255, 255);
            talent1Text.Top.Set(0, 0);
            talent1Text.Left.Set(0, 0);
            talent1Text.OnLeftClick += OnTalentNormalClick;
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
            talent2Text.OnLeftClick += OnTalentSkillClick;
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
            talent3Text.OnLeftClick += OnTalentBurstClick;
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
            talent4Text.OnLeftClick += OnTalentAscension1Click;
            talentPanel.Append(talent4Text);
            talent4LVL = new UIText("Lv. 1", 1.1f);
            talent4LVL.TextColor = new Color(188, 198, 207);
            talent4LVL.Top.Set(200, 0);
            talent4LVL.Left.Set(0, 0);
            talentPanel.Append(talent4LVL);

            talent5Text = new UIText("Ascension 2", 1.1f);
            talent5Text.TextColor = new Color(188, 198, 207);
            talent5Text.Top.Set(240, 0);
            talent5Text.Left.Set(0, 0);
            talent5Text.OnLeftClick += OnTalentAscension2Click;
            talentPanel.Append(talent5Text);
            talent5LVL = new UIText("Lv. 1", 1.1f);
            talent5LVL.TextColor = new Color(188, 198, 207);
            talent5LVL.Top.Set(260, 0);
            talent5LVL.Left.Set(0, 0);
            talentPanel.Append(talent5LVL);

            talent1Text.SetText($"{selectedCharacter.NormalAttack}");
            talent2Text.SetText($"{selectedCharacter.Skill}");
            talent3Text.SetText($"{selectedCharacter.Burst}");
            talent4Text.SetText($"{selectedCharacter.Passive1}");
            talent5Text.SetText($"{selectedCharacter.Passive2}");

            talent1LVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            talent2LVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            talent3LVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
        }

        private void OnTalentNormalClick(UIMouseEvent evt, UIElement listeningElement)
        {
            talentInfoName.SetText(selectedCharacter.NormalAttack);
            talentInfoLVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            talentDescription.SetText(selectedCharacter.NormalAttackDesc);
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
            talentInfoName.SetText(selectedCharacter.Skill);
            talentInfoCatergory.SetText("Combat Talent");
            talentInfoLVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            talentDescription.SetText(selectedCharacter.SkillDesc);
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
            talentInfoName.SetText(selectedCharacter.Burst);
            talentInfoCatergory.SetText("Combat Talent");
            talentInfoLVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
            talentDescription.SetText(selectedCharacter.BurstDesc);
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
            talentInfoName.SetText(selectedCharacter.Passive1);
            talentInfoCatergory.SetText("Passive Talent");
            talentDescription.SetText(selectedCharacter.Passive1Desc);
            talentInfoLVL.SetText($"");
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
            talentInfoName.SetText(selectedCharacter.Passive2);
            talentInfoCatergory.SetText("Passive Talent");
            talentDescription.SetText(selectedCharacter.Passive2Desc);
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
            talent1LVL.TextColor = new Color(188, 198, 207);

            talentSelected = 5;
        }

        private void OnTalentLevelUpClick(UIMouseEvent evt, UIElement listeningElement)
        {
            mainWindow.Append(talentConfirmLvlUpPanel);
        }

         private void OnTalentConfirmClick(UIMouseEvent evt, UIElement listeningElement)
         {
            if (talentSelected == 1)
            {
                selectedCharacter.AttackLevel++;
                talentInfoLVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            }
            else if (talentSelected == 2)
            {
                selectedCharacter.SkillLevel++;
                talentInfoLVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            }
            else if (talentSelected == 3)
            {
                selectedCharacter.BurstLevel++;
                talentInfoLVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
            }

            talent1LVL.SetText($"Lv. {selectedCharacter.AttackLevel}");
            talent2LVL.SetText($"Lv. {selectedCharacter.SkillLevel}");
            talent3LVL.SetText($"Lv. {selectedCharacter.BurstLevel}");
        }

        #endregion

    }
}
