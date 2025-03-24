using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private List<PlayerInput> players;
    private LobbyManager.Route selectedRoute;

    private void Start()
    {
        players = LobbyManager.Instance.GetJoinedPlayers();
        selectedRoute = LobbyManager.Instance.GetSelectedRoute();

        Debug.Log($"🎮 Game started! {players.Count} players will spawn at {selectedRoute.routeName}.");

        SpawnPlayers();
        SetupSplitScreen();
    }

    private void SpawnPlayers()
    {
        Vector3 spawnPosition = selectedRoute.startPoint.position;
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab, spawnPosition + new Vector3(i * 2, 0, 0), Quaternion.identity);
            PlayerInput input = player.GetComponent<PlayerInput>();

            Debug.Log($"🆕 Player {i + 1} spawned at {spawnPosition + new Vector3(i * 2, 0, 0)} with control scheme {players[i].currentControlScheme}");

            input.SwitchCurrentControlScheme(players[i].currentControlScheme, players[i].devices.ToArray());
        }
    }

    private void SetupSplitScreen()
    {
        int playerCount = players.Count;
        for (int i = 0; i < playerCount; i++)
        {
            Camera cam = players[i].GetComponentInChildren<Camera>(); // 🔹 Now finds the player's camera

            if (cam == null)
            {
                Debug.LogError($"❌ Player {i + 1} has no camera! Cannot set up split-screen.");
                continue;
            }

            if (playerCount == 1)
                cam.rect = new Rect(0, 0, 1, 1);
            else if (playerCount == 2)
                cam.rect = new Rect(i * 0.5f, 0, 0.5f, 1);
            else if (playerCount == 3)
                cam.rect = i == 2 ? new Rect(0, 0, 1, 0.5f) : new Rect(i * 0.5f, 0.5f, 0.5f, 0.5f);
            else if (playerCount == 4)
                cam.rect = new Rect((i % 2) * 0.5f, (i / 2) * 0.5f, 0.5f, 0.5f);

            Debug.Log($"📺 Player {i + 1} camera set up for {playerCount}-way split.");
        }
    }
}
