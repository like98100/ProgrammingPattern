using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBoxCollider : MonoBehaviour
{
    protected bool isBorder = false;
    int monNum;
    int colNum;
    GameObject parent;
    CreeperMono creeper;
    SkeletonMono skeleton;
    BoxCollider monCol;
    private void Start()
    {
        // �θ� ������Ʈ �ҷ�����
        parent = transform.parent.gameObject;
        // ���� �߰�
        if(parent.name == "Creeper")
        {
            creeper = parent.GetComponent<CreeperMono>();
            skeleton = new SkeletonMono();
            monCol = creeper.GetCol();
            monNum = 0;
        }
        else if(parent.name == "Skeleton")
        {
            skeleton = parent.GetComponent<SkeletonMono>();
            creeper = new CreeperMono();
            monCol = skeleton.GetCol();
            monNum = 1;
        }    
        else
        {
            skeleton = new SkeletonMono();
            creeper = new CreeperMono();
            monNum = -1;
        }

        switch(this.name)
        {
            case "Attack":  //���� ��Ÿ�(1/5)
                colNum = 0;
                break;
            case "Stroll":  //��ȸ ��Ÿ�(10)
                colNum = 1;
                break;
            case "MoveVision":  //�߰� ��Ÿ�(15)
                colNum = 2;
                break;
            case "MoveAttack":  //�߰� �� ���� ��Ÿ�(2/6)
                colNum = 3;
                break;
        }
    }

    private void Update()
    {
        switch(monNum)
        {
            case 0:
                //Debug.Log("creeper");
                if (colNum != 2) creeper.creeper.UpdateEnemy(creeper.player.transform, this.isBorder, false);
                else if(creeper.creeper.GetFSM() == 3) creeper.creeper.UpdateEnemy(creeper.player.transform, this.isBorder, true);
                break;
            case 1:
                //Debug.Log("skeleton" + skeleton.skeleton.GetFSM());
                if (colNum != 2) skeleton.skeleton.UpdateEnemy(skeleton.player.transform, this.isBorder, false);
                else if (skeleton.skeleton.GetFSM() == 3) skeleton.skeleton.UpdateEnemy(skeleton.player.transform, this.isBorder, true);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + "�� player�� �ε������ϴ�");
            //Debug.Log(this.name);
            isBorder = true;
        }
        else isBorder = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + "�� player�� �ε������ϴ�");
            //Debug.Log(this.name);
            isBorder = true;
        }
        else isBorder = false;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player") isBorder = false;

    }

    public bool GetBorder()
    {
        return isBorder;
    }
}
