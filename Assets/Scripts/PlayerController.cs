using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float rotationSpeed = 10;

    [SerializeField]
    float forwardSpeed = 10;

    [SerializeField]
    bool logEvents = false;

    InputAction move;
    InputAction look;
    InputAction fire;

    
    public bool rotatingClockwise;
    public bool rotatingAntiClockwise;
    public bool movingForwards;


    // Start is called before the first frame update
    void Start()
    {
        var inputActionsMap = playerInput.actions.FindActionMap("Player");

        //Note in order for this code to work the PlayerInput has to be set to C# Events
        move = inputActionsMap.FindAction("Move");
        move.performed += OnMoveEvent;

        look = inputActionsMap.FindAction("Look");
        look.performed += OnLookEvent;

        fire = inputActionsMap.FindAction("Fire");
        fire.performed += OnFireEvent;

    }

    private void OnMoveEvent(InputAction.CallbackContext obj)
    {
        var input = obj.ReadValue<Vector2>();

        if (input.x > 0)
        {
            rotatingClockwise = true;
            rotatingAntiClockwise = false;
        }

        if (input.x < 0)
        {
            rotatingClockwise = false;
            rotatingAntiClockwise = true;
        }

        if (input.x == 0)
        {
            rotatingClockwise = false;
            rotatingAntiClockwise = false;
        }

        if (input.y > 0)
        {
            movingForwards = true;
        }
        else
        {
            movingForwards = false;
        }

        if (logEvents)
        {
            Debug.Log($"OnMoveEvent: x={input.x}    y={input.y}");
        }
    }

    private void OnLookEvent(InputAction.CallbackContext obj)
    {
        var inputVec = obj.ReadValue<Vector2>();

        if (logEvents)
        {
            Debug.Log($"OnLookEvent: x={inputVec.x}    y={inputVec.y}");
        }
    }


    private void OnFireEvent(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();

        if (logEvents)
        {
            Debug.Log($"OnFireEvent: Fire is {value}");
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        var rotation = new Vector3();
        var translation = new Vector3();

        if (rotatingClockwise)
        {
            rotation.y += rotationSpeed;
        }
        
        if ( rotatingAntiClockwise)
        {
            rotation.y -= rotationSpeed;

        }
            
        player.transform.Rotate(rotation);

        if (movingForwards)
        {
            translation.z = forwardSpeed;
        }

        player.transform.Translate(translation, Space.Self);
    }



}


