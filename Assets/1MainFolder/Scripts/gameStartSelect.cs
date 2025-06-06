using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameStartSelect : MonoBehaviour
{
    [SerializeField]private Fishing fishing;

    [SerializeField] GameObject Cat;//�v���C���[
    private float PushStartPos = -1f;//�Q�[���J�n�\�ʒu

    [SerializeField] private GameObject cameraG;

    [SerializeField] private GameObject parentCan;//�eCanvas

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
