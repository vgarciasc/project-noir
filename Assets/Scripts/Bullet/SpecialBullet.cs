using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpecialBullet : MonoBehaviour {
	Animator animator;

	void Start() {
		animator = this.GetComponent<Animator>();
	}

	public void destroy() {
		animator.SetTrigger("destroy");
	}

	void AnimDestroy() {
		Destroy(this.gameObject);
	}
}
