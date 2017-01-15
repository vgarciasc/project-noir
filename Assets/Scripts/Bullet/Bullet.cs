using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    BulletBehaviourData data;
    Rigidbody2D rb;

    void Start() {
        //rb = GetComponent<Rigidbody2D>();
    }

    public void setData(BulletBehaviourData data) {
        this.data = data;
        this.GetComponent<SpriteRenderer>().sprite = data.sprite;
        this.GetComponent<SpriteRenderer>().color = data.color;
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator initialDelay(float delayInFrames) {
        Vector2 velocity = rb.velocity;
        rb.velocity = Vector2.zero;

        for (int i = 0; i < delayInFrames; i++)
            yield return new WaitForEndOfFrame();

        rb.velocity = velocity;
    }

    public void setVelocity(Vector2 v, float speed, float velocityDamp) {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(v.x * speed, v.y * speed);
        rb.drag = velocityDamp / 50f;

        // StartCoroutine(initialDelay(data.delayBeforeMoving));
    }

    public void destroy() {
        gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Arena")
            destroy();
    }
}
