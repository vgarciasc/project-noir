using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeluxe : MonoBehaviour {
	BulletData data;
	Rigidbody2D rb;
	Transform player;

	float speed;
	float angle_grad; 
	
	bool sineMovement;
	float sineAmplitude, 
		sineFrequency; 

	bool shouldStopWhenVelocityZero;

	Vector2 acceleration;
	Vector3 start_position;
	Vector3 tangent;

	int framecount = 0;

	void start() {
		rb = this.GetComponentInChildren<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (sineMovement) {
			framecount++;
			rb.velocity = new Vector2(speed, sineAmplitude * Mathf.Sin(framecount / (sineFrequency + 1)));
			rb.velocity = HushPuppy.rotateVector(rb.velocity, angle_grad);
			return;
		}

		if (rb.velocity.magnitude < 0.5 && shouldStopWhenVelocityZero) {
			rb.velocity = acceleration = Vector2.zero;
		}

		// tangent = new Vector2(this.transform.position.y - start_position.y,
		// 					this.transform.position.x - start_position.x);
		rb.velocity += acceleration * Time.deltaTime;
	}

	#region setters
	public void setData(BulletData bullet_data, Transform player) {
		this.data = bullet_data;
		this.sineAmplitude = data.sineAmplitude;
		this.sineFrequency = data.sineFrequency;
		this.player = player;
	}

	public void setPosition(Vector3 position) {
		this.transform.position = start_position = position;
	}

	public void setRotation(float angle_grad) {
		this.angle_grad = angle_grad;
		this.transform.rotation = Quaternion.identity;
		this.transform.Rotate(new Vector3(0f, 0f, angle_grad));
	}

	public void setVelocity(Vector2 velocity) {
		rb.velocity = velocity;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
		rb.velocity = this.transform.up * speed;
	}

	public void setAngularVelocity(float angle) {
		rb.angularVelocity = angle;
	}
	#endregion

	#region creating and destroying
	public void activate() {
		this.gameObject.SetActive(true);
		start();
	}

    public void destroy() {
        this.gameObject.SetActive(false);
    }
	#endregion

	#region colliders
	public void OnTriggerExit2D(Collider2D target) {
		if (target.gameObject.tag == "Arena")
			destroy();
    }
	#endregion

	#region changing movement
	public void toggleSineMovement(bool value) {
		sineMovement = value;
	}	

	public void addAccelerationEmitterDirection(float magnitude) {
		acceleration = this.transform.up * -magnitude;
	}

	public void addAccelerationPlayerDirection(float magnitude) {
		acceleration = (this.transform.position - player.position) * magnitude;
	}

	public void toggleStopWhenVelocityZero(bool value) {
		shouldStopWhenVelocityZero = value;
	}
	#endregion
}
