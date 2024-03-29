﻿using GenshinMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace GenshinMod.UI
{
    class UISystem : ModSystem
    {
        internal UserInterface GenshinInterface;
        internal CharacterListUI characterListUI;
        internal PartyUI partyUI;
        internal GachaUI gachaUI;

        private GameTime _lastUpdateUiGameTime;

        public static UISystem Instance;

        public override void Load()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            if (!Main.dedServ)
            {
                GenshinInterface = new UserInterface();

                characterListUI = new CharacterListUI();
                characterListUI.Activate();

                partyUI = new PartyUI();
                partyUI.Activate(); 

                gachaUI = new GachaUI();
                gachaUI.Activate();
            }               
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (GenshinInterface?.CurrentState != null)
            {
                GenshinInterface.Update(gameTime);
            }

            base.UpdateUI(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "GenshinMod: UIInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && GenshinInterface?.CurrentState != null)
                        {
                            GenshinInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        internal void ShowCharacterUI()
        {
            //characterListUI.OnInitialize(); // Remove later this is only for testing
            GenshinInterface?.SetState(characterListUI);
            characterListUI.OpenMenu();
        }

        internal void ShowPartyUI()
        {
            //partyUI.OnInitialize();
            GenshinInterface?.SetState(partyUI);
            partyUI.OpenMenu();
        }

        internal void ShowGachaUI()
        {
            //gachaUI.OnInitialize();
            GenshinInterface?.SetState(gachaUI);
        }

        internal void HideUIs()
        {
            characterListUI.CloseMenu();
            partyUI.CloseMenu();
            GenshinInterface?.SetState(null);
        }
    }
}
