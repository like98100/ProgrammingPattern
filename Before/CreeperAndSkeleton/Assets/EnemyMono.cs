using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class EnemyMono
{
    protected Transform enemyPRS;  // �� PRS

    Vector3 randomPos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));  //���� ��ȸ��

    protected enum EnemyFSM // �� FSM
    {
        Attack,         // ����
        Flee,           // ����
        Stroll,         // ��ȸ
        MoveToTarget    // �߰�
    }

    public virtual void UpdateEnemy(Transform player, bool isBorder, bool moveFlag)    //�߰� ���ǹ� �޼��� ���� bool flag �߰�
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
                //Debug.Log("����");
                break;
            case EnemyFSM.Flee:
                enemyPRS.rotation = Quaternion.LookRotation(enemyPRS.position - player.position);
                //Debug.Log("����");
                break;
            case EnemyFSM.Stroll:
                if (Vector3.Distance(randomPos, enemyPRS.position) < 0.1f)
                {
                    randomPos = new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
                }
                enemyPRS.rotation = Quaternion.LookRotation(enemyPRS.position - randomPos);
                //Move
                enemyPRS.Translate(enemyPRS.forward * strollSpeed * Time.deltaTime);
                //Debug.Log("��ȸ");
                break;
            case EnemyFSM.MoveToTarget:
                enemyPRS.transform.position = Vector3.MoveTowards(enemyPRS.transform.position, player.position, attackSpeed * Time.deltaTime);
                //Debug.Log("�߰�");
                break;
        }
    }
}
