using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class Creeper : Enemy
    {
        EnemyFSM creeperMode;

        float health = 100f;

        public Creeper(Transform creeper)
        {
            base.enemy = creeper;
            creeperMode = EnemyFSM.Stroll;
        }


        //Update the creeper's state
        public override void UpdateEnemy(Transform player)
        {
            //The distance between the Creeper and the player
            float distance = (base.enemy.position - player.position).magnitude;

            switch (creeperMode)
            {
                case EnemyFSM.Attack:
                    if(health <= 20.0f)
                    {
                        creeperMode = EnemyFSM.Flee;
                    }
                    else if(distance >= 2.0f)
                    {
                        creeperMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.Flee:
                    if(health >= 60.0f)
                    {
                        creeperMode = EnemyFSM.Stroll;
                    }
                    break;
                case EnemyFSM.Stroll:
                    if(distance <= 10.0f)
                    {
                        creeperMode = EnemyFSM.MoveTowardsPlayer;
                    }
                    break;
                case EnemyFSM.MoveTowardsPlayer:
                    if(distance <= 1.0f)
                    {
                        creeperMode = EnemyFSM.Attack;
                    }
                    else if(distance >= 15.0f)
                    {
                        creeperMode = EnemyFSM.Stroll;
                    }
                    break;
            }

            //Move the enemy based on a state
            DoAction(player, creeperMode);
        }
    }

}
