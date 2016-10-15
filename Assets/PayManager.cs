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


	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		payButton.onClick.AddListener (() => {
			if (iconLayout.childCount > 0)
			{
				Destroy(iconLayout.GetChild(iconLayout.childCount - 1).gameObject);
				if (iconLayout.childCount > 1) {
					explosion.SetActive(false);
					explosion.SetActive(true);
					return;
				}
			}
			gm.noteSpeed = 5;
			Camera.main.GetComponent<AudioSource>().clip = music;
			Camera.main.GetComponent<AudioSource>().Play();
			Camera.main.GetComponent<AudioSource>().Pause();
			iconGroup.SetActive(false);
			explosion.SetActive(false);
			explosion.SetActive(true);
		});
	}
	
	void Update () {
	
	}
}
