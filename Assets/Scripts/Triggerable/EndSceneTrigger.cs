using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : TriggerableBase
{
    [SerializeField]
    public string nextScene;

    private int playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggered()
    {
        if (nextScene.Length > 0)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            IsTriggered = true;
        }
    }
}
