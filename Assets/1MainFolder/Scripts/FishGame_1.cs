using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishGame_1 : MonoBehaviour
{
    private enum InputType
    {
        A,
        D,
        Space,
        NONE
    }
    private InputType inputType;

    [SerializeField] private Fishing fishing;//釣り本体のスクリプト

    [SerializeField] private List<GameObject> Ques = new List<GameObject>();//?マーク
    [SerializeField] private GameObject keyCanvas;//キーキャンバス

    [SerializeField] private Button button_A;//Aキー
    [SerializeField] private Button button_D;//Dキー
    [SerializeField] private Button button_Space;//Spaceキー
    [SerializeField] private GameObject spaceObj;

    [SerializeField] private Camera camera_g;//ゲームカメラ
    [SerializeField] private GameObject camera_g_obj;//ゲームカメラ
    private float targetSize = 2f;//目標かめらサイズ

    [SerializeField]private Image bar;//ゲージ
    private float barIncSpeed = 0.25f;//ゲージ増やす量
    private float barAutoDecSpeed = 0.1f;//ゲージが常に減る量
    private bool GaugeMax;//ゲージがたまったか
    public bool gageMax {get {return GaugeMax; } set{GaugeMax = value; } }

    private int MaxpushCount = 10;//スペースキーを押す回数
    private int pushcount = 0;//
    [SerializeField]private Text PushText;//連打回数テキスト
    private int TextCount;//テキストに表示する値

    private int randKey;//ランダムキー
    private bool KeySeted = false;
    private float durationTime;
    private float currentTime;

    [SerializeField]private float pushingTime;
    [SerializeField]private float failTime;

    private void Start()
    {
        bar.fillAmount = 0.5f;
        PushText.text = MaxpushCount.ToString();
        spaceObj.SetActive(false);
        inputType = InputType.NONE;
    }
    private void Update()
    {
        JudgeKey();//キー入力の管理(enum)
        if (fishing.GameStart && !GaugeMax)//カメラの位置、ゲームの開始
        {
            CameraSet();
            fishing.Started = false;
            keyCanvas.SetActive(true);
            ShowKeySet();
            ColorReset();
            
        }
        else
        {
            bar.fillAmount = 0.5f;
        }
        if (GaugeMax)
        {
            inputType = InputType.Space;
            foreach (var i in Ques)
            {
                i.SetActive(false);
            }
            Debug.Log("スペース表示" + spaceObj);
            keyCanvas.SetActive(false);
            spaceObj.SetActive(true);
            ColorReset();
        }//連打開始
        //Debug.Log("ゲーム開始するか" + fishing.GameStart);
        Debug.Log("InputType" + inputType);
        //Debug.Log("ゲージが満タンか" + GageMax);
    }
    private void ShowKeySet()//左右のどっちを出すか
    {
        if (!KeySeted)
        {
            randKey = Random.Range(0, 2);//右左を決定
            durationTime = 0.5f;//0.5で切り替え
            currentTime = 0;//初期化
            KeySeted = true;
        }
        else
        {
            KeyDown();
        }
    }
    private void KeyDown()//キーを押せるか
    {
        currentTime += Time.deltaTime;
        if (currentTime < durationTime)//超えないうちに押せたらゲージ取得
        {
            Ques[randKey].SetActive(true);//?マークの表示
            if (Input.GetKey(KeyCode.A) && randKey == 0)
            {
                inputType = InputType.A;
            }
            else if(Input.GetKeyDown(KeyCode.D) && randKey == 1)
            {
                inputType = InputType.D;
            }
        }
        else//超えたら?マークの場所を変更
        {
            inputType = InputType.NONE;
            Ques[randKey].SetActive(false);
            KeySeted = false;
        }
    }
    private void Input_Space()//スペース入力
    {
        TextCount = 10;
        PushText.text = (TextCount - pushcount).ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ColorBlock colors_space = button_Space.colors;//現在の色を所得
            colors_space.normalColor = Color.red;//赤に変更
            button_Space.colors = colors_space;//反映
            pushcount++;
            colors_space.normalColor = Color.white;//白に戻す
            if (pushcount >= MaxpushCount)
            {
                pushcount = 0;
                fishing.GameStart = false;
                CameraReset();
                fishing.ShowItem();
            }
        }
    }
    private void Input_A()//Aキー入力
    {
        ColorBlock colors_A = button_A.colors;
        colors_A.normalColor = Color.red;
        button_A.colors = colors_A;
        bar.fillAmount += barIncSpeed * Time.deltaTime; 
        if (bar.fillAmount >= 1f)
        {
            GaugeMax = true;
        }
    }    
    private void Input_D()//Dキー入力
    {
        ColorBlock colors_D = button_D.colors;
        colors_D.normalColor = Color.red;
        button_D.colors = colors_D;
        bar.fillAmount += barIncSpeed * Time.deltaTime;
        if (bar.fillAmount >= 1f)
        {
            GaugeMax = true;
        }
    }
    private void ColorReset()//ボタンの色リセット
    {
        ColorBlock colors_A = button_A.colors;
        colors_A.normalColor = Color.white;
        button_A.colors = colors_A;

        ColorBlock colors_D = button_D.colors;
        colors_D.normalColor = Color.white;
        button_D.colors = colors_D;
    }

    private void CameraSet()//カメラの位置設定
    {
        camera_g_obj.SetActive(true);
        while (camera_g.orthographicSize > targetSize)
        {
            camera_g.orthographicSize -= Time.deltaTime;
        }
    }    
    private void CameraReset()//カメラ位置リセット
    {
        camera_g_obj.SetActive(false);
        while (camera_g.orthographicSize < targetSize)
        {
            camera_g.orthographicSize += Time.deltaTime;
        }
    }

    private void AutoDecFill()//ミニゲーム時のゲージを自動的に減らす
    {
        bar.fillAmount -= barAutoDecSpeed * Time.deltaTime;
        if(bar.fillAmount　<= 0)
        {
            Debug.Log("barが0になった");
            CameraReset();
            fishing.Started = true;
            fishing.GameStart = false;
        }
    }

    private void JudgeKey()//キー判定
    {
        switch(inputType)
        {
            case InputType.A:
                Input_A();
                break;
            case InputType.D:
                Input_D();
                break;
            case InputType.Space:
                Input_Space();
                break;
            case InputType.NONE:
                AutoDecFill();//ゲージが勝手に減る
                break;
        }
    }
}
