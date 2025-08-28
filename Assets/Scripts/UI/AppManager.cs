using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;
    
    [Header("Prefabs")]
    public GameObject emailAppPrefab;
    public GameObject newsEditorPrefab;
    public GameObject fileManagerPrefab;
    public GameObject taskbarButtonPrefab;
    
    [Header("References")]
    public Transform desktopContainer;
    public Transform taskbarButtonContainer;
    
    private List<AppInstance> runningApps = new List<AppInstance>();
    
    void Awake()
    {
        Instance = this;
    }
    
    void OnEnable()
    {
        // Event'leri dinle
        AppEvents.OnAppLaunched += HandleAppLaunched;
        AppEvents.OnAppClosed += HandleAppClosed;
        AppEvents.OnAppMinimized += HandleAppMinimized;
        AppEvents.OnAppFocused += HandleAppFocused;
        AppEvents.OnTaskbarButtonClicked += HandleTaskbarButtonClicked;
    }
    
    void OnDisable()
    {
        // Event'leri temizle
        AppEvents.OnAppLaunched -= HandleAppLaunched;
        AppEvents.OnAppClosed -= HandleAppClosed;
        AppEvents.OnAppMinimized -= HandleAppMinimized;
        AppEvents.OnAppFocused -= HandleAppFocused;
        AppEvents.OnTaskbarButtonClicked -= HandleTaskbarButtonClicked;
    }
    
    // Desktop icon'a tıklandığında
    public void LaunchApp(AppType appType)
    {
        // Zaten açık mı?
        AppInstance existingApp = GetRunningApp(appType);
        if (existingApp != null)
        {
            existingApp.FocusApp();
            return;
        }
        
        // Yeni app oluştur
        CreateNewApp(appType);
    }
    
    private void CreateNewApp(AppType appType)
    {
        GameObject prefab = GetAppPrefab(appType);
        if (prefab == null) return;
        
        GameObject newApp = Instantiate(prefab, desktopContainer);
        AppInstance appInstance = newApp.GetComponent<AppInstance>();
        
        if (appInstance != null)
        {
            appInstance.appType = appType;
            runningApps.Add(appInstance);
        }
    }
    
    private GameObject GetAppPrefab(AppType appType)
    {
        switch (appType)
        {
            case AppType.Email: return emailAppPrefab;
            case AppType.NewsEditor: return newsEditorPrefab;
            case AppType.FileManager: return fileManagerPrefab;
            default: return null;
        }
    }
    
    private AppInstance GetRunningApp(AppType appType)
    {
        return runningApps.Find(app => app.appType == appType);
    }
    
    // Event handlers
    private void HandleAppLaunched(AppInstance app)
    {
        CreateTaskbarButton(app);
        SetActiveApp(app);
    }
    
    private void HandleAppClosed(AppInstance app)
    {
        if (app.taskbarButton != null)
            Destroy(app.taskbarButton);
        
        runningApps.Remove(app);
    }
    
    private void HandleAppMinimized(AppInstance app)
    {
        // Başka app'i aktif yap
        if (runningApps.Count > 1)
        {
            AppInstance nextApp = runningApps.Find(a => a != app && !a.isMinimized);
            if (nextApp != null)
                SetActiveApp(nextApp);
        }
    }
    
    private void HandleAppFocused(AppInstance app)
    {
        SetActiveApp(app);
    }
    
    private void HandleTaskbarButtonClicked(AppInstance app)
    {
        if (app.isMinimized || !app.isActive)
        {
            app.FocusApp();
        }
        else
        {
            app.MinimizeApp();
        }
    }
    
    private void CreateTaskbarButton(AppInstance app)
    {
        if (taskbarButtonPrefab == null || taskbarButtonContainer == null) return;
        
        GameObject button = Instantiate(taskbarButtonPrefab, taskbarButtonContainer);
        app.taskbarButton = button;
        
        // Button'a app referansını ver
        TaskbarButton taskbarBtn = button.GetComponent<TaskbarButton>();
        if (taskbarBtn != null)
        {
            taskbarBtn.SetApp(app);
        }
    }
    
    private void SetActiveApp(AppInstance app)
    {
        // Tüm app'leri deactive yap
        foreach (var runningApp in runningApps)
        {
            runningApp.SetActive(false);
        }
        
        // Bu app'i active yap
        app.SetActive(true);
    }
}