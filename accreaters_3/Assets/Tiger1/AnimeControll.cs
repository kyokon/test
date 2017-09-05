using UnityEngine;
using System.Collections;

public class AnimeControll : MonoBehaviour {

	Animator animator;

	// ゲーム初期化処理
	void Start () {
		animator = GetComponent<Animator>();
	}

	// frameごとに呼び出される
	void Update () {
		if(Input.GetKeyDown("up")) {
			animator.Play ("hit");
		}
	}
}
