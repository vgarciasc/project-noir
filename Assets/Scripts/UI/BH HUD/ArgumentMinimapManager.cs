using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArgumentMinimapManager : MonoBehaviour {

	[SerializeField]
	Transform argumentMinimapContainer;
	[SerializeField]
	GameObject argumentIconPrefab;

	int number_statements = 4;
	List<Animator> arguments = new List<Animator>();

	InterrogationManager interrogation;

	public static ArgumentMinimapManager getArgumentMinimapManager() {
		return (ArgumentMinimapManager) HushPuppy.safeFindComponent("GameController", "ArgumentMinimapManager");
	}

	void Start() {
		interrogation = InterrogationManager.getInterrogationManager();

		number_statements = interrogation.getNumberOfStatements();

		createArgumentIcons(number_statements);
		
		interrogation.nextStatementEvent += nextStatement;
	}

	void createArgumentIcons(int quantity) {
		for (int i = 0; i < quantity; i++) {
			GameObject go = Instantiate(argumentIconPrefab, argumentMinimapContainer, false);
			arguments.Add(go.GetComponent<Animator>());
		}
	}

	void nextStatement(int toActivate) {
		int index = (toActivate + arguments.Count - 2) % arguments.Count;

		arguments[index].SetBool("active", false);

		index = (toActivate + arguments.Count - 1) % arguments.Count;

		arguments[index].SetBool("active", true);
	}
}
