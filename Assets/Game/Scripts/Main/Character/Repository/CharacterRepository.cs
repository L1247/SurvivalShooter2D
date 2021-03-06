#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Main.Character.Repository
{
    public class CharacterRepository
    {
    #region Private Variables

        private readonly Dictionary<string , Character> characters = new Dictionary<string , Character>();

    #endregion

    #region Public Methods

        public List<string> FindAllId()
        {
            return characters.Keys.ToList();
        }

        public Character FindById(string id)
        {
            return characters[id];
        }

        public List<Character> GetAllCharacter()
        {
            return characters.Values.ToList();
        }

        public void Register(string id , Character character)
        {
            characters.Add(id , character);
        }

        public void Remove(string characterId)
        {
            characters.Remove(characterId);
        }

    #endregion
    }
}