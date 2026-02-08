// SteamNetworkManager.cs

using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Steamworks;

public class SteamNetworkManager : MonoBehaviour
{
    private HSteamListenSocket listenSocket;
    private Dictionary<SteamId, HSteamSocket> peerSockets;

    void Start()
    {
        SteamClient.Init(123456); // Replace with your App ID
        CreateListenSocket();
        peerSockets = new Dictionary<SteamId, HSteamSocket>();
    }

    private void CreateListenSocket()
    {
        listenSocket = SteamNetworkingSockets.CreateListenSocket(new SteamDatagramErrMsg());
        Debug.Log("Listening for P2P connections...");
    }

    async UniTask Update()
    {
        await HandleIncomingConnections();
    }

    private async UniTask HandleIncomingConnections()
    {
        while (true)
        {
            var incomingConnection = SteamNetworkingSockets.AcceptConnection(listenSocket);
            if (incomingConnection != null)
            {
                peerSockets[incomingConnection.RemoteSteamID] = incomingConnection;
                Debug.Log("Connected to " + incomingConnection.RemoteSteamID);
            }
            await UniTask.Yield(); // Yield to avoid blocking the main thread
        }
    }

    public void SendMessage(SteamId recipient, byte[] message)
    {
        if (peerSockets.TryGetValue(recipient, out var socket))
        {
            SteamNetworkingSockets.SendMessage(socket, message);
        }
    }

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }
}