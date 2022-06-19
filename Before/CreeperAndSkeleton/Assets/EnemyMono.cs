using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class EnemyMono
{
    protected Transform enemyPRS;  // 적 PRS

    Vector3 randomPos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));  //랜덤 배회값

    protected enum EnemyFSM // 적 FSM
    {
        Attack,         // 공격
        Flee,           // 도주
        Stroll,         // 배회
        MoveToTarget    // 추격
    }

    public virtual void UpdateEnemy(Transform player, bool isBorder, bool moveFlag)    //추격 조건문 달성을 위한 bool flag 추가
    {

    }

    protected void DoAction(Transform player, EnemyFSM enemyMode)
    {
        float fleeSpeed = 10f;
        float strollSpeed = 2f;
        float attackSpeed = 3f;

        switch (enemyMode)
        {
            case EnemyFSM.Attack:
                //Debug.Log("공격");
                break;
            case EnemyFSM.Flee:
                enemyPRS.rotation = Quaternion.LookRotation(enemyPRS.position - player.position);
                //Debug.Log("도주");
                break;
            case EnemyFSM.Stroll:
                if (Vector3.Distance(randomPos, enemyPRS.position) < 0.1f)
                {
                    randomPos = new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
                }
                enemyPRS.rotation = Quaternion.LookRotation(enemyPRS.position - randomPos);
                //Move
                enemyPRS.Translate(enemyPRS.forward * strollSpeed * Time.deltaTime);
                //Debug.Log("배회");
                break;
            case EnemyFSM.MoveToTarget:
                enemyPRS.transform.position = Vector3.MoveTowards(enemyPRS.transform.position, player.position, attackSpeed * Time.deltaTime);
                //Debug.Log("추격");
                break;
        }
    }
}
