using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveFogButton : MonoBehaviour {
	GameManager gm;
	GameObject fog;
	public GameObject sms;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Paid", 0);
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		fog = GameObject.Find("FadeCanvas/Fog");
		GetComponent<Button>().onClick.AddListener(() =>
			{
				fog.SetActive(false);
				GetComponent<Image>().enabled = false;
				transform.GetChild(0).gameObject.SetActive(false);
				StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
					{
						GameObject newSMS = Instantiate(sms, canvas.transform, false) as GameObject;
						PlayerPrefs.SetInt("Paid", 1);
						newSMS.SetActive(true);
					}, 0.5f));
			});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
