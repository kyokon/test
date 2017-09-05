using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicescript : MonoBehaviour {
	public AudioClip SE;
	public AudioClip SE2;
	int  var_random;

	// Use this for initialization
	void Start () {
		//AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		//audioSource.clip = flip;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPlayer () {
		//audioSource.Play();
		//var_random=Random.value;
		var_random = UnityEngine.Random.Range(0, 9);
		if (var_random >= 0) {
			GetComponent<AudioSource> ().PlayOneShot (SE);
		} else {
		}
	}
	void OnPlayer2 () {
		//audioSource.Play();
		var_random = UnityEngine.Random.Range(0, 9);
		if (var_random >= 0) {
			GetComponent<AudioSource>().PlayOneShot(SE2);
		} else {
		}
	}
}

