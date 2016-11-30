using UnityEngine;
using System.Collections;

public enum ShootingBehaviour { STRAIGHT, ARC, SHOTGUN };
[CreateAssetMenu(fileName = "Data", menuName = "Shooting Behaviour Element", order = 1)]
public class ShootingBehaviourData : ScriptableObject {
    public ShootingBehaviour behaviour;

    [Header("Time Attributes")]
    [Tooltip("Interval between bullets measured in frames.")]
    [Range(0, 50)]
    public int bulletInterval = 0;

    [Tooltip("Interval between different arcs (waves of bullets) measured in frames.")]
    [Range(0, 50)]
    public int arcInterval = 0;

    [Tooltip("Waiting time before starting to fire, measured in seconds.")]
    [Range(0f, 10f)]
    public float initialTimeOffset = 0f;

    [Tooltip("Waiting time after ending fire, measured in seconds.")]
    [Range(0f, 10f)]
    public float finalTimeOffset = 0f;

    [Header("Bullet Attributes")]
    public BulletBehaviourData bulletBehaviour;

    [Tooltip("Number of bullets in each arc.")]
    [Range(0, 200)]
    public int bulletQuantity = 1;

    [Tooltip("Initial speed of each bullet shot.")]
    [Range(0f, 25f)]
    public float bulletSpeed = 1f;

    [Tooltip("How much deacceleration will each bullet have.")]
    [Range(0f, 50f)]
    public float bulletVelocityDamp = 0f;

    [Header("Angles Attributes")]
    [Tooltip("Number of times the pattern will repeat itself.")]
    [Range(0, 10)]
    public int arcQuantity = 3;

    [Tooltip("Number of patterns copied around the object.")]
    [Range(0, 10)]
    public int copiesQuantity = 1;

    [Tooltip("Angle between different copies. Note: the angle will be divided between the number of copies.")]
    [Range(0f, 360f)]
    public float copiesAngle = 360f;

    [Tooltip("Total angle covered by the arc pattern.")]
    [Range(0f, 360f)]
    public float radiusArc = 360f;

    [Header("Modifiers")]
    [Tooltip("Will the shotgun shots alternate?")]
    public bool alternating = false;

    [Tooltip("Will the arc wrap around itself when repeating?")]
    public bool arcWrap = false;

    [Tooltip("Will the straight shots focus on the player position?")]
    public bool playerDirection = false;

    public bool sine = true;
}
