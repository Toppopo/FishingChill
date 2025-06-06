using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    [SerializeField]private float elapsedTime;
    [SerializeField] private float durationTime;

    [SerializeField] private Text StartText;
    [SerializeField] private string[] texts = { "", "Press Space Key" };
    private int index = 0;
    void Update()
    {
        MainSceneChange();
        OnStartText();
    }

    private void OnStartText()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= durationTime)
        {
            index = (index + 1) % texts.Length;
            StartText.text = texts[index];
            elapsedTime = 0;
        }
    }

    private void MainSceneChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeManager.Instance.LoadScene("MainScene",3f);
        }
    }
}
