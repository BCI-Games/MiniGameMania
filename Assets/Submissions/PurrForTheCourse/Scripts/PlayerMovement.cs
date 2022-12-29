using Unity.Netcode;
using UnityEngine;

namespace HelloWorld

    //Right now we randomly move the object every frame.
{
    public class PlayerMovement : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(new Vector3(0f, 0f, 0f));   

        public override void OnNetworkSpawn() //Called when a player first spawns into the game
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                //Server assigning the position.
                //var randomPosition = GetRandomPositionOnPlane();
                if (Position.Value != new Vector3(0f, 0f, 0f))
                {
                    transform.position = Position.Value;

                }
                
            }
            else
            {
                //you are a client, so call a server function to change the synced position variable.
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            //Call a func that calculates what you wanna do to the position and on the next frame when update is called we'll move it there
            Position.Value += new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }

        void Update()
        {
            if (NetworkManager.Singleton.IsClient)
            {
                SubmitPositionRequestServerRpc();
            }
                transform.position = Position.Value;
        }
    }
}