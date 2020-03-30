using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class CubeElement : SerializedMonoBehaviour
{
    [OdinSerialize] public Dictionary<SideOfTheCube, GameObject> side = new Dictionary<SideOfTheCube, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
