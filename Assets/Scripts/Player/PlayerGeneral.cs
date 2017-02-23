using UnityEngine;
using System.Collections;

public class PlayerGeneral : MonoBehaviour, Triggerable {
    [Header("Variables")]
    [SerializeField]
    float speedX = 10f;
    [SerializeField]
    float speedY = 10f;

    [Header("References")]
    [SerializeField]
    BulletData bulletData;
    [SerializeField]
    Transform bulletSpawn;

    public delegate void VoidVoidDelegate();
    public event VoidVoidDelegate take_hit_event;
    public event VoidVoidDelegate instakill_event;
    public event VoidVoidDelegate teleport_event;

    int current_bullet_ID;
    Rigidbody2D rb;
    ArenaManager ar_manager;
    CharacterController controller;
    float shooting_cooldown_time;
    bool is_shooting_on_cooldown;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ar_manager = ArenaManager.getArenaManager();

        current_bullet_ID = 0;
        is_shooting_on_cooldown = false;
        shooting_cooldown_time = 5;
    }

    public void move(Vector2 translation) {
        controller = GetComponent<CharacterController>();

        // this.transform.Translate(new Vector3(translation.x * speedX,
        //                                     translation.y * speedY,
        //                                     transform.position.z));

        float x = translation.x * speedX;
        float y = translation.y * speedY;

        if (this.transform.position.x < ar_manager.x_min && x < 0) {
            x = 0;
        }
        else if (this.transform.position.x > ar_manager.x_max && x > 0) {
            x = 0;
        }
        if (this.transform.position.y < ar_manager.y_min && y < 0) {
            y = 0;
        }
        else if (this.transform.position.y > ar_manager.y_max && y > 0) {
            y = 0;
        }

        controller.Move(new Vector3(x,
                                    y,
                                    0));
    }

    float timestamp = 0f;
    float cooldown_seconds = 0.05f; 
    public void shoot() {
        if (timestamp > Time.time) {
            return;
        }
        timestamp = Time.time + cooldown_seconds;

        Vector2 vel = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        if (vel.magnitude < 0.05) {
            return;
        }

        vel = vel.normalized;

        GameObject obj = BulletPoolManager.getBulletPoolManager().getNewBullet();
        obj.transform.position = bulletSpawn.position;
        obj.transform.rotation = bulletSpawn.rotation;
        obj.SetActive(true);

        BulletDeluxe bullet = obj.GetComponent<BulletDeluxe>();
        bullet.setData(bulletData, this.transform);
        bullet.setVelocity(this.transform.up);

        current_bullet_ID++;
        // StartCoroutine(end_cooldown());
    }

    IEnumerator end_cooldown() {
        is_shooting_on_cooldown = true;

        for (int i = 0; i < shooting_cooldown_time; i++) {
            yield return new WaitForEndOfFrame();
        }

        is_shooting_on_cooldown = false;
    }

    public void TriggerEnter(GameObject target, GameObject sender) {
        if (sender.name == "MainCollider") {
            if (target.tag == "Bullet") {
                if (take_hit_event != null) {
                    take_hit_event();
                }
                BulletDeluxe bullet = target.GetComponentInChildren<BulletDeluxe>();
                if (bullet != null) {
                    bullet.destroy();
                }
            }
            if (target.tag == "Enemy") {
                if (instakill_event != null) {
                    instakill_event();
                }
            }
        }
    }

    public void TriggerExit(GameObject target, GameObject sender) {
		//
	}

    void OnParticleTrigger() {
        Debug.Log("ops");
    }
}
