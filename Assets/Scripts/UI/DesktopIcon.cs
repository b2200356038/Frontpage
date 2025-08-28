using UnityEngine;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour
{
    [Header("App Settings")]
    public AppType appType;
    public string appName;
    public Sprite appIcon;
    
    [Header("References")]
    public Image iconImage;
    public TMPro.TextMeshProUGUI nameText;
    public Button button;
    
    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
            
        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>();
            
        if (nameText == null)
            nameText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    
    void Start()
    {
        SetupIcon();
        
        if (button != null)
        {
            button.onClick.AddListener(LaunchApp);
        }
    }
    
    private void SetupIcon()
    {
        if (iconImage != null && appIcon != null)
        {
            iconImage.sprite = appIcon;
        }
        
        if (nameText != null)
        {
            nameText.text = appName;
        }
    }
    
    private void LaunchApp()
    {
        if (AppManager.Instance != null)
        {
            AppManager.Instance.LaunchApp(appType);
        }
    }
}