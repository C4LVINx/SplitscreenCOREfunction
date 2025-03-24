using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerIndex;

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"🎮 Player {playerIndex} changed route.");
            LobbyManager.Instance.OnRouteSelection(context);
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"🆗 Player {playerIndex} pressed Start.");
            LobbyManager.Instance.OnStartGame(context);
        }
    }
}