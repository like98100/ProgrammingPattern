using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : CommandManager.ICommand
{
    public LeftCommand()
    {

    }

    public void Execute()
    {
        Vector3 moveVec = new Vector3(-1, 0, 0);
        GameObject.Find("Player").transform.position += moveVec * 20f * Time.deltaTime;
    }
}
