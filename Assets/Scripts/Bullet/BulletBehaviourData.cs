using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Bullet Behaviour Element", order = 1)]
public class BulletBehaviourData : ScriptableObject {
    public Sprite sprite;
    public Color color;

    [Header("Time Attributes")]
    [Tooltip("Delay before moving, measured in frames.")]
    [Range(0, 240)]
    public int delayBeforeMoving = 0;
}
