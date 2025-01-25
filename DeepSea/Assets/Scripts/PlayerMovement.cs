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

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.GetChild(0).gameObject;
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
