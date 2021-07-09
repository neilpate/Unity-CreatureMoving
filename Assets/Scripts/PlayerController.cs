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
    float jumpStrength = 10;

    [SerializeField]
    bool logEvents = false;

    InputAction move;
    InputAction look;
    InputAction fire;
    InputAction jump;

    Rigidbody rb;

    public bool rotatingClockwise;
    public bool rotatingAntiClockwise;
    public bool movingForwards;
    public bool firing;
    public bool jumping;



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

        jump = inputActionsMap.FindAction("Jump");
        jump.performed += OnJumpEvent;

        rb = player.GetComponent<Rigidbody>();
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

        if (value > 0)
        {
            firing = true;
        }

        if (logEvents)
        {
            Debug.Log($"OnFireEvent: Fire is {value}");
        }
    }

    private void OnJumpEvent(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();

        jumping = value > 0;

        if (logEvents)
        {
            Debug.Log($"OnJumpEvent: Fire is {value}");
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

        if (jumping)
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0));
            jumping = false;
        }


        if (rotatingClockwise)
        {
            rotation.y += rotationSpeed;
        }

        if (rotatingAntiClockwise)
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


