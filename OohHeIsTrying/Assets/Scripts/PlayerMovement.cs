using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float playerSpeedHorizontal = 200f;
    public float playerJumpStrength = 500f;

    public float jumpDelay = 1f;
    private bool canJump;


    private Rigidbody rb;
    

	// Use this for initialization
	void Start () {
        canJump = true;
        rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Movement script
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 movementVector = new Vector3(horizontalAxis, 0, 0);
        if (horizontalAxis != 0)
        {
            rb.AddForce(movementVector);
        }

        if (Input.GetButton("Jump") && canJump == true)
        {
            rb.AddForce(Vector3.up * playerJumpStrength);
            canJump = false;
            Invoke("ResetJump", jumpDelay);
        }
	
	}

    private void ResetJump()
    {
        canJump = true;
    }
}
