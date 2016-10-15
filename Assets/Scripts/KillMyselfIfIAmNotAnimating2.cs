using UnityEngine;
using System.Collections;

public class KillMyselfIfIAmNotAnimating2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		bool isPlayingAnim = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NoteGroup_Appear");

		if (!isPlayingAnim) {
			Debug.Log("I die!");
			Destroy(gameObject);
		} else {
			Debug.Log("I'm Playing!");
		}
	}
}
