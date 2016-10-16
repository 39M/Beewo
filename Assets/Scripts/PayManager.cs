using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PayManager : MonoBehaviour {
	GameManager gm;
	public AudioClip clip;
	public Button payButton;
	public GameObject iconGroup;
	public Transform iconLayout;
	public GameObject explosion;
	public GameObject sms;
	public Canvas canvas;
	AudioSource music;

	public AudioSource normalExpFx;
	public AudioSource criticalExpFx;


	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		payButton.onClick.AddListener (() => {
			StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
				{
					GameObject newSMS = Instantiate(sms, canvas.transform, false) as GameObject;
					newSMS.SetActive(true);
				}, 0.5f));
			if (iconLayout.childCount > 9)
			{
				Destroy(iconLayout.GetChild(iconLayout.childCount - 1).gameObject);
				if (iconLayout.childCount > 10) {
					explosion.SetActive(false);
					explosion.SetActive(true);
					normalExpFx.Play();
					return;
				}
				gm.noteSpeed = 5;
				music = Camera.main.GetComponent<AudioSource>();
				float t = music.time;
				music.clip = clip;
				music.Play();
				music.time = t;
				if (gm.pause)
					music.Pause();
//				gameObject.SetActive(false);
				explosion.SetActive(false);
				explosion.SetActive(true);
				criticalExpFx.Play();
				gm.minPassScore = 0;
				gm.maxCanMiss = int.MaxValue;
				gm.cheat = true;
				return;
			} else {
				gm.noteSpeed *= 0.9f;
				if (gm.noteSpeed < 2)
					gm.noteSpeed = 2;
			}
			normalExpFx.Play();
		});
	}
	
	void Update () {
	
	}
}
