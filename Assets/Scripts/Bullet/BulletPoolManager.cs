using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour {
	[SerializeField]
	GameObject bulletPrefab;
	
	public int pooledAmount;
	
	List<GameObject> bullets;

	public static BulletPoolManager getBulletPoolManager() {
		return (BulletPoolManager) HushPuppy.safeFindComponent("GameController", "BulletPoolManager");
	}

	void Start() {
		bullets = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = Instantiate(bulletPrefab);
			obj.SetActive(false);
			bullets.Add(obj);
		}
	}

	public GameObject getNewBullet() {
		for (int i = 0; i < bullets.Count; i++) {
			if (!bullets[i].activeInHierarchy) {
				return bullets[i];
			}
		}
		
		return null;
	}
}
