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
                    if (Application.isBatchMode)
                    {
                        gameHoster.CreateStandaloneServer();
                    }
                    break;
                 case NETWORKINGSTACK.COLYSEUS : 
                     UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()
                         .First(p => p.name == "NetworkManagerColyseus").SetActive(true);
                     gameHoster = GameObject.Find("NetworkManagerColyseus").GetComponent<GameHoster>();
                     break;    
            }
        }

        if (Application.isBatchMode)
        {
            Application.targetFrameRate = 30;
            Debug.Log("I'm in batch mode and will start standalone server");
            gameHoster.CreateStandaloneServer();
        }

#if UNITY_SERVER 
        
        Debug.Log("I'm a Unity server build ! ");
#endif
    }

    public void JoinGame()
    {
        gameHoster.JoinGame();
    }

    public void CreateServer()
    {
        gameHoster.CreateServer();
    }

    public void CreateStandaloneServer()
    {
        gameHoster.CreateStandaloneServer();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameHoster.CreateServer();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameHoster.JoinGame();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            gameHoster.CreateStandaloneServer();
        }
    }
}

public interface GameHoster
{
    void JoinGame();

    void CreateServer();

    void CreateStandaloneServer();
}

public enum NETWORKINGSTACK
{
    MLAPI, COLYSEUS, NAKAMA
}