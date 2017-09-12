#include <Adafruit_PWMServoDriver.h>
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();
#include <Metro.h>
#include <avr/wdt.h>

//このプログラムだと全ての振動子が同じ強さで振動する

//振動子を接続するピン
const int vibPin[8] = {22, 25, 26, 29, 30, 33, 34, 37};

//振動子の動き方を制御する変数
char mode;

//圧力センサーsetPressに必要だった変数
  double Vo[8];
  double Rf[8];
  double fg[8];
//圧力センサー用
int vsensors[8];
  double R1;
  double hantei;
  int hanteiNumber;

//振動とめるフラグ
 int flagForPress;
 int flagForAdult;
 int flagMoving;
 int flagRW;



//5秒間計測
Metro interval_time = Metro(5000);

void software_reset(){
  wdt_disable();
  wdt_enable(WDTO_15MS);
  while(1) {}
}


//PWM_SetUp
void Set_PWM(){

    pwm.setPWM(0, 2000, 4095);
    pwm.setPWM(2, 2000, 4095);
    pwm.setPWM(4, 2000, 4095);
    pwm.setPWM(6, 2000, 4095);
    pwm.setPWM(8, 2000, 4095);
    pwm.setPWM(10, 2000, 4095);
    pwm.setPWM(12, 2000, 4095);
    pwm.setPWM(14, 2000, 4095);
    
}


void Set_PWM2(){

    pwm.setPWM(0, 1000, 4095);
    pwm.setPWM(2, 1000, 4095);
    pwm.setPWM(4, 1000, 4095);
    pwm.setPWM(6, 1000, 4095);
    pwm.setPWM(8, 1000, 4095);
    pwm.setPWM(10, 1000, 4095);
    pwm.setPWM(12, 1000, 4095);
    pwm.setPWM(14, 1000, 4095);
    
}


void Set_PWM3(){

    pwm.setPWM(0, 3000, 4095);
    pwm.setPWM(2, 3000, 4095);
    pwm.setPWM(4, 3000, 4095);
    pwm.setPWM(6, 3000, 4095);
    pwm.setPWM(8, 3000, 4095);
    pwm.setPWM(10,3000, 4095);
    pwm.setPWM(12, 3000, 4095);
    pwm.setPWM(14, 3000, 4095);
    
}



void setup() {
  for(int j=0; j<8; j++){
    pinMode(vibPin[j], OUTPUT);
  }
  pwm.begin();
  pwm.setPWMFreq(500);
  pwm.setPWM(0, 2000, 4095);
    pwm.setPWM(2, 2000, 4095);
    pwm.setPWM(4, 2000, 4095);
    pwm.setPWM(6, 2000, 4095);
    pwm.setPWM(8, 2000, 4095);
    pwm.setPWM(10, 2000, 4095);
    pwm.setPWM(12, 2000, 4095);
    pwm.setPWM(14, 2000, 4095);
  pwm.begin();
  pwm.setPWMFreq(500);
  Serial.begin(9600);
  R1 = 5.1;
  flagForPress = 0;//圧力センサーによる振動が可能かどうか、Unityからの信号を優先
  flagMoving = 0;//振動子が動いているかどうか
  hanteiNumber = 0;
  flagForAdult = 0;//大人になっているかどうか
  hantei = 0;
  mode = 0;
  flagRW = 0; //読み込みと書き込みの入れ替え、Unity To Arduino 1  Arduino To Unity 0

}

int i = 0;


//もともとの振動子用関数、5秒おきに別のが動く
void normalMode(){
    flagMoving = 1;
    Set_PWM();
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
     
      delay(100);
      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      
     digitalWrite(vibPin[6], HIGH);

      digitalWrite(vibPin[7], HIGH);
     
      delay(100);
      digitalWrite(vibPin[6], LOW);
    
      digitalWrite(vibPin[7], LOW);
      
      
     digitalWrite(vibPin[2], HIGH);

     digitalWrite(vibPin[3], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
      

     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);

      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);

      flagMoving = 0;
}




void IdleMode(){
flagMoving = 1;
  
    Set_PWM();
    
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);

     
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);

     
      //pwm.setPWM(0, 2000, 4095);
      delay(2000);
      digitalWrite(vibPin[0], LOW);
    
      digitalWrite(vibPin[1], LOW);

      flagMoving = 0;
}


void childWalkMode(){
  flagMoving = 1;
    
    Set_PWM();
    
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      delay(200);

      
    
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);


      delay(200);
      
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      

      delay(200);


     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      
      flagMoving = 0;
}


void childRunMode(){
  flagMoving = 1;
    
    Set_PWM();
    
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);

      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      delay(200);

      
    
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);



      delay(200);
      
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);

      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
     

      delay(200);

     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);
      
      flagMoving = 0;
}


void childHitMode(){
  flagMoving = 1;
  
    
    Set_PWM();
    
      
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
      
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      flagMoving = 0;
}


void childSoundMode(){
flagMoving = 1;
  
    
   Set_PWM();
    
    
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);

     
      delay(200);
      
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(200);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
      
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);

      delay(200);
      
      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);
    
      flagMoving = 0;
}


void AdultWalkMode(){
   flagMoving = 1;
   
    Set_PWM2();
    
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      delay(200);

      
    
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);


      delay(200);
      
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      

      delay(200);


     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      
      flagMoving = 0;
}

void AdultRunMode(){
flagMoving = 1;
    
    Set_PWM2();
    
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);

      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      delay(200);

      
    
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);



      delay(200);
      
    
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
      delay(100);

      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);

      delay(200);

     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(100);
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);
      
      flagMoving = 0;
}

void AdultHitMode(){
flagMoving = 1;
 
    Set_PWM2();
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);
     
      delay(100);
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
      
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);
      
      flagMoving = 0;
}

void AdultSoundMode(){
  flagMoving = 1;
  
    
    Set_PWM2();
    
    
     digitalWrite(vibPin[2], HIGH);
     
     digitalWrite(vibPin[3], HIGH);
     
     digitalWrite(vibPin[4], HIGH);
     
     digitalWrite(vibPin[5], HIGH);

     
      delay(200);
      
     digitalWrite(vibPin[0], HIGH);
     
     digitalWrite(vibPin[1], HIGH);
     
     digitalWrite(vibPin[6], HIGH);
     
     digitalWrite(vibPin[7], HIGH);
     
      delay(200);
      
      digitalWrite(vibPin[2], LOW);
      
      digitalWrite(vibPin[3], LOW);
      
      digitalWrite(vibPin[4], LOW);
      
      digitalWrite(vibPin[5], LOW);

      delay(200);
      
      digitalWrite(vibPin[0], LOW);
      
      digitalWrite(vibPin[1], LOW);
      
      digitalWrite(vibPin[6], LOW);
      
      digitalWrite(vibPin[7], LOW);
    
      flagMoving = 0;
}

void silent(){
    Set_PWM();
    
    
      digitalWrite(vibPin[0], LOW);
      digitalWrite(vibPin[1], LOW);
      digitalWrite(vibPin[2], LOW);
      digitalWrite(vibPin[3], LOW);
      digitalWrite(vibPin[4], LOW);
      digitalWrite(vibPin[5], LOW);
      digitalWrite(vibPin[6], LOW);
      digitalWrite(vibPin[7], LOW);
}


//圧力センサーのもともとの関数
void setPress(){
  for(int j = 0; j < 8; j++){
    vsensors[j] = analogRead(j);
    Vo[j] = vsensors[j] * 5.0 / 1024;//アナログ値から出力電圧を計算
     Rf[j] = R1*Vo[j] / (5.0 - Vo[j]);//出力電圧からFRSの抵抗値を計算
     fg[j] = 880.79 /Rf[j] * 47.96;//FRSの抵抗値から圧力センサーの荷重を計算
     
    if(fg[j]>fg[j-1] && j >= 0){
          hantei = fg[j];
          hanteiNumber = j+1;
       }
       //hantei = analogRead(j);
       //hanteiNumber = j;
  }

  
    if(mode == 'r' && flagRW == 0){
       Serial.println(hantei);//最も高い圧力センサーの値
     }
     if(mode == 't' && flagRW == 0){
        Serial.println(hanteiNumber);//最も値が高かった圧力センサーの番号
     }
    
  // if(flagRW == 0){
  if(hantei != NULL){
    Serial.println(hantei);
  }
    //Serial.print("\n");
    //}
     flagRW = 1;
 //}
 //全ての圧力センサーの値を確認する場合は以下をコメントからはずす
 /*Serial.println(fg[0]);
  Serial.println(fg[1]);
  Serial.println(fg[2]);
  Serial.println(fg[3]);
  Serial.println(fg[4]);
  Serial.println(fg[5]);
  Serial.println(fg[6]);
  Serial.println(fg[7]);
  Serial.println('\n');*/
  //ここまで
 delay(500);
}

//圧力センサーによって振動子を動かすための関数
void ChangemodePress(){
      if(hantei > 1500 && hantei < 8000 && flagMoving == 0){
      
        switch(flagForAdult){
            case 1:
           AdultWalkMode();
           break;
         case 0:
            childWalkMode();
           break;
        }
      }
      else if(hantei >= 8000 && hantei < 20000 && flagMoving == 0){
          
        switch(flagForAdult){
            case 1:
           AdultRunMode();
           break;
         case 0:
            childRunMode();
           break;
        }
     }
      else if(hantei >= 20000 && hantei < 35000 && flagMoving == 0){
    
        switch(flagForAdult){
            case 1:
           AdultSoundMode();
           break;
         case 0:
            childSoundMode();
           break;
        }
     }
      else if(hantei >= 35000 && hantei < 175000 && flagMoving == 0){
       
        switch(flagForAdult){
            case 1:
           AdultHitMode();
           break;
         case 0:
            childHitMode();
           break;
        }
     }
      else{
       //IdleMode();
       delay(1000);
      }
}

void loop() {

  setPress();//圧力センサー読み取り
  
   if(Serial.available() && flagRW == 1){
      mode = Serial.read();//Unityからの数値読み込み
      if(mode != NULL){
         flagRW =0;
      }
    }
    //Serial.print(mode);
    
  //以下Unity使用時　専用コード
  if(flagForPress == 0 && mode != NULL){
    switch(mode){//圧力センサーから読み込んだ圧力の数値に対して挙動を変える
        case '0' : mode = 0; silent(); break;
        case '1' : mode = 0; if(flagMoving == 0){normalMode();} flagForPress = 1;break;
        case '2' : mode = 0; if(flagMoving == 0){IdleMode();} flagForPress = 1; break;
        case '3' : mode = 0; if(flagMoving == 0){childWalkMode();} flagForPress = 1;break;
        case '4' : mode = 0; if(flagMoving == 0){childRunMode();} flagForPress = 1; break;
        case '5' : mode = 0; if(flagMoving == 0){childHitMode();} flagForPress = 1; break;
        case '6' : mode = 0; if(flagMoving == 0){childSoundMode();} flagForPress = 1; break;
        case '7' : mode = 0; if(flagMoving == 0){AdultWalkMode();} flagForPress = 1; break;
        case '8' : mode = 0; if(flagMoving == 0){AdultRunMode();} flagForPress = 1; break;
        case '9' : mode = 0; if(flagMoving == 0){AdultHitMode();} flagForPress = 1; break;
        case 's' : mode = 0; if(flagMoving == 0){AdultSoundMode();} flagForPress = 1; break;
        case 'a' : flagForAdult = 1; break;
        case 'f' : Serial.flush();
        default: silent(); //数値がなければ圧力センサーの値によって動きを変える
    }
  }

  //Unity ここまで

   
if(flagForPress == 0){//Unityからの信号で、振動子を動かすものが特になければ圧力センサーの値によって振動させる
  //ChangemodePress();
  flagForPress = 1; 
}
      
hanteiNumber = 0;
hantei = 0;
  mode = 0;
  flagForPress = 0; 
         flagRW =0;
  delay(1);
  Serial.flush();
}


