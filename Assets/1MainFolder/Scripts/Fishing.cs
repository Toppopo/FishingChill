using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct markScale//!�}�[�N�̑傫��
{
    public float width;
    public float height;
}

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject Cat;

    //�ނ��I�u�W�F�N�g
    [SerializeField] GameObject objPos;//�I�u�W�F�N�g���o���|�W�V����
    [SerializeField] List<GameObject> obj = new List<GameObject>();//�ނ��I�u�W�F�N�g

    //!�}�[�N
    [SerializeField] private List<GameObject> marks  = new List<GameObject>();//!�}�[�N�{��
    [SerializeField] private GameObject markPos;//�����ʒu
    private GameObject markInstance;//������̊i�[�ꏊ
    [SerializeField] List<markScale> markScales = new List<markScale>();//�}�[�N�̃T�C�Y(3�i�K)

    //�^�C�~���O
    [SerializeField] private float elapsedTime;
    [SerializeField] private float durationTime;
    [SerializeField] int MarkSizeNum;//�����_���Ō��߂�T�C�Y�ԍ�
    private bool isMarkInstantiated = false;//!�}�[�N���ł���

    //�������̃A�C�e���m�F���
    [SerializeField]private GameObject GetScreen;//�f�����

    //�Q�[���J�n
    private bool started = false;
    public bool Started
    {
        get { return started;}
        set { started = value;}
    }
    //�ނ�Q�[�J�n
    [SerializeField] private GameObject FishingGameObj;//�ނ�Q�[�̃}�l�W���[
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
    void MarkInstance()//�}�[�N�̐���
    {
        float inter = UnityEngine.Random.Range(1f, 5f);
        CoolTime += Time.deltaTime;
        if(CoolTime >= UpdateInterval)
        {
            MarkSizeNum = UnityEngine.Random.Range(1, 5);//1/5�Őԃ}�[�N���ł�
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
    void PushSpace()//�X�y�[�X�������^�C�~���O�̏���
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
     public void ShowItem()//�������̉��o
    {
        FishingGameObj.SetActive(false);
        GetScreen.SetActive(true);
        InstanceObj();
    }
    void InstanceObj()//�ނ萬�����̃I�u�W�F�N�g�̐���
    {
        int r = UnityEngine.Random.Range(0, obj.Count);
        GameObject Item = Instantiate(obj[r], objPos.transform.position, Quaternion.identity);
        Item.transform.localScale += new Vector3(0.5f, 0.5f, 0) * Time.deltaTime;
        Item.transform.position += new Vector3(0, 0.2f, 0) * Time.deltaTime;
    }
}
