using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class InputHandler : MonoBehaviour
{
    public bool isActive;
	SerialPort stream;
    string receivedData = "EMPTY";

    public bool buttonDownPressed;
    public bool buttonLeftPressed;
    public bool buttonUpPressed;
    public bool buttonRightPressed;
    int leftButtonMask = 1 << 9;
    int downButtonMask = 1 << 6;
    int rightButtonMask = 1 << 8;
    int upButtonMask = 1 << 7;
    public int[] buttonDownArr;
    public int[] buttonPressed;
    public float[] accArr;
    public float[] sliderArr;
    public double micAbs;
    public float micValue;
	public string portSting;

    int buttonVal;

    public static InputHandler Instance { get; private set; }

    void Awake()
    {
		portSting = "COM" + Manager.getInstance ().port.ToString();
		Debug.Log (portSting);
		stream = new SerialPort(portSting, 115200);
        isActive = Manager.getInstance().controller;
		Debug.Log ("InputHandler Controller: " + isActive);


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }



    // Use this for initialization
    void Start()
    {
		try{
        	stream.Open();
			Debug.Log("Serial Port opened");
		}
		catch(Exception ue){
			Application.LoadLevel(0);
			Debug.Log ("Couldnt open the Portooo");
		}


        buttonDownArr = new int[4];
        buttonPressed = new int[4];
        accArr = new float[3];
        sliderArr = new float[4];

    }

    // Update is called once per frame
    void Update()
    {
        stream.Write("1");
        receivedData = stream.ReadLine();
        buttonVal = System.Convert.ToInt32(receivedData, 16);
        buttonDowns(buttonVal);

        acc();
        slider();
        micro();
    }

    void OnApplicationQuit()
    {


        stream.Write("l 0 0 \r\n");
        stream.ReadLine();
        stream.Write("l 1 0 \r\n");
        stream.ReadLine();
        stream.Write("l 2 0 \r\n");
        stream.ReadLine();
        stream.Write("l 3 0 \r\n");
        stream.ReadLine();

        stream.Write("m 0 \n\r");
        stream.ReadLine();


        Debug.Log("Controller off");
    }

    public int[] getButtonDowns()
    {
        return buttonDownArr;
    }
    void buttonDowns(int buttonVal)
    {
        buttonDownArr = new int[4];
        buttonPressed = new int[4];

        if ((leftButtonMask & buttonVal) >= 1)
        {
            buttonPressed[0] = 1;
            if (!buttonLeftPressed)
            {
                buttonDownArr[0] = 1;
            }
        }
        if ((rightButtonMask & buttonVal) >= 1)
        {
            buttonPressed[1] = 1;
            if (!buttonRightPressed)
            {
                buttonDownArr[1] = 1;
            }
        }
        if ((upButtonMask & buttonVal) >= 1)
        {
            buttonPressed[2] = 1;
            if (!buttonUpPressed)
            {
                buttonDownArr[2] = 1;
            }
        }
        if ((downButtonMask & buttonVal) >= 1)
        {
            buttonPressed[3] = 1;
            if (!buttonDownPressed)
            {
                buttonDownArr[3] = 1;
            }
        }
        buttonLeftPressed = ((leftButtonMask & buttonVal) >= 1);

        buttonRightPressed = ((rightButtonMask & buttonVal) >= 1);

        buttonUpPressed = ((upButtonMask & buttonVal) >= 1);

        buttonDownPressed = ((downButtonMask & buttonVal) >= 1);
    }

    public void SetOutput(string output)
    {
        stream.Write(output);
        stream.ReadLine();

    }

    private void acc()
    {
        stream.Write("a");
        receivedData = stream.ReadLine();

        string[] data = receivedData.Split();
        for (int i = 1; i < 4; i++)
        {
            accArr[i - 1] = System.Convert.ToInt32(data[i], 16);
            if (accArr[i - 1] >= 128)
                accArr[i - 1] -= 256;
            accArr[i - 1] /= 128f;
        }
        accArr[0] *= -1f;
    }

    private void slider()
    {
        stream.Write("4");
        receivedData = stream.ReadLine();

        string[] data = receivedData.Split();
        for (int i = 1; i < 5; i++)
        {
            sliderArr[i - 1] = System.Convert.ToInt32(data[i], 16) / 4095f * 2f - 1f;
        }
    }

    private void micro()
    {
        stream.Write("s");
        receivedData = stream.ReadLine();
        string[] data = receivedData.Split();
        micAbs = System.Convert.ToDouble(data[1]);
        micValue = (float)(micAbs / 32768f);

    }

}
