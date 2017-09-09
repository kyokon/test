using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SerialTest2 : MonoBehaviour {
	public static SerialLib.MyClass serial;
	Animator animator;
	int anime_flag;
	private int rand, rand2, rand3, rand4;
	private double number;
	int getValue_biglimit;
	int Sensornumber;

	public AudioClip SE, SE2, SE3, SE4, SE5, SE6;
	//int serial_flag;

	void Start()
	{
		animator = GetComponent<Animator> ();
		serial = new SerialLib.MyClass ("COM6", 9600, 256);
		serial.ThreadStart ();
		anime_flag = 0;
		serial.Write ("1");
		//serial_flag = 0;
		Sensornumber = 0;
		number = 0;
		rand3 = 0;
		rand4 = 0;
		getValue_biglimit = 0;
	}

	void Update()
	{

		getValue_biglimit = behaviour_key.getBiglimit ();
		SensorReading();//圧力センサーの値をとってくる

		SensorAnimation (number);

		trytoKey();
		toAdults ();
	}

	void SensorReading(){
		serial.Write ("r");
		if (serial.GetData () == null) {
			number = 1.0;
		} else {
			number = double.Parse (serial.GetData ());
			Debug.Log (number);//コンソールに常に読み込んだ圧力センサーの値を表示
		}

		/*serial.Write ("t");
		if (serial.GetData () == null) {
			Sensornumber = 1;
		} else {
			Sensornumber = int.Parse (serial.GetData ());
		}*/

	}

	void SensorAnimation(double number){
		if (number > 1500 && number <3000) {//圧力の強さによってアニメーションを切り替える
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}
				animator.Play ("walk");
				Sounder(4);
				FlagsOfAnimation_End ();
			}
		} else if (number > 3000 && number<6000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
				animator.Play ("run");
				Sounder(5);
				FlagsOfAnimation_End ();
			}
		} else if (number > 6000 && number<9000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
				animator.Play ("sound");
				Sounder(3);
				FlagsOfAnimation_End ();
			}
		} else if (number > 9000 && number<41000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("5");}
				animator.Play ("hit");
				FlagsOfAnimation_End ();
			}
		} else {
			int rand = UnityEngine.Random.Range (0, 20);//ランダムな時間にアニメーション再生（圧力センサが動いていないとき）
			if (rand==10) {
				RandomAnimation ();
			}else{
				serial.Write("0");//Arduinoに0を送信し、LEDを消灯させる
			}
		}
	}

	void RandomAnimation(){//４つのアニメーションをランダムに選択再生
		rand2 = UnityEngine.Random.Range (0, 4);
		if (rand2 == 0) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
				animator.Play ("sound");
				Sounder(3);
				FlagsOfAnimation_End ();
			}
		} else if (rand2 == 1) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("");}else{serial.Write ("5");}
				animator.Play ("hit");
				FlagsOfAnimation_End ();
			}
		} else if (rand2 == 2) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}
				animator.Play ("walk");
				Sounder(4);
				FlagsOfAnimation_End ();
			}
		} else {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
				animator.Play ("run");
				Sounder(5);
				FlagsOfAnimation_End ();
			}
		}
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

	void OnDestroy()
	{
		serial.Write("0");
		serial.ThreadEnd ();
	}

	private void trytoKey(){
		try{
			if (Input.GetKeyDown(KeyCode.Z)){
				animator.Play ("Idle");
				Sounder(1);
				//serial.Write ("2");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				serial.Write ("2");
				Debug.Log("IdelButtonOn");
			}
			if (Input.GetKeyDown(KeyCode.X)){
				animator.Play ("walk");
				Sounder(4);
				//serial.Write ("3");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}

				Debug.Log("WalkButtonOn");
			}
			if (Input.GetKeyDown(KeyCode.C)){
				animator.Play ("run");
				Sounder(5);

				//serial.Write ("4");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
				Debug.Log("RunButtonOn");
			}
			if (Input.GetKeyDown(KeyCode.V)){
				animator.Play ("hit");

				//serial.Write ("5");
				//serial.Write ("2");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("5");}
				Debug.Log("HitButtonOn");
			}
			if (Input.GetKeyDown(KeyCode.B)){
				animator.Play ("sound");
				Sounder(3);

				Debug.Log("SoundButtonOn");
				//serial.Write ("6");
				//getValue_biglimit = behaviour_key.getBiglimit ();
				if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
			}
			/*if (Input.GetKey(KeyCode.N)){//tamago
				//animator.Play ("hit");
				//	serial.Write ("1");
				//Debug.Log ("Starta");
				}
			    if (Input.GetKey(KeyCode.O)){//tamago
					//serial.Write ("0");
				}*/
		}
		catch(Exception){

		}
	}

	private void toAdults(){
		if (Input.GetKey (KeyCode.T) && getValue_biglimit == 0) {
			this.transform.localScale = new Vector3 (
				gameObject.transform.localScale.x + 0.015f,
				gameObject.transform.localScale.y + 0.015f,
				gameObject.transform.localScale.z + 0.015f
			);
			getValue_biglimit = 1;
			serial.Write ("a");
		}
	}

	private void Sounder(int soundnumber){
		rand3 = UnityEngine.Random.Range (0, 2);
		if (rand3 == 1) {
			switch (soundnumber) {
			case '1':
				OnPlayer (); break;
			case '2':
				OnPlayer2 (); break;
			case '3':
				OnPlayer3 (); break;
			case '4':
				OnPlayer4 (); break;
			case '5':
				OnPlayer5 (); break;
			default:
				break;
			}
		}
	}

	void OnPlayer () {
		rand4 = UnityEngine.Random.Range (0, 5);
		if (rand4 == 1) {
			GetComponent<AudioSource> ().PlayOneShot (SE);
		} else if (rand4 == 5) {
			GetComponent<AudioSource> ().PlayOneShot (SE6);
		}
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