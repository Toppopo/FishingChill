using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct markScale//!マークの大きさ
{
    public float width;
    public float height;
}

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject Cat;

    //釣れるオブジェクト
    [SerializeField] GameObject objPos;//オブジェクトを出すポジション
    [SerializeField] List<GameObject> obj = new List<GameObject>();//釣れるオブジェクト

    //!マーク
    [SerializeField] private List<GameObject> marks  = new List<GameObject>();//!マーク本体
    [SerializeField] private GameObject markPos;//生成位置
    private GameObject markInstance;//生成後の格納場所
    [SerializeField] List<markScale> markScales = new List<markScale>();//マークのサイズ(3段階)

    //タイミング
    [SerializeField] private float elapsedTime;
    [SerializeField] private float durationTime;
    [SerializeField] int MarkSizeNum;//ランダムで決めるサイズ番号
    private bool isMarkInstantiated = false;//!マークがでたか

    //成功時のアイテム確認画面
    [SerializeField]private GameObject GetScreen;//映す画面

    //ゲーム開始
    private bool started = false;
    public bool Started
    {
        get { return started;}
        set { started = value;}
    }
    //釣りゲー開始
    [SerializeField] private GameObject FishingGameObj;//釣りゲーのマネジャー
    private bool gameStart;
    public bool GameStart
    {
        get { return gameStart;}
        set { gameStart = value;}
    }
    [SerializeField] private FishGame_1 fishGame;

    [SerializeField] private GameObject GameStartManager;

    [SerializeField] private float CoolTime = 0f;
    [SerializeField]private float UpdateInterval;

    private void Start()
    {
        FishingGameObj.SetActive(false);
        GetScreen.SetActive(false);
    }
    private void Update()
    {
        if (started)
        {
            MarkInstance();
        }
        else
        {
            return;
        }
        PushSpace();
    }
    void MarkInstance()//マークの生成
    {
        float inter = UnityEngine.Random.Range(1f, 5f);
        CoolTime += Time.deltaTime;
        if(CoolTime >= UpdateInterval)
        {
            MarkSizeNum = UnityEngine.Random.Range(1, 5);//1/5で赤マークがでる
            if (MarkSizeNum != 2 && !isMarkInstantiated)
            {
                markInstance = Instantiate(marks[0], markPos.transform.position, Quaternion.identity);
                markInstance.transform.localScale = new Vector3(markScales[1].width, markScales[1].height, 0);
                Destroy(markInstance,1f);
                
            }
            else
            {
                markInstance = Instantiate(marks[1], markPos.transform.position, Quaternion.identity);
                markInstance.transform.localScale = new Vector3(markScales[2].width, markScales[2].height, 0);
                isMarkInstantiated = true;
            }
            CoolTime = 0f;
        }
    }
    void PushSpace()//スペースを押すタイミングの処理
    {
        if (isMarkInstantiated)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime <= durationTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Destroy(markInstance);
                    GameStartManager.SetActive(false);
                    FishingGameObj.SetActive(true);
                    foreach(Transform child in FishingGameObj.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                    gameStart = true;
                    fishGame.gageMax = false;
                    elapsedTime = 0f;
                    isMarkInstantiated = false;
                }
            }
            else
            {
                Destroy(markInstance);
                elapsedTime = 0f;
                isMarkInstantiated = false;
            }
        }
    }
     public void ShowItem()//成功時の演出
    {
        FishingGameObj.SetActive(false);
        GetScreen.SetActive(true);
        InstanceObj();
    }
    void InstanceObj()//釣り成功時のオブジェクトの生成
    {
        int r = UnityEngine.Random.Range(0, obj.Count);
        GameObject Item = Instantiate(obj[r], objPos.transform.position, Quaternion.identity);
        Item.transform.localScale += new Vector3(0.5f, 0.5f, 0) * Time.deltaTime;
        Item.transform.position += new Vector3(0, 0.2f, 0) * Time.deltaTime;
    }
}
