using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Android;
using System;

public class GameManager : MonoBehaviour
{
    bool IsPhotoMaking;

    [SerializeField, Range(0.25f, 1.25f)] float timeOffset = 0.25f;

    AndroidJavaClass jcUnityPlayer;
    AndroidJavaObject joUnityActivity;
    AndroidJavaObject joAndroidPluginAccess;
    AndroidJavaObject context;

    [SerializeField] Button makePhotoBtn;
    public static Action OnPhotoStartMaking { get; set; } = delegate { };
    public static Action OnPhotoFinishMaking { get; set; } = delegate { };

    private void OnDestroy()
    {
        OnPhotoStartMaking = null;
        OnPhotoFinishMaking = null;
    }

    private void Start()
    {
        Screen.fullScreen = false;

        jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        joUnityActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        joAndroidPluginAccess = new AndroidJavaObject("com.flipmorris.flashlightcoremodule.Controller");
        context = joUnityActivity.Call<AndroidJavaObject>("getApplicationContext");

        makePhotoBtn.onClick.AddListener(() =>
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
            else
            {
                if(IsPhotoMaking)
                {
                    return;
                }

                StartCoroutine(nameof(MakePhoto));
            }
        });
    }

    IEnumerator MakePhoto()
    {
        IsPhotoMaking = true;
        OnPhotoStartMaking?.Invoke();
        joAndroidPluginAccess.Call("EnableFlashlight", context, true);

        yield return new WaitForSeconds(timeOffset);

        joAndroidPluginAccess.Call("EnableFlashlight", context, false);
        OnPhotoFinishMaking?.Invoke();
        IsPhotoMaking = false;
    }
}
