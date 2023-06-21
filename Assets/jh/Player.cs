using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 우빈 무브코드 변수선언
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

    //희민 상호작용코드 변수선언
    Rigidbody2D rb;

    Vector3 DirVec;
    void Start()
    {
        // 무브코드
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    // 무브코드
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

            Vector2 start = transform.position; // A지점, 캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // B지점, 캐릭터가 이동하고자 하는 위치 값

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
        // 무브코드
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
        // 상호작용 코드
        if (xDir != 0)
            DirVec = new Vector3(xDir, 0, 0);
        else if (yDir != 0)
            DirVec = new Vector3(0, yDir, 0);

        SetRay(DirVec, 1f);
    }

    // 상호작용(raycast)코드
    int inx = 0;
    bool isItem = false;
    int ItemCnt = 0;
    public void SetRay(Vector2 rayDir, float dis)
    {
        Debug.DrawRay(transform.position, rayDir * dis, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, dis, LayerMask.GetMask("Ray"));

        if (hit.collider != null && Input.GetKeyDown(KeyCode.Z))  // "Ray" 레이어의 콜라이더에 닿으면 상호작용 활성화
        {
            if (hit.collider.gameObject.CompareTag("npc"))
            {
                // npc를 발견하면 대화창을 염
                MassageManager.instance.gameObject.SetActive(true);

                if (hit.collider.name=="name")
                {
                    //그 npc의 이름에 따라 다른 대화가 출력됨
                    // 예를 들어 (name=배매는 사람) 이면 "배좀 매달라", (name=구보씨) 이면 "누구세요?" 같이 출력

                    if (this.gameObject.activeSelf == true)
                    {
                        if(inx==0) //inx가 0이면
                        {
                            //MassageManager.instance.PrintTalk(0);  //0번 라인의 대사 출력
                            inx += 1;
                        }
                        else if (inx == 1) //inx가 1이면
                        {
                            //MassageManager.instance.PrintTalk(7);  //7번 라인의 대사 출력
                            inx += 1;
                        }
                    }
                    if (MassageManager.instance.gameObject.activeSelf == false) //창이 닫히면 인덱스 초기화
                    {
                        inx = 0;
                        Debug.Log("asdf");
                    }

                    if (isItem) // 아이템을 가져와야 하는 npc의 경우
                    {
                        // 원래와는 다른 대화가 출력 및 이벤트


                        isItem = false;  // 아이템을 가지고 갔으므로 초기화
                    }
                    else
                    {
                        // 아이템을 획득하지 않은 기본 대화
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("Item"))
            {
                // 아이템을 획득함
                ItemCnt += 1;  // 아이템을 여러개 모아야 하는 경우 1개씩 증가
                if (ItemCnt == 5) // 아이템을 5개 필요로 한다면
                {
                    isItem = true; // isItem 참으로 함
                }
            }
            else if (hit.collider.gameObject.CompareTag("stone"))
            {
                //바위가 밀림
            }
            else if (hit.collider.gameObject.CompareTag("portal"))
            {
                // 다음 스테이지 이동
            }
        }
    }
}
