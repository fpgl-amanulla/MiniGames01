using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public Animator helpPanelAnim;
    public Animator infoPanelAnim;

    private void Start()
    {
        
    }

    public void HelpButton()
    {
        helpPanelAnim.SetTrigger("In");
    }
    public void InfoButton()
    {
        infoPanelAnim.SetTrigger("In");
    }

    public void HelpBackButton()
    {
        helpPanelAnim.SetTrigger("Out");
    }
    public void InfoBackButton()
    {
        infoPanelAnim.SetTrigger("Out");
    }
}
