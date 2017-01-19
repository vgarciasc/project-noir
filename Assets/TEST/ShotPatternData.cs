﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Shot Pattern", order = 1)]
public class ShotPatternData : ScriptableObject {
	[Header("Time Attributes")]

    [Tooltip("Interval between bullets measured in frames.")]
    [Range(0, 20)]
    public int delayBetweenBullets = 0;

    [Tooltip("The delay is applied each N bullets. What's the value of N?")]
    [Range(1, 10)]
    public int delayAppliedEachNBullets = 1;	
	
    [Tooltip("Interval between different arcs (waves of bullets) measured in frames.")]
    [Range(0, 50)]
    public int delayBetweenWaves = 0;

	[HeaderAttribute("Loop Attributes")]
    [Range(1, 10)]
    public int loopQuantity = 1;

    [Tooltip("Interval between loops measured in frames.")]
    [Range(0, 20)]
    public int delayBetweenLoops = 0;

    [Header("Bullet Attributes")]
    [Tooltip("Number of bullets in each arc.")]
    [Range(1, 50)]
    public int bulletQuantity = 1;

    [Tooltip("Initial speed of each bullet shot.")]
    [Range(0f, 25f)]
    public float bulletSpeed = 1f;

    [Tooltip("Radial distance from emitter.")]
    [Range(0f, 5f)]
    public float bulletInitialPositionOffset = 0f;

    [Header("Angles Attributes")]
    [Tooltip("Number of times the pattern will repeat itself.")]
    [Range(1, 10)]
    public int waveQuantity = 3;

    [Tooltip("Initial angle offset.")]
    [Range(0, 360)]
    public int angleOffset = 0;

    [Tooltip("Number of patterns copied around the object.")]
    [Range(1, 10)]
    public int threadQuantity = 1;

    [Tooltip("Angle between different threads. Note: the angle will be divided between the number of copies.")]
    [Range(0f, 360f)]
    public float angleBetweenThreads = 360f;

    [Tooltip("Arc filled by all the bullets in a thread and wave.")]
    [Range(0, 360)]
	public float threadArc = 0;

    [Tooltip("By how much will the thread arc be incremented between waves?")]
    [Range(0f, 360f)]
    public float threadArcAngleIncrementBetweenWaves = 0f;

    [Header("Modifiers")]
    [Tooltip("Checking this makes a wave continue where the other one left off.")]
	public bool waveWrap = true;

    [Tooltip("Makes the pattern clockwise.")]
	public bool clockwise = true;

    [Tooltip("Makes the shot be aimed at the player's general direction.")]
	public bool playerDirection = false; //NOT WORKING NOT WORKING

    [TooltipAttribute("Makes the bullet in a sine movement.")]
    public bool sineMovement = false;
}
