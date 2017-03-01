using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticleSystem : MonoBehaviour {

	void Start () {
		StartCoroutine(play());		
	}

	IEnumerator play() {
		this.GetComponentInChildren<ParticleSystem>().Play();
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
}
