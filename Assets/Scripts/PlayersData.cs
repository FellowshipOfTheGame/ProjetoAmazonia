using System.Collections.Generic;
using UnityEngine;

public class PlayersData: MonoBehaviour
{
    public class Player
    {
        public int character;
        
        public Player(int character)
        {
            this.character = character;
        }
    }
    
    public static PlayersData Instance;
    public List<Player> players;

    private void Awake()
    {
        #region Singleton
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        #endregion
    }
}
