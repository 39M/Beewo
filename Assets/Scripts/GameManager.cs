﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

public struct note
{
	public float time;
	public float width;
	public float pos;
	public bool slide;

	public note (float time, float width, float pos, bool slide = false)
	{
		this.time = time;
		this.width = width;
		this.pos = pos;
		this.slide = slide;
	}
}

public class GameManager : MonoBehaviour
{
	public int version;

	Camera mainCamera;
	public AudioSource music;
	public AudioClip failClip;
	public AudioSource hitFX;
	public AudioClip [] hitClips;
	List<note> beatmap = new List<note> ();
	List<note>.Enumerator noteIterator;

	public GameObject notePrefab;
	public GameObject slideNotePrefab;
	List<GameObject> noteObjects = new List<GameObject> ();
	note nextNote;

	public GameObject goodFXPrefab;
	public GameObject perfectFXPrefab;

	public GameObject missTextPrefab;
	public GameObject goodTextPrefab;
	public GameObject perfectTextPrefab;

	public Text scoreLabel;
	public Button pauseButton;
	public Canvas canvas;
	public Animation fadeAnim; 

	public bool unstart;
	public bool pause;
	public bool success;
	public bool failed;
	bool createdLastNote;
	public int score = 0;
	public int minPassScore = 301;
	public int missCount = 0;
	public int maxCanMiss = 10;
	float musicStartTime;
	float musicStopTime;
	float musicTime;
	public bool cheat = false;
	bool notFadeOut = true;

	public float noteSpeed;
	public float noteCreatePosY;
	public float maxJudgePosY;
	public float judgelinePosY;
	public float screenTopY;
	public float screenBottomY;
	public float screenWidth;

	void Start ()
	{
		mainCamera = Camera.main;
		music = mainCamera.GetComponent<AudioSource> ();
		musicStartTime = musicTime = music.time = 19.783f;
		musicStopTime = 49f;
		maxCanMiss = Const.maxMissList[version];

		unstart = true;
		pause = true;
		success = false;
		failed = false;
		createdLastNote = false;
		cheat = false;
		notFadeOut = true;

		noteSpeed = Const.noteSpeedList[version];

		LoadBeatmap ();
		SetPositions ();
		NoteGeneratorUpdate ();

		minPassScore = beatmap.Count * 150;
		missCount = 0;
	}

	void Update ()
	{
		musicTime = music.time;

		CheckStartUpdate ();
		CheckEndUpdate ();

		if (success || failed)
			return;
		if (pause) {
			music.Pause ();
			return;
		} else {
			music.UnPause ();
		}

		NoteGeneratorUpdate ();
		JudgeUpdate ();
		UIUpdate ();
	}

	void LoadBeatmap ()
	{
		var jsonNode = JSON.Parse ("{\"beatmap\": [[19.783, 3, -0.3], [20.089, 3, -0.1], [20.396, 3, 0.1], [21.008, 3, 0.1], [21.62, 3, 0.3], [22.232, 3, 0.1], [22.538, 3, 0.1], [22.845, 3, -0.1], [23.457, 3, -0.3], [24.069, 3, -0.3], [24.681, 3, -0.1], [24.987, 3, 0.1], [25.294, 3, 0.1], [25.906, 3, 0.3], [26.518, 3, 0.1], [27.13, 3, -0.3], [27.436, 3, -0.1], [27.743, 3, -0.3], [28.355, 3, -0.1], [28.967, 3, 0.1], [29.579, 3, 0.1], [29.885, 3, 0.3], [30.192, 3, 0.3], [30.651, 3, 0.1], [30.804, 3, 0.1], [31.416, 3, -0.1], [32.028, 3, -0.1], [32.334, 3, -0.1], [32.64, 3, 0.1], [33.1, 3, 0.3], [33.253, 3, 0.3], [33.865, 3, 0.1], [34.477, 3, -0.1], [34.783, 3, -0.3], [35.089, 3, 0.1], [35.549, 3, 0.1], [35.702, 3, 0.1], [36.314, 3, 0.3], [36.926, 3, 0.3], [37.232, 3, 0.1], [37.538, 3, 0.1], [37.998, 3, -0.3], [38.151, 3, -0.3], [38.763, 3, -0.3], [39.375, 3, 0.1], [40.294, 3, -0.1], [40.447, 3, -0.1], [40.6, 3, -0.1], [41.212, 3, -0.3], [41.671, 3, -0.3], [41.824, 3, -0.3], [42.436, 3, 0.1], [42.743, 3, 0.1], [43.049, 3, 0.3], [43.661, 3, 0.1], [44.12, 3, 0.1], [44.273, 3, 0.1], [45.191, 3, -0.1], [45.345, 3, -0.1], [45.498, 3, -0.1], [46.11, 3, -0.3]]}");
		// , [46.569, 3, -0.1], [46.722, 3, -0.1], [47.028, 3, 0], [49.171, 3, 0.1], [49.477, 3, 0.1], [49.783, 3, 0.1], [50.089, 3, -0.1], [50.396, 3, -0.3], [51.314, 3, -0.3], [51.62, 3, -0.3], [51.926, 3, -0.1], [52.232, 3, 0.1], [52.538, 3, 0.3], [52.845, 3, 0.1], [53.457, 3, 0.1], [53.763, 3, 0.1], [54.069, 3, 0.1], [54.681, 3, -0.1], [54.987, 3, -0.1], [55.294, 3, -0.3], [55.906, 3, -0.1], [56.212, 3, 0.1], [56.518, 3, 0.1], [57.436, 3, 0.1], [57.743, 3, 0.3], [57.896, 3, 0.1], [58.049, 3, 0.1], [58.202, 3, 0.1], [58.355, 3, -0.1], [58.508, 3, -0.1], [58.661, 3, -0.3], [58.967, 3, -0.3], [59.579, 3, -0.3], [60.038, 3, -0.1], [60.191, 3, -0.1], [60.804, 3, 0.1], [61.11, 3, 0.1], [61.416, 3, 0.1], [62.028, 3, -0.1], [62.64, 3, -0.3], [63.253, 3, -0.1], [63.559, 3, -0.1], [63.865, 3, -0.1], [64.477, 3, 0.1], [65.089, 3, 0.1], [65.702, 3, -0.1], [66.161, 3, -0.1], [66.314, 3, -0.1], [66.926, 3, 0.1], [67.232, 3, 0.1], [67.538, 3, 0.1], [68.151, 3, 0.3], [68.304, 3, 0.1], [68.457, 3, 0.1], [68.61, 3, 0.1], [68.763, 3, 0.1], [69.375, 3, -0.1], [69.681, 3, -0.3], [69.987, 3, -0.3], [70.6, 3, 0.1], [71.059, 3, 0.1], [71.212, 3, 0.1], [71.824, 3, 0.3], [72.13, 3, 0.3], [72.436, 3, 0.1], [73.049, 3, -0.1], [73.355, 3, -0.3], [73.661, 3, -0.3], [74.273, 3, 0.1], [74.885, 3, 0.1], [75.345, 3, 0.3], [75.498, 3, 0.3], [76.11, 3, 0.1], [76.416, 3, 0.1], [76.722, 3, 0.1], [77.334, 3, -0.1], [77.64, 3, -0.1], [77.947, 3, -0.1], [78.406, 3, -0.1], [78.559, 3, -0.1], [79.171, 3, 0.1], [79.477, 3, 0.1], [79.783, 3, 0.1], [80.243, 3, 0.3], [80.396, 3, 0.3], [80.855, 3, 0.1], [81.008, 3, 0.1], [81.62, 3, -0.1], [82.079, 3, 0.1], [82.232, 3, 0.1], [82.845, 3, -0.3], [83.151, 3, -0.3], [83.304, 3, -0.3], [83.457, 3, -0.3], [84.069, 3, 0.1], [84.375, 3, 0.1], [84.681, 3, 0.1], [85.294, 3, 0.3], [85.6, 3, 0.1], [85.906, 3, 0.1], [86.518, 3, -0.3], [86.977, 3, -0.1], [87.283, 3, -0.1], [87.589, 3, -0.1], [87.896, 3, -0.1], [88.202, 3, -0.1], [88.355, 3, -0.1], [88.967, 3, 0.1], [89.12, 3, 0.1], [89.579, 3, 0.1], [90.498, 3, -0.1], [90.651, 3, -0.1], [90.804, 3, -0.3], [91.416, 3, 0.1], [91.875, 3, 0.1], [92.334, 3, 0.1], [92.487, 3, 0.1], [92.64, 3, 0.1], [92.794, 3, 0.1], [92.947, 3, 0.1], [93.1, 3, 0.1], [93.253, 3, 0.1], [98.151, 3, 0.1], [98.457, 3, 0.1], [98.61, 3, 0.1], [98.763, 3, 0.1], [99.069, 3, 0.3], [99.375, 3, 0.1], [99.528, 3, 0.1], [99.681, 3, 0.1], [99.834, 3, 0.1], [99.987, 3, 0.1], [100.14, 3, 0.1], [100.294, 3, 0.1], [100.447, 3, 0.1], [100.6, 3, 0.1], [101.059, 3, -0.1], [101.212, 3, -0.3], [101.671, 3, -0.3], [101.824, 3, -0.3], [101.977, 3, -0.1], [102.13, 3, -0.1], [102.283, 3, 0.1], [102.436, 3, 0.1], [102.743, 3, 0.1], [103.049, 3, 0.3], [103.355, 3, 0.1], [103.508, 3, 0.1], [103.661, 3, 0.1], [104.12, 3, -0.3], [104.273, 3, -0.1], [104.579, 3, -0.1], [104.885, 3, -0.1], [105.191, 3, 0.1], [105.498, 3, 0.1], [105.957, 3, -0.1], [106.11, 3, 0.1], [106.569, 3, 0.1], [106.722, 3, 0.3], [106.875, 3, 0.1], [107.028, 3, 0.3], [107.181, 3, 0.1], [107.334, 3, 0.3], [107.487, 3, 0.1], [107.64, 3, 0.3], [107.794, 3, 0.1], [107.947, 3, 0.1], [108.406, 3, 0.1], [108.559, 3, 0.1], [109.018, 3, -0.1], [109.171, 3, -0.1], [109.477, 3, -0.1], [109.783, 3, -0.3], [110.089, 3, -0.1], [110.396, 3, 0.1], [110.855, 3, 0.3], [111.008, 3, 0.1], [111.314, 3, -0.1], [111.62, 3, -0.1], [111.773, 3, -0.1], [111.926, 3, -0.3], [112.079, 3, -0.1], [112.232, 3, -0.3], [112.385, 3, 0.1], [112.538, 3, -0.3], [112.691, 3, 0.1], [112.845, 3, -0.3], [113.304, 3, -0.1], [113.457, 3, 0.1], [113.916, 3, 0.1], [114.069, 3, 0.3], [114.222, 3, 0.3], [114.375, 3, 0.3], [114.528, 3, 0.3], [114.681, 3, 0.3], [114.834, 3, 0.3], [114.987, 3, 0.3], [115.14, 3, 0.3], [115.294, 3, 0.1], [115.753, 3, 0.1], [115.906, 3, -0.1], [116.365, 3, -0.3], [116.518, 3, -0.3], [116.671, 3, -0.1], [116.824, 3, -0.3], [116.977, 3, 0.1], [117.13, 3, -0.3], [117.283, 3, 0.1], [117.436, 3, -0.1], [117.589, 3, 0.1], [117.743, 3, 0.1], [118.202, 3, 0.3], [118.355, 3, 0.3], [118.814, 3, 0.1], [118.967, 3, 0.1], [119.273, 3, -0.3], [119.579, 3, 0.1], [119.885, 3, -0.3], [120.191, 3, 0.1], [120.651, 3, 0.1], [120.804, 3, 0.1], [121.263, 3, 0.3], [121.416, 3, 0.1], [121.569, 3, 0.3], [121.722, 3, 0.1], [122.028, 3, 0.1], [122.334, 3, -0.3], [122.487, 3, -0.1], [122.64, 3, -0.3], [123.1, 3, -0.3], [123.253, 3, -0.1], [123.712, 3, 0.1], [123.865, 3, 0.1], [124.018, 3, 0.3], [124.171, 3, 0.1], [124.324, 3, 0.3], [124.477, 3, 0.1], [124.63, 3, 0.3], [124.783, 3, 0.1], [124.936, 3, 0.3], [125.089, 3, 0.1], [125.549, 3, -0.3], [125.702, 3, -0.3], [126.161, 3, -0.3], [126.314, 3, -0.1], [126.467, 3, -0.1], [126.62, 3, 0.1], [126.926, 3, 0.1], [127.232, 3, 0.1], [127.538, 3, 0.1], [127.998, 3, 0.3], [128.151, 3, 0.3], [128.61, 3, 0.1], [128.763, 3, 0.1], [128.916, 3, -0.3], [129.069, 3, -0.1], [129.222, 3, -0.3], [129.375, 3, -0.3], [129.528, 3, -0.3], [129.681, 3, -0.3], [129.834, 3, -0.1], [129.987, 3, -0.1], [130.447, 3, 0.1], [130.6, 3, 0.1], [131.059, 3, 0.3], [131.212, 3, 0.3], [131.365, 3, 0.1], [131.518, 3, 0.1], [131.671, 3, 0.1], [131.824, 3, 0.1], [131.977, 3, -0.1], [132.13, 3, 0.1], [132.283, 3, -0.1], [132.436, 3, -0.3], [132.896, 3, -0.3], [133.049, 3, -0.1], [133.508, 3, 0.1], [133.661, 3, 0.1], [133.814, 3, 0.1], [133.967, 3, 0.3], [134.12, 3, 0.3], [134.273, 3, 0.1], [134.426, 3, 0.1], [134.579, 3, 0.3], [134.732, 3, 0.3], [134.885, 3, 0.1], [135.345, 3, -0.1], [135.498, 3, -0.1], [136.11, 3, 0]
		var jsonArray = jsonNode ["beatmap"].AsArray;
		for (int i = 0; i < jsonArray.Count; i++) {
			var item = jsonArray [i].AsArray;
			beatmap.Add (new note (item [0].AsFloat, item [1].AsFloat, item [2].AsFloat, false));
		}

		noteIterator = beatmap.GetEnumerator ();
		noteIterator.MoveNext ();
		nextNote = noteIterator.Current;
	}

	void SetPositions ()
	{
		// 屏幕 4／10 高
		maxJudgePosY = mainCamera.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0.425f, 0)).y;
		// 屏幕 1／10 高
		judgelinePosY = mainCamera.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0.125f, 0)).y;
		// 屏幕 11／10 高
		noteCreatePosY = mainCamera.ScreenToWorldPoint (new Vector3 (0, Screen.height * 1.1f, 0)).y;
		// 屏幕顶端
		screenTopY = mainCamera.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y;
		// 屏幕底部
		screenBottomY = mainCamera.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
		screenWidth = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x * 2;
	}

	void CheckStartUpdate ()
	{
		if (unstart && JudgeUpdate ()) {
			unstart = false;
			pause = false;
		}
	}

	void CheckEndUpdate ()
	{
		if (!success && !failed) {
			if (musicTime > musicStopTime - 3.2f) {
				if (score >= minPassScore) {
					success = true;
				} else {
					failed = true;
					music.mute = true;
					GetComponent<AudioSource>().Play();
				}
			}
		}
		if ((musicTime > (musicStopTime - 1.3)) && notFadeOut) {
			notFadeOut = false;
			fadeAnim.Play("EndFadeCover");
		}
		Debug.Log(musicTime + "  |  " + musicStopTime);
		if (musicTime > (musicStopTime)) {
			Debug.Log("WTFFFF");
			if (success) {
				PlayerPrefs.SetInt("NextVersion", version + 1);
				SceneManager.LoadScene("Loading");
			} else {
				PlayerPrefs.SetInt("NextVersion", version);
				SceneManager.LoadScene("Failed");
			}
		}
	}

	void NoteGeneratorUpdate ()
	{
		if (createdLastNote)
			return;

		float offset = (noteCreatePosY - judgelinePosY) / noteSpeed;
		while (nextNote.time < musicTime + offset) {
			float posX = nextNote.pos * screenWidth;
			float posY = judgelinePosY + (nextNote.time - musicTime) * noteSpeed;
			Vector3 createPos = new Vector3 (posX, posY, 0);
			GameObject prefab = nextNote.slide ? slideNotePrefab : notePrefab;
			prefab = Instantiate (prefab, createPos, Quaternion.identity) as GameObject;
			prefab.GetComponent<NoteBehaviour> ().Init (this);
			noteObjects.Add (prefab);

			if (noteIterator.MoveNext ()) {
				nextNote = noteIterator.Current;
			} else {
				createdLastNote = true;
				break;
			}
		}
	}

	bool JudgeUpdate ()
	{
		if (noteObjects.Count <= 0)
			return false;
		
		float bottomNotePosY = noteObjects [0].transform.position.y;
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			// 鼠标点击位置 和 最下方 note 位置 都在判定区域内
			if (mousePos.y < maxJudgePosY && bottomNotePosY < maxJudgePosY) {
				float posX = noteObjects [0].transform.position.x;
				// 鼠标点的是否是这个 note
				// TODO 不应该用nextnode 而是最后一个Object
				if (mousePos.x < posX + nextNote.width / 2 &&
				    mousePos.x > posX - nextNote.width / 2) {
					GameObject fxPrefab = null;
					GameObject textPrefab = null;
					int judgeCode = Mathf.FloorToInt (Mathf.Abs (bottomNotePosY - judgelinePosY) / ((maxJudgePosY - judgelinePosY) / 3));
					if (cheat)
						judgeCode = 0;
					switch (judgeCode) {
					case 2:
						missCount++;
						if (missCount > maxCanMiss)
						{
							minPassScore = int.MaxValue;
							musicStopTime = music.time + 3.2f;
						}
						textPrefab = missTextPrefab;
						Debug.Log ("Miss");
						break;
					case 1:
						score += 100;
						fxPrefab = goodFXPrefab;
						textPrefab = goodTextPrefab;
						Debug.Log ("Good");
						break;
					case 0:
						score += 300;
						fxPrefab = perfectFXPrefab;
						textPrefab = perfectTextPrefab;
						Debug.Log ("Perfect");
						break;
					default:
						Debug.LogWarning ("Bad Note Position: " + judgeCode);
						break;
					}
					DestroyNoteShowJudge (textPrefab, fxPrefab, posX);
					return true;
				}
			}
		}
		if (bottomNotePosY < screenBottomY) {
			Debug.Log ("Miss");
			missCount++;
			if (missCount > maxCanMiss)
			{
				minPassScore = int.MaxValue;
				musicStopTime = music.time + 3.2f;
			}
			DestroyNoteShowJudge (missTextPrefab, null, 0, false);
		}
		return false;
	}

	void UIUpdate ()
	{
		scoreLabel.text = "Score: " + score;
	}

	public void DestroyNoteShowJudge (GameObject text, GameObject fx = null, float posX = 0f, bool playHitFx = true)
	{
		ShowText (text);
		if (version > 0) {
			ShowFX (fx, posX);
		}
		RemoveFirstNote ();
		if (playHitFx && hitFX) {
			if (hitClips.Length > 0) {
				hitFX.clip = hitClips[Random.Range(0, hitClips.Length)];
			}
			hitFX.Play();
		}
	}

	public void RemoveFirstNote ()
	{
		Destroy (noteObjects [0]);
		noteObjects.RemoveAt (0);
	}

	public void ShowFX (GameObject fx, float posX)
	{
		if (fx == null)
			return;

		fx = Instantiate (fx, new Vector3 (posX, 0, 0), Quaternion.identity) as GameObject;
	}

	public void ShowText (GameObject text)
	{
		if (text == null)
			return;

		text = Instantiate (text, canvas.transform, false) as GameObject;
	}
}
