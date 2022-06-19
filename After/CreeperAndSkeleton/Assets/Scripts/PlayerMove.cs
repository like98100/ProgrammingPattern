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
            Move,           // 이동
            Num             // 패턴 갯수
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
                //배회 상태일 때
                case Enemy.EnemyFSM.Stroll:
                    // 거리가 10f 이하면 추적 상태로 변환
                    if (distance[0] < 10.0f)
                    {
                        creeper.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                        sendMessage.EventInvoke(0);
                    }
                        
                    break;
                //추적 상태일 때
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // 거리가 1f 이하면 공격 상태로 변환
                    if(distance[0] < 1.0f) creeper.SetCurState(Enemy.EnemyFSM.Attack);
                    //거리가 15f 이상이면 배회 상태로 변환
                    else if(distance[0] > 15.0f)
                    {
                        creeper.SetCurState(Enemy.EnemyFSM.Stroll);
                        sendMessage.EventInvoke(1);
                    }
                    break;
                // 공격 상태일 때
                case Enemy.EnemyFSM.Attack:
                    //거리가 2f 이상이면 추적 상태로 변환
                    if(distance[0] > 2.0f) creeper.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
            }

            switch (skeleton.GetCurState())
            {
                //배회 상태일 때
                case Enemy.EnemyFSM.Stroll:
                    // 거리가 10f 이하면 추적 상태로 변환
                    if (distance[1] < 10.0f)
                    {
                        skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                        sendMessage.EventInvoke(0);
                    }
                    break;
                //추적 상태일 때
                case Enemy.EnemyFSM.MoveTowardsPlayer:
                    // 거리가 5f 이하면 공격 상태로 변환
                    if (distance[1] < 5.0f) skeleton.SetCurState(Enemy.EnemyFSM.Attack);
                    //거리가 15f 이상이면 배회 상태로 변환
                    else if (distance[1] > 15.0f)
                    {
                        skeleton.SetCurState(Enemy.EnemyFSM.Stroll);
                        sendMessage.EventInvoke(1);
                    }
                    break;
                // 공격 상태일 때
                case Enemy.EnemyFSM.Attack:
                    //거리가 6f 이상이면 추적 상태로 변환
                    if (distance[1] > 6.0f) skeleton.SetCurState(Enemy.EnemyFSM.MoveTowardsPlayer);
                    break;
            }
        }

        // Update is called once per frame
        // 상태 변화 및 사거리 계산을 위한 Update
        void Update()
        {
            // 다음 상태 변화 예약(일정 시간 이후 자동으로 상태가 돌아갈 때 사용)
            if(this.nextStep == PlayerState.None)
            {   // 다음 예정 스텝이 없을 때
                switch(this.step)
                {
                    case PlayerState.Move:
                        break;
                }
            }

            // 다음 상태에 따른 현재 상태 변화(상호 작용 혹은 사운드 재생에 사용)
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

            // 각 상태에서 반복
            switch(this.step)
            {
                case PlayerState.Move:
                    foreach(var key in keys)
                    {
                        if (Input.GetKey(key.Key))  // 공격을 포함한 상호작용이 추가될 경우, 분류할 것
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

        // 이동
        //void Move()
        //{
        //    //moveVec = new Vector3(vAxis, 0, -hAxis).normalized;
        //    moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //    transform.position += moveVec * 20f * Time.deltaTime;
        //}
    }

}