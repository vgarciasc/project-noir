using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Test_Animation : MonoBehaviour {
	public BulletBehaviourData data;
	Animator animator;
	public AnimationClip[] bullet_anim;
	static int id = 0;

	void Start () {
		data = this.GetComponent<Bullet>().data;		
		animator = this.GetComponent<Animator>();
		OverrideAnimationClip("OVERRIDABLE", bullet_anim[(int) Time.time % 2]);
	}

	public RuntimeAnimatorController GetEffectiveController(Animator animator)
	{
		RuntimeAnimatorController controller = animator.runtimeAnimatorController;

		AnimatorOverrideController overrideController = controller as AnimatorOverrideController;
		while (overrideController != null)
		{
			controller = overrideController.runtimeAnimatorController;
			overrideController = controller as AnimatorOverrideController;
		}

		return controller;
	}

	public void OverrideAnimationClip(string name, AnimationClip clip)
	{
		Animator animator = GetComponent<Animator>();

		AnimatorOverrideController overrideController = new AnimatorOverrideController();
		overrideController.runtimeAnimatorController = GetEffectiveController(animator);
		overrideController[name] = clip;
		animator.runtimeAnimatorController = overrideController;
	}
}
