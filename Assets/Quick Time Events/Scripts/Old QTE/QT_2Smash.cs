using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QT_2Smash : MonoBehaviour
{
    [SerializeField]
    private int x = 0;
    [SerializeField]
    private int goal = 100;
    bool start = false;
    bool finish = false;
    bool first = true;
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
        
        if (Input.GetKeyUp(KeyCode.A) && first)
        {
            x++;
            first = false;
            if (!start)
            {
                start = true;
                time = Time.time;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) && !first)
        {
            x++;
            first = true;
        }
        
        if (start)
        {
            theRock.value = x/(float)goal;
        }
        
        if (x==goal && !finish)
        {
            finish = true;
            time = Time.time - time;
            Debug.Log(time);
        }
    }
}
