using UnityEngine;
using System.Collections;

public class NoteBehaviour : MonoBehaviour
{
	public GameManager gm;
	float speed = 5f;

	void Start ()
	{
	
	}

	void Update ()
	{
		if (gm.pause)
			return;
		transform.Translate (0, -speed * Time.deltaTime, 0);
	}

	public void Init (GameManager gameManager)
	{
		gm = gameManager;
		speed = gm.noteSpeed;
	}
}
