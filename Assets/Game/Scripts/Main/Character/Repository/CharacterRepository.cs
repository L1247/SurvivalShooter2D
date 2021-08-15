#region

using System.Collections.Generic;

#endregion

namespace Main.Character.Repository
{
    public class CharacterRepository
    {
    #region Private Variables

        private readonly Dictionary<string , Character> characters = new Dictionary<string , Character>();

    #endregion

    #region Public Methods

        public Character FindById(string id)
        {
            return characters[id];
        }

        public void Register(string id , Character character)
        {
            characters.Add(id , character);
        }

    #endregion
    }
}