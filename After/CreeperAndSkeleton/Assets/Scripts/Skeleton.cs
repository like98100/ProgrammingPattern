using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Skeleton : Enemy
    {
        float health = 100f;

        protected override void Start()
        {
            // Enemy.cs의 Start
            base.Start();
        }

        protected override void Update()
        {
            // 상태 변환
            switch (curState)
            {
                case EnemyFSM.Attack:
                    if (health < 20f) curState = EnemyFSM.Flee;
                    break;
                case EnemyFSM.Flee:
                    if (health > 60f) curState = EnemyFSM.Stroll;
                    break;
                // player가 개입하는 부분은 PlayerMove script에서 통제
                case EnemyFSM.Stroll:
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    break;
            }

            // Enemy.cs의 Update(상태에 맞는 행동)
            base.Update();
        }
    }
}