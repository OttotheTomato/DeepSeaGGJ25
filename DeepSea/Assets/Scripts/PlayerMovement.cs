using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameObject Head;
    InputAction MoveAction;
    InputAction TurnAction;
    InputAction Escape;

    [SerializeField]
    private float MoveSpeed = 10f;
    [SerializeField]
    private float smoothInputSpeed = .2f;
    [SerializeField]
    private float MouseSensitivity = 0.2f;

    private PlayerInput InputComponent;

    private CharacterController CharController;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    private float MouseYRotation;

    private bool Focused = false;

    Vector3 StartPos;

    Vector3 TargetPos;

    [SerializeField]
    float BobAmplitude = 10;
    [SerializeField]
    float BobPeriod = 5f;
    [SerializeField]
    float ReturnSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.GetChild(0).gameObject;
        StartPos = Head.transform.localPosition;
        TargetPos = StartPos;
        InputComponent = GetComponent<PlayerInput>();
        CharController = GetComponent<CharacterController>();
        MoveAction = InputComponent.actions["Walk"];
        TurnAction = InputComponent.actions["Look"];
        Escape = InputComponent.actions["Escape"];
        Focus();
    }

    // Update is called once per frame
    void Update()
    {

        //walking stuff
        Vector2 moveValue = MoveAction.ReadValue<Vector2>();
        if (moveValue.magnitude > 0)
        {
            float theta = Time.timeSinceLevelLoad / BobPeriod;
            float distance = BobAmplitude * Mathf.Sin(theta);

            TargetPos = StartPos + Vector3.up * distance;


        }
        else
        {
            if (Head.transform.localPosition != StartPos)
            {
                TargetPos = new Vector3(StartPos.x, Mathf.Lerp(Head.transform.localPosition.y, StartPos.y, ReturnSpeed * Time.deltaTime), StartPos.z);
            }
        }

        Head.transform.localPosition = Vector3.Lerp(Head.transform.localPosition, TargetPos, ReturnSpeed * Time.deltaTime);

        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveValue, ref smoothInputVelocity, smoothInputSpeed);
        CharController.Move(transform.TransformDirection(currentInputVector.x, 0, currentInputVector.y) * MoveSpeed * Time.deltaTime);


        if (Focused)
        {
            //turning stuff
            Vector2 turnValue = TurnAction.ReadValue<Vector2>();
            float mouseXRoation = turnValue.x * MouseSensitivity;
            transform.Rotate(0, mouseXRoation, 0);

            MouseYRotation -= turnValue.y * MouseSensitivity;
            MouseYRotation = Mathf.Clamp(MouseYRotation, -90, 90);
            Head.transform.localRotation = Quaternion.Euler(MouseYRotation, 0, 0);
        }
        Debug.Log(Focused);

        if (Escape.WasPressedThisFrame())
        {
            Focus();
        }
    }

    void Focus()
    {
        if (!Focused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Focused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Focused = false;
        }
    }
}
