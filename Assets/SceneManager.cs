using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private static SceneManager Instance { get; set; }

    private void Awake()
    {
        GameObject.Find("goToScene1").AddComponent<Button>().onClick.AddListener(() =>
        {
            GoToScene(1);
        });

        GameObject.Find("goToScene2").AddComponent<Button>().onClick.AddListener(() =>
        {
            GoToScene(2);
        });

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GoToScene(0);
        }
    }

    public static void GoToScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
    }
}
