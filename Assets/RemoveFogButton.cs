using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveFogButton : MonoBehaviour {
	GameManager gm;
	GameObject fog;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		fog = GameObject.Find("FadeCanvas/Fog");
		GetComponent<Button>().onClick.AddListener(() =>
			{
				fog.SetActive(false);
			});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
