using GenshinMod.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace GenshinMod.UI
{
    internal class PartyUI : UIState
    {
        UIPanel mainWindow;

        // We assign these values globally so we can edit them whenever the player does something (their values are not static)
        UIText header;
        UIPanel listPanel;
        UIGrid characterList;
        UIPanel characterPanel1, characterPanel2, characterPanel3, characterPanel4;
        UIPanel characterPfp1, characterPfp2, characterPfp3, characterPfp4;
        UIText characterName1, characterName2, characterName3, characterName4;
        UIText characterLife1, characterLife2, characterLife3, characterLife4;
        UIText characterDamage1, characterDamage2, characterDamage3, characterDamage4;
        UIText characterDefense1, characterDefense2, characterDefense3, characterDefense4;
        UIGrid characterItemGrid1, characterItemGrid2, characterItemGrid3, characterItemGrid4;
        VanillaItemSlotWrapper characterWeapon1, characterWeapon2, characterWeapon3, characterWeapon4;
        VanillaItemSlotWrapper character1Artifact1, character1Artifact2, character1Artifact3, character1Artifact4, character1Artifact5;
        VanillaItemSlotWrapper character2Artifact1, character2Artifact2, character2Artifact3, character2Artifact4, character2Artifact5;
        VanillaItemSlotWrapper character3Artifact1, character3Artifact2, character3Artifact3, character3Artifact4, character3Artifact5;
        VanillaItemSlotWrapper character4Artifact1, character4Artifact2, character4Artifact3, character4Artifact4, character4Artifact5;
        UIPanel activeCharacterButton1, activeCharacterButton2, activeCharacterButton3, activeCharacterButton4;
        UIPanel closeCharacterList;

        int characterSelected = 0;

        public override void OnInitialize()
        {
            // Initialize the main window where everything will be in
            mainWindow = new();
            mainWindow.Width.Set(1050, 0);
            mainWindow.Height.Set(320, 0);
            mainWindow.HAlign = 0.5f;
            mainWindow.VAlign = 0.65f;
            Append(mainWindow);

            // Initialize each panel for each character
            // In its own separate methods to make code look cleaner
            // Since its a lot of repetitive code
            IntializeCharacter1Panel();
            IntializeCharacter2Panel();
            IntializeCharacter3Panel();
            IntializeCharacter4Panel();

            // Same list from CharacterUI
            listPanel = new();
            listPanel.Width.Set(1050, 0);
            listPanel.Height.Set(320, 0);
            listPanel.HAlign = 0.5f;
            listPanel.VAlign = 0.65f;
            listPanel.BackgroundColor = new Color(99, 98, 112);
            listPanel.BorderColor = new Color(166, 165, 181);
            listPanel.OverflowHidden = false;
            listPanel.PaddingLeft = listPanel.PaddingRight = 10;

            // The element that will store the buttons for all the characters the player has
            characterList = new();
            characterList.SetPadding(0);
            characterList.Width.Set(1050, 0);
            characterList.Height.Set(320, 0);
            characterList.ListPadding = 10;
            listPanel.Append(characterList);

            // A button to close the Character list Panel
            // If the person doesn't want to choose a character
            closeCharacterList = new();
            closeCharacterList.Width.Set(50, 0);
            closeCharacterList.Height.Set(50, 0);
            closeCharacterList.Left.Set(0, 0);
            closeCharacterList.Top.Set(0, 0);
        }

        public void IntializeCharacter1Panel()
        {
            characterPanel1 = new();
            characterPanel1.BackgroundColor = new Color(0, 0, 0, 0);
            characterPanel1.BorderColor = new Color(0, 0, 0, 0);
            characterPanel1.Width.Set(250, 0);
            characterPanel1.Height.Set(350, 0);
            characterPanel1.Left.Set(0f, 0);
            characterPanel1.Top.Set(0f, 0);
            mainWindow.Append(characterPanel1);

            characterPfp1 = new();
            characterPfp1.Width.Set(100, 0);
            characterPfp1.Height.Set(100, 0);
            characterPfp1.OnLeftClick += OnCharacter1Click;
            characterPanel1.Append(characterPfp1);

            characterName1 = new("Character 1");
            characterName1.HAlign = 0.8f;
            characterPanel1.Append(characterName1);

            characterLife1 = new("200 / 200");
            characterLife1.HAlign = 0.8f;
            characterLife1.VAlign = 0.1f;
            characterPanel1.Append(characterLife1);

            characterDamage1 = new("Damage: 100");
            characterDamage1.HAlign = 0.8f;
            characterDamage1.VAlign = 0.2f;
            characterPanel1.Append(characterDamage1);

            characterDefense1 = new("Defense: 100");
            characterDefense1.HAlign = 0.8f;
            characterDefense1.VAlign = 0.3f;
            characterPanel1.Append(characterDefense1);

            characterWeapon1 = new();
            character1Artifact1 = new();
            character1Artifact2 = new();
            character1Artifact3 = new();
            character1Artifact4 = new();
            character1Artifact5 = new();

            characterItemGrid1 = new();
            characterItemGrid1.Width.Set(130f, 0);
            characterItemGrid1.Height.Set(180f, 0);
            characterItemGrid1.Top.Set(110f, 0);
            characterItemGrid1.Add(characterWeapon1);
            characterItemGrid1.Add(character1Artifact1);
            characterItemGrid1.Add(character1Artifact2);
            characterItemGrid1.Add(character1Artifact3);
            characterItemGrid1.Add(character1Artifact4);
            characterItemGrid1.Add(character1Artifact5);
            characterPanel1.Append(characterItemGrid1);

            activeCharacterButton1 = new();
            activeCharacterButton1.Width.Set(50f, 0);
            activeCharacterButton1.Height.Set(50f, 0);
            activeCharacterButton1.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton1.Top.Set(225f, 0);
            activeCharacterButton1.Left.Set(175f, 0);
            activeCharacterButton1.OnLeftClick += OnActiveCharacter1Select;
            characterPanel1.Append(activeCharacterButton1);
        }

        public void IntializeCharacter2Panel()
        {
            characterPanel2 = new();
            characterPanel2.BackgroundColor = new Color(0, 0, 0, 0);
            characterPanel2.BorderColor = new Color(0, 0, 0, 0);
            characterPanel2.Width.Set(250, 0);
            characterPanel2.Height.Set(300, 0);
            characterPanel2.Left.Set(270f, 0);
            characterPanel2.Top.Set(0f, 0);
            mainWindow.Append(characterPanel2);

            characterPfp2 = new();
            characterPfp2.Width.Set(100, 0);
            characterPfp2.Height.Set(100, 0);
            characterPfp2.OnLeftClick += OnCharacter2Click;
            characterPanel2.Append(characterPfp2);

            characterName2 = new("Character 2");
            characterName2.HAlign = 0.8f;
            characterPanel2.Append(characterName2);

            characterLife2 = new("200 / 200");
            characterLife2.HAlign = 0.8f;
            characterLife2.VAlign = 0.1f;
            characterPanel2.Append(characterLife2);

            characterDamage2 = new("Damage: 100");
            characterDamage2.HAlign = 0.8f;
            characterDamage2.VAlign = 0.2f;
            characterPanel2.Append(characterDamage2);

            characterDefense2 = new("Defense: 100");
            characterDefense2.HAlign = 0.8f;
            characterDefense2.VAlign = 0.3f;
            characterPanel2.Append(characterDefense2);

            characterWeapon2 = new();
            character2Artifact1 = new();
            character2Artifact2 = new();
            character2Artifact3 = new();                                                                      
            character2Artifact4 = new();                                                                      
            character2Artifact5 = new();                                                                      

            characterItemGrid2 = new();
            characterItemGrid2.Width.Set(130f, 0);
            characterItemGrid2.Height.Set(180f, 0);
            characterItemGrid2.Top.Set(110f, 0);
            characterItemGrid2.Add(characterWeapon2);
            characterItemGrid2.Add(character2Artifact1);
            characterItemGrid2.Add(character2Artifact2);
            characterItemGrid2.Add(character2Artifact3);
            characterItemGrid2.Add(character2Artifact4);
            characterItemGrid2.Add(character2Artifact5);
            characterPanel2.Append(characterItemGrid2);

            activeCharacterButton2 = new();
            activeCharacterButton2.Width.Set(50f, 0);
            activeCharacterButton2.Height.Set(50f, 0);
            activeCharacterButton2.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton2.Top.Set(225f, 0);
            activeCharacterButton2.Left.Set(175f, 0);
            activeCharacterButton2.OnLeftClick += OnActiveCharacter2Select;
            characterPanel2.Append(activeCharacterButton2);
        }

        public void IntializeCharacter3Panel()
        {
            characterPanel3 = new();
            characterPanel3.BackgroundColor = new Color(0, 0, 0, 0);
            characterPanel3.BorderColor = new Color(0, 0, 0, 0);
            characterPanel3.Width.Set(250, 0);
            characterPanel3.Height.Set(300, 0);
            characterPanel3.Left.Set(530f, 0);
            characterPanel3.Top.Set(0f, 0);
            mainWindow.Append(characterPanel3);

            characterPfp3 = new();
            characterPfp3.Width.Set(100, 0);
            characterPfp3.Height.Set(100, 0);
            characterPfp3.OnLeftClick += OnCharacter3Click;
            characterPanel3.Append(characterPfp3);

            characterName3 = new("Character 3");
            characterName3.HAlign = 0.8f;
            characterPanel3.Append(characterName3);

            characterLife3 = new("200 / 200");
            characterLife3.HAlign = 0.8f;
            characterLife3.VAlign = 0.1f;
            characterPanel3.Append(characterLife3);

            characterDamage3 = new("Damage: 100");
            characterDamage3.HAlign = 0.8f;
            characterDamage3.VAlign = 0.2f;
            characterPanel3.Append(characterDamage3);

            characterDefense3 = new("Defense: 100");
            characterDefense3.HAlign = 0.8f;
            characterDefense3.VAlign = 0.3f;
            characterPanel3.Append(characterDefense3);

            characterWeapon3 = new();
            character3Artifact1 = new();
            character3Artifact2 = new();                                                                      
            character3Artifact3 = new();                                                                      
            character3Artifact4 = new();                                                                      
            character3Artifact5 = new();                                                                      

            characterItemGrid3 = new();
            characterItemGrid3.Width.Set(130f, 0);
            characterItemGrid3.Height.Set(180f, 0);
            characterItemGrid3.Top.Set(110f, 0);
            characterItemGrid3.Add(characterWeapon3);
            characterItemGrid3.Add(character3Artifact1);
            characterItemGrid3.Add(character3Artifact2);
            characterItemGrid3.Add(character3Artifact3);
            characterItemGrid3.Add(character3Artifact4);
            characterItemGrid3.Add(character3Artifact5);
            characterPanel3.Append(characterItemGrid3);

            activeCharacterButton3 = new();
            activeCharacterButton3.Width.Set(50f, 0);
            activeCharacterButton3.Height.Set(50f, 0);
            activeCharacterButton3.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton3.Top.Set(225f, 0);
            activeCharacterButton3.Left.Set(175f, 0);
            activeCharacterButton3.OnLeftClick += OnActiveCharacter3Select;
            characterPanel3.Append(activeCharacterButton3);
        }

        public void IntializeCharacter4Panel()
        {
            characterPanel4 = new();
            characterPanel4.BackgroundColor = new Color(0, 0, 0, 0);
            characterPanel4.BorderColor = new Color(0, 0, 0, 0);
            characterPanel4.Width.Set(250, 0);
            characterPanel4.Height.Set(300, 0);
            characterPanel4.Left.Set(790f, 0);
            characterPanel4.Top.Set(0f, 0);
            mainWindow.Append(characterPanel4);

            characterPfp4 = new();
            characterPfp4.Width.Set(100, 0);
            characterPfp4.Height.Set(100, 0);
            characterPfp4.OnLeftClick += OnCharacter4Click;
            characterPanel4.Append(characterPfp4);

            characterName4 = new("Character 4");
            characterName4.HAlign = 0.8f;
            characterPanel4.Append(characterName4);

            characterLife4 = new("200 / 200");
            characterLife4.HAlign = 0.8f;
            characterLife4.VAlign = 0.1f;
            characterPanel4.Append(characterLife4);

            characterDamage4 = new("Damage: 100");
            characterDamage4.HAlign = 0.8f;
            characterDamage4.VAlign = 0.2f;
            characterPanel4.Append(characterDamage4);

            characterDefense4 = new("Defense: 100");
            characterDefense4.HAlign = 0.8f;
            characterDefense4.VAlign = 0.3f;
            characterPanel4.Append(characterDefense4);

            characterWeapon4 = new();
            character4Artifact1 = new();
            character4Artifact2 = new();
            character4Artifact3 = new();
            character4Artifact4 = new();
            character4Artifact5 = new();

            characterItemGrid4 = new();
            characterItemGrid4.Width.Set(130f, 0);
            characterItemGrid4.Height.Set(180f, 0);
            characterItemGrid4.Top.Set(110f, 0);
            characterItemGrid4.Add(characterWeapon4);
            characterItemGrid4.Add(character4Artifact1);
            characterItemGrid4.Add(character4Artifact2);
            characterItemGrid4.Add(character4Artifact3);
            characterItemGrid4.Add(character4Artifact4);
            characterItemGrid4.Add(character4Artifact5);
            characterPanel4.Append(characterItemGrid4);

            activeCharacterButton4 = new();
            activeCharacterButton4.Width.Set(50f, 0);
            activeCharacterButton4.Height.Set(50f, 0);
            activeCharacterButton4.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton4.Top.Set(225f, 0);
            activeCharacterButton4.Left.Set(175f, 0);
            activeCharacterButton4.OnLeftClick += OnActiveCharacter4Select;
            characterPanel4.Append(activeCharacterButton4);
        }


        // Update info about the Party UI
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Prevents the player from using items while their mouse is over the UI
            if (listPanel.IsMouseHovering || mainWindow.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            // Update the information of characters onto the UI
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            UpdateCharacterInfo(partyCharacters);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
            Main.hidePlayerCraftingMenu = true;
        }

        // Used so we can load the UI with player information,
        // as doing it in OnInitialize will crash the game
        // as no player has been loaded at that point yet
        public void OpenMenu()
        {
            characterList.Clear();
            listPanel.Remove();
            characterSelected = 0;

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> playerCharacters = modPlayer.GetCharacters();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();

            // Populate the character list with characters the player has
            // so that the player may choose from here for their party
            foreach (Character character in playerCharacters)
            {
                if (character == null) continue;

                UIPanel button = new UIPanel();
                button.Width.Set(100, 0);
                button.Height.Set(100, 0);
                button.Left.Set(0, 0);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnLeftClick += OnCharacterListClick;
                characterList.Add(button);

                UIText text = new UIText(character.Name);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);
            }

            // Fill in the items that each character has
            if(partyCharacters[0] != null)
            {
                partyCharacters[0].weapon = characterWeapon1.Item;
                partyCharacters[0].artifact1 = character1Artifact1.Item;
                partyCharacters[0].artifact2 = character1Artifact2.Item;
                partyCharacters[0].artifact3 = character1Artifact3.Item;
                partyCharacters[0].artifact4 = character1Artifact4.Item;
                partyCharacters[0].artifact5 = character1Artifact5.Item;
            }

            if (partyCharacters[1] != null)
            {
                partyCharacters[1].weapon = characterWeapon2.Item;
                partyCharacters[1].artifact1 = character2Artifact1.Item;
                partyCharacters[1].artifact2 = character2Artifact2.Item;
                partyCharacters[1].artifact3 = character2Artifact3.Item;
                partyCharacters[1].artifact4 = character2Artifact4.Item;
                partyCharacters[1].artifact5 = character2Artifact5.Item;
            }


            if (partyCharacters[2] != null)
            {
                partyCharacters[2].weapon = characterWeapon3.Item;
                partyCharacters[2].artifact1 = character3Artifact1.Item;
                partyCharacters[2].artifact2 = character3Artifact2.Item;
                partyCharacters[2].artifact3 = character3Artifact3.Item;
                partyCharacters[2].artifact4 = character3Artifact4.Item;
                partyCharacters[2].artifact5 = character3Artifact5.Item;
            }

            if (partyCharacters[3] != null)
            {
                partyCharacters[3].weapon = characterWeapon4.Item;
                partyCharacters[3].artifact1 = character4Artifact1.Item;
                partyCharacters[3].artifact2 = character4Artifact2.Item;
                partyCharacters[3].artifact3 = character4Artifact3.Item;
                partyCharacters[3].artifact4 = character4Artifact4.Item;
                partyCharacters[3].artifact5 = character4Artifact5.Item;
            }
        }

        // Used so we can save information when the UI is closed
        // It does bring up the chance that if the game crashes
        // before the UI is closed, items may disappear?
        // Eventually we must find a way to save it as item is placed
        public void CloseMenu()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            partyCharacters[0].weapon = characterWeapon1.Item;
            partyCharacters[0].artifact1 = character1Artifact1.Item;
            partyCharacters[0].artifact2 = character1Artifact2.Item;
            partyCharacters[0].artifact3 = character1Artifact3.Item;
            partyCharacters[0].artifact4 = character1Artifact4.Item;
            partyCharacters[0].artifact5 = character1Artifact5.Item;

            partyCharacters[1].weapon = characterWeapon2.Item;
            partyCharacters[1].artifact1 = character2Artifact1.Item;
            partyCharacters[1].artifact2 = character2Artifact2.Item;
            partyCharacters[1].artifact3 = character2Artifact3.Item;
            partyCharacters[1].artifact4 = character2Artifact4.Item;
            partyCharacters[1].artifact5 = character2Artifact5.Item;

            partyCharacters[2].weapon = characterWeapon3.Item;
            partyCharacters[2].artifact1 = character3Artifact1.Item;
            partyCharacters[2].artifact2 = character3Artifact2.Item;
            partyCharacters[2].artifact3 = character3Artifact3.Item;
            partyCharacters[2].artifact4 = character3Artifact4.Item;
            partyCharacters[2].artifact5 = character3Artifact5.Item;

            partyCharacters[3].weapon = characterWeapon4.Item;
            partyCharacters[3].artifact1 = character4Artifact1.Item;
            partyCharacters[3].artifact2 = character4Artifact2.Item;
            partyCharacters[3].artifact3 = character4Artifact3.Item;
            partyCharacters[3].artifact4 = character4Artifact4.Item;
            partyCharacters[3].artifact5 = character4Artifact5.Item;
        }

        // What to do when a character's icon is clicked
        // It opens up the character list over the party UI
        // It also sets characterSelected to the number corresponding to the
        // selected character so we know which one to replace
        #region Character Icon Click

        private void OnCharacter1Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if(characterSelected == 0)
            {
                Append(listPanel);
                Append(closeCharacterList);
                characterSelected = 1;
            }
            else
            {
                listPanel.Remove();
                closeCharacterList.Remove();
                characterSelected = 0;
            }

        }

        private void OnCharacter2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                Append(closeCharacterList);
                characterSelected = 2;
            }
            else
            {
                listPanel.Remove();
                closeCharacterList.Remove();
                characterSelected = 0;
            }
        }

        private void OnCharacter3Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                Append(closeCharacterList);
                characterSelected = 3;
            }
            else
            {
                listPanel.Remove();
                closeCharacterList.Remove();
                characterSelected = 0;

            }
        }

        private void OnCharacter4Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                Append(closeCharacterList);
                characterSelected = 4;
            }
            else
            {
                listPanel.Remove();
                closeCharacterList.Remove();
                characterSelected = 0;
            }
        }

        #endregion

        // What happens when we click on the UIPanel in the character list
        // In short, it sets the party character to the specificed character in the specified slot
        private void OnCharacterListClick(UIMouseEvent evt, UIElement listeningElement)
        {
            listPanel.Remove();
            closeCharacterList.Remove();

            // Get the text in each UIPanel in the list
            // We may have to change this later when we swtich from text to images
            foreach (UIElement element in listeningElement.Children)
            {
                UIText text = (UIText)element;
                var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
                List<Character> partyCharacters = modPlayer.GetPartyCharacters();

                // If the player selects the character that is already in that position, remove the character from the party
                if (text.Text != partyCharacters[characterSelected - 1].Name) modPlayer.ChangePartyCharacters(text.Text, characterSelected - 1); // Change the character
                else modPlayer.ChangePartyCharacters("None", characterSelected - 1); // Remove them from the party
            }

            characterSelected = 0; // Set back to 0 so we know there is no character selected
        }

        // Temporary code to determine which character is "active" by making the red UIPanel green

        private void OnActiveCharacter1Select(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.selectedCharacter = 0;
            activeCharacterButton1.BackgroundColor = new Color(0, 255, 0, 1);
            activeCharacterButton2.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton3.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton4.BackgroundColor = new Color(255, 0, 0, 1);
        }

        private void OnActiveCharacter2Select(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.selectedCharacter = 1;
            activeCharacterButton2.BackgroundColor = new Color(0, 255, 0, 1);
            activeCharacterButton1.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton3.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton4.BackgroundColor = new Color(255, 0, 0, 1);
        }

        private void OnActiveCharacter3Select(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.selectedCharacter = 2;
            activeCharacterButton3.BackgroundColor = new Color(0, 255, 0, 1);
            activeCharacterButton2.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton1.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton4.BackgroundColor = new Color(255, 0, 0, 1);
        }

        private void OnActiveCharacter4Select(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            modPlayer.selectedCharacter = 3;
            activeCharacterButton4.BackgroundColor = new Color(0, 255, 0, 1);
            activeCharacterButton2.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton3.BackgroundColor = new Color(255, 0, 0, 1);
            activeCharacterButton1.BackgroundColor = new Color(255, 0, 0, 1);
        }

        // Code used to update the UI of the stats of the character in real time
        // Useful to make sure the UI is populated correctly
        // AND to keep track of the character's health while it is an NPC
        private void UpdateCharacterInfo(List<Character> partyCharacters)
        {

            if (partyCharacters.Count >= 1)
            {
                if(partyCharacters[0].Name == "None")
                {
                    characterName1.SetText("");
                    characterLife1.SetText("");
                    characterDamage1.SetText("");
                    characterDefense1.SetText("");
                }
                else
                {
                    characterName1.SetText(partyCharacters[0].Name);
                    partyCharacters[0].weapon = characterWeapon1.Item;
                    partyCharacters[0].artifact1 = character1Artifact1.Item;
                    partyCharacters[0].artifact2 = character1Artifact2.Item;
                    partyCharacters[0].artifact3 = character1Artifact3.Item;
                    partyCharacters[0].artifact4 = character1Artifact4.Item;
                    partyCharacters[0].artifact5 = character1Artifact5.Item;
                    characterLife1.SetText(string.Format("{0} / {1}", partyCharacters[0].life, partyCharacters[0].lifeMax));
                    characterDamage1.SetText(string.Format("Damage: {0}", partyCharacters[0].damage));
                    characterDefense1.SetText(string.Format("Defense: {0}", partyCharacters[0].defense));
                }
            }
            if (partyCharacters.Count >= 2)
            {
                if (partyCharacters[1].Name == "None")
                {
                    characterName2.SetText("");
                    characterLife2.SetText("");
                    characterDamage2.SetText("");
                    characterDefense2.SetText("");
                }
                else
                {
                    characterName2.SetText(partyCharacters[1].Name);
                    partyCharacters[1].weapon = characterWeapon2.Item;
                    partyCharacters[1].artifact1 = character2Artifact1.Item;
                    partyCharacters[1].artifact2 = character2Artifact2.Item;
                    partyCharacters[1].artifact3 = character2Artifact3.Item;
                    partyCharacters[1].artifact4 = character2Artifact4.Item;
                    partyCharacters[1].artifact5 = character2Artifact5.Item;                   
                    characterLife2.SetText(string.Format("{0} / {1}", partyCharacters[1].life, partyCharacters[1].lifeMax));
                    characterDamage2.SetText(string.Format("Damage: {0}", partyCharacters[1].damage));
                    characterDefense2.SetText(string.Format("Defense: {0}", partyCharacters[1].defense));
                }
            }
            if (partyCharacters.Count >= 3)
            {
                if (partyCharacters[2].Name == "None")
                {
                    characterName3.SetText("");
                    characterLife3.SetText("");
                    characterDamage3.SetText("");
                    characterDefense3.SetText("");
                }
                else
                {
                    characterName3.SetText(partyCharacters[2].Name);
                    partyCharacters[2].weapon = characterWeapon3.Item;
                    partyCharacters[2].artifact1 = character3Artifact1.Item;
                    partyCharacters[2].artifact2 = character3Artifact2.Item;
                    partyCharacters[2].artifact3 = character3Artifact3.Item;
                    partyCharacters[2].artifact4 = character3Artifact4.Item;
                    partyCharacters[2].artifact5 = character3Artifact5.Item;
                    characterLife3.SetText(string.Format("{0} / {1}", partyCharacters[2].life, partyCharacters[2].lifeMax));
                    characterDamage3.SetText(string.Format("Damage: {0}", partyCharacters[2].damage));
                    characterDefense3.SetText(string.Format("Defense: {0}", partyCharacters[2].defense));
                }
            }
            if (partyCharacters.Count >= 4)
            {
                if (partyCharacters[3].Name == "None")
                {
                    characterName4.SetText("");
                    characterLife4.SetText("");
                    characterDamage4.SetText("");
                    characterDefense4.SetText("");
                }
                else
                {
                    characterName4.SetText(partyCharacters[3].Name);
                    partyCharacters[3].weapon = characterWeapon4.Item;
                    partyCharacters[3].artifact1 = character4Artifact1.Item;
                    partyCharacters[3].artifact2 = character4Artifact2.Item;
                    partyCharacters[3].artifact3 = character4Artifact3.Item;
                    partyCharacters[3].artifact4 = character4Artifact4.Item;
                    partyCharacters[3].artifact5 = character4Artifact5.Item;
                    characterLife4.SetText(string.Format("{0} / {1}", partyCharacters[3].life, partyCharacters[3].lifeMax));
                    characterDamage4.SetText(string.Format("Damage: {0}", partyCharacters[3].damage));
                    characterDefense4.SetText(string.Format("Defense: {0}", partyCharacters[3].defense));
                }
            }
        }

        #region Save Items

        private void SetCharacter1Weapon()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].weapon = characterWeapon1.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }
        private void SetCharacter1Artifact1()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].artifact1 = character1Artifact1.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }
        private void SetCharacter1Artifact2()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].artifact2 = character1Artifact2.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }
        private void SetCharacter1Artifact3()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].artifact3 = character1Artifact3.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }
        private void SetCharacter1Artifact4()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].artifact4 = character1Artifact4.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }
        private void SetCharacter1Artifact5()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[0] != null)
            {
                partyCharacters[0].artifact5 = character1Artifact5.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[0]);
            }
        }

        private void SetCharacter2Weapon()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].weapon = characterWeapon2.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }
        private void SetCharacter2Artifact1()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].artifact1 = character2Artifact1.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }
        private void SetCharacter2Artifact2()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].artifact2 = character2Artifact2.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }
        private void SetCharacter2Artifact3()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].artifact3 = character2Artifact3.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }
        private void SetCharacter2Artifact4()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].artifact4 = character2Artifact4.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }
        private void SetCharacter2Artifact5()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[1] != null)
            {
                partyCharacters[1].artifact5 = character2Artifact5.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[1]);
            }
        }

        private void SetCharacter3Weapon()
        {
            Main.NewText(characterWeapon3.Item.Name);
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].weapon = characterWeapon3.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }
        private void SetCharacter3Artifact1()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].artifact1 = character3Artifact1.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }
        private void SetCharacter3Artifact2()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].artifact2 = character3Artifact2.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }
        private void SetCharacter3Artifact3()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].artifact3 = character3Artifact3.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }
        private void SetCharacter3Artifact4()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].artifact4 = character3Artifact4.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }
        private void SetCharacter3Artifact5()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[2] != null)
            {
                partyCharacters[2].artifact5 = character3Artifact5.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[2]);
            }
        }

        private void SetCharacter4Weapon()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].weapon = characterWeapon4.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }
        private void SetCharacter4Artifact1()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].artifact1 = character4Artifact1.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }
        private void SetCharacter4Artifact2()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].artifact2 = character4Artifact2.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }
        private void SetCharacter4Artifact3()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].artifact3 = character4Artifact3.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }
        private void SetCharacter4Artifact4()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].artifact4 = character4Artifact4.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }
        private void SetCharacter4Artifact5()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();
            if (partyCharacters[3] != null)
            {
                partyCharacters[3].artifact5 = character4Artifact5.Item;
                ModifyCharacterStats.AdjustCharacterStats(partyCharacters[3]);
            }
        }

        #endregion
    }
}
