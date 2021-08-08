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

    public class CharacterDead
    {
    #region Public Variables

        public string CharacterId { get; }

    #endregion

    #region Constructor

        public CharacterDead(string characterId)
        {
            CharacterId = characterId;
        }

    #endregion
    }

    public class CharacterHurt
    {
    #region Public Variables

        public int    Damage      { get; }
        public string CharacterId { get; }

    #endregion

    #region Constructor

        public CharacterHurt(string characterId , int damage)
        {
            CharacterId = characterId;
            Damage      = damage;
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