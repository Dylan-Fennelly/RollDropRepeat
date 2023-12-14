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
    
    [SerializeField]
    private EmptyGameEvent qteFinished;

    [SerializeField] private Image progressBar;

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

    private void ConfirmGuide()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
