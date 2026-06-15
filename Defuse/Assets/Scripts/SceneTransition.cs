using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private string currentScene;
    private Animator transitionAnimator;
    public static bool canTP = true;
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void TP(string sceneName)
    {
        StartCoroutine(TeleportPlayer(sceneName));
    }

    public IEnumerator TeleportPlayer(string sceneName)
    {
        canTP = false;
        transitionAnimator.SetTrigger("Transition");
        yield return new WaitUntil(() => transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("TransitionBlack"));

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;
        Debug.Log("Unloaded" + sceneName);

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        while (!newScene.isLoaded)
            yield return null;

        SceneManager.SetActiveScene(newScene);

        if (!string.IsNullOrEmpty(currentScene))
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOp.isDone)
                yield return null;
        }

        Debug.Log("Chunkloader Loaded scene " + sceneName + ", unloaded " + currentScene + ".");

        currentScene = sceneName;
        yield return new WaitForSeconds(0.5f);
        FollowPlayer follow = Camera.main.GetComponent<FollowPlayer>();
        if (follow != null)
        {
            follow.SnapToTarget();
        }
        var roomBounds = GameObject.Find("RoomBounds");
        if (roomBounds)
        {
            CameraBounds cameraBound = GameObject.Find("RoomBounds").GetComponent<CameraBounds>();
            if (cameraBound != null)
            {
                cameraBound.ResetBounds();
                yield return null;
                yield return null;
                yield return null;
                cameraBound.ResetBounds();
            }
        }
       
        Debug.Log("TransitionDone");
        transitionAnimator.SetTrigger("StopTransition");
        canTP = true;
    }
}
