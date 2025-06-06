using UnityEngine;

public class BackGame : MonoBehaviour
{
    [SerializeField]private GameObject Panel;
    [SerializeField]private Fishing fishing;
    public void ReturnGame()
    {
        Panel.SetActive(false);
        fishing.Started = true;
    }
}
