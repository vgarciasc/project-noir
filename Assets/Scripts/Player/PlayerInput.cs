using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    PlayerGeneral player;

    public delegate void VoidDelegate(); 
    public event VoidDelegate press_event;
    // public event VoidDelegate next_cutscene_line_event;
    public event VoidDelegate objection_event;

	Rigidbody2D rb;
	Animator animator;

	float lastX, crtX;
	bool crouch = false;
    bool holdingWall = false;
    bool canMove = true;
    ArenaManager ar_manager;

    float standardGravityScale;

    [SerializeField]
    GameObject jumpParticleSystemPrefab;

    float jumpsLeft = 2;

    void Start() {
        player = GetComponent<PlayerGeneral>();
		rb = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
        ar_manager = ArenaManager.getArenaManager();

        standardGravityScale = rb.gravityScale;
    }

    void Update () {
        jump();
        if (canMove) {
            move();
        }
        handleSideGrab();
        handleInput();

        if (isGrounded() || holdingWall) {
            jumpsLeft = 1;
        }
	}

    IEnumerator moveCooldown(int frames) {
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        canMove = false;
        yield return HushPuppy.WaitUntilNFrames(frames);
        canMove = true;
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    void jump() {
        if (jumpsLeft <= 0) {
            return;
        }

        //cant jump if crouching. adding force so changing things at rigidbody inspector
        if (Input.GetKeyDown(KeyCode.Space) && !crouch) {
            if (holdingWall) {
                if (Input.GetAxisRaw("Horizontal") == -1f) {
            	    rb.AddForce(new Vector2(1, 1) * 250, ForceMode2D.Force);
                }
                else if (Input.GetAxisRaw("Horizontal") == 1f) {
            	    rb.AddForce(new Vector2(-1, 1) * 250, ForceMode2D.Force);
                }
                minusJump();
                StartCoroutine(moveCooldown(20));
            }
            else {
            	rb.AddForce(Vector2.up * 250, ForceMode2D.Force);
                GameObject go = Instantiate(jumpParticleSystemPrefab);
                go.transform.position = new Vector2(this.transform.position.x, 
                    this.transform.position.y);
                go.transform.localScale = this.transform.localScale;
                go.SetActive(true);
                minusJump();
            }
		}        
    }

    void minusJump() {
        if (jumpsLeft > 0) {
            jumpsLeft--;
        }
    }

    void move() {
        lastX = crtX; //store last movement
        crtX = Input.GetAxisRaw("Horizontal"); //raw, we will do some things with it (-1, 0 or 1 only)

        animator.SetBool("walking", crtX != 0); //if not walking now, stop walking animation

        //makes for a smooth 'stopping' effect. change stopSmoothModifier to get more or less inertia
        float stopSmoothModifier = 1 / 1.4f;
        if (crtX == 0 && lastX != 0) {
            crtX = lastX * stopSmoothModifier;
        }

        //when crouching, movement is slower and more precise.
		float speedX = 1 / 15f;
		if (crouch) {
			speedX /= 3f;
		}
        
        //change the sprite direction based on which direction it is going.
        if (crtX < -0.5f && !GetComponent<SpriteRenderer>().flipX) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (crtX > 0.5f && GetComponent<SpriteRenderer>().flipX) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if ((this.transform.position.x < ar_manager.x_min && crtX < 0) ||
            (this.transform.position.x > ar_manager.x_max && crtX > 0)) {
            crtX = 0;
        }

        //this is the final movement calculus.
        Vector3 movement = new Vector3(crtX * speedX, 0, 0);
		this.transform.position += movement;
    }

    void handleInput() {
        //change animation based on if the player is grounded
		animator.SetBool("grounded", isGrounded());
        
        //if player is pressing down, crouch. the animation logic is in the animator
		crouch = Input.GetAxisRaw("Vertical") == -1;
		animator.SetBool("crouching", crouch);

        //to navigate clues menu
        if (Input.GetKeyDown(KeyCode.Z)) {
            ClueManager.getClueManager().previousClue();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            ClueManager.getClueManager().nextClue();
        }

        //TEMP: objection
        if (Input.GetKeyDown(KeyCode.O)) {
            if (objection_event != null) {
                objection_event();
            }
        }

        //TEMP: press
        if (Input.GetKeyDown(KeyCode.P)) {
            if (press_event != null) {
                press_event();
            }
        }

        //in case we do press to continue cutscenes (prolly not)
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     // if (next_cutscene_line_event != null) {
        //     //     next_cutscene_line_event();
        //     // }
        // }
    }

    bool isGrounded() {
		Vector3 raycast = transform.TransformDirection(Vector2.down);
        Debug.DrawRay(this.transform.position, raycast * 0.5f, Color.red, 1f);
        return Physics2D.Raycast(this.transform.position, raycast, 0.5f, 1 << LayerMask.NameToLayer("Walls"));
    }

    bool isTouchingLeftWall() {
        Vector3 raycast = transform.TransformDirection(- Vector2.right);
        Debug.DrawRay(this.transform.position, raycast * 0.1f, Color.red, 1f);
        return Physics2D.Raycast(this.transform.position, raycast, 0.1f, 1 << LayerMask.NameToLayer("Side Walls"));
    }

    bool isTouchingRightWall() {
        Vector3 raycast = transform.TransformDirection(Vector2.right);
        Debug.DrawRay(this.transform.position, raycast * 0.1f, Color.red, 1f);
        return Physics2D.Raycast(this.transform.position, raycast, 0.1f, 1 << LayerMask.NameToLayer("Side Walls"));
    }

    bool lastTouch = false;
    bool touching = false;
    bool firstTouch = false;

    void handleSideGrab() {
        lastTouch = touching;
        
        if ((isTouchingLeftWall() && Input.GetAxisRaw("Horizontal") == -1) 
        || (isTouchingRightWall() && Input.GetAxisRaw("Horizontal") == 1)){
            if (!lastTouch) {
                firstTouch = true;
            }

            if (firstTouch) {
                firstTouch = false;
                rb.velocity = Vector2.zero;
            }

            touching = true;
            rb.gravityScale = 0.15f;
            holdingWall = true;
            animator.SetBool("holding_wall", holdingWall);
        }
        else {
            touching = false;            
            rb.gravityScale = standardGravityScale;
            holdingWall = false;
            animator.SetBool("holding_wall", holdingWall);
        }

    }
}
