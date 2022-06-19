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
        //Vector3 moveVec;
        float[] distance;

        public enum PlayerState
        {
            None,           // None
            Move,           // �̵�
            Num             // ���� ����
        }

        PlayerState step;
        PlayerState nextStep;

        Dictionary<KeyCode, CommandManager.ICommand> keys;
        float time;

        SendMessage sendMessage;

        // Start is called before the first frame update
        void Start()
        {
            creeper = GameObject.Find("Creeper").GetComponent<Creeper>();
            skeleton = GameObject.Find("Skeleton").GetComponent<Skeleton>();
            distance = new float[2] {0, 0};

            step = PlayerState.Move;
            nextStep = PlayerState.None;

            keys = new Dictionary<KeyCode, CommandManager.ICommand>()
            { 
                {KeyCode.UpArrow, new UpCommand()},
                {KeyCode.DownArrow, new DownCommand()},
                {KeyCode.LeftArrow, new LeftCommand()},
                {KeyCode.RightArrow, new RightCommand()}
            };

            time = 0f;

            sendMessage = this.gameObject.GetComponent<SendMessage>();

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
                    if (distance[0] < 10.0f)
                    {
                        creeper.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                        sendMessage.EventInvoke(0);
                    }
                        
                    break;
                //���� ������ ��
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // �Ÿ��� 1f ���ϸ� ���� ���·� ��ȯ
                    if(distance[0] < 1.0f) creeper.SetCurState(Enemy.EnemyFSM.Attack);
                    //�Ÿ��� 15f �̻��̸� ��ȸ ���·� ��ȯ
                    else if(distance[0] > 15.0f)
                    {
                        creeper.SetCurState(Enemy.EnemyFSM.Stroll);
                        sendMessage.EventInvoke(1);
                    }
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
                    if (distance[1] < 10.0f)
                    {
                        skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                        sendMessage.EventInvoke(0);
                    }
                    break;
                //���� ������ ��
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // �Ÿ��� 5f ���ϸ� ���� ���·� ��ȯ
                    if (distance[1] < 5.0f) skeleton.SetCurState(Enemy.EnemyFSM.Attack);
                    //�Ÿ��� 15f �̻��̸� ��ȸ ���·� ��ȯ
                    else if (distance[1] > 15.0f)
                    {
                        skeleton.SetCurState(Enemy.EnemyFSM.Stroll);
                        sendMessage.EventInvoke(1);
                    }
                    break;
                // ���� ������ ��
                case Enemy.EnemyFSM.Attack:
                    //�Ÿ��� 6f �̻��̸� ���� ���·� ��ȯ
                    if (distance[1] > 6.0f) skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
            }
        }

        // Update is called once per frame
        // ���� ��ȭ �� ��Ÿ� ����� ���� Update
        void Update()
        {
            // ���� ���� ��ȭ ����(���� �ð� ���� �ڵ����� ���°� ���ư� �� ���)
            if(this.nextStep == PlayerState.None)
            {   // ���� ���� ������ ���� ��
                switch(this.step)
                {
                    case PlayerState.Move:
                        break;
                }
            }

            // ���� ���¿� ���� ���� ���� ��ȭ(��ȣ �ۿ� Ȥ�� ���� ����� ���)
            if(this.nextStep != PlayerState.None)
            {
                this.step = this.nextStep;
                this.nextStep = PlayerState.None;
                switch(this.step)
                {
                    case PlayerState.Move:
                        break;
                }
            }

            // �� ���¿��� �ݺ�
            switch(this.step)
            {
                case PlayerState.Move:
                    foreach(var key in keys)
                    {
                        if (Input.GetKey(key.Key))  // ������ ������ ��ȣ�ۿ��� �߰��� ���, �з��� ��
                            CommandManager.AddCommand(key.Value, time);
                    }
                    break;
            }

            //hAxis = Input.GetAxisRaw("Horizontal");
            //vAxis = Input.GetAxisRaw("Vertical");

            //Move();

            setDistance();
            setMonsterState();

            time += Time.deltaTime;
        }

        // �̵�
        //void Move()
        //{
        //    //moveVec = new Vector3(vAxis, 0, -hAxis).normalized;
        //    moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //    transform.position += moveVec * 20f * Time.deltaTime;
        //}
    }

}