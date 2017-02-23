using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    PlayerGeneral player;

    public delegate void VoidDelegate(); 
    public event VoidDelegate press_event;

    void Start() {
        player = GetComponent<PlayerGeneral>();
    }

    void Update () {
        handleInput();
	}

    void handleInput() {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime,
                                        Input.GetAxis("Vertical") * Time.deltaTime);
        player.move(movement);

        // if (Input.GetButton("Fire1")) {
        //     player.shoot();
        // }

        if (Input.GetKeyDown(KeyCode.P)) {
            if (press_event != null) {
                press_event();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            ClueManager.getClueManager().previousClue();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            ClueManager.getClueManager().nextClue();
        }
    }
}
