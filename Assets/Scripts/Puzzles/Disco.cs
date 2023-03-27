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
    public int puzzleCountNeeded;//quantidade de acertos para resolver o puzzle
    public int puzzleCount;

    private float _timeToReset;
    public float timeStopped = 3f;
    public bool puzzleContinue;

    public GameObject winItems;

    void Awake()
    {
        Instance = this;

        Reset();
    }

    private void Start()
    {
        if(winItems!= null)
        winItems.SetActive(false);
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

         _timeToReset = Time.time + timeStopped;
        puzzleContinue = false;
        yield return new WaitUntil(() => Time.time >= _timeToReset || puzzleContinue);

        if (puzzleCount >= puzzleCountNeeded)
        {
            EndPuzzle();
        }
        else if (puzzleContinue)
        {
            puzzleContinue = false;
            puzzleCount++;
            StartRotation(); 
        }
        else
            Reset();
        
    }


    public void Reset()
    {
        foreach (var _color in colors)
        {
            _color.SetActive(true);
        }

        puzzleCount = 0;
    }

    public void EndPuzzle()
    {
        if (winItems != null)
        {
            winItems.SetActive(true);
        }

        Reset();
    }

    internal void Interact(ColorStatue.Cor cor)
    {
        switch (cor)
        {
            case ColorStatue.Cor.Azul:
                foreach (var _color in colors)
                {
                    if(_color.name == "Azul" && _color.activeInHierarchy)
                    {
                        puzzleContinue = true;
                    }
                }
                break;

            case ColorStatue.Cor.Amarelo:
                foreach (var _color in colors)
                {
                    if (_color.name == "Amarelo" && _color.activeInHierarchy)
                    {
                        puzzleContinue = true;
                    }
                }
                break;

            case ColorStatue.Cor.Verde:
                foreach (var _color in colors)
                {
                    if (_color.name == "Verde" && _color.activeInHierarchy)
                    {
                        puzzleContinue = true;
                    }
                }
                break;

            case ColorStatue.Cor.Vermelho:
                foreach (var _color in colors)
                {
                    if (_color.name == "Vermelho" && _color.activeInHierarchy)
                    {
                        puzzleContinue = true;
                    }
                }
                break;
        }

        if (!puzzleContinue)
            Reset();
    }

    public bool CanInteract()
    {
        return Time.time < _timeToReset;
    }

    public void StartPuzzle()
    {
        puzzleCount = 0;
        StartRotation();
    }
}
