using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float moveSpeed = 3f;

    private InputAction moveAction;

    private const string PLAYER_ACTION_MAP = "Player";
    private const string PLAYER_INPUT_ACTION = "Move";

    private bool isGrounded = false;

    void Awake()
    {
        moveAction = actions.FindActionMap(PLAYER_ACTION_MAP).FindAction(PLAYER_INPUT_ACTION);
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

         if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ) {
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
         }
         else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            rigidbody.position += Vector3.back * Time.deltaTime * moveSpeed;
         }
         else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody.position += Vector3.left * Time.deltaTime * moveSpeed;
         }
         else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rigidbody.position += Vector3.right * Time.deltaTime * moveSpeed;
         }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}