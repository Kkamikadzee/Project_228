using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControls : MonoBehaviour
{
    public Initialization initialization;

    private Swipe swipeControls;

    private int _status;
    private Stack<int> _lastStatus = new Stack<int>();
    public GameObject[] sceneCanvas;

    private int _numberOfLevel = LevelReader.levelList.NumberOfLevel;
    public int NumberOfLevel { get { return _numberOfLevel; } set { if (value >= 0) _numberOfLevel = value; } }
    private Game _game;
    [HideInInspector]
    public int Level;

    public Scoring scoring;
    [HideInInspector]
    public int TotalScrore;
    public GameObject TTotalScore;

    public GameObject Field;
    private RenderingGame _GameField;

    public GameObject LevelPassedMenu;

    public GameObject ResetMenu;
    public GameObject RestartMenu;
    public GameObject ExitMenu;

    public GameObject ComingSoon;

    public AudioController audioController;

    private void Start()
    {
        swipeControls = GetComponent<Swipe>();
        _game = GetComponent<Game>();
        _GameField = Field.GetComponent<RenderingGame>();
        UpdateTotalScore();
    }

    private void Update()
    {
        #region Start
        if(_status == 0)
        {
            if (swipeControls.IsDraging)
                if (swipeControls.OneSwipeDirection() == 2)
                    SetStatus(1);
        }
        #endregion

        #region StartGame
        else if(_status == 1)
        {   
            if(Level >= LevelReader.levelList.NumberOfLevel)
            {
                if(!ComingSoon.activeSelf)
                    ComingSoon.SetActive(true);
                if(swipeControls.IsDraging)
                    if (swipeControls.OneSwipeDirection() == 1)
                        SetStatus(0);
            }
            else if (swipeControls.IsDraging)
            {
                if (swipeControls.OneSwipeDirection() == 2)
                    SetStatus(2);
                else if (swipeControls.OneSwipeDirection() == 1)
                    SetStatus(0);
            }
            if (Level < LevelReader.levelList.NumberOfLevel)
            {
                if (ComingSoon.activeSelf)
                    ComingSoon.SetActive(false);
            }
        }
        #endregion

        #region Game
        else if(_status == 2)
        {
            if (_lastStatus.Count != 0)
                _lastStatus.Clear();
            if(Level >= _numberOfLevel)
            {
                SetStatus(1);
            }
            else if(_game.Field == null)
            {
                _game.LevelInitialization(Level);
                _GameField.ColumnCount = _game.Width;
                _GameField.RowCount = _game.Height;
                _GameField.CreateField(_game.Field);
            }
            else
            {
                if (_game.EndGame())
                {
                    LevelPassed();
                }

                else if (swipeControls.IsDraging)
                {
                    switch (swipeControls.OneSwipeDirection())
                    {
                        case 1:
                           if(_game.StepLeft())
                            {
                                UpdateField(1);
                                audioController.PlaySound();
                            }
                            else if (_GameField.lastDirMoves == 2)
                            {
                                StepToUndo();
                                scoring.Fine();
                            }
                            break;
                        case 2:
                            if(_game.StepRight())
                            {
                                UpdateField(2);
                                audioController.PlaySound();
                            }
                            else if (_GameField.lastDirMoves == 1)
                            {
                                StepToUndo();
                                scoring.Fine();
                            }
                            break;
                        case 3:
                            if(_game.StepUp())
                            {
                                UpdateField(3);
                                audioController.PlaySound();
                            }
                            else if (_GameField.lastDirMoves == 4)
                            {
                                StepToUndo();
                                scoring.Fine();
                            }
                            break;
                        case 4:
                            if(_game.StepDown())
                            {
                                UpdateField(4);
                                audioController.PlaySound();
                            }
                            else if (_GameField.lastDirMoves == 3)
                            {
                                StepToUndo();
                                scoring.Fine();
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region Settings
        else if(_status == 3)
        {
            if (swipeControls.IsDraging)
                if (swipeControls.OneSwipeDirection() == 2)
                {
                    audioController.SaveAudioSettings();
                    SetLastStatus();
                }
        }
        #endregion

        #region Help
        else if (_status == 4)
        {
            if (swipeControls.IsDraging)
                if (swipeControls.OneSwipeDirection() == 2)
                    SetLastStatus();
        }
        #endregion
    }

    public void SetStatus(int newStatus)
    {
        switch(_status)
        {
            case 0:
                sceneCanvas[0].SetActive(false);
                break;
            case 1:
                sceneCanvas[1].SetActive(false);
                break;
            case 2:
                sceneCanvas[2].SetActive(false);
                break;
            case 3:
                sceneCanvas[3].SetActive(false);
                break;
            case 4:
                sceneCanvas[4].SetActive(false);
                break;
        }
        switch (newStatus)
        {
            case 0:
                sceneCanvas[0].SetActive(true);
                break;
            case 1:
                sceneCanvas[1].SetActive(true);
                break;
            case 2:
                sceneCanvas[2].SetActive(true);
                break;
            case 3:
                sceneCanvas[3].SetActive(true);
                break;
            case 4:
                sceneCanvas[4].SetActive(true);
                break;
        }
        _lastStatus.Push(_status);
        _status = newStatus;
    }

    public void SetLastStatus()
    {
        switch (_status)
        {
            case 0:
                sceneCanvas[0].SetActive(false);
                break;
            case 1:
                sceneCanvas[1].SetActive(false);
                break;
            case 2:
                sceneCanvas[2].SetActive(false);
                break;
            case 3:
                sceneCanvas[3].SetActive(false);
                break;
            case 4:
                sceneCanvas[4].SetActive(false);
                break;
        }
        if (_lastStatus.Count != 0)
            _status = _lastStatus.Pop();
        switch (_status)
        {
            case 0:
                sceneCanvas[0].SetActive(true);
                break;
            case 1:
                sceneCanvas[1].SetActive(true);
                break;
            case 2:
                sceneCanvas[2].SetActive(true);
                break;
            case 3:
                sceneCanvas[3].SetActive(true);
                break;
            case 4:
                sceneCanvas[4].SetActive(true);
                break;
        }

    }

    public void UpdateField(int dir)
    {
        _GameField.UpdateField((int)_game._stepX.Peek(), (int)_game._stepY.Peek(), dir);
    }

    public void ClearField()
    {
        _game.ClearField();
        _GameField.ClearGame();
        ClearHintField();
        _GameField.CreateField(_game.Field);
    }

    public void StopAndClearGame()
    {
        _GameField.ClearGame();
        ClearHintField();
        _game.ClearGame();
        scoring.ResetScore();
    }

    private void LevelPassed()
    {
        LevelPassedMenu.SetActive(true);
    }

    public void LoadNextLevel()
    {
        TotalScrore += Mathf.RoundToInt(scoring.Score);
        scoring.ResetScore();
        Level++;
        initialization.SaveProgress(Level, TotalScrore);
        UpdateTotalScore();
        StopAndClearGame();
    }

    public void StepToUndo()
    {
        int lastX = _game.PlayerPositionX, lastY = _game.PlayerPositionY;
        if(_game.UndoToStep())
        {
            _GameField.UndoToStep(lastX, lastY, _game.PlayerPositionX, _game.PlayerPositionY);
        }
        audioController.PlaySound();
    }

    public void ResetProgress()
    {
        initialization.ResetProgress();
        Level = 0;
        TotalScrore = 0;
        StopAndClearGame();
        UpdateTotalScore();
    }

    public void ResetMenuEnable()
    {
        ResetMenu.SetActive(true);
    }
    public void ResetMenuDisable()
    {
        ResetMenu.SetActive(false);
    }
    public void RestartMenuEnable()
    {
        RestartMenu.SetActive(true);
    }
    public void RestartMenuDisable()
    {
        RestartMenu.SetActive(false);
    }
    public void ExitMenuEnable()
    {
        ExitMenu.SetActive(true);
    }
    public void ExitMenuDisable()
    {
        ExitMenu.SetActive(false);
    }

    public void UpdateTotalScore()
    {
        if(TTotalScore.activeSelf)
            TTotalScore.GetComponent<Text>().text = LangSystem.lng.TotalScore + ": " + Mathf.Round(TotalScrore).ToString();
    }

    private int _hintCounter = 0;
    public void NextHint()
    {
        if(HintController.StepX.Count == 0)
        {
            HintController.StepX.Push(_game._startPlayerPositionX);
            HintController.StepY.Push(_game._startPlayerPositionY);
        }
        if(_hintCounter < HintController.Hint.level[Level].NumberOfStep)
        {
            if(_hintCounter != 0)
                _GameField.UpdateHintField((int)HintController.StepX.Peek(), (int)HintController.StepY.Peek(), HintController.Hint.level[Level].direction[_hintCounter], HintController.Hint.level[Level].direction[_hintCounter - 1]);
            else
                _GameField.UpdateHintField((int)HintController.StepX.Peek(), (int)HintController.StepY.Peek(), HintController.Hint.level[Level].direction[_hintCounter], -1);
            switch (HintController.Hint.level[Level].direction[_hintCounter])
            {
                case 1:
                    HintController.StepX.Push((int)HintController.StepX.Peek() - 1);
                    HintController.StepY.Push((int)HintController.StepY.Peek());
                    break;
                case 2:
                    HintController.StepX.Push((int)HintController.StepX.Peek() + 1);
                    HintController.StepY.Push((int)HintController.StepY.Peek());
                    break;
                case 3:
                    HintController.StepX.Push((int)HintController.StepX.Peek());
                    HintController.StepY.Push((int)HintController.StepY.Peek() - 1);
                    break;
                case 4:
                    HintController.StepX.Push((int)HintController.StepX.Peek());
                    HintController.StepY.Push((int)HintController.StepY.Peek() + 1);
                    break;
            }
            _hintCounter++;
            scoring.HintFine();
        }
    }
    private void ClearHintField()
    {
        HintController.ClearStackStep();
        _hintCounter = 0;
        _GameField.ClearHintField();
    }
}
