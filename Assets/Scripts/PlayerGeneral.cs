using UnityEngine;
using System.Collections;

public class PlayerGeneral : MonoBehaviour {
    [Header("Variables")]
    [SerializeField]
    float speedX = 10f;
    [SerializeField]
    float speedY = 10f;

    [Header("References")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;

    public GameObject internalTrigger;
    public GameObject externalCollider;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void move(Vector2 translation) {
        this.transform.Translate(new Vector3(translation.x * speedX, translation.y * speedY, this.transform.position.z));
    }

    public Bullet shoot() {
        GameObject aux = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as GameObject;
        Bullet bullet = aux.GetComponent<Bullet>();
        //bullet.setVelocity(new Vector2(0f, 1f));
        
        return bullet;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        GameObject target = coll.gameObject;
        if (target.tag == "Bullet") {
            Destroy(this.gameObject);
            target.GetComponent<Bullet>().destroy();
        }
    }

    void OnParticleCollision(GameObject go) {
        //Debug.Log("AA");
        //StartCoroutine(destroy());
        Destroy(this.gameObject);
        //Destroy(go);
    }

    IEnumerator destroy() {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForEndOfFrame();
        }

        //this.GetComponent<SpriteRenderer>().enabled = false;
        //this.externalCollider.SetActive(false);
        //this.internalTrigger.SetActive(true);
        Destroy(this.gameObject);
    }
}
