using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CardEmitter))]
public class CardPreviewer : MonoBehaviour {

	[SerializeField]
	ShotPatternData shot;	

	void Update () {
		if (Application.isEditor && !Application.isPlaying) {
			
		}
	}
}
