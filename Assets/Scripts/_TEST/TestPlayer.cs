using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	Rigidbody2D rb;
	Animator animator;

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
	}
	
	float lastX, crtX;
	bool crouch = false;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !crouch) {
			rb.AddForce(Vector2.up * 250, ForceMode2D.Force);
		}

		lastX = crtX;
        
        crtX = Input.GetAxisRaw("Horizontal");

        if (crtX == 0 && lastX != 0) {
            crtX = lastX / 1.4f;
        }

		float speedX = 1 / 15f;

		if (crouch) {
			speedX /= 3f;
		}

        Vector3 movement = new Vector3(crtX * speedX, 0, 0);
        
        if (crtX < -0.5f && !GetComponent<SpriteRenderer>().flipX) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (crtX > 0.5f && GetComponent<SpriteRenderer>().flipX) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

		this.transform.position += movement;

		Vector3 raycast = transform.TransformDirection(Vector2.down);
		
		animator.SetBool("grounded", Physics2D.Raycast(this.transform.position, raycast, 0.5f, 1 << LayerMask.NameToLayer("Walls")));

		crouch = Input.GetKey(KeyCode.S);
		animator.SetBool("crouching", crouch);
		
	}
}
