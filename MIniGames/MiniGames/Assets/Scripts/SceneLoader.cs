using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public Animator animator;
    public float transitionTime = 1.0f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string _name)
    {
        StartCoroutine(StartLoadScene(_name));
    }
    public IEnumerator StartLoadScene(string _name)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_name);
    }
    public void LoadScene(int _index)
    {
        StartCoroutine(StartLoadScene(_index));
    }
    public IEnumerator StartLoadScene(int _index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_index);
    }
}
