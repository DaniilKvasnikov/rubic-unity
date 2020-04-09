using System.Collections.Generic;
using System.Linq;
using Rubik;

public class Node
{
    private readonly RubikCube rubik;

    public Node(RubikCube rubik)
    {
        this.rubik = rubik;
    }

    public RubikCube Rubik => rubik;

    public List<Node> Successors(HeuristicType heuristicType)
    {
        var list = new List<Node>();
        foreach (RubikCube rubikCube in Rubik.Successors(heuristicType))
        {
            var node = new Node(rubikCube);
            list.Add(node);
        }
        return list.OrderBy(e => e.Heuristic(heuristicType)).ToList();
    }

    public bool IsGoal()
    {
        return Rubik.IsCorrect();
    }

    public float Heuristic(HeuristicType heuristicType)
    {
        return Rubik.Heuristic(heuristicType);
    }

    public override string ToString()
    {
        return Rubik.ToString();
    }

    public string Command()
    {
        return Rubik.Decision;
    }

    public float Cost(Node successor, HeuristicType heuristicType)
    {
        return Rubik.Cost(successor, heuristicType);
    }
}