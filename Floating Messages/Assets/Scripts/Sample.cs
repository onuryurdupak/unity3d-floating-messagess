using UnityEngine;
using WF.FloatingMessages;

public class Sample : MonoBehaviour
{
    private void Start()
    {
        Messager.Print("Print with default font settings; last 30 seconds", 30);
        Messager.Print("Print green with default size; last 10 seconds", 10, Color.green);
        Messager.Print("Print red with size of 32; last 30 seconds", 30, Color.red, 32);
        Messager.Print("Very long text: lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum", 30, Color.green, 50);
    }
}
