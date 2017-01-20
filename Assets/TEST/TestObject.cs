using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour {

	Rigidbody2D rb;
	Vector2 acceleration;

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(1, 0);
	}
	
	void FixedUpdate () {
		Vector2 acceleration_sinoid_y;
		acceleration_sinoid_y = new Vector2(0, 5 * Mathf.Cos(5 * Time.time));

		rb.velocity += acceleration_sinoid_y * Time.deltaTime;
	}
}
