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

    [SerializeField] private Fishing fishing;//�ނ�{�̂̃X�N���v�g

    [SerializeField] private List<GameObject> Ques = new List<GameObject>();//?�}�[�N
    [SerializeField] private GameObject keyCanvas;//�L�[�L�����o�X

    [SerializeField] private Button button_A;//A�L�[
    [SerializeField] private Button button_D;//D�L�[
    [SerializeField] private Button button_Space;//Space�L�[
    [SerializeField] private GameObject spaceObj;

    [SerializeField] private Camera camera_g;//�Q�[���J����
    [SerializeField] private GameObject camera_g_obj;//�Q�[���J����
    private float targetSize = 2f;//�ڕW���߂�T�C�Y

    [SerializeField]private Image bar;//�Q�[�W
    private float barIncSpeed = 0.25f;//�Q�[�W���₷��
    private float barAutoDecSpeed = 0.1f;//�Q�[�W����Ɍ����
    private bool GaugeMax;//�Q�[�W�����܂�����
    public bool gageMax {get {return GaugeMax; } set{GaugeMax = value; } }

    private int MaxpushCount = 10;//�X�y�[�X�L�[��������
    private int pushcount = 0;//
    [SerializeField]private Text PushText;//�A�ŉ񐔃e�L�X�g
    private int TextCount;//�e�L�X�g�ɕ\������l

    private int randKey;//�����_���L�[
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
        JudgeKey();//�L�[���͂̊Ǘ�(enum)
        if (fishing.GameStart && !GaugeMax)//�J�����̈ʒu�A�Q�[���̊J�n
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
            Debug.Log("�X�y�[�X�\��" + spaceObj);
            keyCanvas.SetActive(false);
            spaceObj.SetActive(true);
            ColorReset();
        }//�A�ŊJ�n
        //Debug.Log("�Q�[���J�n���邩" + fishing.GameStart);
        Debug.Log("InputType" + inputType);
        //Debug.Log("�Q�[�W�����^����" + GageMax);
    }
    private void ShowKeySet()//���E�̂ǂ������o����
    {
        if (!KeySeted)
        {
            randKey = Random.Range(0, 2);//�E��������
            durationTime = 0.5f;//0.5�Ő؂�ւ�
            currentTime = 0;//������
            KeySeted = true;
        }
        else
        {
            KeyDown();
        }
    }
    private void KeyDown()//�L�[�������邩
    {
        currentTime += Time.deltaTime;
        if (currentTime < durationTime)//�����Ȃ������ɉ�������Q�[�W�擾
        {
            Ques[randKey].SetActive(true);//?�}�[�N�̕\��
            if (Input.GetKey(KeyCode.A) && randKey == 0)
            {
                inputType = InputType.A;
            }
            else if(Input.GetKeyDown(KeyCode.D) && randKey == 1)
            {
                inputType = InputType.D;
            }
        }
        else//��������?�}�[�N�̏ꏊ��ύX
        {
            inputType = InputType.NONE;
            Ques[randKey].SetActive(false);
            KeySeted = false;
        }
    }
    private void Input_Space()//�X�y�[�X����
    {
        TextCount = 10;
        PushText.text = (TextCount - pushcount).ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ColorBlock colors_space = button_Space.colors;//���݂̐F������
            colors_space.normalColor = Color.red;//�ԂɕύX
            button_Space.colors = colors_space;//���f
            pushcount++;
            colors_space.normalColor = Color.white;//���ɖ߂�
            if (pushcount >= MaxpushCount)
            {
                pushcount = 0;
                fishing.GameStart = false;
                CameraReset();
                fishing.ShowItem();
            }
        }
    }
    private void Input_A()//A�L�[����
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
    private void Input_D()//D�L�[����
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
    private void ColorReset()//�{�^���̐F���Z�b�g
    {
        ColorBlock colors_A = button_A.colors;
        colors_A.normalColor = Color.white;
        button_A.colors = colors_A;

        ColorBlock colors_D = button_D.colors;
        colors_D.normalColor = Color.white;
        button_D.colors = colors_D;
    }

    private void CameraSet()//�J�����̈ʒu�ݒ�
    {
        camera_g_obj.SetActive(true);
        while (camera_g.orthographicSize > targetSize)
        {
            camera_g.orthographicSize -= Time.deltaTime;
        }
    }    
    private void CameraReset()//�J�����ʒu���Z�b�g
    {
        camera_g_obj.SetActive(false);
        while (camera_g.orthographicSize < targetSize)
        {
            camera_g.orthographicSize += Time.deltaTime;
        }
    }

    private void AutoDecFill()//�~�j�Q�[�����̃Q�[�W�������I�Ɍ��炷
    {
        bar.fillAmount -= barAutoDecSpeed * Time.deltaTime;
        if(bar.fillAmount�@<= 0)
        {
            Debug.Log("bar��0�ɂȂ���");
            CameraReset();
            fishing.Started = true;
            fishing.GameStart = false;
        }
    }

    private void JudgeKey()//�L�[����
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
                AutoDecFill();//�Q�[�W������Ɍ���
                break;
        }
    }
}
