using Microsoft.Xna.Framework;
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
        DragableUIPanel mainWindow;

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
        VanillaItemSlotWrapper characterWeapon1, characterWeapon2, characterWeapon3, characterWeapon4;
        VanillaItemSlotWrapper character1Artifact1, character1Artifact2, character1Artifact3, character1Artifact4, character1Artifact5;
        VanillaItemSlotWrapper character2Artifact1, character2Artifact2, character2Artifact3, character2Artifact4, character2Artifact5;
        VanillaItemSlotWrapper character3Artifact1, character3Artifact2, character3Artifact3, character3Artifact4, character3Artifact5;
        VanillaItemSlotWrapper character4Artifact1, character4Artifact2, character4Artifact3, character4Artifact4, character4Artifact5;

        int characterSelected = 0;

        public override void OnInitialize()
        {
            IntializeCharacter1Panel();
            IntializeCharacter2Panel();
            IntializeCharacter3Panel();
            IntializeCharacter4Panel();

            // Same list from CharacterUI
            listPanel = new();
            listPanel.Width.Set(270, 0f);
            listPanel.Height.Set(370, 0f);
            listPanel.Left.Set(250, 0f);
            listPanel.Top.Set(220, 0f);
            listPanel.BackgroundColor = new Color(99, 98, 112);
            listPanel.BorderColor = new Color(166, 165, 181);
            listPanel.OverflowHidden = false;
            listPanel.PaddingLeft = listPanel.PaddingRight = 10;

            // The element that will store the buttons for all the characters the player has
            characterList = new();
            characterList.SetPadding(0);
            characterList.Width.Set(270, 0f);
            characterList.Height.Set(370, 0f);
            characterList.Left.Set(0, 0);
            characterList.Top.Set(0, 0);
            characterList.ListPadding = 10;
            listPanel.Append(characterList);
        }

        public void IntializeCharacter1Panel()
        {
            characterPanel1 = new();
            characterPanel1.Width.Set(250, 0);
            characterPanel1.Height.Set(300, 0);
            characterPanel1.HAlign = 0.45f;
            characterPanel1.Top.Set(20f, 0);
            Append(characterPanel1);

            characterPfp1 = new();
            characterPfp1.Width.Set(50, 0);
            characterPfp1.Height.Set(50, 0);
            characterPfp1.OnClick += OnCharacter1Click;
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
            characterWeapon1.Top.Set(100f, 0);
            characterPanel1.Append(characterWeapon1);

            character1Artifact1 = new();
            character1Artifact1.Top.Set(160f, 0);
            characterPanel1.Append(character1Artifact1);

            character1Artifact2 = new();
            character1Artifact2.Top.Set(220f, 0);
            characterPanel1.Append(character1Artifact2);

            character1Artifact3 = new();
            character1Artifact3.Left.Set(60f, 0);
            character1Artifact3.Top.Set(100f, 0);
            characterPanel1.Append(character1Artifact3);

            character1Artifact4 = new();
            character1Artifact4.Left.Set(60f, 0);
            character1Artifact4.Top.Set(160f, 0);
            characterPanel1.Append(character1Artifact4);

            character1Artifact5 = new();
            character1Artifact5.Left.Set(60f, 0);
            character1Artifact5.Top.Set(220f, 0);
            characterPanel1.Append(character1Artifact5);
        }

        public void IntializeCharacter2Panel()
        {
            characterPanel2 = new();
            characterPanel2.Width.Set(250, 0);
            characterPanel2.Height.Set(300, 0);
            characterPanel2.HAlign = 0.65f;
            characterPanel2.Top.Set(20f, 0);
            Append(characterPanel2);

            characterPfp2 = new();
            characterPfp2.Width.Set(50, 0);
            characterPfp2.Height.Set(50, 0);
            characterPfp2.OnClick += OnCharacter2Click;
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
            characterWeapon2.Top.Set(100f, 0);
            characterPanel2.Append(characterWeapon2);

            character2Artifact2 = new();
            character2Artifact2.Top.Set(160f, 0);
            characterPanel2.Append(character2Artifact2);
        
            character2Artifact2 = new();
            character2Artifact2.Top.Set(220f, 0);
            characterPanel2.Append(character2Artifact2);

            character2Artifact3 = new();
            character2Artifact3.Left.Set(60f, 0);
            character2Artifact3.Top.Set(100f, 0);
            characterPanel2.Append(character2Artifact3);

            character2Artifact4 = new();
            character2Artifact4.Left.Set(60f, 0);
            character2Artifact4.Top.Set(160f, 0);
            characterPanel2.Append(character2Artifact4);

            character2Artifact5 = new();
            character2Artifact5.Left.Set(60f, 0);
            character2Artifact5.Top.Set(220f, 0);
            characterPanel2.Append(character2Artifact5);
        }

        public void IntializeCharacter3Panel()
        {
            characterPanel3 = new();
            characterPanel3.Width.Set(250, 0);
            characterPanel3.Height.Set(300, 0);
            characterPanel3.HAlign = 0.45f;
            characterPanel3.Top.Set(330f, 0);
            Append(characterPanel3);

            characterPfp3 = new();
            characterPfp3.Width.Set(50, 0);
            characterPfp3.Height.Set(50, 0);
            characterPfp3.OnClick += OnCharacter3Click;
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
            characterWeapon3.Top.Set(100f, 0);
            characterPanel3.Append(characterWeapon3);

            character3Artifact3 = new();
            character3Artifact3.Top.Set(160f, 0);
            characterPanel3.Append(character3Artifact3);

            character3Artifact3 = new();
            character3Artifact3.Top.Set(220f, 0);
            characterPanel3.Append(character3Artifact3);

            character3Artifact3 = new();
            character3Artifact3.Left.Set(60f, 0);
            character3Artifact3.Top.Set(100f, 0);
            characterPanel3.Append(character3Artifact3);

            character3Artifact4 = new();
            character3Artifact4.Left.Set(60f, 0);
            character3Artifact4.Top.Set(160f, 0);
            characterPanel3.Append(character3Artifact4);

            character3Artifact5 = new();
            character3Artifact5.Left.Set(60f, 0);
            character3Artifact5.Top.Set(220f, 0);
            characterPanel3.Append(character3Artifact5);
        }

        public void IntializeCharacter4Panel()
        {
            characterPanel4 = new();
            characterPanel4.Width.Set(250, 0);
            characterPanel4.Height.Set(300, 0);
            characterPanel4.HAlign = 0.65f;
            characterPanel4.Top.Set(330f, 0);
            Append(characterPanel4);

            characterPfp4 = new();
            characterPfp4.Width.Set(50, 0);
            characterPfp4.Height.Set(50, 0);
            characterPfp4.OnClick += OnCharacter4Click;
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
            characterWeapon4.Top.Set(100f, 0);
            characterPanel4.Append(characterWeapon4);

            character4Artifact4 = new();
            character4Artifact4.Top.Set(160f, 0);
            characterPanel4.Append(character4Artifact4);

            character4Artifact4 = new();
            character4Artifact4.Top.Set(220f, 0);
            characterPanel4.Append(character4Artifact4);

            character4Artifact4 = new();
            character4Artifact4.Left.Set(60f, 0);
            character4Artifact4.Top.Set(100f, 0);
            characterPanel4.Append(character4Artifact4);

            character4Artifact4 = new();
            character4Artifact4.Left.Set(60f, 0);
            character4Artifact4.Top.Set(160f, 0);
            characterPanel4.Append(character4Artifact4);

            character4Artifact5 = new();
            character4Artifact5.Left.Set(60f, 0);
            character4Artifact5.Top.Set(220f, 0);
            characterPanel4.Append(character4Artifact5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (listPanel.IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        // Used so we can laod the UI with player information,
        // as doing it in OnInitialize will crash the game
        // as no player has been loaded at that point yet
        public void OpenMenu()
        {
            characterList.Clear();
            characterSelected = -1;
            //header.SetText("Party Setup");

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> playerCharacters = modPlayer.GetCharacters();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();

            if (playerCharacters.Count <= 0) return;

            characterSelected = 0;

            UpdateCharacterInfo(partyCharacters);

            foreach (Character character in playerCharacters)
            {
                UIPanel button = new UIPanel();
                button.Width.Set(50, 0);
                button.Height.Set(50, 0);
                button.Left.Set(0, 0f);
                button.Top.Set(100, 0);
                button.BackgroundColor = new Color(48, 51, 59);
                button.OnClick += OnCharacterListClick;
                characterList.Add(button);

                UIText text = new UIText(character.Name);
                text.HAlign = text.VAlign = 0.5f;
                button.Append(text);
            }

            listPanel.Remove();
        }

        private void OnCharacter1Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if(characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 1;
                //header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                //header.SetText("Party Setup");
            }

        }

        private void OnCharacter2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 2;
                //header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                //header.SetText("Party Setup");
            }
        }

        private void OnCharacter3Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 3;
                //header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                //header.SetText("Party Setup");

            }
        }

        private void OnCharacter4Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 4;
                //header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                //header.SetText("Party Setup");
            }
        }

        private void OnCharacterListClick(UIMouseEvent evt, UIElement listeningElement)
        {
            listPanel.Remove();
            //header.SetText("Party Setup");

            foreach (UIElement element in listeningElement.Children)
            {
                UIText text = (UIText)element;
                var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
                List<Character> partyCharacters = modPlayer.GetPartyCharacters();

                // If the player selects the character that is already in that position, remove the character from the party
                if (text.Text != partyCharacters[characterSelected - 1].Name) modPlayer.ChangePartyCharacters(text.Text, characterSelected - 1);
                else modPlayer.ChangePartyCharacters("None", characterSelected - 1);

                UpdateCharacterInfo(partyCharacters);
            }

            characterSelected = 0;
        }

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
                    characterLife4.SetText(string.Format("{0} / {1}", partyCharacters[3].life, partyCharacters[3].lifeMax));
                    characterDamage4.SetText(string.Format("Damage: {0}", partyCharacters[3].damage));
                    characterDefense4.SetText(string.Format("Defense: {0}", partyCharacters[3].defense));
                }
            }
        }
    }
}
