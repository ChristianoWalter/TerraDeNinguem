using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public InputActionAsset inputActions;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject disconnectedPanel;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    InputAction s_attackAction;
    InputAction tertiaryAction;
    InputAction pauseAction;

    Gamepad pad;
    Coroutine stopRumbleAfterTime;

    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public bool tryToJump = false;
    [HideInInspector] public bool tryToAttack = false;
    [HideInInspector] public bool tryToSpecialAttack = false;
    [HideInInspector] public bool tryTertiaryAction = false;
    [HideInInspector] public bool pauseWasPressed = false;

    private void OnEnable()
    {
        inputActions.FindActionMap("Gameplay").Enable();
        //playerInput.onControlsChanged += SwitchControls;
        //InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Gameplay").Disable();
        //playerInput.onControlsChanged -= SwitchControls;
        //InputSystem.onDeviceChange -= OnDeviceChange;
    }


    private void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("PrimaryAction");
        s_attackAction = InputSystem.actions.FindAction("SecondaryAction");
        tertiaryAction = InputSystem.actions.FindAction("TertiaryAction");
    }

    private void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
        tryToJump = jumpAction.WasPressedThisFrame();
        tryTertiaryAction = tertiaryAction.WasPressedThisFrame();
        tryToAttack = attackAction.WasPressedThisFrame();
        tryToSpecialAttack = s_attackAction.WasPressedThisFrame();
        pauseWasPressed = pauseAction.WasPressedThisFrame();
    }

    public bool Pause()
    {
        return pauseAction.WasPressedThisFrame();
    }

    #region Device changes
    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        

        switch (change)
        {
            case InputDeviceChange.Added:
                Debug.Log($"Dispositivo conectado: {device.displayName}");
                break;

            case InputDeviceChange.Removed:
                Debug.Log($"Dispositivo removido: {device.displayName}");
                break;

            case InputDeviceChange.Disconnected:
                Debug.Log($"Dispositivo desconectado: {device.displayName}");
                if (Keyboard.current == null && device is not Keyboard)
                {
                    disconnectedPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
                break;

            case InputDeviceChange.Reconnected:
                Debug.Log($"Dispositivo reconectado: {device.displayName}");
                if (Keyboard.current == null && device is not Keyboard)
                {
                    disconnectedPanel.SetActive(false);
                    Time.timeScale = 1f;
                }
                break;
        }
    }

    void SwitchControls(PlayerInput input)
    {
        Debug.Log("Control Scheme are now: " + input.currentControlScheme);
    }
    #endregion

    //Make gamepad rumble
    #region RumbleFeedback
    public void RumblePad(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            stopRumbleAfterTime = StartCoroutine(StopRumble(duration, pad));
        }
    }

    IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
    }
    #endregion
}
