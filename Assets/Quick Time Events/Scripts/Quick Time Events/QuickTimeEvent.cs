using DefaultNamespace;
using Events.Base;
using Events.GameEvents;
using Sounds.Scripts;
using Unity.VisualScripting;
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
    
    [SerializeField]
    protected Camera cam;
    
    [SerializeField]
    protected Audio_Data_Bundle audioData;
    
    protected PlayerMovement playerMovement;

    private ProceduralGenerationManager manager;

    // Update is called once per frame
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if(playerMovement == null)
        {
            Debug.LogError("No player movement found");
        }
        manager = FindObjectOfType<ProceduralGenerationManager>();
        if(manager == null)
        {
            Debug.LogError("No procedural generation manager found");
        }
    }   
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
    
    public virtual void StartQTE()
    {
        audioData.audioEvent.Raise(audioData);
        IsRunning = true;
        IsFinished = false;
        if(playerMovement != null)
        {
            playerMovement.canMove = false;
        }
        if (manager != null)
        {
            manager.OnMiniGameStart();
        }
        Progress = 0;
        Time = UnityEngine.Time.time;
        cam.enabled = true;
    }

    protected void Finish()
    {
        IsFinished = true;
        IsRunning = false;
        if(playerMovement != null)
        {
            playerMovement.canMove = true;
        }
        if (manager != null)
        {
            manager.OnMiniGameEnd();
        }
        Time = UnityEngine.Time.time - Time;
        audioData.resetSounds.Raise(new Empty());
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
        cam.enabled = false;
    }
}
