using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipCutScene : MonoBehaviour
{
   [SerializeField] PlayableDirector cutscene;
    [SerializeField] float skipTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            cutscene.time=skipTo;
    }
}
