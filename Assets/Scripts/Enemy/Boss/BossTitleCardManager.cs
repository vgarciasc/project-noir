using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTitleCardManager : MonoBehaviour {
	[SerializeField]
	Text titleText;
	[SerializeField]
	Text subtitleText;

	public void setTitle(string title, string subtitle) {
		titleText.text = title;
		subtitleText.text = subtitle;
	}

	public void intro() {
		this.GetComponent<Animator>().SetTrigger("intro");
	}
}
