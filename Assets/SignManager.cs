using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignManager : MonoBehaviour
{
	GameManager gm;

	public GameObject signBackground;
	public Button thirdDay;
	public GameObject sign2Day;
	public GameObject sign3Day;
	public Button buttonPay;

	public GameObject chouPanel;
	public GameObject needChou;
	public Button chouBtn;
	public Button chou1Btn;
	public Button chou10Btn;

	float startTime;
	float time;
	bool paid = false;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		thirdDay.onClick.AddListener (() => {
			thirdDay.gameObject.SetActive (false);
			sign2Day.gameObject.SetActive (false);
			sign3Day.gameObject.SetActive (true);
		});

		buttonPay.onClick.AddListener (() => {
			signBackground.gameObject.SetActive (false);
		});

		chouBtn.onClick.AddListener (() => {
			chouPanel.SetActive (true);
		});

		chou1Btn.onClick.AddListener (() => {
				
		});

		chou10Btn.onClick.AddListener (() => {
			chouPanel.SetActive (false);
			paid = true;
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gm.unstart) {
			startTime = Time.time;
			time = 0;
		} else {
			time = Time.time - startTime;
		}
		if (time > 1)
		{
			gm.pause = !paid;
			needChou.SetActive(!paid);
		}
	}
}
