using UnityEngine;

public class CloudDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CloudDestroy")
        {
            Debug.Log("destroy");
            Destroy(this.gameObject);
        }
    }
}
