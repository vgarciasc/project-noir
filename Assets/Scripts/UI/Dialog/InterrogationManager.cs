using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InterrogationManager : MonoBehaviour {
    [SerializeField]
    TextAsset inkAsset;
    [SerializeField]
    TextBox textBox;

    Story inkStory;

    public delegate void PressDelegate();
    public event PressDelegate startPressEvent;
    public event PressDelegate endPressEvent;

    public static InterrogationManager getInterrogationManager() {
        return (InterrogationManager) HushPuppy.safeFindComponent("GameController", "InterrogationManager");
    }

    void Start() {
        inkStory = new Story(inkAsset.text);
    }

    public void Next() {
        if (inkStory.canContinue) {
            nextText();
        }
        else if (inkStory.currentChoices.Count > 0) {
            //por enquanto nunca da press
            inkStory.ChooseChoiceIndex(1);
            Next();
        }
        else {
            //closeText();
            //LOOPA
            inkStory = new Story(inkAsset.text);
            Next();
        }
    }

    public void Press() {
        inkStory.ChooseChoiceIndex(0);
        if (startPressEvent != null) {
            startPressEvent();
        }
    }

    void nextText() {
        string txt = inkStory.Continue();
        textBox.displayText(txt);

        if (inkStory.currentTags.Contains("endpress")) {
            if (endPressEvent != null) {
                endPressEvent();
            }
        }
    }

    void closeText() {
        textBox.closeText();
    }

    void nextOptions() {
        List<string> aux = new List<string>();
        for (int i = 0; i < inkStory.currentChoices.Count; i++) {
            aux.Add(inkStory.currentChoices[i].text);
        }
    }

    void selectChoice(int index) {
        inkStory.ChooseChoiceIndex(index);
        nextText();
    }
}
