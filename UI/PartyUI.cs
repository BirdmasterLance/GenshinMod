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
    internal class PartyUI : UIState
    {
        DragableUIPanel mainWindow;

        UIText header;
        UIPanel listPanel;
        UIList characterList;
        UIText characterName1, characterName2, characterName3, characterName4;
        UIPanel characterPanel1, characterPanel2, characterPanel3, characterPanel4;
        int characterSelected = 0;

        public override void OnInitialize()
        {
            mainWindow = new DragableUIPanel();
            mainWindow.Width.Set(900, 0);
            mainWindow.Height.Set(400, 0);
            mainWindow.BackgroundColor = new Color(43, 41, 153);
            mainWindow.HAlign = mainWindow.VAlign = 0.5f; // Center the main window
            mainWindow.SetPadding(0);

            header = new UIText("Party Setup");
            header.TextColor = new Color(232, 190, 128);
            header.Top.Set(20, 0);
            header.Left.Set(40, 0);
            mainWindow.Append(header);

            characterName1 = new UIText("None");
            characterName1.Top.Set(60, 0);
            characterName1.HAlign = 0.1f;
            mainWindow.Append(characterName1);
            characterPanel1 = new UIPanel();
            characterPanel1.Top.Set(100, 0);
            characterPanel1.Left.Set(36f, 0);
            characterPanel1.Width.Set(175, 0);
            characterPanel1.Height.Set(225, 0);
            characterPanel1.OnClick += OnCharacter1Click;
            mainWindow.Append(characterPanel1);

            characterName2 = new UIText("None");
            characterName2.Top.Set(60, 0);
            characterName2.HAlign = 0.36f;
            mainWindow.Append(characterName2);
            characterPanel2 = new UIPanel();
            characterPanel2.Top.Set(100, 0);
            characterPanel2.Left.Set(247.5f, 0);
            characterPanel2.Width.Set(175, 0);
            characterPanel2.Height.Set(225, 0);
            characterPanel2.OnClick += OnCharacter2Click;
            mainWindow.Append(characterPanel2);

            characterName3 = new UIText("None");
            characterName3.Top.Set(60, 0);
            characterName3.HAlign = 0.64f;
            mainWindow.Append(characterName3);
            characterPanel3 = new UIPanel();
            characterPanel3.Top.Set(100, 0);
            characterPanel3.Left.Set(477.5f, 0);
            characterPanel3.Width.Set(175, 0);
            characterPanel3.Height.Set(225, 0);
            characterPanel3.OnClick += OnCharacter3Click;
            mainWindow.Append(characterPanel3);

            characterName4 = new UIText("None");
            characterName4.Top.Set(60, 0);
            characterName4.HAlign = 0.9f;
            mainWindow.Append(characterName4);
            characterPanel4 = new UIPanel();
            characterPanel4.Top.Set(100, 0);
            characterPanel4.Left.Set(692.5f, 0);
            characterPanel4.Width.Set(175, 0);
            characterPanel4.Height.Set(225, 0);
            characterPanel4.OnClick += OnCharacter4Click;
            mainWindow.Append(characterPanel4);

            // Same list from CharacterUI
            listPanel = new UIPanel();
            listPanel.Width.Set(70, 0f);
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
            characterList.Width.Set(70, 0f);
            characterList.Height.Set(370, 0f);
            characterList.Left.Set(0, 0);
            characterList.Top.Set(0, 0);
            characterList.ListPadding = 10;
            listPanel.Append(characterList);

            Append(mainWindow);
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
            header.SetText("Party Setup");

            var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
            List<Character> playerCharacters = modPlayer.GetCharacters();
            List<Character> partyCharacters = modPlayer.GetPartyCharacters();

            if (playerCharacters.Count <= 0) return;

            characterSelected = 0;           

            if (partyCharacters.Count >= 1) characterName1.SetText(partyCharacters[0].Name);
            if (partyCharacters.Count >= 2) characterName2.SetText(partyCharacters[1].Name);
            if (partyCharacters.Count >= 3) characterName3.SetText(partyCharacters[2].Name);
            if (partyCharacters.Count >= 4) characterName4.SetText(partyCharacters[3].Name);

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
                header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                header.SetText("Party Setup");
            }

        }

        private void OnCharacter2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 2;
                header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                header.SetText("Party Setup");
            }
        }

        private void OnCharacter3Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 3;
                header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                header.SetText("Party Setup");

            }
        }

        private void OnCharacter4Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (characterSelected == -1) return;

            if (characterSelected == 0)
            {
                Append(listPanel);
                characterSelected = 4;
                header.SetText("Select a character from the list");
            }
            else
            {
                listPanel.Remove();
                characterSelected = 0;
                header.SetText("Party Setup");
            }
        }

        private void OnCharacterListClick(UIMouseEvent evt, UIElement listeningElement)
        {
            listPanel.Remove();
            header.SetText("Party Setup");

            foreach (UIElement element in listeningElement.Children)
            {
                UIText text = (UIText)element;
                var modPlayer = Main.LocalPlayer.GetModPlayer<PlayerCharacterCode>();
                List<Character> partyCharacters = modPlayer.GetPartyCharacters();

                // If the player selects the character that is already in that position, remove the character from the party
                if(text.Text != partyCharacters[characterSelected-1].Name) modPlayer.ChangePartyCharacters(text.Text, characterSelected-1);
                else modPlayer.ChangePartyCharacters("Remove", characterSelected - 1);

                if (partyCharacters.Count > 0) characterName1.SetText(partyCharacters[0].Name);
                if (partyCharacters.Count > 1) characterName2.SetText(partyCharacters[1].Name);
                if (partyCharacters.Count > 2) characterName3.SetText(partyCharacters[2].Name);
                if (partyCharacters.Count > 3) characterName4.SetText(partyCharacters[3].Name);
            }

            characterSelected = 0;
        }
    }
}
