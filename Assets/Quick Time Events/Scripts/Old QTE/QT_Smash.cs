using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QT : MonoBehaviour
{
    [SerializeField]
    private int x = 0;
    [SerializeField]
    private int goal = 50;
    bool start = false;
    bool finish = false;
    float time;
    
    [SerializeField]
    Slider theRock;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            x++;
            if (!start)
            {
                start = true;
                time = Time.time;
            }
        }
        if (x==goal && !finish)
        {
            finish = true;
            time = Time.time - time;
            Debug.Log(time);
        }
        
        if (start)
        {
            theRock.value = x/(float)goal;
        }
    }
}
