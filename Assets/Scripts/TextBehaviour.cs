using UnityEngine;
using System.Collections;

public class TextBehaviour : MonoBehaviour
{
	Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Rank_Appear")) {
			Destroy (gameObject);
		}
	}
}
