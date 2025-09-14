//Purpose: To handle all inputs in a dynamic system that can be updated in future iterations of the game and add new inputs easily.
//Contributor and Author: Logan Baysinger. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputHandler : MonoBehaviour
{

    [SerializeField] private InputActionAsset playerControls;

    [SerializeField] private string actionMapName = "Touch";

    [SerializeField] private string movement = "Movement";
    [SerializeField] private string touch = "Touch";

    private InputAction moveAction;
    private InputAction touchAction;

    public Vector2 MoveInput { get; private set; }
    public bool TouchInput { get; private set; }

    public static InputHandler Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(movement);
        touchAction = playerControls.FindActionMap(actionMapName).FindAction(touch);
        RegisterInputActions();

    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        touchAction.performed += context => TouchInput = true;
        touchAction.canceled += context => TouchInput = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        touchAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        touchAction.Disable();
    }
}
