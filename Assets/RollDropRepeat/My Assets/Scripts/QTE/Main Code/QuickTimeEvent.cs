using DefaultNamespace;
using Events.Base;
using Events.EventListeners;
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
    private bool IsFinished = false;
    private float Time;

    
    [SerializeField]
    GameObject guideWindow;
    private bool guideShow = false;
    
    protected float RockPosition = 0;
    [SerializeField]
    protected Slider rockSlider;
    
    [SerializeField]
    protected Camera cam;
    
    [SerializeField]
    protected Audio_Data_Bundle audioData;
    
    [SerializeField] private Image progressBar;
    
    
    [Header("Events")]
    [SerializeField]
    private EmptyGameEvent qteStarted;
    [SerializeField]
    private EmptyGameEvent qteFinished;
    
    [SerializeField]
    private FloatGameEvent willEvent;
    
    [SerializeField]
    private EmptyGameEvent QTEWinEvent;
    
    [SerializeField]
    private EmptyGameEvent QTELoseEvent;

    // Update is called once per frame
    void Awake()
    {
    }
    
    void Update()
    {
        if (guideShow)
        {
            ConfirmGuide();
        }
        if (IsRunning)
        {
            HandleInput();
            HandleMovement();
            CheckFinish();
        }
    }

    protected abstract void HandleInput();

    protected virtual void ConfirmGuide()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            guideShow = false;
            guideWindow.SetActive(false);
            Time = UnityEngine.Time.time;
            IsRunning = true;
            PlaySfxSounds();
        }
    }

    protected virtual void HandleMovement()
    {
        rockSlider.value = RockPosition;
    }
    
    private void CheckFinish()
    {
        progressBar.fillAmount = (float)Progress / data.goal;
        if (Progress == data.goal && !IsFinished)
        {
            Finish();
        }
    }
    
    public virtual void StartQTE()
    {
        qteStarted.Raise(new Empty());
        audioData.audioEvents.playSound.Raise(audioData.MusicData);
        guideShow = true;
        IsFinished = false;
        Progress = 0;
        cam.enabled = true;
        guideWindow.SetActive(true);
        
        progressBar.fillAmount = 0;
        rockSlider.value = RockPosition;
    }

    private void Finish()
    {
        IsFinished = true;
        IsRunning = false;
        qteFinished.Raise(new Empty());
        Time = UnityEngine.Time.time - Time;
        audioData.audioEvents.resetSound.Raise(new Empty());
        HandleOutcome(Time);
        Debug.Log(Time);
    }

    private void HandleOutcome(float time)
    {
        if (Time < data.averageTime.x)
        {
            willEvent.Raise(7.5f);
            QTEWinEvent.Raise(new Empty());
        }
        else if (Time > data.averageTime.y)
        {
            willEvent.Raise(-15f);
            QTELoseEvent.Raise(new Empty());
        }
        else
        {
            willEvent.Raise(0f);
            QTEWinEvent.Raise(new Empty());
        }
        cam.enabled = false;
    }
    
    protected void PlayUISound()
    {
        audioData.audioEvents.playSound.Raise(audioData.UIData);
    }
    
    private void PlaySfxSounds()
    {
        audioData.audioEvents.playSound.Raise(audioData.SFXData);
        audioData.audioEvents.playSound.Raise(audioData.RockData);
        audioData.audioEvents.playSound.Raise(audioData.SissyData);
    }
}
