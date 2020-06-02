using System.Collections;
using System.Collections.Generic;
using Rubik;
using Sirenix.OdinInspector;
using UnityEngine;

public class RubikVisualization : SerializedMonoBehaviour
{
    
    private RubikCube rubikCube;
    public RubikCube RubikCube => rubikCube ?? (rubikCube = new RubikCube());
    
    [SerializeField] private SideMiniCube[] miniCubes;
    [SerializeField] private Dictionary<RubikSide, Color> colors = new Dictionary<RubikSide, Color>();
    [SerializeField] private Rubik.Rubik rubik;

    [Button]
    public void ResetCube()
    {
        RubikCube.Reset();
        DrawCube();
    }

    [Button]
    private void MixUpCube()
    {
        ResetCube();
        RubikCube.UseCommand(rubik.Command);
        DrawCube();
    }

    [Button]
    private void UseDecision()
    {
        MixUpCube();
        RubikCube.UseDecision(rubik.Decision);
        DrawCube();
    }

    [Button]
    public void RandomCommand(int len)
    {
        rubik.Command = RubikCube.GetRandomCommands(len);
    }
    
    private void DrawCube()
    {
        var h = RubikHeuristicManhattan.GetHeuristicMap(RubikCube);
        for (int i = 0; i < RubikCube.Cube.Length; i++)
        {
            miniCubes[i].SetCube(GetColor, RubikCube.Cube[i], h[i]);
        }
    }
    
    public Color GetColor(RubikSide rubikInfoSide)
    {
        return colors[rubikInfoSide];
    }
}
