using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool RunHeld { get; private set; }
    public bool isCrouching { get; private set; }

    [SerializeField] private Camera cam;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        RunHeld = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.C))
            isCrouching = !isCrouching;
    }

    public PlayerInputs HandleCharacterInput()
    {
        PlayerInputs characterInputs = new PlayerInputs();

        MoveInput = new Vector2(Input.GetAxisRaw(VerticalInput), Input.GetAxisRaw(HorizontalInput));

        characterInputs.MoveAxisForward = MoveInput.x;  
        characterInputs.MoveAxisRight = MoveInput.y;
        characterInputs.CameraRotation = cam.transform.rotation;
        characterInputs.JumpDown = JumpPressed;
        characterInputs.Crouching = isCrouching;

        return characterInputs;
    }
}