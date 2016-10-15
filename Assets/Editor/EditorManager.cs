using UnityEditor;
using UnityEngine;

// Clear all the editor prefs keys.
//
// Warning: this will also remove editor preferences as the opened projects, etc

public class ClearEditorPrefs : MonoBehaviour {
	[MenuItem ("Custom/Clear all Player Preferences")]
	static void DoDeselect() {
		if(EditorUtility.DisplayDialog("Delete all player preferences?",
			"Are you sure you want to delete all the player preferences? this action cannot be undone.",
			"Yes",
			"No"))
			PlayerPrefs.DeleteAll();
	}
}
