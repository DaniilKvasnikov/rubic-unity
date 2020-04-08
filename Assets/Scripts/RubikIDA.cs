using System.Collections.Generic;
using Rubik;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public class RubikIDA : MonoBehaviour
{
    private float Found { get; } = float.NaN;
    
    private bool IsFound(float t)
    {
        return float.IsNaN(t);
    }
    
    [SerializeField] private Rubik.Rubik rubikMonoBehaviour;
    [SerializeField] private HeuristicType heuristicType;

    [Button]
    public void RunTest()
    {
        var rubik = new RubikCube();
        rubik.UseCommand(rubikMonoBehaviour.Command);
        var node = new Node(rubik);
        var idaResults = IdaStar(node);
        if (idaResults == null) return;
        rubikMonoBehaviour.Decision = idaResults.Value.path.ToArray()[0].Command();
        Debug.Log(idaResults.Value.bound);
        Debug.Log(rubikMonoBehaviour.Decision);
    }

    private (Stack<Node> path, float bound)? IdaStar(Node root)
    {
        var bound = root.Heuristic(heuristicType);
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
        var f = g + node.Heuristic(heuristicType);
        if (f > bound) return f;
        if (node.IsGoal()) return Found;
        var min = float.PositiveInfinity;
        foreach (var successor in node.Successors(heuristicType))
        {
            if (path.Contains(successor)) continue;
            path.Push(successor);
            var t = Search(path, g + node.Cost(successor, heuristicType), bound);
            if (IsFound(t)) return Found;
            if (t < min) min = t;
            path.Pop();
        }
    
        return min;
    }
}