using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int size = 3;

    public List<GameObject> cubes;

    [ContextMenu("Rubic")]
    public void Generate()
    {
        DestroyOld();
        cubes = new List<GameObject>();
        var positionParent = transform.position;
        var transformParent = transform;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    var position = positionParent + new Vector3(i, j, k);
                    var cube = Instantiate(prefab, position, Quaternion.identity, transformParent);
                    cubes.Add(cube);
                }
            }
        }
    }

    private void DestroyOld()
    {
        if (cubes == null || cubes.Count == 0) return;
        for (var index = 0; index < cubes.Count; index++)
        {
            Destroy(cubes[index]);
        }
    }
}
