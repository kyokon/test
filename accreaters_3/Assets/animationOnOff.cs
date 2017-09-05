using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationOnOff : MonoBehaviour {
	int anime_flag;

	// Use this for initialization
	void Start () {
		anime_flag = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FlagsOfAnimation_Start(){
		if(anime_flag == 0){
			anime_flag = 1;
			Debug.Log ("Start");
		}
	}
	void FlagsOfAnimation_End(){
		if (anime_flag == 1) {
			anime_flag = 0;
			Debug.Log ("End");
		}
	}
}
