using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public struct Connection { }

public class DialogeSegments : Node
{
    [Input]
    public Connection input;

    public string dialogetext;

    [Output(dynamicPortList = true)]
    public List<string> answers;
}
