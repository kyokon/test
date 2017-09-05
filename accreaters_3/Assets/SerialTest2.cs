﻿using UnityEngine;
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
	private int rand, rand2;
	private double number;
	int getValue_biglimit;
	//int serial_flag;

	void Start()
	{
		animator = GetComponent<Animator> ();
		serial = new SerialLib.MyClass ("COM6", 9600, 256);
		serial.ThreadStart ();
		anime_flag = 0;
		serial.Write ("1");
		//serial_flag = 0;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.N)) {//tamago
				animator.Play ("hit");
				serial.Write ("1");
				//serial_flag = 1;

		} else if (Input.GetKeyDown (KeyCode.O)) {//tamago
				serial.Write ("0");
				//serial_flag = 1;

		} else if (Input.GetKeyDown (KeyCode.V)) {//tamago
				serial.Write ("2");
				//serial_flag = 1;
		}


		getValue_biglimit = behaviour_key.getBiglimit ();
		number = double.Parse(serial.GetData ());//圧力センサーの値をとってくる
		//SensorAnimation(number);
		//Debug.Log (number);//コンソールに常に読み込んだ圧力センサーの値を表示
		trytoKey();
		toAdults ();
	}

	void SensorAnimation(double number){
		if (number > 500 && number <1000) {//圧力の強さによってアニメーションを切り替える
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("11");}else{serial.Write ("6");}
				animator.Play ("sound");
				FlagsOfAnimation_End ();
			}
		} else if (number > 10000 && number<11000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("10");}else{serial.Write ("5");}
				animator.Play ("hit");
				FlagsOfAnimation_End ();
			}
		} else if (number > 20000 && number<21000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("3");}
				animator.Play ("walk");
				FlagsOfAnimation_End ();
			}
		} else if (number > 40000 && number<41000) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("4");}
				animator.Play ("run");
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
				//if(getValue_biglimit == 1){serial.Write ("11");}else{serial.Write ("6");}
				animator.Play ("sound");
				FlagsOfAnimation_End ();
			}
		} else if (rand2 == 1) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("10");}else{serial.Write ("5");}
				animator.Play ("hit");
				FlagsOfAnimation_End ();
			}
		} else if (rand2 == 2) {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("3");}
				animator.Play ("walk");
				FlagsOfAnimation_End ();
			}
		} else {
			if (anime_flag == 0) {
				FlagsOfAnimation_Start ();
				//if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("4");}
				animator.Play ("run");
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
		}
	}
}