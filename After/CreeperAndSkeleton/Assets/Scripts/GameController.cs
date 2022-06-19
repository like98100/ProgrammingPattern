using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
    public class GameController : MonoBehaviour
    {

        [SerializeField]
        GameObject creeper;

        [SerializeField]
        GameObject skeleton;

        // Start is called before the first frame update
        void Start()
        {
            creeper.AddComponent<Creeper>();
            skeleton.AddComponent<Skeleton>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


}
