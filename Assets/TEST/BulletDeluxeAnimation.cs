using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeluxeAnimation : MonoBehaviour {
	BulletData data;
	Animator animator;

	public void start (BulletData data) {
		this.data = data;	
		animator = this.GetComponent<Animator>();
		OverrideAnimationClip("OVERRIDABLE", data.animation);
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
