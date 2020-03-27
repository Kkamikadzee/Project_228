using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text TScrore;

    private const float _startScore = 228;
    private const float _fine = 5;
    private const float _hintFine = 22.8f;
    private float _score = _startScore;

    public float Score { get { return _score; } }

    IEnumerator IEScoring()
    {
        while(_score != 0)
        {
            yield return new WaitForSecondsRealtime(1);
            _score--;
            TScrore.text = LangSystem.lng.Score + ": " + _score.ToString();
        }
    }

    void Update ()
    {
        //StartCoroutine("IEScoring");
        if(!GameObject.Find("LevelPassed"))
        {
            if(_score > 0)
            {
                _score -= Time.deltaTime;
                TScrore.text = LangSystem.lng.Score + ": " + Mathf.Round(_score).ToString();
            }
            else
            {
                _score = 0;
                TScrore.text = LangSystem.lng.Score + ": 0";
            }
        }
    }

    public void ResetScore()
    {
        _score = _startScore;
    }

    public void Fine()
    {
        _score -= _fine;
    }

    public void HintFine()
    {
        _score -= _hintFine;
    }
}
