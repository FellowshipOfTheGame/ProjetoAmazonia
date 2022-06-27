using System.Collections.Generic;
using UnityEngine;

public class PlayersData: MonoBehaviour
{
    public class Player
    {
        public readonly int character;
        
        public Player(int character)
        {
            this.character = character;
        }
    }
    
    public static PlayersData instance;
    public List<Player> players;

    private void Awake()
    {
        #region Singleton
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        #endregion
    }
}
