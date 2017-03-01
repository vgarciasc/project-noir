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
    public delegate void ObjectionDelegate();
    public event ObjectionDelegate wrongObjection;
    public event ObjectionDelegate correctObjection;

    bool inMemory = false;
    bool canStartMemory = false;
    bool canPress = false;
    bool currentLineContainsContradiction = false;

    MemoryFragmentManager mem_manager;
    ClueManager clue_manager;

    public static InterrogationManager getInterrogationManager() {
        return (InterrogationManager) HushPuppy.safeFindComponent("GameController", "InterrogationManager");
    }

    void Start() {
        inkStory = new Story(inkAsset.text);
        
        mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
        clue_manager = ClueManager.getClueManager();

        mem_manager.unpauseEvent += exitMemoryCutscene;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().objection_event += runObjection;
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
            else if (currentLineContainsContradiction) {
                inkStory.ChooseChoiceIndex(1);
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
        if (inkStory.currentChoices.Count > 0) {
            inkStory.ChooseChoiceIndex(0);
            if (startPressEvent != null) {
                startPressEvent();
            }
        }
    }

    void nextText() {
        string txt = inkStory.Continue();
        canPress = inkStory.currentTags.Contains("pressoption");
        currentLineContainsContradiction = inkStory.currentTags.Contains("cont");

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

    #region Memory
    public void captureMemoryFragment() {
        if (inkStory.currentChoices.Count > 1) {
            enterMemoryCutscene();
            inkStory.ChooseChoiceIndex(1);

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
    #endregion

    #region objection
    void runObjection() {
        if (inkStory.currentTags.Contains("clue_" + clue_manager.currentClue.ID)) {
            //contradiction is there, and you got it right
            Debug.Log("Correct Contradiction");
            inkStory.ChooseChoiceIndex(0);
            inkStory.Continue();
            inkStory.ChooseChoiceIndex(0);
            if (correctObjection != null) {
                correctObjection();
            }
        }
        else if (inkStory.currentTags.Contains("cont")) {
            //contradiction is there, but you're wrong
            Debug.Log("Wrong Contradiction, Right Objection");
            inkStory.ChooseChoiceIndex(0);
            inkStory.Continue();
            inkStory.ChooseChoiceIndex(1);
            if (wrongObjection != null) {
                wrongObjection();
            }
        } 
        else if (inkStory.currentTags.Contains("obj")) {
            //line is objectionable, but contradiction is not there
            Debug.Log("Wrong Contradiction, Wrong Objection");
            selectChoice(0); //wrong cont
            if (wrongObjection != null) {
                wrongObjection();
            }
        }
    }
    #endregion
}
