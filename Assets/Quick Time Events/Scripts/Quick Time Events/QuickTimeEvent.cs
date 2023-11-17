using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuickTimeEvent : MonoBehaviour
{
    [SerializeField]
    protected QTE_Data data;
    
    protected int Progress = 0;
    
    [SerializeField]
    protected bool IsRunning = false;
    protected bool IsFinished = false;
    protected float Time;
    
    protected float RockPosition = 0;
    [SerializeField]
    protected Slider rockSlider;
    
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            HandleInput();
            HandleMovement();
            CheckFinish();
        }
    }

    protected abstract void HandleInput();


    protected virtual void HandleMovement()
    {
        rockSlider.value = RockPosition;
    }
    
    protected void CheckFinish()
    {
        if (Progress == data.goal && !IsFinished)
        {
            Finish();
        }
    }
    
    public void StartQTE()
    {
        IsRunning = true;
        IsFinished = false;
        Progress = 0;
        Time = UnityEngine.Time.time;
        RockPosition = 0;
    }

    protected void Finish()
    {
        IsFinished = true;
        IsRunning = false;
        Time = UnityEngine.Time.time - Time;
        HandleOutcome(Time);
        Debug.Log(Time);
    }

    protected void HandleOutcome(float time)
    {
        if (Time < data.averageTime.x)
        {
            //Perfect outcome
        }
        else if (Time > data.averageTime.y)
        {
            //Worst outcome
        }
        else
        {
            //Average outcome
        }
    }
}
