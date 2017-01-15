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
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;

    public delegate void VoidVoidDelegate();
    public event VoidVoidDelegate take_hit_event;
    public event VoidVoidDelegate instakill_event;

    Rigidbody2D rb;
    ArenaManager ar_manager;
    CharacterController controller;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ar_manager = ArenaManager.getArenaManager();
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

    public Bullet shoot() {
        GameObject aux = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as GameObject;
        Bullet bullet = aux.GetComponent<Bullet>();
        Vector2 vel = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        vel = vel.normalized;

        bullet.setVelocity(vel, 20, 0);
        
        return bullet;
    }

    public void teleport() {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (newPos.x > ar_manager.x_max || newPos.x < ar_manager.x_min ||
            newPos.y > ar_manager.y_max || newPos.y < ar_manager.y_min) {
                return;
        }
        this.transform.position = newPos;
    }

    public void TriggerEnter(GameObject target, GameObject sender) {
        if (sender.name == "MainCollider") {
            if (target.tag == "Enemy Bullet") {
                if (take_hit_event != null) {
                    take_hit_event();
                    target.GetComponent<Bullet>().destroy();
                }
            }
            if (target.tag == "Enemy") {
                if (instakill_event != null) {
                    instakill_event();
                }
            }
        }
    }
}
