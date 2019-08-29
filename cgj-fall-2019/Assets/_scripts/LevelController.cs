using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : GameBehaviour
{
    public int TotalNumberOfLevels;
    public int CurrentLevel = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        Map.CreateMap(CurrentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
