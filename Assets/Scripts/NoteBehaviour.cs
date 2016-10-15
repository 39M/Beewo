using UnityEngine;
using System.Collections;

public class NoteBehaviour : MonoBehaviour {
	public GameManager gm;
	float speed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, -speed * Time.deltaTime, 0);
	}

	public void Init(GameManager gameManager)
	{
		gm = gameManager;
		speed = gm.noteSpeed;
	}
}
