using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitManager : MonoBehaviour {
	public Animation fadeCover;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 5)
			fadeCover.Play("EndFadeCover");
		if (Time.time > 5.95)
			SceneManager.LoadScene("VersionA");
	}
}
