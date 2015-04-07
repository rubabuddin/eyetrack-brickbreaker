using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketClient : MonoBehaviour {

	private const int listenPort = 6666;
	public GameObject hero;

	private float xPos = 10.0f;
	private float yPos = 10.0f;

	Thread receiveThread;
	UdpClient client;
	public int port;

	//info
	public string lastReceivedUDPPacket = "";
	public string allReceivedUDPPackets = "";

	void Start () {
		init();
	}

	void OnGUI(){
		Rect  rectObj=new Rect (40,10,200,250);
		
		GUIStyle  style  = new GUIStyle ();
		
		style .alignment  = TextAnchor.UpperLeft;
		
		GUI .Box (rectObj,"\n UDP Receiving @ 127.0.0.1 Port "+port
		          		          	          
		          + "\nLast Packet: "+ lastReceivedUDPPacket

		          + "\nLast xPos value: " + xPos

		          + "\n\nNumber of Balls Used: " + Balls.ballCount

		          + "\n" + Move.ScoreText
		          //+ "\n\nAll Messages: \n"+allReceivedUDPPackets
		          
		          ,style );

	}
	private void init(){
		// Define the endpoint from which the messages are sent.
		print ("UPDSend.init()");

		port = 6666;

		print ("Send address 127.0.0.1 Port: " + port);

		// // Listening ---------------------------- 
		// define local endpoint ( where messages are received ) . 
		// Create a new thread for receiving incoming messages create .
		receiveThread = new Thread (new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();

	}

	private void ReceiveData(){

		client = new UdpClient (port);
		while (true) {
			try{
				//Bytes Received
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
				byte[] data = client.Receive(ref anyIP);

				// Bytes encode with the UTF8 encoding in text format .
				string text = Encoding.UTF8.GetString(data);
				lastReceivedUDPPacket=text;
				//current format is: STREAMDATA + tab + Timestamp + tab + GazeX + tab + GazeY.
				char [] delimiterChars = { ' ','\t' };
				string[] textArray = text.Split(delimiterChars);

				string actualX = textArray[2];
				string actualY = textArray[3];
				// Display the retrieved text
				print (">> " + actualX);

				// Latest UDPpacket

				//print ("TEXT received " + text);

				//allReceivedUDPPackets=allReceivedUDPPackets+text;
				xPos = float.Parse(actualX);
				xPos *= 0.0030625f;
				float xMin = -4; //-10;
				float xMax = 10;
				if(xPos < xMin)
					xPos = xMin;
				else if(xPos > xMax)
					xPos = xMax;
				else{}

			}catch(Exception e){
				print (e.ToString());
			}
		}
	}

	public string getLatestUDPPacket(){
		allReceivedUDPPackets = "";
		return lastReceivedUDPPacket;
	}
	
	// Update is called once per frame
	void Update () {
		hero.transform.position = new Vector3(xPos,-4,0);
	}

	void OnApplicationQuit(){
		if (receiveThread != null) {
			receiveThread.Abort();
			Debug.Log("This should say false: " + receiveThread.IsAlive); //must be false
		}
		client.Close(); 
	}
}
