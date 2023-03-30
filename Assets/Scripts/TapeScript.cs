using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapeScript : DialogMain
{
    public GameObject startButton;
    public Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        AudioManager.instance.PlaySuspenseMusic();

        base.Start();

        startButton.SetActive(true);

        anim.SetBool("on", false);

        if (PlayerPrefs.HasKey("Cutscene"))
            index = PlayerPrefs.GetInt("Cutscene");
    }

    public override void StartDialog()
    {
        ConversationManager.Instance.StartConversation(Dialog[index]);
        startButton.SetActive(false);
        anim.SetBool("on", true);
    }

    public override void NextDialog()
    {
        index++;
        PlayerPrefs.SetInt("Cutscene", index);
        anim.SetBool("on", false);
    }

    public void LoadSceneL1()
    {
        AudioManager.instance.PlayLevelMusic();
        SceneManager.LoadScene("FirstSceneL1");
    }
    public void LoadSceneTutorial()
    {
        AudioManager.instance.PlayTutorialMusic();
        SceneManager.LoadScene("TutorialParte1");
    }

    public void ActiveButton()
    {
        startButton.SetActive(true);
        anim.SetBool("on", false);
    }

  
}
