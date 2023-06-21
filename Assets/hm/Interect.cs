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

    int inx = 0;
    public void SetRay(Vector2 rayDir, float dis)
    {
        Debug.DrawRay(transform.position, rayDir * dis, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, dis, LayerMask.GetMask("Ray"));

        if (hit.collider != null && Input.GetKeyDown(KeyCode.Z))
        {
            if (hit.collider.gameObject.CompareTag("npc"))
            {
                MassageManager.instance.gameObject.SetActive(true);
                if (this.gameObject.activeSelf == true)
                {
                    Debug.Log(inx);
                    MassageManager.instance.PrintTalk(inx);
                    inx += 1;
                }
                if (MassageManager.instance.gameObject.activeSelf == false)
                {
                    inx = 0;
                    Debug.Log("asdf");
                }
            }
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("0987y");
            }
        }
    }
}
