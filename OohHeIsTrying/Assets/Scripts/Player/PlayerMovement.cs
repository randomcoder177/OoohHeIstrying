using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float playerSpeedHorizontal = 200f;
    public float playerJumpStrength = 500f;
    public float playerSpeedLimit = 100f;
    public float frictionWhenIdle = 2f; 

    public float jumpDelay = 1f;
    private bool canJump;

    private float defaultFriction = 0f;


    private Rigidbody rb;

    

	// Use this for initialization
	void Start () {
        canJump = true;
        rb = GetComponent<Rigidbody>();
	
	}
	
	void FixedUpdate () {
        // Pohyb
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.AddForce(movementVector * playerSpeedHorizontal);
        }

        // Jumping
        if (Input.GetButton("Jump") && canJump == true)
        {
            rb.AddForce(Vector3.up * playerJumpStrength);
            canJump = false;
            Invoke("ResetJump", jumpDelay);
        }

        // Limit player speed - checks velocity magnitude vector and if it's over the speedLimit, limits the speed
        if (rb.velocity.magnitude > playerSpeedLimit)
        {
            rb.velocity = rb.velocity.normalized * playerSpeedLimit;
        }

        // Když není aktivní input, zvýším friction na physics materiálu, aby hráč "neklouzal" po celé ploše při
        // neaktivním inputu.
        if (Input.GetAxis("Horizontal") != 0)
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = defaultFriction;
        }
        else
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = frictionWhenIdle;
        }

        Debug.Log(gameObject.GetComponent<Collider>().material.dynamicFriction);
	}

    private void ResetJump()
    {
        canJump = true;
    }
}
