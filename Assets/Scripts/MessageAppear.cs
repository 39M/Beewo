using UnityEngine;
using System.Collections;

public class MessageAppear : MonoBehaviour {
	public GameObject SMSAudioSource;

	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
	
		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Message_Hide")){
			Destroy(gameObject);

		}

	}

	public void CallMessageIn() {
		GetComponent<Animator>().SetTrigger("MessageIn");

	}

	public void PlaySMSTone() {
		SMSAudioSource.GetComponent<AudioSource>().Play();
		
	}
}
