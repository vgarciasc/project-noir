using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpecialCardManager : MonoBehaviour {
	[SerializeField]
	Text specialText;

	Animator animator;

	void Start() {
		animator = this.GetComponent<Animator>();
	}

	public void setText(string text) {
		specialText.text = text;
	}

	public void show() {
		animator.SetTrigger("show");
	}
}
