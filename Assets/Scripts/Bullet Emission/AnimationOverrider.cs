using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrider : MonoBehaviour {
	Animator animator;

	public void setAnimation (AnimationClip clip) {
		animator = this.GetComponent<Animator>();
		// animator.SetTrigger(data.animation.name);
		OverrideAnimationClip("OVERRIDABLE", clip);
	}

	RuntimeAnimatorController GetEffectiveController(Animator animator)
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

	void OverrideAnimationClip(string name, AnimationClip clip)
	{
		Animator animator = GetComponent<Animator>();

		AnimatorOverrideController overrideController = new AnimatorOverrideController();
		overrideController.runtimeAnimatorController = GetEffectiveController(animator);
		overrideController[name] = clip;
		animator.runtimeAnimatorController = overrideController;
	}
}
