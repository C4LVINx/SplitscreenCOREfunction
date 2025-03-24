using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;
    private List<PlayerInput> joinedPlayers = new List<PlayerInput>();

    [System.Serializable]
    public class Route
    {
        public string routeName;
        public Transform startPoint;
        public Transform endPoint;
    }

    public List<Route> availableRoutes;
    private int selectedRouteIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔹 Logs when a player joins
    public void OnPlayerJoin(PlayerInput playerInput)
    {
        if (!joinedPlayers.Contains(playerInput) && joinedPlayers.Count < 4)
        {
            joinedPlayers.Add(playerInput);
            Debug.Log($"✅ Player {joinedPlayers.Count} joined! (Device: {playerInput.devices[0]})");

            playerInput.GetComponent<PlayerController>().playerIndex = joinedPlayers.Count - 1;
        }
    }

    // 🔹 Logs route selection
    public void OnRouteSelection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int direction = (int)context.ReadValue<float>();
            selectedRouteIndex = (selectedRouteIndex + direction) % availableRoutes.Count;
            if (selectedRouteIndex < 0) selectedRouteIndex = availableRoutes.Count - 1;

            Debug.Log($"🔄 Route changed: {availableRoutes[selectedRouteIndex].routeName}");
        }
    }

    // 🔹 Logs when the game starts
    public void OnStartGame(InputAction.CallbackContext context)
    {
        if (context.performed && joinedPlayers.Count > 0)
        {
            Debug.Log($"🚀 Starting game with {joinedPlayers.Count} players. Loading GameScene...");
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogWarning("⚠️ Cannot start game: No players joined!");
        }
    }

    public Route GetSelectedRoute()
    {
        return availableRoutes[selectedRouteIndex];
    }

    public List<PlayerInput> GetJoinedPlayers()
    {
        return joinedPlayers;
    }
}