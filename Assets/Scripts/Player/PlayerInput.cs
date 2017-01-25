using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    PlayerGeneral player;

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
    }
}
