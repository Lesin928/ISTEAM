using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void OnDisable()
    {
        GameManager.Instance.ContinueGame();      
    }
}

