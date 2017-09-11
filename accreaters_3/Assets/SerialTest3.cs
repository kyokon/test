using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SerialTest3 : MonoBehaviour {
    public static SerialLib.MyClass serial;
    Animator animator;
    int anime_flag;
    private int rand, rand2, rand3, rand4;
    private double number;
    int getValue_biglimit;
    int Sensornumber;
    int Flags_SensorRW;
    int  flag_sensorAnimation;

    public AudioClip SE, SE2, SE3, SE4, SE5, SE6;
    double before_number,try_number;

    //以下画面フェード用変数
    public bool enableFade = true;
    public bool enableFadeIn = true;
    public bool enableFadeOut = true;

    public float speed = 0.02f;

    public Image FadeImage;

    private float count = 1f;

    //private bool enableAlphaTop = false;

    public delegate void FadeOutCallback();
    public FadeOutCallback fadeoutcallback;

    //ここまで
    Image image;
    //int serial_flag;

    void Start()
    {

        enableFade = true;
        enableFadeIn = true;
        setAlpha (FadeImage, count);

        animator = GetComponent<Animator> ();
        serial = new SerialLib.MyClass ("COM6", 9600, 256);
        serial.ThreadStart ();
        anime_flag = 0;
        //serial.Write ("1");
        //serial_flag = 0;
        Sensornumber = 0;
        number = 0;
        rand3 = 0;
        rand4 = 0;
        getValue_biglimit = 0;
        Flags_SensorRW = 0;
        before_number = 0;
        try_number = 0;
        flag_sensorAnimation = 0;


        Debug.Log("OpenMode");

    }

    void Update()
    {
        //Wakeupを自動で始めに行う
        if (enableFadeIn) {
            FadeIn (FadeImage);
        }
        //ここまで
        //以下振動子が必要なプログラム
        getValue_biglimit = getBiglimit();//大人か子供か判断

        Debug.Log ("getValue_biglimit"+getValue_biglimit);
        if (Flags_SensorRW == 0) {
            try{
                SensorReading ();//圧力センサーの値をとってくる

            }
            catch{
                Debug.Log("error"+number); 
                number = 8.10;
            }
        } 

        try{
            Debug.Log(number);
            Debug.Log ("anime_flag"+anime_flag);
            trytoKey();
            toAdults ();

            Debug.Log ("flag_sensorAnimation"+flag_sensorAnimation);
            if(flag_sensorAnimation == 0){
                SensorAnimation(number);
            }
        }catch{
        }
        flag_sensorAnimation = 0;

        //Debug.Log(count); //フェード確認用

        toWakeUp ();

        toSleeping ();
    }

    void SensorReading(){
        //serial.Write ("r");
        Debug.Log (serial.GetData ());

        if (serial.GetData () == null) {
            try_number = 1.0;
        } else {
            try_number = double.Parse (serial.GetData ());
            if (try_number != before_number) {
                number = try_number;
            } else {
                flag_sensorAnimation = 1;
            }

            before_number = try_number;
        }
        Debug.Log (number);//コンソールに常に読み込んだ圧力センサーの値を表示
    }

    void SensorAnimation(double number){

        if (number > 1500.0 && number <8000.0) {//圧力の強さによってアニメーションを切り替える

            Debug.Log ("Into SensorAnimation");
            if (anime_flag == 0) {
                Flags_SensorRW = 1;

                FlagsOfAnimation_Start ();//Today
                if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}//Today
                animator.Play ("hit");
                Sounder(4);
                FlagsOfAnimation_End ();//Today
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } 
        else if (number > 8000 && number<20000) {
            if (anime_flag == 0) {
                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
                animator.Play ("run");
                Sounder(5);
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;

            }
        } else if (number >= 20000 && number<35000) {
            if (anime_flag == 0) {
                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
                animator.Play ("sound");
                Sounder(3);
                FlagsOfAnimation_End ();
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } else if (number >= 35000 && number<175000) {
            if (anime_flag == 0) {
                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("5");}
                animator.Play ("hit");
                serial.Write ("2");
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } else {
            rand = UnityEngine.Random.Range (0, 20);//ランダムな時間にアニメーション再生（圧力センサが動いていないとき）
            if (rand==10) {

                Debug.Log ("Into RandomAnimation");
                //RandomAnimation ();
            }else{
                Flags_SensorRW = 1;

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        }
    }

    void RandomAnimation(){//４つのアニメーションをランダムに選択再生
        rand2 = UnityEngine.Random.Range (0, 4);
        if (rand2 == 0) {
            if (anime_flag == 0) {

                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
                animator.Play ("sound");
                Sounder(3);
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } else if (rand2 == 1) {
            if (anime_flag == 0) {

                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("5");}
                animator.Play ("hit");
                serial.Write ("2");
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } else if (rand2 == 2) {
            if (anime_flag == 0) {

                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}
                animator.Play ("walk");
                Sounder(4);
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
        } else {
            if (anime_flag == 0) {

                Flags_SensorRW = 1;
                FlagsOfAnimation_Start ();
                if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
                animator.Play ("run");
                Sounder(5);
                FlagsOfAnimation_End ();

                DelayMethod (60);
                Flags_SensorRW = 0;
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
                Flags_SensorRW = 1;
                animator.Play ("Idle");
                Sounder(1);
                serial.Write ("2");
                Debug.Log("IdelButtonOn");

                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.X)){
                Flags_SensorRW = 1;
                animator.Play ("walk");
                Sounder(4);
                if(getValue_biglimit == 1){serial.Write ("7");}else{serial.Write ("3");}

                Debug.Log("WalkButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.C)){
                Flags_SensorRW = 1;
                animator.Play ("run");
                Sounder(5);
                if(getValue_biglimit == 1){serial.Write ("8");}else{serial.Write ("4");}
                Debug.Log("RunButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.V)){
                Flags_SensorRW = 1;
                animator.Play ("hit");
                serial.Write ("2");
                if(getValue_biglimit == 1){serial.Write ("9");}else{serial.Write ("5");}
                Debug.Log("HitButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.B)){
                Flags_SensorRW = 1;
                animator.Play ("sound");
                Sounder(3);
                if(getValue_biglimit == 1){serial.Write ("s");}else{serial.Write ("6");}
                Debug.Log("SoundButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKey(KeyCode.N)){//tamago
                Flags_SensorRW = 1;
                serial.Write ("1");
                Debug.Log("EGGButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKey(KeyCode.O)){//Arduino reset
                Flags_SensorRW = 1;
                serial.Write ("0");
                Debug.Log("OFF");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
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

    private void toSleeping(){
        if (Input.GetKey (KeyCode.P)) {

            serial.Write ("0");
            enableFade = true;
            if (enableFadeOut) {
                FadeOut (FadeImage);
            }
        }
    }


    private void toWakeUp(){
        if (Input.GetKey (KeyCode.K)) {

            //serial.Write ("0");

            enableFade = true;
            if (enableFadeIn) {
                FadeIn (FadeImage);
            }
        }
    }


    private int getBiglimit(){
        return getValue_biglimit;
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

    private IEnumerator DelayMethod(int delayFrameCount)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
    }

    //以下画面フェード用関数
    void setFadeOutCallback (FadeOutCallback fc) {
        this.fadeoutcallback = fc;      
    }

    void setAlpha(Image image,float alpha) {
        image.color = new Color (image.color.r, image.color.g, image.color.b, alpha);
    }

    public void FadeOut(Image image) {
        if (enableFade) {
            count += speed;
            setAlpha (image, count);
            if (image.color.a >= 0.98f) {

                enableFade = false;
                if (enableFadeOut) {

                    Debug.Log("SleepMode");
                    // if(callback != null) callback ();//なんか動かなかった
                } 
            }
        }
    }

    public void FadeIn(Image image) {
        if (enableFade) {
            count -= speed;
            setAlpha (image, count);
            if (image.color.a <= 0.03f) {
                enableFade = false;
                enableFadeIn = false;

                Debug.Log("SleepMode");
            }
        }
    }
    //ここまでフェード
}