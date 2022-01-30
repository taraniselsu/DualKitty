using System.Collections;
using UnityEngine;

public class CatTree : MonoBehaviour
{
    [SerializeField] private float waitAfterDone = 5f;
    private bool done = false;

    private void Start()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.PlayGroovyJungleMusic();
        }
    }

    public void OnReachedTop()
    {
        if (done)
        {
            // Already reached the top
            return;
        }

        if (AudioManager.instance)
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayHeavyDrumBeatsSFX();
        }

        StartCoroutine(Done());
        done = true;
    }

    private IEnumerator Done()
    {
        yield return new WaitForSecondsRealtime(waitAfterDone);

        if (SceneChanger.instance)
            SceneChanger.instance.LoadGameScene();
    }

}
