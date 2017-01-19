using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Bullet Behaviour Element", order = 1)]
public class BulletBehaviourData : ScriptableObject {
    [Header("Bullet Spawn Attributes")]
    [Tooltip("Does the bullet come from the Bullet Pool, or is it special?")]
    public bool fromBulletPool = true;
    [Tooltip("If the bullet is special, what is it prefab?")]
    public GameObject bulletPrefab;

    [Header("Bullet Aesthetics Attributes")]
    public Sprite[] sprites;
    public Color[] colors;

    [Header("Misc Attributes")]
    [Tooltip("Is the bullet supposed to come from enemies?")]
    public bool enemy;    

    public float delayBeforeMoving;
}
