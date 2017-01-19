using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public BulletBehaviourData data;
    Rigidbody2D rb;

    void Start() {
        //rb = GetComponent<Rigidbody2D>();
    }

    public void setData(BulletBehaviourData data, int bullet_ID) {
        this.data = data;
        this.GetComponentInChildren<SpriteRenderer>().sprite = data.sprites[bullet_ID % (data.sprites.Length)];
        this.GetComponentInChildren<SpriteRenderer>().color = data.colors[bullet_ID % (data.colors.Length)];
        rb = GetComponentInChildren<Rigidbody2D>();

        if (data.enemy) this.tag = "Enemy Bullet";
        else this.tag = "Bullet";
    }

    IEnumerator initialDelay(float delayInFrames) {
        Vector2 velocity = rb.velocity;
        rb.velocity = Vector2.zero;

        for (int i = 0; i < delayInFrames; i++)
            yield return new WaitForEndOfFrame();

        rb.velocity = velocity;
    }

    public void setVelocity(Vector2 v, float speed, float velocityDamp) {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.velocity = new Vector2(v.x * speed, v.y * speed);
        rb.drag = velocityDamp / 50f;

        StartCoroutine(initialDelay(data.delayBeforeMoving));
    }

    public void destroy() {
        gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Arena")
            destroy();
    }

	void OnTriggerEnter2D(Collider2D target) {
		if ((target.tag == "Enemy" && this.tag == "Bullet") ||
            (target.tag == "Player" && this.tag == "Enemy Bullet")) {
			destroy();
		}
	}
}
