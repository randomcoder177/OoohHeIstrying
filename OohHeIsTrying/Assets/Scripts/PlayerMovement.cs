using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float playerSpeedHorizontal = 20f;
    public float playerSpeedVertical = 10f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector3 movementVector = new Vector3(horizontalAxis, verticalAxis, 0);
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            rb.AddRelativeForce(movementVector);
        }
	
	}
}
