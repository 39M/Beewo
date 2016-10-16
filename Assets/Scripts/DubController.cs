using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DubController : MonoBehaviour {

	public List<AudioClip> dubClips;

	private AudioSource dub;

	public float bpm = 140.0F;
	public int numBeatsPerSegment = 16;
	public AudioClip[] clips = new AudioClip[2];
	private double nextEventTime;
	private int flip = 0;
	private AudioSource[] audioSources = new AudioSource[2];
	private bool running = false;


	// Use this for initialization
	void Start () {
		dub = GetComponent<AudioSource>();

		int i = 0;
		while (i < 2) {
			GameObject child = new GameObject("Player");
			child.transform.parent = gameObject.transform;
			audioSources[i] = child.AddComponent<AudioSource>();
			i++;
		}
		nextEventTime = AudioSettings.dspTime + 2.0F;
		running = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			
			dub.PlayScheduled(AudioSettings.dspTime+2.0f);
			//PlayClip("Dub_8");


		}


		//if (!running)
		//	return;

		/*
		double time = AudioSettings.dspTime;

		if (time + 1.0F > nextEventTime) {
			audioSources[flip].clip = clips[flip];
			audioSources[flip].PlayScheduled(nextEventTime);
			Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);
			nextEventTime += 60.0F / bpm * numBeatsPerSegment;
			flip = 1 - flip;
		}*/
	
	}

	public void PlayClip(string clipName){
		//dub.clip = clipName;
		switch (clipName) {
			case "Dub_8":
				dub.clip = dubClips[8];
				break;
			default:
				break;
		}
				



	}
}
