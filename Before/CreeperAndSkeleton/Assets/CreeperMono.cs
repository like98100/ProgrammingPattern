using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : EnemyMono
{
    EnemyFSM creeperMode;

    float health = 100f;

    public Creeper(Transform creeper)
    {
        base.enemyPRS = creeper;
        creeperMode = EnemyFSM.Stroll;
    }

    public int GetFSM()
    {
        switch(creeperMode)
        {
            case EnemyFSM.Attack:
                return 0;
            case EnemyFSM.Flee:
                return 1;
            case EnemyFSM.Stroll:
                return 2;
            case EnemyFSM.MoveToTarget:
                return 3;
                
        }

        return -1;
    }

    // <1 >2 <10 >15
    public override void UpdateEnemy(Transform player, bool isBorder, bool moveFlag)
    {
        switch(creeperMode)
        {
            case EnemyFSM.Attack:
                if (health <= 20.0f)
                {
                    creeperMode = EnemyFSM.Flee;
                }
                // �浹 ���°� �ƴ� ��
                else if (!isBorder)
                {
                    // �߰� ���·� ��ȯ
                    creeperMode = EnemyFSM.MoveToTarget;
                }
                break;
            case EnemyFSM.Flee:
                if (health >= 60.0f)
                {
                    creeperMode = EnemyFSM.Stroll;
                }
                break;
            case EnemyFSM.Stroll:
                // �浹 ������ ��
                if (isBorder)
                {
                    // �߰� ���·� ��ȯ
                    creeperMode = EnemyFSM.MoveToTarget;
                }
                break;
            case EnemyFSM.MoveToTarget:
                // ���� ���� �켱 ���
                if(!moveFlag)
                {
                    // �浹 ������ ��
                    if (isBorder)
                    {
                        // ���� ���·� ��ȯ
                        creeperMode = EnemyFSM.Attack;
                    }
                }
                // ���� ���� ��� �� �� ���
                else
                {
                    // �浹 ���°� �ƴ� ��
                    if (!isBorder)
                    {
                        // ��ȸ ���·� ��ȯ
                        creeperMode = EnemyFSM.Stroll;
                    }
                }
                break;
        }

        DoAction(player, creeperMode);
    }

}

public class CreeperMono : MonoBehaviour
{
    public Creeper creeper;
    public GameObject player;

    Rigidbody rigid;
    BoxCollider col;
    bool isBorder;

    [SerializeField]
    Material[] mesh;

    // Start is called before the first frame update
    void Start()
    {
        creeper = new Creeper(this.gameObject.transform);
        player = GameObject.FindGameObjectWithTag("Player");

        rigid = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        col.size = new Vector3(1f, 1f, 1f);
        isBorder = false;

        this.gameObject.GetComponent<MeshRenderer>().material = mesh[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int meshNum = 0;
        switch(creeper.GetFSM())
        {
            case 0:
                meshNum = 1;
                break;
            case 1:
                meshNum = 0;
                break;
            case 2:
                meshNum = 0;
                break;
            case 3:
                meshNum = 1;
                break;
        }

        this.gameObject.GetComponent<MeshRenderer>().material = mesh[meshNum];
    }

    public BoxCollider GetCol()
    {
        return col;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + "�� player�� �ε������ϴ�");
            isBorder = true;
        }
        else isBorder = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + "�� player�� �ε������ϴ�");
            isBorder = true;
        }
        else isBorder = false;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player") isBorder = false;
        
    }
}
