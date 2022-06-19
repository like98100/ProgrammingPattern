using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyMono
{
    EnemyFSM skeletonMode;

    float health = 100f;

    public Skeleton(Transform skeleton)
    {
        base.enemyPRS = skeleton;
        skeletonMode = EnemyFSM.Stroll;
    }

    public int GetFSM()
    {
        switch (skeletonMode)
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

    // <5 >6 <10 >15
    public override void UpdateEnemy(Transform player, bool isBorder, bool moveFlag)
    {

        switch (skeletonMode)
        {
            case EnemyFSM.Attack:
                //���� ���� ����� ��
                if (health <= 20.0f)
                {
                    skeletonMode = EnemyFSM.Flee;
                }
                //else if�� �� ��� hp�� 20 ������ �� �Ʒ� �Լ��� �������� ����
                //�ʿ��� ��Ȳ�� ���� else�� ������ ��(�� ������Ʈ���� ü���� ���� �� ������ �켱���� else�� ������)
                // �浹 ���°� �ƴ� ��
                else if (!isBorder)
                {
                    // �߰� ���·� ��ȯ
                    skeletonMode = EnemyFSM.MoveToTarget;
                }
                break;
            case EnemyFSM.Flee:
                if (health >= 60.0f)
                {
                    skeletonMode = EnemyFSM.Stroll;
                }
                break;
            case EnemyFSM.Stroll:
                // �浹 ������ ��
                if (isBorder)
                {
                    // �߰� ���·� ��ȯ
                    skeletonMode = EnemyFSM.MoveToTarget;
                }
                break;
            case EnemyFSM.MoveToTarget:
                // ���� ���� �켱 ���
                if (!moveFlag)
                {
                    // �浹 ������ ��
                    if (isBorder)
                    {
                        // ���� ���·� ��ȯ
                        skeletonMode = EnemyFSM.Attack;
                    }
                }
                // ���� ���� ��� �� �� ���
                else
                {
                    // �浹 ���°� �ƴ� ��
                    if (!isBorder)
                    {
                        // ��ȸ ���·� ��ȯ
                        skeletonMode = EnemyFSM.Stroll;
                    }
                }
                break;
        }

        DoAction(player, skeletonMode);
    }

}

public class SkeletonMono : MonoBehaviour
{
    public Skeleton skeleton;
    public GameObject player;

    Rigidbody rigid;
    BoxCollider col;
    bool isBorder;

    [SerializeField]
    Material[] mesh;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = new Skeleton(this.gameObject.transform);
        player = GameObject.FindGameObjectWithTag("Player");

        rigid = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        col.size = new Vector3(1f, 1f, 1f);
        isBorder = false;

        this.gameObject.GetComponent<MeshRenderer>().material = mesh[0];
    }

    // Update is called once per frame
    void Update()
    {
        int meshNum = 0;
        switch (skeleton.GetFSM())
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
