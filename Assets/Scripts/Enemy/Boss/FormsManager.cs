using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Form {
	public string triggerName;
	public string formFlavor;
	public int HP;
}

public class FormsManager : MonoBehaviour {
	[SerializeField]
	List<Form> forms = new List<Form>();
	[SerializeField]
	GameObject boss;
	[SerializeField]
	float initialDelayInSeconds;

	Enemy enemy;

	public int nextFormIndex;

	void Start () {
		enemy = this.GetComponent<Enemy>();
		enemy.no_health_event += changeForm;

		nextFormIndex = 0;
	}
	
	void firstForm() {
		Form frm = forms[nextFormIndex];
		boss.GetComponent<Enemy>().set_health(frm.HP);
		boss.GetComponent<Enemy>().reset_health();
		StartCoroutine(start_form_delay());
	}

	IEnumerator start_form_delay() {
		Form frm = forms[nextFormIndex];
		yield return new WaitForSeconds(initialDelayInSeconds);
		boss.GetComponent<EnemyUIManager>().newForm(frm.formFlavor);
		yield return new WaitForSeconds(1.0f);

		boss.GetComponent<Animator>().SetTrigger(frm.triggerName);
		
		nextFormIndex = (nextFormIndex + 1) % forms.Count;
	}

	void changeForm() {
		if (nextFormIndex == 0) {
			Debug.Log("end battle");
		}

		Form frm = forms[nextFormIndex];

		boss.GetComponent<Animator>().SetTrigger("end_form");
		boss.GetComponent<Animator>().SetTrigger(frm.triggerName);
		boss.GetComponent<Enemy>().set_health(frm.HP);
		boss.GetComponent<Enemy>().AnimStopAllShots();
		boss.GetComponent<EnemyUIManager>().newForm(frm.formFlavor);
		
		nextFormIndex = (nextFormIndex + 1) % forms.Count;
	}
}
