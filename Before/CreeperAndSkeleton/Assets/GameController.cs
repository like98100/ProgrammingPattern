using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        GameObject player;

        [SerializeField]
        GameObject creeper;

        [SerializeField]
        GameObject skeleton;

        List<Enemy> enemies = new List<Enemy>();

        // Start is called before the first frame update
        void Start()
        {
            enemies.Add(new Creeper(creeper.transform));
            enemies.Add(new Skeleton(skeleton.transform));
        }

        // Update is called once per frame
        void Update()
        {
            for (int i=0; i < enemies.Count; i++)
            {
                enemies[i].UpdateEnemy(player.transform);
            }

        }
    }


}
