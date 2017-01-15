using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AttackData {
    public ShootingBehaviourData behaviour;
    public Transform position;
    public float delay;
}

public class Enemy : MonoBehaviour, Triggerable {
    [Header("References")]
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]
    bool startShooting;
    
    BulletPoolManager bp_manager;
    GameObject player;
    Animator animator;
    Coroutine currentShootingCoroutine;
    bool invincible;

    public int current_health;
    public int max_health;

    public List<AttackData> thisAttacks = new List<AttackData>();
    public delegate void VoidVoidDelegate();
    public event VoidVoidDelegate take_hit_event;
    public event VoidVoidDelegate no_health_event;
    public delegate void VoidIntDelegate(int new_health);
    public event VoidIntDelegate set_health_event;

    void Start () {
        animator = (Animator) HushPuppy.safeComponent(this.gameObject, "Animator");
        bp_manager = BulletPoolManager.getBulletPoolManager();
        player = HushPuppy.safeFind("Player");

        invincible = true;

        if (startShooting) {
            StartCoroutine(shootRoutine());
        }
    }

    #region Health
    public void set_health(int health) {
        if (set_health_event != null) {
            set_health_event(health);
        }

        max_health = health;
    }

    public void reset_health() {
        current_health = max_health;
    }

    public void take_hit() {
        if (invincible) {
            return;
        }

        if (take_hit_event != null) {
            take_hit();
        }

        if (current_health > 0) {
            current_health--;
            if (current_health == 0) {
                if (no_health_event != null) {
                    no_health_event();
                }
            }
        }
        animator.SetTrigger("taking_damage");
    }

    public void TriggerEnter(GameObject target, GameObject sender) {
        if (sender.name == "Sprite") {
            if (target.tag == "Bullet") {
                take_hit();
                target.GetComponent<Bullet>().destroy();
            }
        }
    }
    #endregion

    Bullet createBullet(BulletBehaviourData data, Vector3 position) {
        GameObject obj = bp_manager.getNewBullet();
        Bullet b = obj.GetComponent<Bullet>();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;
        b.setData(data);
        return b;
    }

    void bulletLookAtPlayer(bool value, Bullet bullet) {
        if (!value) return;

        if (player != null)
            bullet.transform.up = bullet.transform.position - player.transform.position;
    }

    IEnumerator shootRoutine() {
        int i = 0;
        while (true) {
            yield return new WaitForSeconds(thisAttacks[i].behaviour.initialTimeOffset);
            switch (thisAttacks[i].behaviour.behaviour) {
                default: 
                case ShootingBehaviour.STRAIGHT:
                    yield return straightShot(thisAttacks[i]);
                    break;
                case ShootingBehaviour.ARC:
                    yield return arcShot(thisAttacks[i]);
                    break;
                case ShootingBehaviour.SHOTGUN:
                    yield return shotgunShot(thisAttacks[i]);
                    break;
            }

            i = (i + 1) % thisAttacks.Count;
            yield return new WaitForSeconds(thisAttacks[i].delay);
        }
    }

    IEnumerator straightShot(AttackData atdata) {
        yield return new WaitForSeconds(atdata.behaviour.initialTimeOffset);
        Transform spawn = atdata.position;
        if (spawn == null) {
            spawn = bulletSpawn;
        }

        for (int i = 0; i < atdata.behaviour.bulletQuantity; i++) {
            for (int h = 0; h < atdata.behaviour.copiesQuantity; h++) {
                Bullet bullet = createBullet(atdata.behaviour.bulletBehaviour, spawn.position);

                float angle = spawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                angle += (360 / atdata.behaviour.copiesQuantity) * h; //angle of bullet based on its arc_number of the multiple arcs

                bulletLookAtPlayer(atdata.behaviour.playerDirection, bullet);
                bullet.transform.Rotate(new Vector3(0f, 0f, angle));

                bullet.setVelocity(Vector3.Normalize(bullet.transform.up), atdata.behaviour.bulletSpeed, atdata.behaviour.bulletVelocityDamp);
            }

            for (int j = 0; j < atdata.behaviour.bulletInterval && i != atdata.behaviour.arcQuantity - 1; j++)
                yield return new WaitForEndOfFrame();
        }

        currentShootingCoroutine = null;
    }

    IEnumerator arcShot(AttackData atdata) {
        yield return new WaitForSeconds(atdata.behaviour.initialTimeOffset);
        Transform spawn = atdata.position;
        if (spawn == null) {
            spawn = bulletSpawn;
        }

        int wrap = 1;
        bool alt = false;
        float alternatingAngle = 360 / (atdata.behaviour.copiesQuantity * 2);

        for (int i = 0; i < atdata.behaviour.arcQuantity; i++) {
            if (atdata.behaviour.arcWrap) wrap *= -1;
            for (int j = 0; j < atdata.behaviour.bulletQuantity; j++) {
                for (int h = 0; h < atdata.behaviour.copiesQuantity; h++) {
                    float angle = atdata.behaviour.angleOffset + (j * atdata.behaviour.radiusArc / (atdata.behaviour.bulletQuantity)); //angle of bullet based on its order of release
                    angle += spawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                    angle -= (atdata.behaviour.radiusArc / 2); //angle of bullet divided between the two hemispheres 
                    angle += ((atdata.behaviour.copiesAngle / atdata.behaviour.copiesQuantity) * h); //angle of bullet based on which arc it is of the multiple arcs
                    angle *= wrap; //angle of bullet if it s going to wrap around and become spiral
                    if (alt) angle += alternatingAngle; //angle of bullet based on if it is alternating

                    Bullet bullet = createBullet(atdata.behaviour.bulletBehaviour, spawn.position);
                    bulletLookAtPlayer(atdata.behaviour.playerDirection, bullet);
                    bullet.transform.Rotate(new Vector3(0f, 0f, angle));
                    bullet.setVelocity(Vector3.Normalize(bullet.transform.up), atdata.behaviour.bulletSpeed, atdata.behaviour.bulletVelocityDamp);
                }

                for (int k = 0; k < atdata.behaviour.bulletInterval; k++)
                    yield return new WaitForFixedUpdate();
            }

            for (int k = 0; k < atdata.behaviour.arcInterval; k++)
                yield return new WaitForFixedUpdate();
        }

        currentShootingCoroutine = null;
    }

    IEnumerator shotgunShot(AttackData atdata) {
        yield return new WaitForSeconds(atdata.behaviour.initialTimeOffset);
        Transform spawn = atdata.position;
        if (spawn == null) {
            spawn = bulletSpawn;
        }
        
        bool alt = false;
        float alternatingAngle = 360 / (atdata.behaviour.copiesQuantity * 2);

        for (int i = 0; i < atdata.behaviour.arcQuantity; i++) {
            if (atdata.behaviour.alternating) alt = !alt;

            for (int j = 0; j < atdata.behaviour.bulletQuantity; j++) {
                for (int h = 0; h < atdata.behaviour.copiesQuantity; h++) {
                    float angle = (j * atdata.behaviour.radiusArc / (atdata.behaviour.bulletQuantity)); //angle of bullet based on its order of release
                    angle += spawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                    angle -= (atdata.behaviour.radiusArc / 2); //angle of bullet divided between the two hemispheres 
                    angle += ((360 / atdata.behaviour.copiesQuantity) * h); //angle of bullet based on which arc it is of the multiple arcs
                    if (alt) angle += alternatingAngle; //angle of bullet based on if it is alternating

                    Bullet bullet = createBullet(atdata.behaviour.bulletBehaviour, spawn.position);
                    bulletLookAtPlayer(atdata.behaviour.playerDirection, bullet);
                    bullet.transform.Rotate(new Vector3(0f, 0f, angle));
                    bullet.setVelocity(Vector3.Normalize(bullet.transform.up), atdata.behaviour.bulletSpeed, atdata.behaviour.bulletVelocityDamp);
                }
            }

            for (int k = 0; k < atdata.behaviour.bulletInterval && i != atdata.behaviour.arcQuantity - 1; k++)
                yield return new WaitForFixedUpdate();
        }

        currentShootingCoroutine = null;
    }

    #region Animation
    public void AnimShot(int attackIndex) { 
        if (currentShootingCoroutine != null) {
            return;
        }
        
        switch (thisAttacks[attackIndex].behaviour.behaviour) {
            default: 
            case ShootingBehaviour.STRAIGHT:
                currentShootingCoroutine = StartCoroutine(straightShot(thisAttacks[attackIndex]));
                break;
            case ShootingBehaviour.ARC:
                currentShootingCoroutine = StartCoroutine(arcShot(thisAttacks[attackIndex]));
                break;
            case ShootingBehaviour.SHOTGUN:
                currentShootingCoroutine = StartCoroutine(shotgunShot(thisAttacks[attackIndex]));
                break;
        }
    }

    public void AnimStopAllShots() {
        if (currentShootingCoroutine != null) {
            StopCoroutine(currentShootingCoroutine);
            currentShootingCoroutine = null;
        }
    }

    public void AnimToggleInvincible(int value) {
        if (value == 0) {
            invincible = false;
        }
        else {
            invincible = true;
        }
    }
    #endregion
}