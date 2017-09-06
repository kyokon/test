using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicescript : MonoBehaviour {
	public AudioClip SE, SE2, SE3, SE4, SE5;
	//int  var_random;

	// Use this for initialization
	void Start () {
		//AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		//audioSource.clip = flip;
	}

	// Update is called once per frame
	void Update () {

	}

	/*void OnPlayer () {
		//audioSource.Play();
		//var_random=Random.value;
		//var_random = UnityEngine.Random.Range(0, 9);
		//if (var_random >= 0) {
			GetComponent<AudioSource> ().PlayOneShot (SE);
		//} else {
		//}
	}*/
	void OnPlayer () {
		GetComponent<AudioSource> ().PlayOneShot (SE);
	}

	void OnPlayer2 () {
		GetComponent<AudioSource>().PlayOneShot(SE2);
	}

	void OnPlayer3 () {
		GetComponent<AudioSource>().PlayOneShot(SE3);
	}

	void OnPlayer4 () {
		GetComponent<AudioSource>().PlayOneShot(SE4);
	}

	void OnPlayer5 () {
		GetComponent<AudioSource>().PlayOneShot(SE5);
	}
}

