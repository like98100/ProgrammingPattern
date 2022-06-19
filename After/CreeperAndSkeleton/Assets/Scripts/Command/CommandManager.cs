using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public interface ICommand
    {
        void Execute();
    }

    public static CommandManager Instance { get; private set; }

    static List<ICommand> commands = new List<ICommand>();

    public List<ICommand> GetCommands()
    {
        return commands;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static void AddCommand(ICommand com)
    {
        com.Execute();
        commands.Add(com);
        Debug.Log("가장 마지막에 들어온 명령 : " + commands[commands.Count - 1]);
    }
}
