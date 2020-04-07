using System.Collections.Generic;
using Rubik;

public class Node
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

    public string Command()
    {
        return rubik.Command();
    }
}