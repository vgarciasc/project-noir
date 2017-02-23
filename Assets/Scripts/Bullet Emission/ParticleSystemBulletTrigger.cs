using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemBulletTrigger : MonoBehaviour {
	ParticleSystem ps;
	PlayerHealthManager player;

	[SerializeField]
	int damage = 1;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
		player = (PlayerHealthManager) HushPuppy.safeFindComponent("Player", "PlayerHealthManager");
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

		if (numEnter > 0) {
			player.BulletHit(damage);
		}
    }
}
