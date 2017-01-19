using UnityEngine;

public interface Triggerable {
	void TriggerEnter(GameObject target, GameObject sender);
	void TriggerExit(GameObject target, GameObject sender);
}
