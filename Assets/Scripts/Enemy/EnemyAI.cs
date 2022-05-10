using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public EnemyState state = EnemyState.PATROL; //처음에는 패트롤 상태로 둔다.

    private Transform playerTr;
    //플레이어의 위치를 저장할 변수. 나중에는 게임매니저에서 가져온다.

    public float attackDist = 5.0f;
    public float traceDist = 10.0f;
    public float judgeDelay = 0.3f;
    public float damping = 0.1f;
    
    public bool isDie = false;
    private WaitForSeconds ws;
    private MoveAgent moveAgent;

    private Animator anim;
    private readonly int hashMove = Animator.StringToHash("isMove");
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashDie = Animator.StringToHash("die");
    private readonly int hashDieIndex = Animator.StringToHash("dieIndex");


    private EnemyFOV fov;

    void Awake()
    {
        moveAgent = GetComponent<MoveAgent>();
        anim = GetComponent<Animator>();
        fov = GetComponent<EnemyFOV>();
    }

    void Start()
    {
        // GameObject player = GameObject.FindWithTag("PLAYER");
        // if(player != null){
        //     playerTr = player.transform;
        // }
        playerTr = GameManager.GetPlayer();
        ws = new WaitForSeconds(judgeDelay);//AI가 판단을 내리는 딜레이시간
    }

    void OnEnable()
    {
        state = EnemyState.PATROL; //재사용을 위한 코드
        isDie = false;
        StartCoroutine(CheckState());
        StartCoroutine(DoAction());
    }

    void Update()
    {
        //anim.SetFloat(hashSpeed, moveAgent.speed);
        switch (state)
        {
            case EnemyState.TRACE:
            case EnemyState.ATTACK:
                Quaternion rot = Quaternion.LookRotation(
                        playerTr.position - transform.position);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, rot, Time.deltaTime * damping);
                break;
        }
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == EnemyState.DIE)
                yield break; //코루틴 종료

            if (playerTr == null)
            {
                yield return ws;
            }

            float dist = (playerTr.position - transform.position).sqrMagnitude;
             
            //공격사거리 내라면 공격            
            if (dist <= attackDist * attackDist)
            {
                if (fov.IsTracePlayer() && fov.IsViewPlayer())
                {
                    state = EnemyState.ATTACK;
                    
                }
            }
            else if (fov.IsTracePlayer() && fov.IsViewPlayer())
            {
                state = EnemyState.TRACE;
            }
            else
            {
                state = EnemyState.PATROL;
            }
            yield return ws; //저지 시간만큼 딜레이
        }

    }

    IEnumerator DoAction()
    {
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case EnemyState.PATROL:
                    moveAgent.patrolling = true;
                    //anim.SetBool(hashMove, true);
                    break;
                case EnemyState.TRACE:
                    moveAgent.traceTarget = playerTr.position;
                    //anim.SetBool(hashMove, true);
                    break;
                case EnemyState.ATTACK:
                    Debug.Log("DEAD");
                    SceneManager.LoadScene("GameOverScene");
                    moveAgent.Stop();
                    //anim.SetBool(hashMove, false);
                    break;
                case EnemyState.DIE:
                    moveAgent.Stop();
                    isDie = true;
                    //anim.SetInteger(hashDieIndex, Random.Range(0, 3));
                    //anim.SetTrigger(hashDie);
                    break;
            }
        }
    }
    //플레이어가 사망했을때 적이 모두 사망 하도록 , 다시 생성 X

    public void SetDead()
    {
        state = EnemyState.DIE;
        StartCoroutine(DeadProcess());

    }

    private IEnumerator DeadProcess()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}