using UnityEngine;

public class PlayersData: MonoBehaviour
{
    public static PlayersData Instance;
    public Player[] players = new Player[1];
    public struct Player
    {
        public int character;
        
        public Player(int character)
        {
            this.character = character;
        }
    }

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
