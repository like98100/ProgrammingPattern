using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCommand : CommandManager.ICommand
{
    public DownCommand()
    {

    }

    public void Execute()
    {
        Vector3 moveVec = new Vector3(0, 0, -1);
        GameObject.Find("Player").transform.position += moveVec * 20f * Time.deltaTime;
    }

}
