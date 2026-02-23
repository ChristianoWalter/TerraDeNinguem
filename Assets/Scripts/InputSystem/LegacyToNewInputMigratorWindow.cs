#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LegacyToNewInputMigratorWindow : EditorWindow
{
    private const string DefaultOutputFolder = "Assets/Input/Generated";
    private const string DefaultAssetName = "ProjectInputActions";

    [Serializable]
    public class LegacyAxis
    {
        public string name;
        public string positiveButton;
        public string altPositiveButton;
        public string negativeButton;
        public string altNegativeButton;
        public int type;   // 0 Key/Mouse Button, 1 Mouse Movement, 2 Joystick Axis
        public int axis;
        public int joyNum; // 0 = all
    }

    private Vector2 _scroll;
    private readonly List<LegacyAxis> _axes = new();

    private string _outputFolder = DefaultOutputFolder;
    private string _assetName = DefaultAssetName;
    private bool _addDebugMap = true;

    [MenuItem("Tools/Input/Legacy -> New Input (V1)")]
    public static void ShowWindow()
    {
        GetWindow<LegacyToNewInputMigratorWindow>("Legacy -> New Input (V1)");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Legacy -> New Input System (V1)", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "V1 generates a .inputactions (JSON) asset and sets up scene UI module + PlayerInput.\n" +
            "It does NOT refactor gameplay scripts automatically (that is a later step).",
            MessageType.Info);

        EditorGUILayout.Space(6);
        _outputFolder = EditorGUILayout.TextField("Output Folder", _outputFolder);
        _assetName = EditorGUILayout.TextField("Asset Name", _assetName);
        _addDebugMap = EditorGUILayout.Toggle("Generate Debug Map", _addDebugMap);

        EditorGUILayout.Space(8);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("1) Read Legacy Axes", GUILayout.Height(28)))
                ReadLegacyAxes();

            if (GUILayout.Button("2) Generate Input Actions", GUILayout.Height(28)))
            {
                if (_axes.Count == 0)
                    ReadLegacyAxes();

                GenerateInputActions();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("3) Setup Scene (EventSystem + InputRoot)", GUILayout.Height(28)))
                SetupScene();

            if (GUILayout.Button("Run All (1→3)", GUILayout.Height(28)))
            {
                ReadLegacyAxes();
                GenerateInputActions();
                SetupScene();
            }
        }

        EditorGUILayout.Space(10);
        DrawAxesList();
    }

    private void DrawAxesList()
    {
        EditorGUILayout.LabelField($"Legacy axes found: {_axes.Count}", EditorStyles.boldLabel);

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        foreach (var a in _axes)
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField(a.name, EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Type: {a.type} | Axis: {a.axis} | JoyNum: {a.joyNum}");
                if (!string.IsNullOrEmpty(a.positiveButton) || !string.IsNullOrEmpty(a.altPositiveButton))
                    EditorGUILayout.LabelField($"Positive: {a.positiveButton} | Alt: {a.altPositiveButton}");
                if (!string.IsNullOrEmpty(a.negativeButton) || !string.IsNullOrEmpty(a.altNegativeButton))
                    EditorGUILayout.LabelField($"Negative: {a.negativeButton} | Alt: {a.altNegativeButton}");
            }
        }
        EditorGUILayout.EndScrollView();
    }

    private void ReadLegacyAxes()
    {
        _axes.Clear();

        UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset");
        if (objs == null || objs.Length == 0)
        {
            Debug.LogError("[LegacyToNewInput] Could not load ProjectSettings/InputManager.asset");
            return;
        }

        var inputManager = objs[0];
        var so = new SerializedObject(inputManager);

        var axesProp = so.FindProperty("m_Axes");
        if (axesProp == null || !axesProp.isArray)
        {
            Debug.LogError("[LegacyToNewInput] m_Axes not found or not an array in InputManager.asset");
            return;
        }

        for (int i = 0; i < axesProp.arraySize; i++)
        {
            var axis = axesProp.GetArrayElementAtIndex(i);

            var a = new LegacyAxis
            {
                name = axis.FindPropertyRelative("m_Name")?.stringValue ?? $"Axis_{i}",
                positiveButton = axis.FindPropertyRelative("positiveButton")?.stringValue ?? "",
                altPositiveButton = axis.FindPropertyRelative("altPositiveButton")?.stringValue ?? "",
                negativeButton = axis.FindPropertyRelative("negativeButton")?.stringValue ?? "",
                altNegativeButton = axis.FindPropertyRelative("altNegativeButton")?.stringValue ?? "",
                type = axis.FindPropertyRelative("type")?.intValue ?? 0,
                axis = axis.FindPropertyRelative("axis")?.intValue ?? 0,
                joyNum = axis.FindPropertyRelative("joyNum")?.intValue ?? 0
            };

            _axes.Add(a);
        }

        Debug.Log($"[LegacyToNewInput] Read {_axes.Count} axes from InputManager.asset");
    }

    private void GenerateInputActions()
    {
        EnsureFolder(_outputFolder);

        // Create a new asset in memory
        var asset = ScriptableObject.CreateInstance<InputActionAsset>();

        // Maps
        var gameplay = asset.AddActionMap("Gameplay");
        var ui = asset.AddActionMap("UI");
        InputActionMap debug = null;
        if (_addDebugMap)
            debug = asset.AddActionMap("Debug");

        // Gameplay actions (compatible)
        var move = gameplay.AddAction("Move", InputActionType.Value);
        move.expectedControlType = "Vector2";

        var look = gameplay.AddAction("Look", InputActionType.Value);
        look.expectedControlType = "Vector2";

        var jump = gameplay.AddAction("Jump", InputActionType.Button);
        jump.expectedControlType = "Button";

        var primary = gameplay.AddAction("PrimaryAction", InputActionType.Button);
        primary.expectedControlType = "Button";

        var secondary = gameplay.AddAction("SecondaryAction", InputActionType.Button);
        secondary.expectedControlType = "Button";

        var tertiary = gameplay.AddAction("TertiaryAction", InputActionType.Button);
        tertiary.expectedControlType = "Button";

        var pause = gameplay.AddAction("Pause", InputActionType.Button);
        pause.expectedControlType = "Button";

        // UI actions
        var navigate = ui.AddAction("Navigate", InputActionType.Value);
        navigate.expectedControlType = "Vector2";

        var submit = ui.AddAction("Submit", InputActionType.Button);
        submit.expectedControlType = "Button";

        var cancel = ui.AddAction("Cancel", InputActionType.Button);
        cancel.expectedControlType = "Button";

        var point = ui.AddAction("Point", InputActionType.PassThrough);
        point.expectedControlType = "Vector2";

        var click = ui.AddAction("Click", InputActionType.PassThrough);
        click.expectedControlType = "Button";

        var scroll = ui.AddAction("ScrollWheel", InputActionType.PassThrough);
        scroll.expectedControlType = "Vector2";

        // Bindings (safe defaults)
        AddMoveBindings(move);
        AddLookBindings(look);
        AddButtonBindings(jump, "<Keyboard>/space", "<Gamepad>/buttonSouth");
        AddButtonBindings(primary, "<Mouse>/leftButton", "<Gamepad>/rightTrigger");
        AddButtonBindings(secondary, "<Mouse>/rightButton", "<Gamepad>/leftTrigger");
        AddButtonBindings(tertiary, "<Keyboard>/leftShift", "<Gamepad>/buttonWest");
        AddButtonBindings(pause, "<Keyboard>/escape", "<Gamepad>/start");

        AddUINavigateBindings(navigate);
        AddButtonBindings(submit, "<Keyboard>/enter", "<Gamepad>/buttonSouth");
        AddButtonBindings(cancel, "<Keyboard>/escape", "<Gamepad>/buttonEast");
        point.AddBinding("<Mouse>/position");
        click.AddBinding("<Mouse>/leftButton");
        scroll.AddBinding("<Mouse>/scroll");

        if (debug != null)
            TryAddDebugActions(debug);

        // IMPORTANT: .inputactions must be JSON on disk.
        string assetPath = Path.Combine(_outputFolder, $"{_assetName}.inputactions").Replace("\\", "/");
        string fullPath = Path.GetFullPath(assetPath);

        try
        {
            // Overwrite file as JSON
            var json = asset.ToJson();
            File.WriteAllText(fullPath, json);

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);

            var imported = AssetDatabase.LoadAssetAtPath<InputActionAsset>(assetPath);
            if (imported == null)
            {
                Debug.LogError($"[LegacyToNewInput] Failed to import InputActionAsset at: {assetPath}");
                return;
            }

            Debug.Log($"[LegacyToNewInput] Generated Input Actions at: {assetPath}");
            Selection.activeObject = imported;
        }
        catch (Exception e)
        {
            Debug.LogError($"[LegacyToNewInput] Failed to write/import .inputactions JSON. Error: {e}");
        }
    }

    private void SetupScene()
    {
        // Find or create EventSystem
        var eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            var go = new GameObject("EventSystem");
            eventSystem = go.AddComponent<EventSystem>();
            Undo.RegisterCreatedObjectUndo(go, "Create EventSystem");
        }

        // Remove StandaloneInputModule if present
        var standalone = eventSystem.GetComponent<StandaloneInputModule>();
        if (standalone != null)
            Undo.DestroyObjectImmediate(standalone);

        // Ensure InputSystemUIInputModule exists
        var uiModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (uiModule == null)
            uiModule = Undo.AddComponent<InputSystemUIInputModule>(eventSystem.gameObject);

        // Create/ensure InputRoot with PlayerInput + GameInputRuntime
        var inputRoot = GameObject.Find("InputRoot");
        if (inputRoot == null)
        {
            inputRoot = new GameObject("InputRoot");
            Undo.RegisterCreatedObjectUndo(inputRoot, "Create InputRoot");
        }

        var playerInput = inputRoot.GetComponent<PlayerInput>();
        if (playerInput == null)
            playerInput = Undo.AddComponent<PlayerInput>(inputRoot);

        var runtime = inputRoot.GetComponent<GameInputRuntime>();
        if (runtime == null)
            runtime = Undo.AddComponent<GameInputRuntime>(inputRoot);

        // Load generated asset
        string assetPath = Path.Combine(_outputFolder, $"{_assetName}.inputactions").Replace("\\", "/");
        var generatedAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(assetPath);

        if (generatedAsset == null)
        {
            Debug.LogWarning("[LegacyToNewInput] Generated actions asset not found or failed to import. Run 'Generate Input Actions' first.");
            return;
        }

        playerInput.actions = generatedAsset;
        runtime.Actions = generatedAsset;

        // Hook UI module actions
        uiModule.actionsAsset = generatedAsset;

        // NOTE: If your version doesn't have InputActionReference.Create, tell me and I'll give the fallback.
        var navAction = generatedAsset.FindAction("UI/Navigate", false);
        var submitAction = generatedAsset.FindAction("UI/Submit", false);
        var cancelAction = generatedAsset.FindAction("UI/Cancel", false);
        var pointAction = generatedAsset.FindAction("UI/Point", false);
        var clickAction = generatedAsset.FindAction("UI/Click", false);
        var scrollAction = generatedAsset.FindAction("UI/ScrollWheel", false);

        if (navAction != null) uiModule.move = InputActionReference.Create(navAction);
        if (submitAction != null) uiModule.submit = InputActionReference.Create(submitAction);
        if (cancelAction != null) uiModule.cancel = InputActionReference.Create(cancelAction);
        if (pointAction != null) uiModule.point = InputActionReference.Create(pointAction);
        if (clickAction != null) uiModule.leftClick = InputActionReference.Create(clickAction);
        if (scrollAction != null) uiModule.scrollWheel = InputActionReference.Create(scrollAction);

        Debug.Log("[LegacyToNewInput] Scene setup completed.");
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    // ---------- Bindings helpers ----------

    private static void AddMoveBindings(InputAction move)
    {
        var wasd = move.AddCompositeBinding("2DVector");
        wasd.With("Up", "<Keyboard>/w");
        wasd.With("Down", "<Keyboard>/s");
        wasd.With("Left", "<Keyboard>/a");
        wasd.With("Right", "<Keyboard>/d");

        var arrows = move.AddCompositeBinding("2DVector");
        arrows.With("Up", "<Keyboard>/upArrow");
        arrows.With("Down", "<Keyboard>/downArrow");
        arrows.With("Left", "<Keyboard>/leftArrow");
        arrows.With("Right", "<Keyboard>/rightArrow");

        move.AddBinding("<Gamepad>/leftStick");
        move.AddBinding("<Gamepad>/dpad");
    }

    private static void AddLookBindings(InputAction look)
    {
        look.AddBinding("<Mouse>/delta");
        look.AddBinding("<Gamepad>/rightStick");
    }

    private static void AddUINavigateBindings(InputAction navigate)
    {
        navigate.AddBinding("<Gamepad>/leftStick");
        navigate.AddBinding("<Gamepad>/dpad");

        var arrows = navigate.AddCompositeBinding("2DVector");
        arrows.With("Up", "<Keyboard>/upArrow");
        arrows.With("Down", "<Keyboard>/downArrow");
        arrows.With("Left", "<Keyboard>/leftArrow");
        arrows.With("Right", "<Keyboard>/rightArrow");

        var wasd = navigate.AddCompositeBinding("2DVector");
        wasd.With("Up", "<Keyboard>/w");
        wasd.With("Down", "<Keyboard>/s");
        wasd.With("Left", "<Keyboard>/a");
        wasd.With("Right", "<Keyboard>/d");
    }

    private static void AddButtonBindings(InputAction action, string keyboardBinding, string gamepadBinding)
    {
        if (!string.IsNullOrEmpty(keyboardBinding))
            action.AddBinding(keyboardBinding);
        if (!string.IsNullOrEmpty(gamepadBinding))
            action.AddBinding(gamepadBinding);
    }

    private void TryAddDebugActions(InputActionMap debug)
    {
        var legacyNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var a in _axes)
            legacyNames.Add(a.name);

        void AddIfExists(string legacy, string actionName, string defaultBinding)
        {
            if (!legacyNames.Contains(legacy))
                return;

            var act = debug.AddAction(actionName, InputActionType.Button);
            act.expectedControlType = "Button";
            if (!string.IsNullOrEmpty(defaultBinding))
                act.AddBinding(defaultBinding);
        }

        AddIfExists("Debug Next", "DebugNext", "<Keyboard>/pageDown");
        AddIfExists("Debug Previous", "DebugPrevious", "<Keyboard>/pageUp");
        AddIfExists("Debug Validate", "DebugValidate", "<Keyboard>/enter");
        AddIfExists("Debug Reset", "DebugReset", "<Keyboard>/backspace");
        AddIfExists("Debug Persistent", "DebugPersistent", "<Keyboard>/p");
        AddIfExists("Debug Multiplier", "DebugMultiplier", "<Keyboard>/leftShift");
        AddIfExists("Enable Debug Button 1", "EnableDebug1", "<Keyboard>/f1");
        AddIfExists("Enable Debug Button 2", "EnableDebug2", "<Keyboard>/f2");
    }

    private static void EnsureFolder(string folderPath)
    {
        folderPath = folderPath.Replace("\\", "/");
        if (AssetDatabase.IsValidFolder(folderPath))
            return;

        var parts = folderPath.Split('/');
        if (parts.Length < 2 || parts[0] != "Assets")
            throw new Exception("Output folder must be inside Assets/");

        string current = "Assets";
        for (int i = 1; i < parts.Length; i++)
        {
            string next = $"{current}/{parts[i]}";
            if (!AssetDatabase.IsValidFolder(next))
                AssetDatabase.CreateFolder(current, parts[i]);
            current = next;
        }
    }
}
#endif
