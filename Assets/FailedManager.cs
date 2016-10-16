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
	public AudioSource music;
	public AudioClip clip, beforeClip, afterClip;
	int flag = 0;

	void Start ()
	{
		fadeAnim = GameObject.Find("FadeCanvas/FadeCover").GetComponent<Animation>();
		audioAnim = Camera.main.GetComponent<Animator>();
		cannotAnim = GameObject.Find("Canvas/CannotGiveUp").GetComponent<Animator>();
		stayDeterminAnim = GameObject.Find("Canvas/StayDetermined").GetComponent<Animator>();
		startTime = time = Time.time;

		clip = PlayerPrefs.GetInt("NextVersion") < 3 ? beforeClip : afterClip;
	}
	
	void Update ()
	{
		if (!music.isPlaying) {
			if (flag == 1) {
				music.clip = clip;
				music.Play();
			}
			if (flag == 2) {
				SceneManager.LoadScene(Const.levels[PlayerPrefs.GetInt("NextVersion")]);
			}
			flag++;
		}

		if (music.clip.length - music.time <= 1)
			fadeAnim.Play("EndFadeCover");


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
		}

		if (time > 11.78 && flag == 0)
		{
			flag = 1;
			music.Stop();
		}
	}
}
