using UnityEngine;
using System.Collections;

public enum ShootingBehaviour { STRAIGHT, RADIUS, ARC, SHOTGUN };
[CreateAssetMenu(fileName = "Data", menuName = "ShootingBehaviourElement", order = 1)]
public class ShootingBehaviourData : ScriptableObject {
    public ShootingBehaviour behaviour;
    public int framesBetweenBullets = 0;
    public float intervalBetweenShots;
    public float initialTimeOffset = 0f;
    public float finalTimeOffset = 0f;
    public int bulletQt = 1;
    public float speed = 1f;
    public bool arcWrap = false;
    public bool playerDirection = false;

    [Range(0f, 10f)]
    public float bulletVelocityDamp = 0f;

    [Range(0f, 10f)]
    public float minVelocity = 0f;

    [Header("Radius Shot")]
    [Range(0f, 2f)]
    public float radiusArc = 2f;

    [Header("Arc Shot")]
    public int arcQt = 2;
    public int multiShotQt = 1;

    [Header("Shotgun Shot")]
    public bool alternating = false;
}
