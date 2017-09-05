using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class dolphin_behaviour : MonoBehaviour {
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent (typeof(Animator)) as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Z)){
			animator.Play ("dolphinanime_idle");
		}
		if (Input.GetKey(KeyCode.X)){
			animator.Play ("dolphinanime_walk");
		}
		if (Input.GetKey(KeyCode.C)){
			animator.Play ("dolphinanime_run");
		}
		if (Input.GetKey(KeyCode.V)){
			animator.Play ("dolphinanime_hit");
		}
		if (Input.GetKey(KeyCode.B)){
			animator.Play ("dolphinanime_voice");
		}
	}
}
