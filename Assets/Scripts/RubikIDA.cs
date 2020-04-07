using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public class RubikIda : MonoBehaviour
{
    private float Found { get; } = float.NaN;
    
    private bool IsFound(float t)
    {
        return float.IsNaN(t);
    }
    
    [SerializeField] private Rubik.Rubik rubik;

    [Button]
    public void RunTest()
    {
        var node = new Node(rubik.rubikCube);
        var idaResults = IdaStar(node);
        if (idaResults == null) return;
        Debug.Log(idaResults.Value.bound);
        foreach (var node1 in idaResults.Value.path)
        {
            Debug.Log(node1.Command());
        }
    }

    private (Stack<Node> path, float bound)? IdaStar(Node root)
    {
        var bound = root.Heuristic();
        var path = new Stack<Node>();
        path.Push(root);
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