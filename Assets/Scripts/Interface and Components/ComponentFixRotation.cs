using UnityEngine;

public class ComponentFixRotation : MonoBehaviour {
	Quaternion rotation;
	
	void Start () {
		rotation = transform.rotation;
	}
	
	void LateUpdate () {
		transform.rotation = rotation;
	}
}
