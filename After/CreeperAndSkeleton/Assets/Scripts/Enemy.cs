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
            Attack,                 // 공격
            Flee,                   // 도망
            Stroll,                 // 배회 
            MoveTowardsPlayer       // 추격
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

            // 상태에 맞는 행동
            switch(curState)
            {
                case EnemyFSM.Attack:
                    break;
                case EnemyFSM.Flee:
                    transform.rotation = Quaternion.LookRotation(transform.position - player.position); // 회전
                    transform.Translate(transform.forward * fleeSpeed * Time.deltaTime);                // 이동
                    break;
                case EnemyFSM.Stroll:
                    if (Vector3.Distance(randomPos, transform.position) < 0.1f)
                        randomPos = new Vector3(Random.Range(0, 10f), 0f, Random.Range(0f, 10f));       // 임의 위치 선정

                    transform.rotation = Quaternion.LookRotation(transform.position - randomPos);       // 회전
                    transform.Translate(transform.forward * strollSpeed * Time.deltaTime);              // 이동
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    transform.position = Vector3.MoveTowards(transform.position, player.position, attackSpeed * Time.deltaTime);
                    break;
            }
        }

    }
}
