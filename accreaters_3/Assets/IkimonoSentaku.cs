using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IkimonoSentaku : MonoBehaviour {
	public AudioClip SEstart;
    // Use this for initialization
    void Start () {
		GetComponent<AudioSource> ().PlayOneShot (SEstart);
    }
     
    // Update is called once per frame
   void Update () {
	if (Input.GetKey(KeyCode.A)){
		SceneLoad2();
	}
		if (Input.GetKey(KeyCode.S)){
			SceneLoad3();
		}
		if (Input.GetKey(KeyCode.D)){
			SceneLoad4();
		}
		if (Input.GetKey(KeyCode.F)){
			SceneLoad5();
		}
		if (Input.GetKey(KeyCode.G)){
			SceneLoad6();
		}
		if (Input.GetKey(KeyCode.H)){
			SceneLoad7();
		}
		if (Input.GetKey(KeyCode.J)){
			SceneLoad8();
		}
		if (Input.GetKey(KeyCode.Q)){
			SceneLoad1();
		}
		if (Input.GetKey(KeyCode.K)){
			SceneLoad9();
		}
		if (Input.GetKey(KeyCode.L)){
			SceneLoad10();
		}
}
	
	public void SceneLoad1(){
		SceneManager.LoadScene ("IkimonoSentaku");
	}
	public void SceneLoad2(){
		SceneManager.LoadScene ("Ikimono2sDolphin");
	}		
	public void SceneLoad3(){
		SceneManager.LoadScene ("Ikimono3sfox");
	}
	public void SceneLoad4(){
		SceneManager.LoadScene ("Ikimono4sdinaso");
	}
	public void SceneLoad5(){
		SceneManager.LoadScene ("Ikimono5sdragon");
	}
	public void SceneLoad6(){
		SceneManager.LoadScene ("Ikimono6srabbit");
	}	
	public void SceneLoad7(){
			SceneManager.LoadScene ("Ikimono7spenguin");
	}
	public void SceneLoad8(){
		SceneManager.LoadScene ("Ikimono8spuppy");
	}	
	public void SceneLoad9(){
		SceneManager.LoadScene ("Ikimono9_tiger");
	}
	public void SceneLoad10(){
		SceneManager.LoadScene ("Ikimono10_cat");
	}
}