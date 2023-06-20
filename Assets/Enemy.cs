using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.01f;
    public float raycastDis = 8;
    public float tranceDis = 8; // 추적 거리

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 dir = player.position - transform.position;

        if (dir.magnitude > tranceDis)
            return;

        Vector2 dirNomal = dir.normalized;

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dirNomal, raycastDis);
        Debug.DrawRay(transform.position, dirNomal * raycastDis, Color.red);

        foreach (RaycastHit2D rHit in hit)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obj"))
            {
                Vector3 altDir = Quaternion.Euler(0, 0, -90) * dir;
                transform.Translate(altDir * speed);
                //장해물을 만나면 비켜가기
            }
            else
            {
                Vector2 moveDir;

                if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
                    moveDir = new Vector2(dir.x, 0);
                else
                    moveDir = new Vector2(0, dir.y);

                transform.Translate(moveDir * speed);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speed = 0;
            Debug.Log("Game Over");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speed = 0.01f;
            Debug.Log("DoMang");
        }
    }
}
