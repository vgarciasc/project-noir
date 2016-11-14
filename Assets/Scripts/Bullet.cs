using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [Header("Variables")]

    Rigidbody2D rb;
    SpriteRenderer sr;
    float minVelocity = 0f;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(destroyOutOfBounds());
    }

    void FixedUpdate() {
        //capVelocity();
    }

    void capVelocity() {
        float auxX = rb.velocity.x, auxY = rb.velocity.y;
        if (Mathf.Abs(rb.velocity.x) < minVelocity) auxX = minVelocity * Mathf.Sign(rb.velocity.x);
        if (Mathf.Abs(rb.velocity.y) < minVelocity) auxY = minVelocity * Mathf.Sign(rb.velocity.y);

        rb.velocity = new Vector2(auxX, auxY);
    }

    public void setVelocity(Vector2 v, float speed, float minVelocity, float velocityDamp) {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(v.x * speed, v.y * speed);
        rb.drag = velocityDamp;
        this.minVelocity = minVelocity;
    }

    public void destroy() {
        Destroy(this.gameObject);
    }

    bool outOfScreen() {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (viewportPos.x > 1f || viewportPos.y > 1f || viewportPos.x < 0f || viewportPos.y < 0f)
            return true;

        return false;
    }

    IEnumerator destroyOutOfBounds() {
        while (true) {
            if (outOfScreen()) {
                yield return new WaitForSeconds(0.5f);
                destroy();
            } else {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
