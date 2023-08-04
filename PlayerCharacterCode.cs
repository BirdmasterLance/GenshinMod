using GenshinMod.Characters;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GenshinMod
{
    class PlayerCharacterCode : ModPlayer
    {
        private List<Character> characters;
        public List<Character> partyCharacters;
        public List<Character> activeCharacters;

        public int selectedCharacter = -1;
        public bool replaceTexture; // Checks if we want to replace the texture

        public override void ResetEffects()
        {
            // replaceTexture = false;
        }

        public override void Initialize()
        {
            characters = new List<Character>();
            partyCharacters = new List<Character>();
            activeCharacters = new List<Character>();
        }

        public override void SaveData(TagCompound tag)
        {
            if(!tag.ContainsKey("characters"))
            {
                tag.Add("characters", characters);
            }
            else
            {
                tag.Set("characters", characters);
            }

            if(!tag.ContainsKey("partyCharacters"))
            {
                tag.Add("partyCharacters", partyCharacters);
            }
            else
            {
                tag.Set("partyCharacters", partyCharacters);
            }
        }

        public override void LoadData(TagCompound tag)
        {
            // Uncomment these if you need to wipe out all data related to characters
            tag.Remove("characters");
            tag.Remove("partyCharacters");

            characters = (List<Character>)tag.GetList<Character>("characters");
            partyCharacters = (List<Character>)tag.GetList<Character>("partyCharacters");
            if (partyCharacters.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    partyCharacters.Add(new Character("None"));
                }
            }
            activeCharacters.Clear();
        }

        // We're going to use this as a loop for important player Character information
        // Since it always runs when the player exists
        public override void PostUpdate()
        {
            // Just going to store the number of characters that have a specific element in an array
            // 0: Anemo
            // 1: Geo
            // 2: Electro
            // 3: Dendro
            // 4: Hydro
            // 5: Pyro
            // 6: Cryo
            int[] elementCounts = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            int oneOfEachElement = 0;

            foreach (Character character in partyCharacters) // Count how many characters are of a certain element for a player
            {
                switch (character.Element)
                {
                    case Elements.Element.Anemo:
                        elementCounts[0]++;
                        break;
                    case Elements.Element.Geo:
                        elementCounts[1]++;
                        break;
                    case Elements.Element.Electro:
                        elementCounts[2]++;
                        break;
                    case Elements.Element.Dendro:
                        elementCounts[3]++;
                        break;
                    case Elements.Element.Hydro:
                        elementCounts[4]++;
                        break;
                    case Elements.Element.Pyro:
                        elementCounts[5]++;
                        break;
                    case Elements.Element.Cryo:
                        elementCounts[6]++;
                        break;
                }
            }

            if (elementCounts[(int) Elements.Element.Anemo] >= 2)
            {
                Player.moveSpeed += 0.1f;
            }
            if (elementCounts[(int)Elements.Element.Hydro] >= 2)
            {
                Player.statLifeMax2 = (int)(Player.statLifeMax2 * 1.25);
            }
            if (elementCounts[(int)Elements.Element.Pyro] >= 2)
            {
                Player.GetDamage(DamageClass.Generic) += 0.25f; // Additive stat boost as this is how Terraria normally does it
            }
            // Use a loop to check if there is only 1 of an element in the array
            foreach(int i in elementCounts)
            {
                if (i == 1) oneOfEachElement++;
            }
            if(oneOfEachElement == 4)
            {
                Player.statDefense *= 1.15f; // How TMod recommends we do multiplicative increases
            }

            // Calculate the stats for every character to make sure the values are always up to date
            // May be inefficient, but without this, bugs can occur
            // For example, if you switch someone off of your party after they are affected by
            // Elemental Resonance, they keep their adjusted stats despite not being in any party.
            for(int i = 0; i < characters.Count; i++)
            {
                ModifyCharacterStats.AdjustCharacterStats(characters[i]);
            }

            // Adjust the stats of every character in the party due to Elemental Resonance
            for (int i = 0; i < partyCharacters.Count; i++)
            { 
                if (elementCounts[(int)Elements.Element.Hydro] >= 2)
                {
                    partyCharacters[i].lifeMax = (int)(partyCharacters[i].lifeMax * 1.25f);
                    partyCharacters[i].life = (int)(partyCharacters[i].life * 1.25f);
                }
                if (elementCounts[(int)Elements.Element.Pyro] >= 2)
                {
                    partyCharacters[i].damage = (int)(partyCharacters[i].damage * 1.25f);
                }
                if(oneOfEachElement == 4)
                {
                    partyCharacters[i].defense = (int)(partyCharacters[i].defense * 1.15f);
                }
            }
        }

        /// <summary>
        /// Get the string List of all the characters a player has.
        /// </summary>
        public ref List<Character> GetCharacters()
        {
            return ref characters;
        }

        /// <summary>
        /// Returns true or false depending on if the player has said character.
        /// </summary>
        public bool HasCharacter(string character)
        {
            foreach(Character c in characters)
            {
                if(c.Name == character)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a character to the player's list of characters, if they don't have it.
        /// </summary>
        public bool AddCharacter(string character)
        {
            if (!HasCharacter(character))
            {
                //Character partyCharacter = GetCharacter(character);
                //if (partyCharacter.ConstellationUpgrade < 6) partyCharacter.ConstellationUpgrade++;
                //return true;
                Character characterToAdd = CharacterLists.GetNewCharacter(character);
                if (characterToAdd == null)
                {
                    Main.NewText(string.Format("Could not add {0}!", character));
                }
                characters.Add(characterToAdd);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a character from the player's list of characters, if they have it.
        /// </summary>
        public bool RemoveCharacter(string character)
        {
            if (HasCharacter(character))
            {
                characters.Remove(characters.Find(chara => chara.Name == character));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all characters in the player's party formation.
        /// </summary>
        public ref List<Character> GetPartyCharacters()
        {
            return ref partyCharacters;
        }

        public bool AddPartyCharacter(string character)
        {
            if (HasCharacter(character))
            {
                Character characterToAdd = characters.Find(chara => chara.Name == character);
                if(characterToAdd == null)
                {
                    Main.NewText(string.Format("Could not add {0}!", characterToAdd));
                }
                partyCharacters.Add(characters.Find(chara => chara.Name == character));  
                return true;
            }
            return false;
        }

        public bool RemovePartyCharacter(string character)
        {
            if (HasCharacter(character))
            {
                partyCharacters.Remove(characters.Find(chara => chara.Name == character));
            }
            return false;
        }

        /// <summary>
        /// Swaps out the characters in the party formation based on the specified slot.
        /// </summary>
        public void ChangePartyCharacters(string character, int slot)
        {
            if (slot >= 4) return;
            if (character == "Remove" || character == "None")
            {
                partyCharacters[slot] = new Character("None");
            }
            if (!HasCharacter(character)) return;
            if(HasPartyCharacter(character))
            {
                Character oldChar = partyCharacters[slot];
                Character newChar = null;
                int newSlot = -1;
                for(int i = 0; i < partyCharacters.Count; i++)
                {
                    if(partyCharacters[i].Name == character)
                    {
                        newChar = partyCharacters[i];
                        newSlot = i;
                        break;
                    }
                }
                partyCharacters[slot] = newChar;
                partyCharacters[newSlot] = oldChar; 
            }
            else
            {
                partyCharacters[slot] = characters.Find(chara => chara.Name == character);
            }
        }

        public ref List<Character> GetActiveCharacters()
        {
            return ref activeCharacters;
        }

        private bool HasPartyCharacter(string character)
        {
            foreach (Character c in partyCharacters)
            {
                if (c.Name == character)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SetActiveCharacter(string character)
        {
            if (activeCharacters.Count >= 2) return false;
            if (!HasCharacter(character)) return false;
            if(activeCharacters.Count == 0)
            {
                activeCharacters.Add(characters.Find(chara => chara.Name == character));
            }
            else
            {
                activeCharacters[0] = characters.Find(chara => chara.Name == character);
            }
            return true;
        }

        public bool RemoveActiveCharacter(string character)
        {
            if (activeCharacters.Count <= 0) return false;
            if (!HasCharacter(character)) return false;
            activeCharacters.Remove(characters.Find(chara => chara.Name == character));
            return true;
        }

        public bool IsActiveCharacter(string character)
        {
            if (activeCharacters.Count <= 0) return false;
            if (!HasCharacter(character)) return false;
            return activeCharacters.Contains(characters.Find(chara => chara.Name == character));
        }

        ///// <summary>
        ///// Changes the player's active character, as well as change their sprite.
        ///// </summary>
        //public bool ChangeActiveCharacter(string character)
        //{
        //    if (!HasCharacter(character))
        //    {
        //        //Main.NewText($"Your don't have: {activeCharacter.Name}!");
        //        return false;
        //    }
        //    activeCharacterName = character;
        //    activeCharacter = GetCharacter(character);
        //    replaceTexture = true;
        //    Main.NewText($"Your active character is: {activeCharacter.Name}");

        //    // TODO: when sprites are added, enable all of this code

        //    // Handles equipping the correct texture for the character
        //    //int equipSlotHead = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Head);
        //    //int equipSlotBody = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Body);
        //    //int equipSlotLegs = EquipLoader.GetEquipSlot(GenshinMod.Instance, activeCharacter, EquipType.Legs);

        //    //// Determines whether we show the head, body, and legs
        //    //ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
        //    //ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
        //    //ArmorIDs.Body.Sets.HidesBottomSkin[equipSlotBody] = true;
        //    //ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
        //    //ArmorIDs.Legs.Sets.HidesTopSkin[equipSlotLegs] = true;
        //    //ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;         
        //    //ArmorIDs.Legs.Sets.OverridesLegs[equipSlotLegs] = true;

        //    return true;
        //}

        //public void RemoveActiveCharacter()
        //{
        //    replaceTexture = false;
        //    activeCharacter = null;
        //    Main.NewText($"Removed active character");
        //}

        // Handles replacing the player animations
        // According to TModLoader, this method does not run when the game is paused
        // This means that the changed character will not show when the game is paused (whenever the game is frozen)
        public override void FrameEffects()
        {
            if(replaceTexture)
            {
                // Replaces the player's head/body/legs with the correct equipment
                // We find the equipment based on its name when we loaded it all in GenshinTest.cs
                //var exampleCostume = ModContent.GetInstance<CharacterCostume>();
                //Player.head = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Head);
                //Player.body = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Body);
                //Player.legs = EquipLoader.GetEquipSlot(Mod, activeCharacter.Name, EquipType.Legs);
            }
        }
    }
}
