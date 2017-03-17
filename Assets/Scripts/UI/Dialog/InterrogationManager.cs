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
    public delegate void StatementDelegate(int currentStatement);
    public event StatementDelegate nextStatementEvent;

    bool inMemory = false;
    bool canStartMemory = false;
    bool mainStatement = true;    

    int currentExamination = 1; //base 1
    int currentStatementIndex = 1; //base 1
    int currentStatementsQuantity = 0;

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
        currentStatementsQuantity = getNumberOfStatements();

        if (inkStory.canContinue) {
            Debug.Log("Current Cross-Examination: " + currentExamination + ".\nCurrent Statement: " + currentStatementIndex + "/" + currentStatementsQuantity + ".");
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
                if (nextStatementEvent != null) {
                    currentStatementIndex = getCurrentStatement();
                    nextStatementEvent(currentStatementIndex);
                }
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
        Debug.Log("Press pressed.");
        if (inkStory.currentChoices.Count > 0 &&
            inkStory.currentTags.Contains("pressoption")) {
                
            inkStory.ChooseChoiceIndex(0); //by default, press is choice 0
            if (startPressEvent != null) {
                Debug.Log("Delegate called.");
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
            // inkStory.ChooseChoiceIndex(2);
            inkStory.ChoosePathString("CrossExamination_1.CEI_Objection");
            
            if (correctObjection != null) {
                correctObjection();
            }
        }
        else if (inkStory.currentTags.Contains("obj")) {
            //line is objectionable, but either it's not the right clue, or the contradiction isn't even there.
            Debug.Log("Wrong Contradiction, Wrong Objection");
            // inkStory.ChooseChoiceIndex(0);
            inkStory.ChoosePathString("Wrong_Objection");

            if (wrongObjection != null) {
                wrongObjection();
            }
        }
    }

    //called by enemy animator manager. it goes to the previous scene
    public void PreviousScene() {
        inkStory.ChoosePathString((string) inkStory.variablesState["ReturnTo"]);
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

    public int getNumberOfStatements() {
        if (inkStory == null) {
            return 1;
        }

        return (int) inkStory.variablesState["number_statements"];
    }

    int getCurrentStatement() {
        if (inkStory == null) {
            return 1;
        }

        return (int) inkStory.variablesState["current_statement"];
    }
}
