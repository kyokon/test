using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Runtime.InteropServices;

public class SerialTest : MonoBehaviour {
	public GameObject rocket;
	public static SerialLib.UnitySerial serial;

	Animator animator;
	int r;

	void Start()
	{
		serial = new SerialLib.UnitySerial ("COM4", 115200, 256);
		serial.ThreadStart ();
		animator = GetComponent<Animator> ();
	}

	void Update()
	{
		double number = double.Parse (serial.GetData ());
		if (number>10000) {
			animator.Play ("hit");
		}
		if (number<=10000) {
			animator.Play ("walk");
		}
		Debug.Log(serial.GetData());
	}

	void OnDestroy()
	{
		serial.ThreadEnd ();
	}
}