using UnityEngine;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class MyScript : MonoBehaviour {
    Story inkStory;
    [SerializeField]
    TextAsset inkAsset;

    void Awake() {
        inkStory = new Story(inkAsset.text);

        while (inkStory.canContinue) {
            Debug.Log(inkStory.Continue());
        }
    }
}
