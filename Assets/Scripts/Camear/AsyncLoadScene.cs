using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    public Slider loadingSlider;
    public Text loadingText;

    private float loadingSpeed = 1;
    private float loadingProgress;

    private AsyncOperation nextScene;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        loadingSlider.value = 0.0f;

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        nextScene = SceneManager.LoadSceneAsync(SceneName.nextSceneName);
        //阻止当加载完成自动切换
        nextScene.allowSceneActivation = false;

        yield return nextScene;
    }

    // Update is called once per frame
    void Update()
    {
        loadingProgress = nextScene.progress;

        if (nextScene.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            loadingProgress = 1.0f;
        }

        if (loadingProgress != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, loadingProgress, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - loadingProgress) < 0.01f)
            {
                loadingSlider.value = loadingProgress;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            nextScene.allowSceneActivation = true;
        }
    }
}


public class SceneName
{
    public static string nextSceneName;
}
