using System;
using System.Collections.Generic;
using UnityEngine;

public class RubikGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int size = 3;

    public List<GameObject> cubes;
    public List<CubeElement> cubeElements;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        cubes = new List<GameObject>();
        cubeElements= new List<CubeElement>();
        var positionParent = transform.position;
        var transformParent = transform;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    var position = positionParent + new Vector3(x, y, z);
                    var cube = Instantiate(prefab, position, Quaternion.identity, transformParent);
                    cubes.Add(cube);
                    
                    var cubeElement = cube.GetComponent<CubeElement>();
                    cubeElement.side[SideOfTheCube.FRONT].active = (x == size - 1);
                    cubeElement.side[SideOfTheCube.BACK].active = (x == 0);
                    cubeElement.side[SideOfTheCube.UP].active = (y == size - 1);
                    cubeElement.side[SideOfTheCube.DOWN].active = (y == 0);
                    cubeElement.side[SideOfTheCube.LEFT].active = (z == 0);
                    cubeElement.side[SideOfTheCube.RIGHT].active = (z == size - 1);
                    if (cubeElement == null) throw new Exception("CubeElement not found");
                    cubeElements.Add(cubeElement);
                }
            }
        }
    }
}
