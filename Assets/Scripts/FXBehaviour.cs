using UnityEngine;
using System.Collections;

public class FXBehaviour : MonoBehaviour
{
	Animation anim;

	void Start ()
	{
		anim = GetComponent<Animation> ();
	}

	void Update ()
	{
		if (!anim.isPlaying)
			Destroy (gameObject);
	}
}
