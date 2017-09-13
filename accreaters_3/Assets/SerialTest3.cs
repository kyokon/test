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
    int ArduinoStartEnd;

    public AudioClip SE, SE2, SE3, SE4, SE5, SE6, SE7, SE8, SE9;
    double before_number,try_number;

    //以下画面フェード用変数
    public bool enableFade = true;
    public bool enableFadeIn = true;
    public bool enableFadeOut = true;
    public bool enableFadeOn = true;

    public float speed = 0.01f;

    public Image FadeImage;

    private float count = 1f;


    private bool enableAlphaTop = true;


    //ここまで
    Image image;
    //int serial_flag;
    int flag_fadeon;

    void Start()
    {

        enableFade = true;
        enableFadeIn = true;
        setAlpha (FadeImage, count);

        animator = GetComponent<Animator> ();
        serial = new SerialLib.MyClass ("COM6", 9600, 256);
        serial.ThreadStart ();
        anime_flag = 0;
        Sensornumber = 0;
        number = 0;
        rand3 = 0;
        rand4 = 0;
        getValue_biglimit = 0;
        Flags_SensorRW = 0;
        before_number = 0;
        try_number = 0;
        flag_sensorAnimation = 0;
        flag_fadeon = 0;
        ArduinoStartEnd = 0;

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


        Debug.Log("ArduinoStartEnd"+ArduinoStartEnd);
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
            if((flag_sensorAnimation == 0) && (ArduinoStartEnd == 1)){
                //SensorAnimation(number);
            }
        }catch{
        }
        flag_sensorAnimation = 0;

        //Debug.Log(count); //フェード確認用

        toWakeUp ();

        toSleeping ();


        Debug.Log ("UpdateFade"+flag_fadeon);

        if(flag_fadeon == 1){

            if (enableFadeOn) {

                Debug.Log ("WorkingFadeOn");
                FadeInAndOut (FadeImage);
            }
        }
    }

    void SensorReading(){
        //serial.Write ("r");
        //Debug.Log (serial.GetData ());

        if (serial.GetData () == null) {
            try_number = 8.10;
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
        try{//なつき
            if (Input.GetKeyDown(KeyCode.Z) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("Idle");
                OnPlayer ();
                serial.Write ("2");
                Debug.Log("IdelButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.X) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("walk");
                if(getValue_biglimit == 1){
                    serial.Write ("7");GetComponent<AudioSource>().PlayOneShot(SE);
                }else{
                    serial.Write ("3");GetComponent<AudioSource>().PlayOneShot(SE);
                }
                Debug.Log("WalkButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.C) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("run");
                if(getValue_biglimit == 1){
                    serial.Write ("8");GetComponent<AudioSource>().PlayOneShot(SE2);
                }else{
                    serial.Write ("4");GetComponent<AudioSource>().PlayOneShot(SE2);
                }
                Debug.Log("RunButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.V) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("hit");
                if(getValue_biglimit == 1){
                    serial.Write ("9");GetComponent<AudioSource>().PlayOneShot(SE3);
                }else{
                    serial.Write ("5");GetComponent<AudioSource>().PlayOneShot(SE3);
                }
                Debug.Log("HitButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.B) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("sound");
                if(getValue_biglimit == 1){
                    serial.Write ("s");GetComponent<AudioSource>().PlayOneShot(SE4);
                }else{
                    serial.Write ("6");GetComponent<AudioSource>().PlayOneShot(SE4);
                }
                Debug.Log("SoundButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }

            //以下怒り
            if (Input.GetKeyDown(KeyCode.H) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("walk");
                if(getValue_biglimit == 1){
                    serial.Write ("i");GetComponent<AudioSource>().PlayOneShot(SE5);
                }else{
                    serial.Write ("r");GetComponent<AudioSource>().PlayOneShot(SE5);
                }

                Debug.Log("WalkButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.J) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("run");

                if(getValue_biglimit == 1){
                    serial.Write ("o");GetComponent<AudioSource>().PlayOneShot(SE6);
                }else{
                    serial.Write ("t");GetComponent<AudioSource>().PlayOneShot(SE6);
                }
                Debug.Log("RunButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.K) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("hit");
                if(getValue_biglimit == 1){
                    serial.Write ("p");GetComponent<AudioSource>().PlayOneShot(SE7);
                }else{
                    serial.Write ("y");GetComponent<AudioSource>().PlayOneShot(SE7);
                }
                Debug.Log("HitButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.L) && (ArduinoStartEnd == 1)){
                Flags_SensorRW = 1;
                animator.Play ("sound");
                if(getValue_biglimit == 1){
                    serial.Write ("l");GetComponent<AudioSource>().PlayOneShot(SE8);
                }else{
                    serial.Write ("u");GetComponent<AudioSource>().PlayOneShot(SE8);
                }
                Debug.Log("SoundButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            //ここまで
            if (Input.GetKeyDown(KeyCode.N)){//ArduinoOpen
                ArduinoStartEnd = 1;
                Flags_SensorRW = 1;
                serial.Write ("k");
                DelayMethod (60);
                Debug.Log("OpenButtonOn");
                Flags_SensorRW = 0;
            } 
            if (Input.GetKeyDown(KeyCode.G)){//たまごわれる
                ArduinoStartEnd = 1;
                Flags_SensorRW = 1;
                serial.Write ("g");
                Debug.Log("EGGButtonOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.M)){//こどう
                ArduinoStartEnd = 1;
                Flags_SensorRW = 1;
                serial.Write ("1");
                Debug.Log("HeartOn");
                DelayMethod (60);
                Flags_SensorRW = 0;
            }
            if (Input.GetKeyDown(KeyCode.O)){//Arduino reset
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
        if (Input.GetKeyDown (KeyCode.T) && getValue_biglimit == 0) {
            this.transform.localScale = new Vector3 (
                gameObject.transform.localScale.x + 0.01f,
                gameObject.transform.localScale.y + 0.01f,
                gameObject.transform.localScale.z + 0.01f
            );
            getValue_biglimit = 1;
            serial.Write ("a");
            flag_fadeon = 1;
            Debug.Log (flag_fadeon);

            enableFade = true;
        }
    }

    private void toSleeping(){
        if (Input.GetKey (KeyCode.P)) {
            serial.Write ("e");
            ArduinoStartEnd = 0;

            enableFade = true;
            if (enableFadeOut) {
                FadeOut (FadeImage);
            }
        }
    }


    private void toWakeUp(){
        if (Input.GetKey (KeyCode.R)) {

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
        rand3 = UnityEngine.Random.Range (0, 1);
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
        rand4 = UnityEngine.Random.Range (0, 20);
        if ((rand4 == 2) && (ArduinoStartEnd == 1)) {
            GetComponent<AudioSource> ().PlayOneShot (SE);
        } else if ((rand4 == 8) && (ArduinoStartEnd == 1)) {
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


    void setAlpha(Image image,float alpha) {
        image.color = new Color (image.color.r, image.color.g, image.color.b, alpha);
    }

    public void FadeOut(Image image) {
        if (enableFade) {
            count += speed;
            setAlpha (image, count);
            if (image.color.a >= 0.98f) {

                enableFade = false;
                //GetComponent<AudioSource>().PlayOneShot(SE7);
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

                Debug.Log("WakeUpMode");
            }
        }
    }

    void FadeInAndOut(Image image) {

        if (enableFade) {
            if (!enableAlphaTop) {
                count -= speed;

                if (image.color.a <= 0.05f) {
                    enableFade = false;
                    enableFadeIn = false;
                    enableFadeOn = false;
                    flag_fadeon = 0;

                    Debug.Log("flag_fadeon"+flag_fadeon);
                }

                Debug.Log ("FadeonCountS" + count);

            } else {

                count += speed;
                Debug.Log ("FadeonCount" + count);
                if (image.color.a >= 0.97f) {
                    enableFade = true;
                    enableFadeOn = true;
                    enableAlphaTop = false;
                }
            }
            setAlpha (image, count);
            if (image.color.a <= 0.03f) {
                enableAlphaTop = true;
            }
        }
    }

    //ここまでフェード
}