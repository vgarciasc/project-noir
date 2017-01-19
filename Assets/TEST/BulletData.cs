using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotPatternVisualStyle { RANDOM, BULLET_ALTERNATED, THREAD_ALTERNATED }

[CreateAssetMenu(fileName = "Data", menuName = "Bullet Data", order = 1)]
public class BulletData : ScriptableObject {
	[HeaderAttribute("Visuals")]
	[Tooltip("Sprites used by bullet.")]
	public Sprite[] sprites;

	public ShotPatternVisualStyle coloringStyle;

	public ShotPatternVisualStyle spritingStyle;

	[Tooltip("Will the sprite rotate in the direction of the bullet?")]
	public bool rotateDirectionShot = true;

	[HeaderAttribute("Sine")]
	[RangeAttribute(0f, 5f)]
	[Tooltip("The amplitude of the sine movement of the bullet.")]
	public float sineAmplitude;
	
	[RangeAttribute(0f, 5f)]
	[Tooltip("The frequency of the sine movement of the bullet.")]
	public float sineFrequency;

	//stop when reach position
}
