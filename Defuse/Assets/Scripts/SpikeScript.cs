using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    bool alreadyDied = false;
    SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("Clicker").GetComponent<SceneTransition>();
    }


    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigged");
            if (!alreadyDied) {
                alreadyDied = true;
                sceneTransition.TP("Warp2");
            }
        }
    }
}
