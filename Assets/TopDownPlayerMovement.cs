using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;



public class TopDownPlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private string playerType;
    private SpriteRenderer playerSprite;
    //Variables important to the player moving.
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    private float originalSpeed;
    private Rigidbody2D rb;

    [Header("Shooting Settings")]
    public float bulletSpeed;
    public float shootTimer;
    private bool canFire;
    public float timeBetweenFiring;
    public GameObject bullet;
    public Transform bulletTransform;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = rb.GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.currentControlScheme == "Gamepad")
        {
            playerSprite.color = Color.blue;
            gameObject.tag = "Gamepad Player";
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            playerSprite.color = Color.red;
            gameObject.tag = "Player";
        }
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Vector2 mouseWorldPosition =
                Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction =
                mouseWorldPosition - (Vector2)transform.position;

            if (direction.sqrMagnitude > 0.001f)
            {
                float angle =
                    Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        if (!canFire)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > timeBetweenFiring)
            {
                canFire = true;
                shootTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    private float horizontal;
    private float vertical;

    //When plugged into the "Move" Section of the PlayerInput component, allows the player to move.
    public void MovePlayer(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void Aim(InputAction.CallbackContext context) 
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        //If the player is using M&K.
        //if (context.control.device is Pointer)
        //{
        //    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(lookInput);

        //    Vector3 rotation = mouseWorldPosition - transform.position;

        //   if (rotation.sqrMagnitude < 0.001f)
        //     return;

        //    float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;
            
        //    transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //    Debug.Log($"Look: {lookInput} | Device: {context.control.device}");
        //}

        // If the player is using a controller.
        if (context.control.device is Gamepad)
        {
            if (lookInput.sqrMagnitude < 0.03f)
                return;

            float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (canFire)
        {
            canFire = false;
            GameObject bulletObj = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            bulletObj.transform.up = transform.up.normalized;

            if (playerInput.currentControlScheme == "Gamepad")
            {
                bulletObj.tag = "P1";
            }
            else if (playerInput.currentControlScheme == "Keyboard&Mouse")
            {
                bulletObj.tag = "P2";
            }
        }
    }
 }
