using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FailedManager : MonoBehaviour
{
	Animation fadeAnim;
	Animator audioAnim;
	Animator cannotAnim;
	Animator stayDeterminAnim;
	float startTime;
	float time;

	void Start ()
	{
		fadeAnim = GameObject.Find("FadeCanvas/FadeCover").GetComponent<Animation>();
		audioAnim = Camera.main.GetComponent<Animator>();
		cannotAnim = GameObject.Find("Canvas/CannotGiveUp").GetComponent<Animator>();
		stayDeterminAnim = GameObject.Find("Canvas/StayDetermined").GetComponent<Animator>();
		startTime = time = Time.time;
	}
	
	void Update ()
	{
		time = Time.time - startTime;

		if (time > 1 && time < 1.1)
		{
			cannotAnim.Play("TextFadeIn");
		}

		if (time > 6 && time < 6.1)
		{
			stayDeterminAnim.Play("TextFadeIn");
		}

		if (time > 10.8 && time < 11)
		{
			audioAnim.Play("DeterminedAudio");
			fadeAnim.Play("EndFadeCover");
		}

		if (time > 11.78)
		{
			SceneManager.LoadScene(Const.levels[PlayerPrefs.GetInt("NextVersion")]);
		}
	}
}
