using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    [Header("References")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;
    public List<ShootingBehaviourData> thisBehaviours = new List<ShootingBehaviourData>();

    GameObject player;

    void Start () {
        StartCoroutine(shootRoutine());
        player = HushPuppy.safeFind("Player");
    }

    void OnTriggerEnter2D(Collider2D coll) {
        GameObject target = coll.gameObject;
        //if (target.tag == "Bullet") {
        //    Destroy(this.gameObject);
        //    target.GetComponent<Bullet>().destroy();
        //}
    }

    Bullet createBullet(Vector3 position) {
        GameObject aux = (GameObject) Instantiate(bulletPrefab, position, Quaternion.identity);
        return aux.GetComponent<Bullet>();
    }

    void bulletLookAtPlayer(bool value, Bullet bullet) {
        if (!value) return;

        if (player != null)
            bullet.transform.up = bullet.transform.position - player.transform.position;
    }

    IEnumerator shootRoutine() {
        int i = 0;
        while (true) {
            yield return new WaitForSeconds(thisBehaviours[i].initialTimeOffset);
            switch (thisBehaviours[i].behaviour) {
                default: 
                case ShootingBehaviour.STRAIGHT:
                    yield return straightShot(thisBehaviours[i]);
                    break;
                case ShootingBehaviour.RADIUS:
                    yield return radiusShot(thisBehaviours[i]);
                    break;
                case ShootingBehaviour.ARC:
                    yield return arcShot(thisBehaviours[i]);
                    break;
                case ShootingBehaviour.SHOTGUN:
                    yield return shotgunShot(thisBehaviours[i]);
                    break;
            }

            i = (i + 1) % thisBehaviours.Count;
            yield return new WaitForSeconds(thisBehaviours[i].finalTimeOffset);
        }
    }

    IEnumerator straightShot(ShootingBehaviourData sbdata) {
        for (int i = 0; i < sbdata.bulletQt; i++) {
            for (int h = 0; h < sbdata.multiShotQt; h++) {
                Bullet bullet = createBullet(bulletSpawn.position);

                float angle = bulletSpawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                angle += (360 / sbdata.multiShotQt) * h; //angle of bullet based on its arc_number of the multiple arcs

                bulletLookAtPlayer(sbdata.playerDirection, bullet);
                bullet.transform.Rotate(new Vector3(0f, 0f, angle));

                bullet.setVelocity(Vector3.Normalize(bullet.transform.up), sbdata.speed, sbdata.minVelocity, sbdata.bulletVelocityDamp);
            }

            for (int j = 0; j < sbdata.framesBetweenBullets && i != sbdata.arcQt - 1; j++)
                yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator radiusShot(ShootingBehaviourData sbdata) {
        for (int i = 0; i < sbdata.bulletQt; i++) {
            float angle = (180 * i * sbdata.radiusArc / (sbdata.bulletQt)) + bulletSpawn.rotation.z * 180 - (180 * sbdata.radiusArc / 2);

            Bullet bullet = createBullet(bulletSpawn.position);
            bulletLookAtPlayer(sbdata.playerDirection, bullet);
            bullet.transform.Rotate(new Vector3(0f, 0f, angle));
            bullet.setVelocity(Vector3.Normalize(bullet.transform.up), sbdata.speed, sbdata.minVelocity, sbdata.bulletVelocityDamp);
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator arcShot(ShootingBehaviourData sbdata) {
        int wrap = 1;
        for (int i = 0; i < sbdata.arcQt; i++) {
            if (sbdata.arcWrap) wrap *= -1;
            for (int j = 0; j < sbdata.bulletQt; j++) {
                for (int h = 0; h < sbdata.multiShotQt; h++) {
                    float angle = (180 * j * sbdata.radiusArc / (sbdata.bulletQt)); //angle of bullet based on its order of release
                    angle += bulletSpawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                    angle -= (180 * sbdata.radiusArc / 2); //angle of bullet divided between the two hemispheres 
                    angle += ((360 / sbdata.multiShotQt) * h); //angle of bullet based on which arc it is of the multiple arcs
                    angle *= wrap; //angle of bullet if it s going to wrap around and become spiral

                    Bullet bullet = createBullet(bulletSpawn.position);
                    bulletLookAtPlayer(sbdata.playerDirection, bullet);
                    bullet.transform.Rotate(new Vector3(0f, 0f, angle));
                    bullet.setVelocity(Vector3.Normalize(bullet.transform.up), sbdata.speed, sbdata.minVelocity, sbdata.bulletVelocityDamp);
                }

                for (int k = 0; k < sbdata.framesBetweenBullets; k++)
                    yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator shotgunShot(ShootingBehaviourData sbdata) {
        bool alt = false;
        float alternatingAngle = 360 / (sbdata.multiShotQt * 2);

        for (int i = 0; i < sbdata.arcQt; i++) {
            if (sbdata.alternating) alt = !alt;

            for (int j = 0; j < sbdata.bulletQt; j++) {
                for (int h = 0; h < sbdata.multiShotQt; h++) {
                    float angle = (180 * j * sbdata.radiusArc / (sbdata.bulletQt)); //angle of bullet based on its order of release
                    angle += bulletSpawn.rotation.z * 180; //angle of bullet based on the way the enemy is facing
                    angle -= (180 * sbdata.radiusArc / 2); //angle of bullet divided between the two hemispheres 
                    angle += ((360 / sbdata.multiShotQt) * h); //angle of bullet based on which arc it is of the multiple arcs
                    if (alt) angle += alternatingAngle; //angle of bullet based on if it is alternating

                    Bullet bullet = createBullet(bulletSpawn.position);
                    bulletLookAtPlayer(sbdata.playerDirection, bullet);
                    bullet.transform.Rotate(new Vector3(0f, 0f, angle));
                    bullet.setVelocity(Vector3.Normalize(bullet.transform.up), sbdata.speed, sbdata.minVelocity, sbdata.bulletVelocityDamp);
                }
            }

            for (int k = 0; k < sbdata.framesBetweenBullets && i != sbdata.arcQt - 1; k++)
                yield return new WaitForFixedUpdate();
        }
    }
}