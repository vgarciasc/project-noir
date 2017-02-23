using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Clue Data", order = 1)]
[System.Serializable]
public class ClueData : ScriptableObject {
	public string title;
	public Sprite sprite;
}
