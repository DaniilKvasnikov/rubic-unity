using System.Collections.Generic;
using System.Linq;
using Rubik;

public class Node
{
    private readonly RubikCube rubik;
    private HeuristicSettings settings;

    public float H => rubik.H;

    public Node(RubikCube rubik, HeuristicSettings settings)
    {
        this.rubik = rubik;
        this.settings = settings;
    }

    public RubikCube Rubik => rubik;

    public List<Node> Successors()
    {
        return Rubik.Successors().Select(rubikCube => new Node(rubikCube, settings)).OrderBy(e => e.H).ToList();
    }

    public bool IsGoal()
    {
        return Rubik.IsCorrect();
    }
    
    public override string ToString()
    {
        return Rubik.ToString();
    }

    public string Command()
    {
        return Rubik.Decision;
    }

    public float Cost(Node successor)
    {
        return Rubik.Cost(successor);
    }
}