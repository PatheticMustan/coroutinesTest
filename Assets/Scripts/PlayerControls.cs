using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    private CharacterController controller;

    [SerializeField] private Vector3 playerVelocity;
    public float playerSpeed = 20;
    public float jumpHeight = 2.5f;

    private float gravityValue = -9.81f;

    public bool queuedJump;
    private Camera cam;

    private float xSpeed, zSpeed;


    private void Start() {
        controller = GetComponent<CharacterController>();
        queuedJump = false;
        cam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // if the player is on the ground, zero out their y velocity
        if (controller.isGrounded) {
            playerVelocity.y = Mathf.Max(0f, playerVelocity.y);
        }

        // player input/sprinting
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.z *= (Input.GetKey("left ctrl") && input.z > 0) ? 2 : 1;

        // handle horizontal rotation
        Quaternion hr = cam.transform.rotation;
        hr.eulerAngles = new Vector3(0, hr.eulerAngles.y, 0);

        Vector3 addedVelocity = input * playerSpeed;
        playerVelocity.x = Mathf.Lerp(playerVelocity.x, addedVelocity.x, Time.deltaTime*20);
        playerVelocity.z = Mathf.Lerp(playerVelocity.z, addedVelocity.z, Time.deltaTime*40);


        // restarts
        if (Input.GetKeyDown(KeyCode.R)) {
            GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(0, 1, 0);
            GetComponent<CharacterController>().enabled = true;
            playerVelocity = new Vector3(0, 0, 0);
        }



        // add bunnyhopping
        if (Input.GetButtonDown("Jump")) queuedJump = true;
        if (Input.GetButtonUp("Jump")) queuedJump = false;
        if (controller.isGrounded && queuedJump) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
            queuedJump = false;
        }
        // handle gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // use the resulting vector as the player's velocity, multiplied by horizontal rotation
        controller.Move(hr * playerVelocity * Time.deltaTime);
    }
}