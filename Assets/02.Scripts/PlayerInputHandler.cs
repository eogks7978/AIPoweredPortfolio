using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool RunHeld { get; private set; }

    private void Update()
    {
        MoveInput = new Vector2(
            Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0,
            Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0
        );
        JumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        AttackPressed = Mouse.current.leftButton.wasPressedThisFrame;
        RunHeld = Keyboard.current.leftShiftKey.isPressed;
    }
}
