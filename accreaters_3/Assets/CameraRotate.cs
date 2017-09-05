using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraRotate : MonoBehaviour
{
	public float RotateSpeed = 0.1f;
	public float UpDownSpeed = 0.01f;
	public int updownflags,lrflags;

	float a;

	void Start(){
		a = this.transform.eulerAngles.y;
		updownflags = 0;
		lrflags = 0;
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.A) && lrflags > -15) {
			transform.Rotate (new Vector3 (0, -1, 0));
			lrflags -= 1;
		}
		if (Input.GetKey (KeyCode.D) && lrflags < 15) {
			transform.Rotate (new Vector3 (0, 1, 0));
			lrflags += 1;
		}
		if (Input.GetKey (KeyCode.W) && updownflags > -5) {
			transform.Rotate (new Vector3 (-1, 0, 0));
			updownflags -= 1;
		}
		if (Input.GetKey (KeyCode.S) && updownflags < 5) {
			transform.Rotate (new Vector3 (1, 0, 0));
			updownflags += 1;
		}

		//bool isPush = Input.GetMouseButton( 0 ); 
		if (Input.GetKey(KeyCode.M)){
			// 移動量 
			transform.rotation = Quaternion.Euler(0, 0, 0);
			updownflags = 0;
		}

	}
}