using System;
using System.Collections.Generic;
using System.Text;

namespace RobotInterface
{ }
public class Robot
{
    public string receivedText = "";
    public float distanceTelemetreDroit;
    public float distanceTelemetreCentre;
    public float distanceTelemetreGauche;

    public Queue<byte> byteListReceived = new Queue<byte>();

    public Robot()
    {

    }
}

