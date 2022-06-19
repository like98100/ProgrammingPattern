using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class PlayerMove : MonoBehaviour
    {
        Creeper creeper;
        Skeleton skeleton;

        public float hAxis;
        public float vAxis;
        Vector3 moveVec;
        float[] distance;

        public enum PlayerState
        {
            None,           // None
            Idle,           // ���
            Move,           // �̵�
            Num             // ���� ����
        }

        PlayerState step;
        PlayerState nextStep;

        // Start is called before the first frame update
        void Start()
        {
            creeper = GameObject.Find("Creeper").GetComponent<Creeper>();
            skeleton = GameObject.Find("Skeleton").GetComponent<Skeleton>();
            distance = new float[2] {0, 0};

            step = PlayerState.None;
            nextStep = PlayerState.None;

            setDistance();
        }

        void setDistance()
        {
            distance[0] = (GameObject.Find("Creeper").transform.position - this.gameObject.transform.position).magnitude;
            distance[1] = (GameObject.Find("Skeleton").transform.position - this.gameObject.transform.position).magnitude;
        }

        void setMonsterState()
        {
            switch(creeper.GetCurState())
            {
                //��ȸ ������ ��
                case Enemy.EnemyFSM.Stroll:
                    // �Ÿ��� 10f ���ϸ� ���� ���·� ��ȯ
                    if (distance[0] < 10.0f) creeper.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
                //���� ������ ��
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // �Ÿ��� 1f ���ϸ� ���� ���·� ��ȯ
                    if(distance[0] < 1.0f) creeper.SetCurState(Enemy.EnemyFSM.Attack);
                    //�Ÿ��� 15f �̻��̸� ���� ���·� ��ȯ
                    else if(distance[0] > 15.0f) creeper.SetCurState(Enemy.EnemyFSM.Stroll);
                    break;
                // ���� ������ ��
                case Enemy.EnemyFSM.Attack:
                    //�Ÿ��� 2f �̻��̸� ���� ���·� ��ȯ
                    if(distance[0] > 2.0f) creeper.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
            }

            switch (skeleton.GetCurState())
            {
                //��ȸ ������ ��
                case Enemy.EnemyFSM.Stroll:
                    // �Ÿ��� 10f ���ϸ� ���� ���·� ��ȯ
                    if (distance[1] < 10.0f) skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
                //���� ������ ��
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // �Ÿ��� 5f ���ϸ� ���� ���·� ��ȯ
                    if (distance[1] < 5.0f) skeleton.SetCurState(Enemy.EnemyFSM.Attack);
                    //�Ÿ��� 15f �̻��̸� ���� ���·� ��ȯ
                    else if (distance[1] > 15.0f) skeleton.SetCurState(Enemy.EnemyFSM.Stroll);
                    break;
                // ���� ������ ��
                case Enemy.EnemyFSM.Attack:
                    //�Ÿ��� 6f �̻��̸� ���� ���·� ��ȯ
                    if (distance[1] > 6.0f) skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
            }
        }

        // Update is called once per frame
        // �̵� ��� �� ��Ÿ� ����� ���� Update
        void Update()
        {
            // ���� ���� ��ȭ ����
            if(this.nextStep == PlayerState.None)
            {   // ���� ���� ������ ���� ��
                switch(this.step)
                {
                    case PlayerState.Move:
                        // ��� �Է��� �������� ��
                        break;
                }
            }

            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            Move();

            setDistance();
            setMonsterState();
        }

        // �̵�
        void Move()
        {
            //moveVec = new Vector3(vAxis, 0, -hAxis).normalized;
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * 20f * Time.deltaTime;
        }
    }

}