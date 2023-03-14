using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    public static Disco Instance;

    [SerializeField] List<GameObject> colors = new List<GameObject>();

    private int index;
    public float rotationSpeed = .5f;
    public int rotationCount;

    public float timeStopped = 3f;
    public bool puzzleContinue;

    void Awake()
    {
        Instance = this;

        Reset();
    }

    void Start()
    {
        StartRotation();
    }

    public void ChangeColor()
    {
        colors[index].SetActive(false);
        index++;
        if(index >= colors.Count)
        {
            index = 0;
        }
        colors[index].SetActive(true);
    }

    public void StartRotation()
    {
        foreach (var _color in colors)
        {
            _color.SetActive(false);
        }

        index = Random.Range(0, colors.Count - 1);
        StartCoroutine("DiscRotation");
    }

    IEnumerator DiscRotation()
    {
        for (int i = 0; i < rotationCount * colors.Count; i++)
        {
            ChangeColor();
            yield return new WaitForSeconds(rotationSpeed);
        }

        float _timeToReset = Time.time + timeStopped;
        puzzleContinue = false;
        yield return new WaitUntil(() => Time.time >= _timeToReset || puzzleContinue);


        if (puzzleContinue) StartRotation();
        else
        {
            Reset();
        }
    }


    public void Reset()
    {
        foreach (var _color in colors)
        {
            _color.SetActive(true);
        }
    }
}
