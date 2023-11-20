using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QT_HoldOnMark : MonoBehaviour
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
    Slider theGoalMark;

    [SerializeField]
    private float rock = 0;
    [SerializeField]
    private float rockGoal = 70;
    private float markMovement = 0.1f;
    [SerializeField] private int rockMargin = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!start)
            {
                start = true;
                time = Time.time;
            }
            else
            {
                rock += 0.1f;
            }
        }
        else
        {
            rock -= 0.1f;
        }
        
        if (rock< 0) rock = 0;   
        if (rock > 100) rock = 100;
        

        if (x == goal && !finish)
        {
            finish = true;
            time = Time.time - time;
            Debug.Log(time);
        }

        if (start)
        {
            if (rock <= rockGoal + rockMargin && rock >= rockGoal - rockMargin)
            {
                x++;
            }

            if (timeElapsed > 0.5)
            {
                timeElapsed = 0;
                markMovement = Random.Range(-0.2f, 0.2f);
            }
            else
            {
                timeElapsed += Time.deltaTime;
            }
            rockGoal += markMovement;
            if (rockGoal < 30) rockGoal = 30;
            if (rockGoal > 100) rockGoal = 100;
        }
        
        theRock.value = rock;
        theGoalMark.value = rockGoal;
    }
}
