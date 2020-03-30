using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostGameColyseus : MonoBehaviour, GameHoster
{

    public ColyseusClient colyseusClient;
    // Start is called before the first frame update
    void Start()
    {
        colyseusClient = GetComponent<ColyseusClient>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void JoinGame()
    {
        await colyseusClient.ConnectToServer();
        colyseusClient.JoinRoom();
        
    }

    public async void CreateServer()
    {
        await colyseusClient.ConnectToServer();
        colyseusClient.CreateRoom();
        //colyseusClient.JoinRoom();
    }
}
