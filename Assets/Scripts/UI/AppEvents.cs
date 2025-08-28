using System;
using UnityEngine;

public static class AppEvents
{
    public static event Action<AppInstance> OnAppLaunched;
    public static event Action<AppInstance> OnAppClosed;
    public static event Action<AppInstance> OnAppMinimized;
    public static event Action<AppInstance> OnAppFocused;
    public static event Action<AppInstance> OnTaskbarButtonClicked;

    public static void TriggerAppLaunched(AppInstance app) => OnAppLaunched?.Invoke(app);
    public static void TriggerAppClosed(AppInstance app) => OnAppClosed?.Invoke(app);
    public static void TriggerAppMinimized(AppInstance app) => OnAppMinimized?.Invoke(app);
    public static void TriggerAppFocused(AppInstance app) => OnAppFocused?.Invoke(app);
    public static void TriggerTaskbarButtonClicked(AppInstance app) => OnTaskbarButtonClicked?.Invoke(app);
}