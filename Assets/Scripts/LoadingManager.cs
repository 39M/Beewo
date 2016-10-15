using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour {
	public Animation fadeAnim;
	AudioSource audio;
	public AudioClip [] clips;
	public Sprite [] sprites;
	public Image background;

	bool over = false;

	void Start () {
		audio = Camera.main.GetComponent<AudioSource>();
		audio.clip = clips[PlayerPrefs.GetInt("NextVersion")];
		audio.Play();

		background.sprite = sprites[PlayerPrefs.GetInt("NextVersion")];
	}
	
	void Update () {
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
