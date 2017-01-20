using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bullet Data", order = 1)]
public class BulletData : ScriptableObject {
	[Tooltip("Will the sprite rotate in the direction of the bullet?")]
	public bool rotateDirectionShot = true;

	[HeaderAttribute("Sinusoidal Motion")]
	[RangeAttribute(0f, 10f)]
	[Tooltip("The amplitude of the sine movement of the bullet.")]
	public float amplitude = 5;
	
	[RangeAttribute(-5f, 5f)]
	public float amplitudeAcceleration = 0;
	
	[RangeAttribute(1f, 10f)]
	[Tooltip("The frequency of the sine movement of the bullet.")]
	public float period = 5;
	
	[RangeAttribute(-5f, 5f)]
	public float periodAcceleration = 0;

	//stop when reach position
}
