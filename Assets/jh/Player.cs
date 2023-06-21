using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ��� �����ڵ� ��������
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;

    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    //��� ��ȣ�ۿ��ڵ� ��������
    Rigidbody2D rb;

    Vector3 DirVec;
    void Start()
    {
        // �����ڵ�
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    // �����ڵ�
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position; // A����, ĳ������ ���� ��ġ ��
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // B����, ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    void Update()
    {
        // �����ڵ�
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");
        if (canMove)
        {
            if (xDir != 0 || yDir != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
        // ��ȣ�ۿ� �ڵ�
        if (xDir != 0)
            DirVec = new Vector3(xDir, 0, 0);
        else if (yDir != 0)
            DirVec = new Vector3(0, yDir, 0);

        SetRay(DirVec, 1f);
    }

    // ��ȣ�ۿ�(raycast)�ڵ�
    int inx = 0;
    bool isItem = false;
    int ItemCnt = 0;
    public void SetRay(Vector2 rayDir, float dis)
    {
        Debug.DrawRay(transform.position, rayDir * dis, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, dis, LayerMask.GetMask("Ray"));

        if (hit.collider != null && Input.GetKeyDown(KeyCode.Z))  // "Ray" ���̾��� �ݶ��̴��� ������ ��ȣ�ۿ� Ȱ��ȭ
        {
            if (hit.collider.gameObject.CompareTag("npc"))
            {
                // npc�� �߰��ϸ� ��ȭâ�� ��
                MassageManager.instance.gameObject.SetActive(true);

                if (hit.collider.name=="name")
                {
                    //�� npc�� �̸��� ���� �ٸ� ��ȭ�� ��µ�
                    // ���� ��� (name=��Ŵ� ���) �̸� "���� �Ŵ޶�", (name=������) �̸� "��������?" ���� ���

                    if (this.gameObject.activeSelf == true)
                    {
                        if(inx==0) //inx�� 0�̸�
                        {
                            //MassageManager.instance.PrintTalk(0);  //0�� ������ ��� ���
                            inx += 1;
                        }
                        else if (inx == 1) //inx�� 1�̸�
                        {
                            //MassageManager.instance.PrintTalk(7);  //7�� ������ ��� ���
                            inx += 1;
                        }
                    }
                    if (MassageManager.instance.gameObject.activeSelf == false) //â�� ������ �ε��� �ʱ�ȭ
                    {
                        inx = 0;
                        Debug.Log("asdf");
                    }

                    if (isItem) // �������� �����;� �ϴ� npc�� ���
                    {
                        // �����ʹ� �ٸ� ��ȭ�� ��� �� �̺�Ʈ


                        isItem = false;  // �������� ������ �����Ƿ� �ʱ�ȭ
                    }
                    else
                    {
                        // �������� ȹ������ ���� �⺻ ��ȭ
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("Item"))
            {
                // �������� ȹ����
                ItemCnt += 1;  // �������� ������ ��ƾ� �ϴ� ��� 1���� ����
                if (ItemCnt == 5) // �������� 5�� �ʿ�� �Ѵٸ�
                {
                    isItem = true; // isItem ������ ��
                }
            }
            else if (hit.collider.gameObject.CompareTag("stone"))
            {
                //������ �и�
            }
            else if (hit.collider.gameObject.CompareTag("portal"))
            {
                // ���� �������� �̵�
            }
        }
    }
}
