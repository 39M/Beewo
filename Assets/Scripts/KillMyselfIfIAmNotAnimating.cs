using UnityEngine;
using System.Collections;

public class KillMyselfIfIAmNotAnimating : MonoBehaviour {
	private Animation anim;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator>().GetComponent<Animation>().isPlaying;


	}
	
	// Update is called once per frame
	void Update () {
		bool isPlayingAnim = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Rank_Appear");

		if (!isPlayingAnim) {
			Debug.Log("I die!");
			Destroy(gameObject);
		} else {
			Debug.Log("I'm Playing!");
		}
	
	}
}
