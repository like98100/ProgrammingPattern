using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Skeleton : Enemy
    {
        EnemyFSM skeletonMode = EnemyFSM.Stroll;

        [SerializeField]
        float health = 100f;

        public Skeleton(Transform skeletonObj)
        {
            base.enemy = skeletonObj;
        }


        //Update the creeper's state
        public override void UpdateEnemy(Transform player)
        {
            //The distance between the Creeper and the player
            float distance = (base.enemy.position - player.position).magnitude;
            
            //상태 변환
            switch (skeletonMode)
            {
                case EnemyFSM.Attack:
                    //조건 서순 고려할 것
                    if(health <= 20.0f)
                    {
                        skeletonMode = EnemyFSM.Flee;
                    }
                    //else if를 쓸 경우 hp가 20 이하일 때 아래 함수를 실행하지 않음
                    //필요한 상황에 따라 else를 기입할 것(이 프로젝트에선 체력이 낮을 때 도망을 우선시해 else를 기입함)
                    else if(distance >= 6.0f)
                    {
                        skeletonMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.Flee:
                    if(health >= 60.0f)
                    {
                        skeletonMode = EnemyFSM.Stroll;
                    }
                    break;
                case EnemyFSM.Stroll:
                    if(distance <= 10.0f)
                    {
                        skeletonMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    if(distance <= 5.0f)
                    {
                        skeletonMode = EnemyFSM.Attack;
                    }
                    else if (distance >= 15.0f)
                    {
                        skeletonMode = EnemyFSM.Stroll;
                    }
                    break;
            }

            //Move the enemy based on a state
            DoAction(player, skeletonMode);
        }
    }
}