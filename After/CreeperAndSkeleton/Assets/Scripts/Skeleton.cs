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
            // Enemy.cs�� Start
            base.Start();
        }

        protected override void Update()
        {
            // ���� ��ȯ
            switch (curState)
            {
                case EnemyFSM.Attack:
                    if (health < 20f) curState = EnemyFSM.Flee;
                    break;
                case EnemyFSM.Flee:
                    if (health > 60f) curState = EnemyFSM.Stroll;
                    break;
                // player�� �����ϴ� �κ��� PlayerMove script���� ����
                case EnemyFSM.Stroll:
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    break;
            }

            // Enemy.cs�� Update(���¿� �´� �ൿ)
            base.Update();
        }
    }
}