#region

#endregion

namespace Main.Event
{
    public class TriggerEnter
    {
    #region Public Variables

        public Character.Character Character   { get; }
        public string              CharacterId { get; }

    #endregion

    #region Constructor

        public TriggerEnter(string characterId , Character.Character character)
        {
            CharacterId = characterId;
            Character   = character;
        }

    #endregion
    }

    public class TriggerExit
    {
    #region Public Variables

        public Character.Character Character   { get; }
        public string              CharacterId { get; }

    #endregion

    #region Constructor

        public TriggerExit(string characterId , Character.Character character)
        {
            CharacterId = characterId;
            Character   = character;
        }

    #endregion
    }
}