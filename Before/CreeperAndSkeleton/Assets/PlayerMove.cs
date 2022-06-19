using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float hAxis;
    public float vAxis;
    Vector3 moveVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Move();
    }

    void Move()
    {
        //moveVec = new Vector3(vAxis, 0, -hAxis).normalized;
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * 20f * Time.deltaTime;
    }
}
