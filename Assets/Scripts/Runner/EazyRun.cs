using System;
using System.Collections;
using System.Collections.Generic;
using Rubik;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Threading.Tasks;

public class EazyRun : MonoBehaviour
{
    [SerializeField] private Rubik.Rubik rubikMonoBehaviour;
    [SerializeField] private float TimeOut = 10f;
    [SerializeField] public HeuristicSettings settings;

    [Button]
    public void RunTest()
    {
        var rubik = new RubikCube(settings);
        rubik.UseCommand(rubikMonoBehaviour.command);
        var node = new Node(rubik, settings);
        
        Debug.Log("Start ida");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        var task = Task.Run(() => RubikIDA.IdaStar(node));
        if (!task.Wait(TimeSpan.FromSeconds(TimeOut)))
            throw new Exception("Timed out");

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        
        var idaResults = task.Result;
        if (idaResults == null) return;
        
        rubikMonoBehaviour.decision = idaResults.Value.path.ToArray()[0].Command();
        
        Debug.Log("Time " + TimeSpan.FromMilliseconds(elapsedMs).TotalSeconds);
        Debug.Log(rubikMonoBehaviour.decision);
    }
}
