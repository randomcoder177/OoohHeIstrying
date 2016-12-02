using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float playerSpeedHorizontal = 200f;
    public float playerJumpStrength = 500f;
    public float playerSpeedLimit = 100f;

    public float jumpDelay = 1f;
    private bool canJump;


    private Rigidbody rb;

    // TODO: snížit rychlost pohybu do strany při skoku
    

	// Use this for initialization
	void Start () {
        canJump = true;
        rb = GetComponent<Rigidbody>();
	
	}
	
	void FixedUpdate () {
        // Movement
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.AddRelativeForce(movementVector * playerSpeedHorizontal);
        }

        // Jumping
        if (Input.GetButton("Jump") && canJump == true)
        {
            rb.AddForce(Vector3.up * playerJumpStrength);
            canJump = false;
            Invoke("ResetJump", jumpDelay);
        }
        // Limit player speed - checks velocity magnitude vector and if it's over the speedLimit,
        // increases drag
        if (rb.velocity.magnitude > playerSpeedLimit)
        {
            rb.velocity = rb.velocity.normalized * playerSpeedLimit;
        }
	
	}

    private void ResetJump()
    {
        canJump = true;
    }
}
