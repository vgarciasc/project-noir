using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InterrogationManager : MonoBehaviour {
    [SerializeField]
    TextAsset inkAsset;
    [SerializeField]
    TextBox standardTextBox;
    [SerializeField]
    TextBox memoryTextBox;

    Story inkStory;

    public delegate void PressDelegate();
    public event PressDelegate startPressEvent;
    public event PressDelegate endPressEvent;
    public delegate void MemoryDelegate();
    public event MemoryDelegate showMemoryEvent;
    public event MemoryDelegate getMemoryEvent;

    bool inMemory = false;
    bool canStartMemory = false;
    bool canPress = false;

    MemoryFragmentManager mem_manager;

    public static InterrogationManager getInterrogationManager() {
        return (InterrogationManager) HushPuppy.safeFindComponent("GameController", "InterrogationManager");
    }

    void Start() {
        inkStory = new Story(inkAsset.text);
        
        mem_manager = MemoryFragmentManager.getMemoryFragmentManager();

        mem_manager.pauseEvent += enterMemoryCutscene;
        mem_manager.unpauseEvent += exitMemoryCutscene;
    }

    public void Next() {
        if (inkStory.canContinue) {
            nextText();
        }
        else if (inkStory.currentChoices.Count > 0) {
            if (canStartMemory) {
                //default pra dentro de memfrag é não entrar na memoria
                inkStory.ChooseChoiceIndex(1);
                inkStory.Continue();
                inkStory.ChooseChoiceIndex(0);
            }
            else if (canPress) {
                //default pra argumento de linha argumentativa é não dar press
                inkStory.ChooseChoiceIndex(1);
            }
            else {
                //default pra linha elaborativa é não dar objection
                inkStory.ChooseChoiceIndex(1);
            }
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
        canPress = inkStory.currentTags.Contains("pressoption");

        if (inMemory) {
            memoryTextBox.displayText(txt);
        }
        else {
            standardTextBox.displayText(txt);
        }

        if (inkStory.currentTags.Contains("endpress")) {
            if (endPressEvent != null) {
                endPressEvent();
            }
        }

        if (canStartMemory = inkStory.currentTags.Contains("memfrag")) {
            if (showMemoryEvent != null) {
                showMemoryEvent();
            }
        }
    }

    void nextOptions() {
        List<string> aux = new List<string>();
        for (int i = 0; i < inkStory.currentChoices.Count; i++) {
            aux.Add(inkStory.currentChoices[i].text);
        }
    }

    void selectChoice(int index) {
        if (inkStory.currentChoices.Count > 0) {
            inkStory.ChooseChoiceIndex(index);
            nextText();
        }
    }

    public void captureMemoryFragment() {
        if (inkStory.currentChoices.Count > 1) {
            inkStory.ChooseChoiceIndex(1);
            inkStory.Continue();

            if (getMemoryEvent != null) {
                getMemoryEvent();
            }
        }         
    }

    void enterMemoryCutscene() {
        inMemory = true;
    }

    void exitMemoryCutscene() {
        inMemory = false;
    }
}
