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
            
            //���� ��ȯ
            switch (skeletonMode)
            {
                case EnemyFSM.Attack:
                    //���� ���� ����� ��
                    if(health <= 20.0f)
                    {
                        skeletonMode = EnemyFSM.Flee;
                    }
                    //else if�� �� ��� hp�� 20 ������ �� �Ʒ� �Լ��� �������� ����
                    //�ʿ��� ��Ȳ�� ���� else�� ������ ��(�� ������Ʈ���� ü���� ���� �� ������ �켱���� else�� ������)
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