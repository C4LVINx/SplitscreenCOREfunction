using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string lobbySceneName = "LobbyScene"; // Set this to your actual lobby scene name

    public void LoadLobby()
    {
        Debug.Log("🟢 UI Button Clicked! Loading Lobby Scene...");
        SceneManager.LoadScene(lobbySceneName);
    }
}