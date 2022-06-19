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

    private void Awake()
    {
        Instance = this;
    }

    public static void AddCommand(ICommand com, float time)
    {
        com.Execute();
    }
}
