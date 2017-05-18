using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public static class Tags {

    public static string Player
    {
        get { return "Player"; }
    }

    public static string Enemy
    {
        get { return "Enemy"; }
    }

    public static string GameController
    {
        get { return "GameController"; }
    }

    /// <summary>
    /// Determine if the game object with <see cref="tag1"/> is an adversary of <see cref="tag2"/>.
    /// </summary>
    /// <param name="tag1"></param>
    /// <param name="tag2"></param>
    /// <returns></returns>
    public static bool isEnemy(string tag1, string tag2)
    {
        return ((tag1.Equals(Player) && tag2.Equals(Enemy)) || (tag1.Equals(Enemy) && tag2.Equals(Player)));
    }
    
}
