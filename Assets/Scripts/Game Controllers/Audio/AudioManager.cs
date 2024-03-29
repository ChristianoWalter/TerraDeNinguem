using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    public AudioSource mainMenuMusic, levelOneMusic, skeletonBossMusic, aboboroBossMusic, suspenseMusic, tutorialLevelMusic, endMusic;

    public AudioSource[] sfx;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    

    public void PlayMainMenu()
    {
        levelOneMusic.Stop();
        skeletonBossMusic.Stop();
        aboboroBossMusic.Stop();
        suspenseMusic.Stop();
        tutorialLevelMusic.Stop();

        mainMenuMusic.Play();
    }
    
    public void PlayAboboroBoss()
    {
        mainMenuMusic.Stop();
        levelOneMusic.Stop();
        skeletonBossMusic.Stop();
        suspenseMusic.Stop();
        tutorialLevelMusic.Stop();

        aboboroBossMusic.Play();

    }

    public void PlaySuspenseMusic()
    {
        tutorialLevelMusic.Stop(); 
        mainMenuMusic.Stop();
        skeletonBossMusic.Stop();
        aboboroBossMusic.Stop();
        levelOneMusic.Stop();

        suspenseMusic.Play();
    }
    public void PlayEndMusic()
    {
        tutorialLevelMusic.Stop(); 
        mainMenuMusic.Stop();
        skeletonBossMusic.Stop();
        aboboroBossMusic.Stop();
        levelOneMusic.Stop();
        suspenseMusic.Stop();

        endMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelOneMusic.isPlaying)
        {
            mainMenuMusic.Stop();
            skeletonBossMusic.Stop();
            aboboroBossMusic.Stop();
            tutorialLevelMusic.Stop();
            suspenseMusic.Stop();

            levelOneMusic.Play();
        }
    }

    public void PlayTutorialMusic()
    {

        if (!tutorialLevelMusic.isPlaying)
        {
            tutorialLevelMusic.Play();

            mainMenuMusic.Stop();
            skeletonBossMusic.Stop();
            aboboroBossMusic.Stop();
            suspenseMusic.Stop();
            levelOneMusic.Stop();
        }
    }

    public void PlaySkeletonBossMusic()
    {
        mainMenuMusic.Stop();
        aboboroBossMusic.Stop();
        tutorialLevelMusic.Stop();
        levelOneMusic.Stop();
        suspenseMusic.Stop();

        skeletonBossMusic.Play();
    }

    public void PlaySfx(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySfxAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySfx(sfxToAdjust);
    }
}
