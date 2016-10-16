using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoneyButton : MonoBehaviour {

	public GameObject canvas;
	public GameObject alwaysPerfectRank;
	public GameObject moneyScore;
	public GameObject rankPrefab;
	public GameObject rankCanvas;
	public GameObject goldenParticles;
	public GameObject SMSfab;
	public GameObject fadeCover;

	public AudioClip endmoneyClip;

	public GameObject noteGroup;

	private static int moneyCount;
	private int moneyAddInterval;
	private string costText;
	AudioSource music;
	int flag = 0;

	// Use this for initialization
	void Start () {
		moneyCount = 1288;
		moneyAddInterval = 250;

		GameObject rank = alwaysPerfectRank;
		GameObject cost = moneyScore;

		string costText = cost.GetComponent<Text>().text;
		Debug.Log(costText);

		music = Camera.main.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!music.isPlaying && flag == 0){
			music.Stop();
			music.clip = endmoneyClip;
			music.Play();
			flag = 1;
		}
		if (flag == 1 && music.clip.length - music.time <= 1f) {
			fadeCover.GetComponent<Animation>().Play("EndFadeCover");
			flag = 2;
		}
			

		if (Input.GetKey(KeyCode.J)){
			ExecuteEvents.Execute<IPointerClickHandler>(gameObject,  new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
		}



	}


	public void ClickMoneyButton () {


		// 增加金钱
		moneyCount += moneyAddInterval;
		costText = "已充值金额：" + moneyCount.ToString();
		moneyScore.GetComponent<Text>().text = costText;

		Debug.Log("Changed Money.");


		// 播放 Rank 提示
		//alwaysPerfectRank.GetComponent<Animator>().Play("Rank_Appear");


		// GameObject InitRank = Instantiate(rankPrefab) as GameObject;
		//GameObject InitRank = (GameObject)Instantiate(rankPrefab, rankCanvas.transform, false);
		GameObject InitRank = Instantiate(rankPrefab, rankCanvas.transform, false) as GameObject;
		// InitRank.transform.parent = rankCanvas.transform;
		// InitRank.GetComponent<Transform>().position = alwaysPerfectRank.GetComponent<Transform>().position;
		InitRank.SetActive(true);
		InitRank.GetComponent<Animator>().SetTrigger("StartAppear");


		float tempX = Random.Range(-1.0f, 1.0f) * 3.0f;

		Debug.Log("TempX = " + tempX);
		Vector3 tempVec = new Vector3(tempX, 0, 0);
		GameObject InitNoteGroup = (GameObject)Instantiate(noteGroup, tempVec, transform.rotation);
		InitNoteGroup.SetActive(true);

		goldenParticles.GetComponent<ParticleSystem>().Play();


		StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
		{
			GameObject newSMS = Instantiate(SMSfab, canvas.transform, false) as GameObject;
			newSMS.transform.GetChild(0).GetComponent<AudioSource>().volume = 0.5f;
			newSMS.SetActive(true);
		}, 5.5f));

		GetComponent<AudioSource>().Play();
	}

}
