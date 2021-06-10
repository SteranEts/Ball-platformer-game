using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float speed = 0;
    private GameObject ball;
    private Rigidbody ballRB;
    private string messageWb;
    public List<Button> ListMenu;
    private Button activeButton;
    private float movementX;
    private float movementY;
    WebSocket websocket;

    // Start is called before the first frame update
    async void Start()
    {
        ball = GameObject.Find("Player");
        ballRB = ball.GetComponent<Rigidbody>();
        speed = ball.GetComponent<PlayerController>().speed;
        websocket = new WebSocket("ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            // getting the message as a string
             var message = System.Text.Encoding.UTF8.GetString(bytes);
             Debug.Log("OnMessage! " + message);
            messageWb = message;
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
        if (!string.IsNullOrEmpty(messageWb))
        {
            switch (messageWb)
            {
                case "Right":
                    ballRB.AddForce(Vector3.right * speed);
                    break;
                case "Left":
                    ballRB.AddForce(Vector3.left * speed);
                    break;
                case "Forward":
                    ballRB.AddForce(Vector3.forward * speed);
                    break;
                case "Down":
                    ballRB.AddForce(Vector3.back * speed);
                    break;
                case "Jump":
                    ballRB.AddForce(Vector3.up * speed);
                    break;
                case "Select":
                    //
                    break;
                case "ScrollUp":
                    //
                    break;
                case "ScrollDown":
                    //
                    break;
                case "Neutral":
                    //
                    break;
            }    

        }
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    //private toggleActiveButton() 
    //{
    //    if (activeButton == null)
    //    {
    //        activeButton = ListMenu[0];
    //    }
    //    else 
    //    {
    //        if (activeButton == ListMenu[0])
    //        {
    //            activeButton = ListMenu[1];
    //        }
    //        else 
    //        {
    //            activeButton =
    //        }
    //    }
    //}

}
