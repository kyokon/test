using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.IO.Ports;
using System.Runtime.InteropServices;

public class behaviour_key : MonoBehaviour {

	Animator animator;
	public static int biglimit;
	public static SerialLib.MyClass serial;
	int getValue_biglimit;

	// Use this for initialization
	void Start () {
		animator = GetComponent (typeof(Animator)) as Animator;
		serial = new SerialLib.MyClass ("COM6", 9600, 256);
		serial.ThreadStart ();
		biglimit = 0;
	}

	// Update is called once per frame
	void Update () {
			/*try{
				if (Input.GetKey(KeyCode.Z)){
					animator.Play ("Idle");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				//if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("2");}
				}
				if (Input.GetKey(KeyCode.X)){
					animator.Play ("walk");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				//if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("3");}
				}
				if (Input.GetKey(KeyCode.C)){
					animator.Play ("run");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				//if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("4");}
				}
				if (Input.GetKey(KeyCode.V)){
					animator.Play ("hit");
				serial.Write ("2");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				//if(getValue_biglimit == 1){serial.Write ("10");}else{serial.Write ("5");}
				}
				if (Input.GetKey(KeyCode.B)){
					animator.Play ("sound");

				//getValue_biglimit = behaviour_key.getBiglimit ();
				//if(getValue_biglimit == 1){serial.Write ("11");}else{serial.Write ("6");}
				}
				/*if (Input.GetKey(KeyCode.N)){//tamago
				//animator.Play ("hit");
				//	serial.Write ("1");
				//Debug.Log ("Starta");
				}
			    if (Input.GetKey(KeyCode.O)){//tamago
					//serial.Write ("0");
				}
			}
			catch(Exception){

			}*/

	}

/*	private void toAdults(){
		if (Input.GetKey (KeyCode.T) && biglimit == 0) {
			this.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x + 0.015f,
				gameObject.transform.localScale.y + 0.015f,
				gameObject.transform.localScale.z + 0.015f
			);
			biglimit = 1;
		}
	}*/
	public static int getBiglimit(){
		return biglimit;
	}
}
