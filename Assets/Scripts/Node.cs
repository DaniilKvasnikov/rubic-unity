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

    public IEnumerable<Node> Successors(HeuristicType heuristicType)
    {
        var list = new List<Node>();
        foreach (var command in RubikCube.Commands)
        {
            for (int i = 0; i < 2; i++)
            {
                bool reverse = i == 1;
                string commandString = command + (reverse ? "\'" : "");
                RubikCube rubikCube = new RubikCube(Rubik);
                rubikCube.UseDecision(commandString);
                var node = new Node(rubikCube);
                list.Add(node);
            }
        }
        
        return list.OrderBy(e => e.Heuristic(heuristicType));
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