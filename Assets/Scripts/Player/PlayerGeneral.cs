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
    BulletBehaviourData bulletData;
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]
    GameObject teleportLinePrefab;
    [SerializeField]
    GameObject teleportPlayerSurrogatePrefab;

    public delegate void VoidVoidDelegate();
    public event VoidVoidDelegate take_hit_event;
    public event VoidVoidDelegate instakill_event;

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

        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.setData(bulletData, current_bullet_ID);
        bullet.setVelocity(vel, 20, 0);

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

    public void teleport() {
        Vector2 originalPos = this.transform.position;
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (newPos.x > ar_manager.x_max || newPos.x < ar_manager.x_min ||
            newPos.y > ar_manager.y_max || newPos.y < ar_manager.y_min) {
                return;
        }

        this.transform.position = newPos;
        GameObject player_surrogate = Instantiate(teleportPlayerSurrogatePrefab,
                                                originalPos,
                                                Quaternion.identity);
        player_surrogate.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
        player_surrogate.transform.localScale = this.transform.localScale;
        player_surrogate.GetComponent<Animator>().SetTrigger("despawn");
        this.GetComponent<Animator>().SetTrigger("spawn");
        // TeleportLine line = Instantiate(teleportLinePrefab).GetComponent<TeleportLine>();
        // line.setPoints(originalPos, newPos);
    }

    public void TriggerEnter(GameObject target, GameObject sender) {
        if (sender.name == "MainCollider") {
            if (target.tag == "Enemy Bullet") {
                if (take_hit_event != null) {
                    take_hit_event();
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
}
