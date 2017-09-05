#pragma strict

var ball:Transform;

function Update () {
	var i = Random.Range(1,150);
	if(i%149==0){
	Instantiate(ball,transform.position,transform.rotation);
	}
}
