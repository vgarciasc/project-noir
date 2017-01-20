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
	bool cosineMovement;
	float sinusoidalAmplitude,
		sinusoidalPeriod;

	bool shouldStopWhenVelocityZero;

	Vector2 acceleration;
	Vector2 acceleration_senoid_y;
	Vector2 acceleration_player_direction;
	Vector2 acceleration_emitter_direction;

	Vector3 start_position;
	Vector3 tangent;

	bool player_direction;
	float player_direction_magnitude;

	bool emitter_direction;
	float emitter_direction_magnitude;

	float framecount;

	void start() {
		rb = this.GetComponentInChildren<Rigidbody2D>();

		framecount = 0;
		rb.velocity = Vector2.zero;
		acceleration = acceleration_senoid_y = Vector2.zero;
	}

	void FixedUpdate() {
		if (sineMovement || cosineMovement) {
			framecount++;

			sinusoidalAmplitude += data.amplitudeAcceleration * Time.deltaTime;
			sinusoidalPeriod += data.periodAcceleration * Time.deltaTime;

			acceleration_senoid_y = new Vector2(0, sinusoidalAmplitude * Mathf.Cos(framecount / (sinusoidalPeriod * 2)));
			acceleration_senoid_y = HushPuppy.rotateVector(acceleration_senoid_y, angle_grad);

			if (sineMovement) acceleration_senoid_y *= -1;
		}

		if (player_direction) {
			acceleration_player_direction = (player.position - this.transform.position).normalized * player_direction_magnitude;
		}

		if (emitter_direction) {
			acceleration_emitter_direction = (start_position - this.transform.position).normalized * emitter_direction_magnitude;
		}

		rb.velocity += (acceleration +
						acceleration_senoid_y + 
						acceleration_player_direction + 
						acceleration_emitter_direction)
						* Time.deltaTime;

		// if (rb.velocity.magnitude < 0.5 && shouldStopWhenVelocityZero) {
		// 	rb.velocity = acceleration = Vector2.zero;
		// }
	}

	#region setters
	public void setData(BulletData bullet_data, Transform player) {
		this.data = Instantiate(bullet_data);
		this.sinusoidalAmplitude = data.amplitude;
		this.sinusoidalPeriod = data.period;
		this.player = player;
	}

	public void setSprite(Sprite sprite) {
		this.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
	}

	public void setColor(Color color) {
		this.GetComponentInChildren<SpriteRenderer>().color = color;
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

	#region changing motion
	#region sinusoidal motion

	public void startCosineMovement() {
		cosineMovement = true;
		rb.velocity = new Vector2(this.transform.right.x, this.transform.right.y);
	}

	public void startSineMovement() {
		sineMovement = true;
		rb.velocity = new Vector2(this.transform.right.x, this.transform.right.y);
	}

	public void endSinusoidalMovement() {
		sineMovement = false;
		cosineMovement = false;
	}

	#endregion

	public void addAccelerationEmitterDirection(float magnitude) {
		emitter_direction = true;
		emitter_direction_magnitude = magnitude;
	}

	public void addAccelerationPlayerDirection(float magnitude) {
		player_direction = true;
		player_direction_magnitude = magnitude;
	}

	public void toggleStopWhenVelocityZero(bool value) {
		shouldStopWhenVelocityZero = value;
	}
	#endregion
}
