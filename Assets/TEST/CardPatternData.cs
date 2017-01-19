using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cardInfo {
	public ShotPatternData shot;
	public BulletData bullet;
}

[CreateAssetMenu(fileName = "Data", menuName = "Card Pattern", order = 1)]
public class CardPatternData : ScriptableObject {
	public cardInfo[] array;
}
