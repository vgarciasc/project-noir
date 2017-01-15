using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderSignalizer : MonoBehaviour {
	Enemy enemy;

	void Start () {
		Transform aux = this.transform;
		do { 
			enemy = aux.GetComponent<Enemy>();
			aux = aux.parent;
		} while (enemy == null);
	}

   
}
