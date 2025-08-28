using UnityEngine;
using UnityEngine.UI;

public class TaskbarButton : MonoBehaviour
{
    [Header("References")]
    public Image iconImage;
    public Button button;
    
    private AppInstance associatedApp;
    
    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>();
    }
    
    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }
    
    public void SetApp(AppInstance app)
    {
        associatedApp = app;
        UpdateButtonAppearance();
    }
    
    private void UpdateButtonAppearance()
    {
        if (associatedApp == null) return;
        if (iconImage != null && associatedApp.appIcon != null)
        {
            iconImage.sprite = associatedApp.appIcon;
        }
        UpdateActiveState();
    }
    
    public void UpdateActiveState()
    {
        if (associatedApp == null || button == null) return;
        
        ColorBlock colors = button.colors;
        
        if (associatedApp.isActive)
        {
            colors.normalColor = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            colors.normalColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
        
        button.colors = colors;
    }
    
    private void OnButtonClicked()
    {
        if (associatedApp != null)
        {
            AppEvents.TriggerTaskbarButtonClicked(associatedApp);
        }
    }
}