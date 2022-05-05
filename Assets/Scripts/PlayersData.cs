using UnityEngine;

public class PlayersData: MonoBehaviour
{
    public static PlayersData Instance;
    public int playersCount;
    public struct Player
    {
        public int index;
        public int character;
        
        public Player(int index, int character)
        {
            this.index = index;
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

        playersCount = PlayerPrefsController.GetPlayersCount();
    }

    public Player[] players;
}
