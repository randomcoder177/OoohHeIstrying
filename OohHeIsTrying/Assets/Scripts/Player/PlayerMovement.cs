using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float defaultPlayerSpeed = 200f;
    public float playerJumpStrength = 500f;
    public float playerSpeedLimit = 100f;
    public float frictionWhenIdle = 2f;
    public float fallingJumpDistance = 0.1f;
    public float jumpFallStrength = 400f;
    public float sprintModifier = 2f;

    public float jumpDelay = 1f;
    private bool canJump;

    private float defaultFriction = 0f;
    private float playerSpeedHorizontal; // pomocná proměnná, při startu nabude stejné hodnoty, jako defaultPlayerSpeed


    private Rigidbody rb;

    // TODO: "Vyhladit" skoky - skok nahoru je moc sekavý, dolů zase moc pomalý 
    // TODO: vyřešit omezení rychlosti při pohybu ve skoku směrem dolů+do strany - capne se rychlost a hráč padá pomaleji   

	// Use this for initialization
	void Start () {
        canJump = true;
        playerSpeedHorizontal = defaultPlayerSpeed;
        rb = GetComponent<Rigidbody>();
	
	}
	
	void FixedUpdate () {
        // Pohyb
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.AddForce(movementVector * playerSpeedHorizontal);
        }

        // Sprint
        switch (Input.GetButton("Sprint"))
        {
            case true:
                playerSpeedHorizontal = defaultPlayerSpeed * sprintModifier;
                break;
            case false:
                playerSpeedHorizontal = defaultPlayerSpeed;
                break;
            default:
                Debug.Log("Něco je brutálně špatně se sprintem (skript PlayerMovement)");
                break;
        }

        // Jumping - pokud je hráč na zemi, tak skáče pomocí Jump buttonu
        if (Input.GetButton("Jump") && canJump == true)
        {
            
            if(IsGrounded() == true)
            {
                rb.AddForce(Vector3.up * playerJumpStrength);
            }
        }

        // Limit player speed - checks velocity magnitude vector and if it's over the speedLimit, limits the speed
        if (rb.velocity.x > playerSpeedLimit)
        {
            Vector3 playerVelocityVector = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            playerVelocityVector.x = playerSpeedLimit;
            //rb.velocity = rb.velocity.normalized * playerSpeedLimit;
            
        }

        // Když není aktivní input, zvýším friction na physics materiálu, aby hráč "neklouzal" po celé ploše při
        // neaktivním inputu.
        if (Input.GetAxis("Horizontal") != 0 && IsGrounded() == true)
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = defaultFriction;
        }
        else
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = frictionWhenIdle;
        }

        //Debug.Log(gameObject.GetComponent<Collider>().material.dynamicFriction);
	}

    private void ResetJump()
    {
        canJump = true;
    }


    // Zjistí, jestli je Player na zemi - vyšle raycast z pozice hráče směrem dolů ve vzdálenosti collider boxu + "rezerva" fallingJumpDistance
    private bool IsGrounded()
    {
        bool onGround = new bool(); 
        onGround = Physics.Raycast(gameObject.transform.position, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + fallingJumpDistance);
        return onGround;
    }
}
