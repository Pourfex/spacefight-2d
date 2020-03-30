using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HostGame : MonoBehaviour, GameHoster
{
    public GameHoster gameHoster;

    public NETWORKINGSTACK stack;
    private void Start()
    {
        if (gameHoster == null)
        {
            switch (stack)
            {
                case NETWORKINGSTACK.MLAPI :
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()
                        .First(p => p.name == "NetworkManagerMLAPI").SetActive(true);
                    gameHoster = GameObject.Find("NetworkManagerMLAPI").GetComponent<GameHoster>();
                    break;
                 case NETWORKINGSTACK.COLYSEUS : 
                     UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()
                         .First(p => p.name == "NetworkManagerColyseus").SetActive(true);
                     gameHoster = GameObject.Find("NetworkManagerColyseus").GetComponent<GameHoster>();
                     break;    
            }
        }
    }

    public void JoinGame()
    {
        gameHoster.JoinGame();
    }

    public void CreateServer()
    {
        gameHoster.CreateServer();
    }
}

public interface GameHoster
{
    void JoinGame();

    void CreateServer();
}

public enum NETWORKINGSTACK
{
    MLAPI, COLYSEUS, NAKAMA
}