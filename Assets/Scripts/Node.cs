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

    public List<Node> Successors(HeuristicSettings settings)
    {
        var list = new List<Node>();
        foreach (RubikCube rubikCube in Rubik.Successors(settings.heuristicType))
        {
            var node = new Node(rubikCube);
            list.Add(node);
        }
        return list.OrderBy(e => e.Heuristic(settings)).ToList();
    }

    public bool IsGoal()
    {
        return Rubik.IsCorrect();
    }

    public float Heuristic(HeuristicSettings settings)
    {
        return Rubik.Heuristic(settings);
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