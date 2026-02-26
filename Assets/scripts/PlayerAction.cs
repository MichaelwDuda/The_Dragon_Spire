using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameStateHandler gameStateHandler;

    private void Start()
    {
        if (player == null) Debug.LogError("Player Action Script is missing player reference.");
    }

    /// <summary>
    /// Input Event for movement
    /// Sets the move vector in the player script
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        player.SetMoveVector(context.ReadValue<Vector2>().normalized);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        player.ToggleSprintSpeed(context.performed);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) gameStateHandler.TogglePauseGame();
    }
}