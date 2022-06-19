using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{

    public delegate void IsChasing();
    public event IsChasing ChaseEvent;
    public event IsChasing StrollEvent;

    public int MyProperty { get; set; }


    public void EventInvoke(int flag)
    {
        switch(flag)
        {
            case 0:
                ChaseEvent?.Invoke();
                break;
            case 1:
                StrollEvent?.Invoke();
                break;
        }
    }
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
