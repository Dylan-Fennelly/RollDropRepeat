using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QT_Timming : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int goal = 5;
    bool start = false;
    bool finish = false;
    private bool scored = false;
    float speed = 0.1f;
    int direction = 1;
    float time;
    float timeElapsed = 0;

    [SerializeField]
    private float rock = 0;
    [SerializeField]
    private int rockGoal;
    [SerializeField] private int rockMargin = 5;
    
    [SerializeField]
    Slider theRock;
    
    [SerializeField]
    Slider theGoalMark;

    // Start is called before the first frame update
    void Start()
    {
        rockGoal = Random.Range(10, 90);
        theGoalMark.value = rockGoal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (!start && !finish)
            {
                start = true;
                time = Time.time;
            }
            else if(rock <= rockGoal + rockMargin && rock  >= rockGoal- rockMargin && !scored)
            {
                x++;
            }
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
            if (timeElapsed >0.4)
            {
                speed = Random.Range(0.1f, 0.22f);
                timeElapsed = 0;
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
            
            rock += speed * direction;
            if (rock is > 100 or < 0)
            {
                direction *= -1;
                scored = false;
            }
            
            theRock.value = rock;
        }


        
    }
}
