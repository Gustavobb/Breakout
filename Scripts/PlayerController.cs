using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int maxPlayers = 2;
    int playerCount = 1;

    public bool DuplicatePlayer()
    {
        if (playerCount < maxPlayers) 
        {
            playerCount ++;
            return true;
        }

        return false;
    }

    public void DestroyPlayer() => playerCount --;
}
