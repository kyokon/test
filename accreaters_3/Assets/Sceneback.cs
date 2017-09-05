using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Sceneback : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q)){
			SceneLoad1();
		}
	}

	public void SceneLoad1(){
		SceneManager.LoadScene ("IkimonoSentaku");
	}
}
