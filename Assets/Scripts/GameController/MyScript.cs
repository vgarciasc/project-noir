using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class MyScript : MonoBehaviour {
    Story inkStory;
    [SerializeField]
    TextAsset inkAsset;
    [SerializeField]
    TextBox textBox;
    [SerializeField]
    ChoiceBox choiceBox;    

    void Awake() {
        inkStory = new Story(inkAsset.text);
        choiceBox.selectChoiceEvent += selectChoice;
    }

    public void next() {
        if (inkStory.canContinue)
            nextText();
        else if (inkStory.currentChoices.Count > 0)
            nextOptions();
        else
            closeText();
    }

    void nextText() {
        textBox.displayText(inkStory.Continue());
    }

    void closeText() {
        textBox.closeText();
    }

    void nextOptions() {
        List<string> aux = new List<string>();
        for (int i = 0; i < inkStory.currentChoices.Count; i++)
            aux.Add(inkStory.currentChoices[i].text);

        choiceBox.displayChoices(aux);
    }

    void selectChoice(int index) {
        inkStory.ChooseChoiceIndex(index);
        choiceBox.clearChoices();
        nextText();
    }
}
