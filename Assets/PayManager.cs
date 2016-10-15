using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PayManager : MonoBehaviour {
	GameManager gm;
	public AudioClip music;
	public Button payButton;
	public GameObject iconGroup;
	public Transform iconLayout;
	public GameObject explosion;
	public GameObject sms;
	public Canvas canvas;


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
					return;
				}
				gm.noteSpeed = 5;
				Camera.main.GetComponent<AudioSource>().clip = music;
				Camera.main.GetComponent<AudioSource>().Play();
				Camera.main.GetComponent<AudioSource>().Pause();
//				gameObject.SetActive(false);
				explosion.SetActive(false);
				explosion.SetActive(true);
			} else {
				gm.noteSpeed *= 0.9f;
				if (gm.noteSpeed < 2)
					gm.noteSpeed = 2;
			}
		});
	}
	
	void Update () {
	
	}
}
