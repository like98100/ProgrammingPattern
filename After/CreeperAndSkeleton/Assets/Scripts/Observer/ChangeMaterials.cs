using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// observer

namespace StatePattern
{
    public class ChangeMaterials : MonoBehaviour
    {
        [SerializeField]
        Material[] mesh;
        //int curMesh;

        SendMessage player;
        GameObject creeper, skeleton;

        void ChangeMesh()
        {
            //if (this.curMesh == 0) this.curMesh = 1;
            //else this.curMesh = 0;
            
            if(creeper.GetComponent<Creeper>().GetCurState() == Enemy.EnemyFSM.MoveTowardsPlayer ||
                creeper.GetComponent<Creeper>().GetCurState() == Enemy.EnemyFSM.Attack)
            {
                creeper.GetComponent<MeshRenderer>().material = creeper.GetComponent<ChangeMaterials>().mesh[1];
            }
            else creeper.GetComponent<MeshRenderer>().material = creeper.GetComponent<ChangeMaterials>().mesh[0];


            if (skeleton.GetComponent<Skeleton>().GetCurState() == Enemy.EnemyFSM.MoveTowardsPlayer ||
                skeleton.GetComponent<Skeleton>().GetCurState() == Enemy.EnemyFSM.Attack)
            {
                skeleton.GetComponent<MeshRenderer>().material = skeleton.GetComponent<ChangeMaterials>().mesh[1];
            }
            else skeleton.GetComponent<MeshRenderer>().material = skeleton.GetComponent<ChangeMaterials>().mesh[0];
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").GetComponent<SendMessage>();
            creeper = GameObject.Find("Creeper");
            skeleton = GameObject.Find("Skeleton");

            //this.curMesh = 0;
            //this.gameObject.GetComponent<MeshRenderer>().material = mesh[0];

            creeper.GetComponent<MeshRenderer>().material = creeper.GetComponent<ChangeMaterials>().mesh[0];
            skeleton.GetComponent<MeshRenderer>().material = skeleton.GetComponent<ChangeMaterials>().mesh[0];
            player.ChaseEvent += ChangeMesh;
            player.StrollEvent += ChangeMesh;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

