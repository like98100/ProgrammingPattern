using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class Enemy : MonoBehaviour
    {

        protected Transform player;

        Vector3 randomPos;

        public enum EnemyFSM
        {
            Attack,                 // ����
            Flee,                   // ����
            Stroll,                 // ��ȸ 
            MoveTowardsPlayer       // �߰�
        }

        [SerializeField]
        protected EnemyFSM curState;

        public EnemyFSM GetCurState()
        {
            return curState;
        }

        public void SetCurState(EnemyFSM state)
        {
            curState = state;
        }

        protected virtual void Start()
        {
            curState = EnemyFSM.Stroll;
            player = GameObject.Find("Player").transform;
            randomPos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
        }

        protected virtual void Update()
        {
            float fleeSpeed = 10f;
            float strollSpeed = 1f;
            float attackSpeed = 5f;

            // ���¿� �´� �ൿ
            switch(curState)
            {
                case EnemyFSM.Attack:
                    break;
                case EnemyFSM.Flee:
                    transform.rotation = Quaternion.LookRotation(transform.position - player.position); // ȸ��
                    transform.Translate(transform.forward * fleeSpeed * Time.deltaTime);                // �̵�
                    break;
                case EnemyFSM.Stroll:
                    if (Vector3.Distance(randomPos, transform.position) < 0.1f)
                        randomPos = new Vector3(Random.Range(0, 10f), 0f, Random.Range(0f, 10f));       // ���� ��ġ ����

                    transform.rotation = Quaternion.LookRotation(transform.position - randomPos);       // ȸ��
                    transform.Translate(transform.forward * strollSpeed * Time.deltaTime);              // �̵�
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    transform.position = Vector3.MoveTowards(transform.position, player.position, attackSpeed * Time.deltaTime);
                    break;
            }
        }

    }
}
