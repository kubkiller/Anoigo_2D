using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 DirVec;

    float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");

        if (xDir != 0)
            DirVec = new Vector3(xDir, 0, 0);
        else if (yDir != 0)
            DirVec = new Vector3(0, yDir, 0);

        if (xDir != 0 || yDir != 0)
            Move(DirVec, speed);

        SetRay(DirVec, 1f);
        //Debug.Log(xDir.ToString() + yDir.ToString() +"∑π¿Ã");
    }

    public void Move(Vector3 moveDir, float speed)
    {
        transform.position += moveDir * speed;
    }

    public void SetRay(Vector2 rayDir, float dis)
    {
        Debug.DrawRay(transform.position, rayDir * dis, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, dis, LayerMask.GetMask("Ray"));

        if (hit.collider!=null && Input.GetKeyDown(KeyCode.Z))
        {
            MassageManager.instance.gameObject.SetActive(true);
        }
    }
}
