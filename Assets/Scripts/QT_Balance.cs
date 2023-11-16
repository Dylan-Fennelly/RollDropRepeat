using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QT_Balance : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int goal = 50000;
    bool start = false;
    bool finish = false;
    float time;
    float timeElapsed = 0;

    [SerializeField]
    Slider theRock;
    
    [SerializeField]
    private float rock = 50;
    private float rockMovement = 0.1f;
    [SerializeField]
    private int rockGoal = 50;
    [SerializeField] private int rockMargin = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (!start && !finish)
            {
                start = true;
                time = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rock -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rock += 0.1f;
        }
        if (x==goal && !finish)
        {
            finish = true;
            time = Time.time - time;
            Debug.Log(time);
            start = false;
        }

        if (start)
        {
            if(rock <= rockGoal + rockMargin && rock  >= rockGoal- rockMargin)
            {
                x++;
            }

            if (timeElapsed > 0.4)
            {
                
            }
            else
            {
                timeElapsed += Time.deltaTime;   
            }
            rock += rockMovement;
            if (rock < 0) rock = 0;
            if (rock > 100) rock = 100;
            
            theRock.value = rock;
        }


        
    }
}
