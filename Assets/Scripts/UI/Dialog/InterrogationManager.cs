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
    bool mainStatement = true;    

    MemoryFragmentManager mem_manager;
    ClueManager clue_manager;
    PlayerInput playerInput;

    public static InterrogationManager getInterrogationManager() {
        return (InterrogationManager) HushPuppy.safeFindComponent("GameController", "InterrogationManager");
    }

    void Start() {
        inkStory = new Story(inkAsset.text);
        
        mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
        clue_manager = ClueManager.getClueManager();

        mem_manager.unpauseEvent += exitMemoryCutscene;
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerInput.objection_event += objection;
        playerInput.press_event += press;
    }

    public void Next() {
        if (inkStory.canContinue) {
            nextText();
        }
        else if (inkStory.currentChoices.Count > 0) {
            inkStory.ChooseChoiceIndex(1);
            Next();
        }
        else {
            inkStory = new Story(inkAsset.text);
            Next();
        }
    }

    void nextText() {
        string txt = inkStory.Continue();
        mainStatement = inkStory.currentTags.Contains("pressoption");

        if (inMemory) {
            memoryTextBox.displayText(txt);
        }
        else {
            if (mainStatement) {
                standardTextBox.displayText("<color=#E5B368FF>" + txt + "</color>");
            }
            else {
                standardTextBox.displayText(txt);
            }
        }

        if (inkStory.currentTags.Contains("endpress")) {
            if (endPressEvent != null) {
                endPressEvent();
            }
        }

        if (canStartMemory = inkStory.currentTags.Contains("memfrag") &&
            inkStory.currentChoices.Count > 2) {
            if (showMemoryEvent != null) {
                showMemoryEvent();
            }
        }
    }

    #region press
    //function is called when the Press Button is pressed.
    //it is responsible for warning everyone that it is pressed,
    //as well as checking if it can be pressed right now.
    public void press() {
        if (inkStory.currentChoices.Count > 0 &&
            inkStory.currentTags.Contains("pressoption")) {

            inkStory.ChooseChoiceIndex(0); //by default, press is choice 0
            if (startPressEvent != null) {
                startPressEvent(); //warns all observers that a press is ocurring
            }
        }
    }
    #endregion

    #region objection
    //function is called when the Objection Button is pressed.
    //it is responsible for checking if there can be an objection
    //as well as checking if the objection is correct or wrong
    //and warning every observer that is related.
    void objection() {
        if (inkStory.currentTags.Contains("clue_" + clue_manager.currentClue.ID)) {
            //contradiction is there, and you got it right
            Debug.Log("Correct Contradiction");
            inkStory.ChooseChoiceIndex(2);
            
            if (correctObjection != null) {
                correctObjection();
            }
        }
        else if (inkStory.currentTags.Contains("obj")) {
            //line is objectionable, but either it's not the right clue, or the contradiction isn't even there.
            Debug.Log("Wrong Contradiction, Wrong Objection");
            inkStory.ChooseChoiceIndex(0);
            
            if (wrongObjection != null) {
                wrongObjection();
            }
        }
    }
    #endregion

    #region Memory
    public void captureMemoryFragment() {
        if (inkStory.currentChoices.Count > 1) {
            enterMemoryCutscene();
            inkStory.ChooseChoiceIndex(2);

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
        inkStory.Continue();
    }
    #endregion
}
