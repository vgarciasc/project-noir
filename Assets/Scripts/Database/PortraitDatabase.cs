using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortraitData {
	public string emote;	
	public Sprite spriteMouthOpen;
	public Sprite spriteMouthClosed;
}

[System.Serializable]
public class CharacterPortraitData {
	public string characterID;
	public string displayedName;
	public List<PortraitData> portraits;
}

public class PortraitDatabase : MonoBehaviour {
	[SerializeField]
	public List<CharacterPortraitData> characters;

	public static PortraitDatabase getPortraitDatabase() {
		return (PortraitDatabase) HushPuppy.safeFindComponent("Databases", "PortraitDatabase");
	}	
	
	public PortraitData getPortraitSprite(string character, string emote) {
		CharacterPortraitData charac = getCharacterPortrait(character);

		for (int i = 0; i < charac.portraits.Count; i++) {
			if (charac.portraits[i].emote == emote) {
				return charac.portraits[i];
			}
		}
		
		Debug.Log("Portrait '" + character + "' not found. Character '" + character  + "' does not have emote '" + emote + "'.");
		Debug.Break();
		return null;
	}

	public string getPortraitTitle(string character) {
		CharacterPortraitData charac = getCharacterPortrait(character);

		return charac.displayedName;
	}

	CharacterPortraitData getCharacterPortrait(string character) {
		CharacterPortraitData charac = null;
		
		for (int i = 0; i < characters.Count; i++) {
			if (characters[i].characterID == character) {
				charac = characters[i];
			}
		}

		if (charac == null) {
			Debug.Log("Portrait of character '" + character + "' not found. Character does not exist.");
			Debug.Break();
		}

		return charac;
	}
}
