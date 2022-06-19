using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCommand : CommandManager.ICommand
{
    public RightCommand()
    {

    }

    public void Execute()
    {
        Vector3 moveVec = new Vector3(1, 0, 0);
        GameObject.Find("Player").transform.position += moveVec * 20f * Time.deltaTime;
    }
}
