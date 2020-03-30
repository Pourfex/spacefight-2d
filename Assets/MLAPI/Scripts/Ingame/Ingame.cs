using System.Linq;
using Camera;
using MLAPI;
using UnityEngine;

namespace Ingame {
    public class Ingame : MonoBehaviour
    {
        private CameraFollowTarget cameraTarget;
        
        private void Start() {
            SetupCamera();
        }

        private void SetupCamera() {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            var player = players.First(p => p.GetComponent<NetworkedBehaviour>().IsLocalPlayer);
            Debug.Log(player);
            if (cameraTarget == null)
            {
                cameraTarget = GameObject.Find("Main Camera").GetComponent<CameraFollowTarget>();
            }
            cameraTarget.target = player.transform;
        }
    }
}
