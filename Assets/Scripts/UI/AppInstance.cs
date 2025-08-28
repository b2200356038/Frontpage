using UnityEngine;

public class AppInstance : MonoBehaviour
{
    [Header("App Info")]
    public AppType appType;
    public string appName;
    public Sprite appIcon;
    
    [Header("State")]
    public bool isMinimized = false;
    public bool isActive = false;
    
    [Header("References")]
    public RectTransform windowRectTransform;
    public GameObject taskbarButton;
    
    private WindowAnimator windowAnimator;
    
    void Awake()
    {
        if (windowRectTransform == null)
            windowRectTransform = GetComponent<RectTransform>();
            
        windowAnimator = GetComponent<WindowAnimator>();
    }
    
    void Start()
    {
        if (windowAnimator != null)
        {
            windowAnimator.PlayOpenAnimation();
        }
        
        AppEvents.TriggerAppLaunched(this);
        SetActive(true);
    }
    
    public void CloseApp()
    {
        if (windowAnimator != null)
        {
            windowAnimator.PlayCloseAnimation(() => {
                AppEvents.TriggerAppClosed(this);
                Destroy(gameObject);
            });
        }
        else
        {
            AppEvents.TriggerAppClosed(this);
            Destroy(gameObject);
        }
    }
    
    public void MinimizeApp()
    {
        isMinimized = true;
    
        if (windowAnimator != null && taskbarButton != null)
        {
            RectTransform taskbarRect = taskbarButton.GetComponent<RectTransform>();
            Vector3 taskbarPos = taskbarRect.TransformPoint(Vector3.zero);
            Vector3 taskbarLocalPos = windowRectTransform.parent.InverseTransformPoint(taskbarPos);
        
            windowAnimator.PlayMinimizeAnimation(taskbarLocalPos, () => {
                gameObject.SetActive(false);
                AppEvents.TriggerAppMinimized(this);
            });
        }
        else
        {
            gameObject.SetActive(false);
            AppEvents.TriggerAppMinimized(this);
        }
    }
    
    public void FocusApp()
    {
        isMinimized = false;
        transform.SetAsLastSibling(); // En öne getir
        
        if (windowAnimator != null)
        {
            windowAnimator.PlayRestoreAnimation();
        }
        else
        {
            gameObject.SetActive(true);
        }
        
        SetActive(true);
        AppEvents.TriggerAppFocused(this);
    }
    
    public void SetActive(bool active)
    {
        isActive = active;
        UpdateTaskbarButtonAppearance();
    }
    
    private void UpdateTaskbarButtonAppearance()
    {
        if (taskbarButton != null)
        {
            TaskbarButton taskbarBtn = taskbarButton.GetComponent<TaskbarButton>();
            if (taskbarBtn != null)
            {
                taskbarBtn.UpdateActiveState();
            }
        }
    }
}