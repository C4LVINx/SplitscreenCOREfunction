using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    [System.Serializable]
    public class Route
    {
        public string routeName;
        public Transform startPoint;
        public Transform endPoint;
    }

    public List<Route> availableRoutes;
    public int selectedRouteIndex = 0;
    private List<PlayerInput> joinedPlayers = new List<PlayerInput>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        if (!joinedPlayers.Contains(playerInput) && joinedPlayers.Count < 4)
        {
            joinedPlayers.Add(playerInput);
            Debug.Log($"Player {joinedPlayers.Count} joined!");
        }
    }

    public void ChangeRoute(int direction)
    {
        selectedRouteIndex = (selectedRouteIndex + direction) % availableRoutes.Count;
        if (selectedRouteIndex < 0) selectedRouteIndex = availableRoutes.Count - 1;
        Debug.Log("Selected Route: " + availableRoutes[selectedRouteIndex].routeName);
    }

    public void StartGame()
    {
        if (joinedPlayers.Count > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public List<PlayerInput> GetJoinedPlayers()
    {
        return joinedPlayers;
    }

    public Route GetSelectedRoute()
    {
        return availableRoutes[selectedRouteIndex];
    }
}

