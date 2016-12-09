using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float playerJumpStrength = 1800f;
    public float playerRunningJumpForce = 200f;
    public float playerSpeedHorizontal = 5000f;
    public float frictionWhenStanding = 3f;

    private float defaultFriction = 0f;


    // Možné stavy, ve kterých se může hráč nacházet
    enum PlayerState
    {
        state_STAND,
        state_JUMP,
        state_WALK
    }
    private PlayerState state_;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        // State machine
        switch (state_)
        {
            // Pokud hráč stojí a zmáčkne tlačítko Jump, skočí. Ve stavu "state_JUMP" následně každý frame zjišťuje, jestli je už na zemi,
            // pokud ano, vrátí do původního stavu "state_STAND"
            case PlayerState.state_STAND:
                // šipka 1 - pokud stojím a zmáčknu "Jump", skočím na místě
                if (Input.GetButton("Jump"))
                {
                    PlayerJump();
                }
                // pokud hráč stoji a spadne třeba z římsy, chci aby byl jakoby ve skoku
                else if (CheckGround() == false)
                {
                    state_ = PlayerState.state_JUMP;
                }
                // šipka 4 - pokud je aktivní input, rozběhně hráče
                else if (CheckHorizontalInput() == true)
                {
                    PlayerWalk();
                    state_ = PlayerState.state_WALK;
                }
                
                break;

            case PlayerState.state_JUMP:
                // šipka 2 - zjistím, jestli jsem na zemi, pokud ano -> hodí to hráče do state_STAND
                if (CheckGround() == true)
                {
                    state_ = PlayerState.state_STAND;
                }
                // šipka 5 - pokud je ve vzduchu a je aktivní Input, pohne se do strany při skoku
                else if (CheckHorizontalInput() == true)
                {
                    PlayerWalk();
                    state_ = PlayerState.state_WALK;
                }
                break;

            case PlayerState.state_WALK:
                PlayerWalk();
                // šipka 6 - pokud hráč běží a skočí, použiju jinou funkci než pro normální jump kvůli přídané
                // "friction" - byl problém s různými sílami skoku, jestli stojím nebo běžím
                if (Input.GetButton("Jump"))
                {
                    PlayerRunningJump();
                }
                break;
                // šipka 3 - pokud nemačkám nic, stojím na místě. Teoreticky by šlo taky pořešit přídaním CheckHorizontalInput
                // do case pro walk, takto je to snažší
            default:
                state_ = PlayerState.state_STAND;
                break;
        }
        Debug.Log(state_);

        // Pokud hráč stojí, zvýší odpor materiálu, aby plynule zastavoval atp.
        if (state_ == PlayerState.state_STAND)
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = frictionWhenStanding;
        }
        else
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = defaultFriction;
        }
    }

    private bool CheckGround()
    {
        bool onGround = Physics.Raycast(gameObject.transform.position, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + 0.01f);
        return onGround;
    }
    private bool CheckHorizontalInput()
    {
        bool horizontalInputActive;
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalInputActive = true;
        }
        else
        {
            horizontalInputActive = false;
        }
        return horizontalInputActive;
    }

    private void PlayerJump()
    {
        rb.AddForce(Vector3.up * playerJumpStrength);
        state_ = PlayerState.state_JUMP;
    }

    private void PlayerRunningJump()
    {
        rb.AddForce(Vector3.up *playerRunningJumpForce);
        state_ = PlayerState.state_JUMP;
    }

    private void PlayerWalk()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.AddForce(movementVector * playerSpeedHorizontal);
        }
        else
        {
            state_ = PlayerState.state_STAND;
        }
    }
    
}
