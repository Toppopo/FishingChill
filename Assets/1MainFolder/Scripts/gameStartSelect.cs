using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameStartSelect : MonoBehaviour
{
    [SerializeField]private Fishing fishing;

    [SerializeField] GameObject Cat;//プレイヤー
    private float PushStartPos = -1f;//ゲーム開始可能位置

    [SerializeField] private GameObject cameraG;

    [SerializeField] private GameObject parentCan;//親Canvas

    [SerializeField] private Text startText;
    [SerializeField] private Text YesText;
    [SerializeField] private Text NoText;
    private void Start()
    {
        cameraG.SetActive(false);
        parentCan.SetActive(false);
    }
    private void Update()
    {
        if (Cat.transform.position.x >= PushStartPos)
        {
            parentCan.SetActive(true);
            if (fishing.Started)
            {
                parentCan.SetActive(false);
            }
        }
    }
    public void gameStart()
    {
        fishing.Started = true;
    }
    public void BackTitle()
    {
        FadeManager.Instance.LoadScene("Title", 3f);
    }
}
