using UnityEngine;

public class CatMoveM : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;

    private float speed = 1f;
    private float SitStartPosX = 0f;

    private bool isMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isMove)
        {
            rb.velocity = new Vector2(speed, 0);
            ani.SetBool("Sit", false);
            MoveStop();
        }
        else
        {
            rb.velocity = Vector2.zero;
            ani.SetBool("Sit", true);
        }
    }
    private void MoveStop()
    {
        if(transform.position.x >= SitStartPosX)
        {
            isMove = false;
            speed = 0f;
        }
    }
}
