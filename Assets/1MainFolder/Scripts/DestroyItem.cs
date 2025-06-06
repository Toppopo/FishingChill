using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public void Update()
    {
        Destroy(this.gameObject, 1f);
    }
}
