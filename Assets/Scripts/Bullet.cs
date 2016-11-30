//using UnityEngine;
//using System.Collections;

//public class Bullet : MonoBehaviour {
//    [Header("Variables")]

//    //Rigidbody2D rb;
//    SpriteRenderer sr;
//    float minVelocity = 0f;
//    public float linearDrag;
//    public Vector2 velocity;

//    void FixedUpdate() {
//        //capVelocity();
//        updateDrag();
//        updatePosition();
//    }

//    void updatePosition() {
//        this.transform.position += new Vector3(velocity.x, velocity.y);
//    }

//    void updateDrag() {
//        if (velocity.SqrMagnitude() * 1000 <= minVelocity) return;

//        velocity *= linearDrag;
//    }

//    public void setVelocity(Vector2 v, float speed, float minVelocity, float velocityDamp) {
//        float vel_modifier = speed / 25f;
//        velocity = new Vector2(v.x * vel_modifier, v.y * vel_modifier);
//        linearDrag = 1f - velocityDamp/1000f;
//        this.minVelocity = minVelocity;
//    }

//    public void destroy() {
//        Destroy(this.gameObject);
//    }

//    void OnTriggerExit2D(Collider2D coll) {
//        if (coll.gameObject.tag == "Arena")
//            destroy();
//    }
//}

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    [Header("Variables")]

    public BulletBehaviourData data;
    Rigidbody2D rb;

    void Start() {
        //rb = GetComponent<Rigidbody2D>();
    }

    public void setData(BulletBehaviourData data) {
        this.data = data;
        this.GetComponent<SpriteRenderer>().sprite = data.sprite;
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

        StartCoroutine(initialDelay(data.delayBeforeMoving));
    }

    public void destroy() {
        Destroy(this.gameObject);
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Arena")
            destroy();
    }
}
