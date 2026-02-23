using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputRuntime : MonoBehaviour
{
    public static GameInputRuntime Instance { get; private set; }

    [Header("Input Actions")]
    public InputActionAsset Actions;

    [Header("Cached Values")]
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public bool JumpPressedThisFrame { get; private set; }
    public bool PrimaryPressedThisFrame { get; private set; }
    public bool SecondaryPressedThisFrame { get; private set; }
    public bool TertiaryPressedThisFrame { get; private set; }
    public bool PausePressedThisFrame { get; private set; }

    public bool SubmitPressedThisFrame { get; private set; }
    public bool CancelPressedThisFrame { get; private set; }

    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _primary;
    private InputAction _secondary;
    private InputAction _tertiary;
    private InputAction _pause;

    private InputAction _submit;
    private InputAction _cancel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        BindActions();
        if (Actions != null)
            Actions.Enable();
    }

    private void OnDisable()
    {
        if (Actions != null)
            Actions.Disable();
    }

    private void BindActions()
    {
        if (Actions == null)
            return;

        _move = Actions.FindAction("Gameplay/Move", throwIfNotFound: false);
        _look = Actions.FindAction("Gameplay/Look", throwIfNotFound: false);

        _jump = Actions.FindAction("Gameplay/Jump", throwIfNotFound: false);
        _primary = Actions.FindAction("Gameplay/PrimaryAction", throwIfNotFound: false);
        _secondary = Actions.FindAction("Gameplay/SecondaryAction", throwIfNotFound: false);
        _tertiary = Actions.FindAction("Gameplay/TertiaryAction", throwIfNotFound: false);
        _pause = Actions.FindAction("Gameplay/Pause", throwIfNotFound: false);

        _submit = Actions.FindAction("UI/Submit", throwIfNotFound: false);
        _cancel = Actions.FindAction("UI/Cancel", throwIfNotFound: false);
    }

    private void Update()
    {
        // Cache values in Update to preserve button edges.
        Move = _move != null ? _move.ReadValue<Vector2>() : default;
        Look = _look != null ? _look.ReadValue<Vector2>() : default;

        JumpPressedThisFrame = _jump != null && _jump.triggered;
        PrimaryPressedThisFrame = _primary != null && _primary.triggered;
        SecondaryPressedThisFrame = _secondary != null && _secondary.triggered;
        TertiaryPressedThisFrame = _tertiary != null && _tertiary.triggered;
        PausePressedThisFrame = _pause != null && _pause.triggered;

        SubmitPressedThisFrame = _submit != null && _submit.triggered;
        CancelPressedThisFrame = _cancel != null && _cancel.triggered;
    }
}
