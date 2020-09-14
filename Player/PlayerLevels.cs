using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevels : MonoBehaviour
{

    public int playerCurrentXP;
    public int playerCurrentLevel;
    public int[] playerLevels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentLevel < playerLevels.Length)
        {
            if (playerCurrentXP >= playerLevels[playerCurrentLevel])
            {
                LevelUp();
            }
        }
    }

    public void addXP(int xp)
    {
        playerCurrentXP += xp;
    }

    public void LevelUp()
    {
        playerCurrentLevel++;
    }

}
