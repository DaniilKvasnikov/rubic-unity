using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using NUnit.Framework;
using Rubik;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class RubikIDA : MonoBehaviour
{
    private float Found { get; } = float.NaN;

    private bool IsFound(float t)
    {
        return float.IsNaN(t);
    }

    [SerializeField] private RubikMonobehaviour rubik;

    [Button]
    public void RunTest()
    {
        var node = new Node(rubik.rubikCube);
        var idaResults = IdaStar(node);
        if (idaResults != null)
        {
            Debug.Log(idaResults.Value.bound);
            foreach (var node1 in idaResults.Value.path)
            {
                Debug.Log(node1);
            }
        }
    }

    private (Stack<Node> path, float bound)? IdaStar(Node root)
    {
        var bound = root.Heuristic();
        var path = new Stack<Node>();
        while (true)
        {
            var t = Search(path, 0f, bound);
            if (IsFound(t)) return (path, bound);
            if (float.IsInfinity(t)) return null;
            bound = t;
        }
    }

    private float Search(Stack<Node> path, float g, float bound)
    {
        var node = path.Peek();
        var f = g + node.Heuristic();
        if (f > bound) return f;
        if (node.IsGoal()) return Found;
        var min = float.PositiveInfinity;
        foreach (var successor in node.Successors())
        {
            if (path.Contains(successor)) continue;
            path.Push(successor);
            var t = Search(path, g + Cost(node, successor), bound);
            if (IsFound(t)) return Found;
            if (t < min) min = t;
            path.Pop();
        }

        return min;
    }

    private float Cost(Node node, Node successor)
    {
        return 1f;
    }
}

internal class Node
{
    private readonly RubikCube rubik;

    public Node(RubikCube rubik)
    {
        this.rubik = rubik;
    }

    public IEnumerable<Node> Successors()
    {
        var list = new List<Node>();
        foreach (var command in RubikCube.Commands)
        {
            for (int i = 0; i < 2; i++)
            {
                bool reverse = i == 1;
                string commandString = command + (reverse ? "\'" : "");
                RubikCube rubikCube = new RubikCube(rubik);
                rubikCube.UseCommands(commandString);
                var node = new Node(rubikCube);
                list.Add(node);
            }
        }

        return list;
    }

    public bool IsGoal()
    {
        return rubik.IsCorrect();
    }

    public float Heuristic()
    {
        return rubik.Heuristic();
    }

    public override string ToString()
    {
        return rubik.ToString();
    }
}
