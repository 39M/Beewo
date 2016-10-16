using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour {
	public Animation fadeAnim;
	public AudioClip startClip;
	public AudioClip paidSuccClip;
	public AudioClip unPaidSuccClip;
	public AudioSource startAudio;
	AudioSource audio;
	public AudioClip [] clips;
	public Sprite [] sprites;
	public Image background;
	bool started = false;

	bool over = false;

	void Start () {
		audio = Camera.main.GetComponent<AudioSource>();
		audio.clip = clips[PlayerPrefs.GetInt("NextVersion")];

		if (PlayerPrefs.GetInt("NextVersion") != 3) {
			startAudio = null;
			startClip = null;
			audio.Play();
			fadeAnim.Play("StartFadeCover");
		} else {
			startAudio = GetComponent<AudioSource>();
			startClip = PlayerPrefs.GetInt("Paid") == 0 ? unPaidSuccClip : paidSuccClip;
			startAudio.clip = startClip;
			startAudio.Play();
		}


		background.sprite = sprites[PlayerPrefs.GetInt("NextVersion")];
	}
	
	void Update () {
		if (startAudio != null) {
			if (!startAudio.isPlaying && !started) {
				audio.Play();
				fadeAnim.Play("StartFadeCover");
				started = true;
			}
		}

		if (startAudio == null || started){
			if (!audio.isPlaying) {
				if (!over) {
					over = true;
					fadeAnim.Play("EndFadeCover");
				} else if (!fadeAnim.isPlaying) {
					SceneManager.LoadScene(Const.levels[PlayerPrefs.GetInt("NextVersion")]);
				}
			}
		}
	}
}
