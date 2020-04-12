using System;
using System.Collections.Generic;
using UnityEngine;

public class RubikGenerator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private const int Size = 3;

    public List<GameObject> cubes;
    public List<CubeElement> cubeElements;
    
    private void Awake()
    {
        Generate();
    }
    
    [ContextMenu("GenerateRubik")]
    private void Generate()
    {
        cubes = new List<GameObject>();
        cubeElements= new List<CubeElement>();
        var positionParent = transform.position;
        var transformParent = transform;
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int z = 0; z < Size; z++)
                {
                    var position = positionParent + new Vector3(x, y, z);
                    var cube = Instantiate(prefab, position, Quaternion.identity, transformParent);
                    cubes.Add(cube);
                    
                    var cubeElement = cube.GetComponent<CubeElement>();
                    if (cubeElement == null) throw new Exception("CubeElement not found");
                    cubeElement.side[SideOfTheCube.FRONT].SetActive(x == Size - 1);
                    cubeElement.side[SideOfTheCube.BACK].SetActive(x == 0);
                    cubeElement.side[SideOfTheCube.UP].SetActive(y == Size - 1);
                    cubeElement.side[SideOfTheCube.DOWN].SetActive(y == 0);
                    cubeElement.side[SideOfTheCube.LEFT].SetActive(z == 0);
                    cubeElement.side[SideOfTheCube.RIGHT].SetActive(z == Size - 1);
                    cubeElements.Add(cubeElement);
                }
            }
        }
    }
}
