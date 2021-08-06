#region

using System.Collections.Generic;

#endregion

namespace Main.Character
{
    public class CharacterRepository
    {
    #region Private Variables

        private readonly Dictionary<string , Character> characters = new Dictionary<string , Character>();

    #endregion

    #region Public Methods

        public void Register(string id , Character character)
        {
            characters.Add(id , character);
        }

    #endregion
    }
}