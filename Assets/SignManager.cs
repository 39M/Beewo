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

	public GameObject sms;
	public Canvas canvas;

	float startTime;
	float time;
	bool paid = false;
	bool playedTips = false;

	public AudioSource threeDayFx;
	public AudioSource allDayFx;
	public AudioSource oneChouFx;
	public AudioSource tenChouFx;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		thirdDay.onClick.AddListener (() => {
			thirdDay.gameObject.SetActive (false);
			sign2Day.gameObject.SetActive (false);
			sign3Day.gameObject.SetActive (true);
			threeDayFx.Play();
		});

		buttonPay.onClick.AddListener (() => {
			signBackground.gameObject.SetActive (false);
			StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
				{
					GameObject newSMS = Instantiate(sms, canvas.transform, false) as GameObject;
					newSMS.SetActive(true);
				}, 0.5f));
			allDayFx.Play();
		});

		chouBtn.onClick.AddListener (() => {
			chouPanel.SetActive (true);
		});

		chou1Btn.onClick.AddListener (() => {
			StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
				{
					GameObject newSMS = Instantiate(sms, canvas.transform, false) as GameObject;
					newSMS.SetActive(true);
				}, 0.5f));
			oneChouFx.Play();
		});

		chou10Btn.onClick.AddListener (() => {
			chouPanel.SetActive (false);
			paid = true;
			StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
				{
					GameObject newSMS = Instantiate(sms, canvas.transform, false) as GameObject;
					newSMS.SetActive(true);
				}, 0.5f));
			tenChouFx.Play();
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
		if (time > 4.7f)
		{
			if (!playedTips) {
				GetComponent<AudioSource>().Play();
				playedTips = true;
			}

			gm.pause = !paid;
			needChou.SetActive(!paid);
		}
	}
}
