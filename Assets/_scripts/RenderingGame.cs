using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderingGame : MonoBehaviour {

    public GameObject Cells;

    private int _ColumnCount;
    private int _RowCount;
    public int ColumnCount { set { if (value > 0) _ColumnCount = value; } }
    public int RowCount { set { if (value > 0) _RowCount = value; } }

    private const int _FieldSizeX = 700, _FieldSizeY = 900;
    private int _CellSize;

    private List<GameObject> _field = new List<GameObject>();

    #region Game
    [Header("Game")]
    public Sprite IEmptyCell;
    public Sprite IInvalidCell;
    //public Sprite IPlayerPositionCell;
    //public Sprite IFilledCell;

    public Sprite[] IFilledCell = new Sprite[6];
    public Sprite[] IFilledCellStart = new Sprite[4];
    public Sprite[] IFilledCellState = new Sprite[5];
    #endregion

    #region Hint
    [Header("Hint")]
    public Sprite HintIEmptyCell;
    public Sprite HintIInvalidCell;
    public Sprite[] HintIFilledCell = new Sprite[6];
    public Sprite[] HintIFilledCellStart = new Sprite[4];
    public Sprite[] HintIFilledCellState = new Sprite[5];

    private List<GameObject> _hintField = new List<GameObject>();
    #endregion

    private Stack _dirMoves = new Stack();
    public int lastDirMoves { get{ return (int)_dirMoves.Peek(); } }
    [Space]
    public GameObject Contour;

    public GameObject HintField;

    public void UpdateField(int[,] _field)
    {
        for (int i = 0; i < _RowCount; i++)
        {
            for (int j = 0; j < _ColumnCount; j++)
            {
                UpdateCell(_field[i, j], j, i);
            }
        }
    }
    private void UpdateCell(int status, int numberX, int numberY)
    {
        switch (status)
        {
            case 0:
                _field[(numberY * _ColumnCount) + numberX].GetComponent<Image>().sprite = IEmptyCell;
                break;
            case 1:
                _field[(numberY * _ColumnCount) + numberX].GetComponent<Image>().sprite = IInvalidCell;
                break;
            case 2:
                _field[(numberY * _ColumnCount) + numberX].GetComponent<Image>().sprite = IFilledCellStart[5];
                break;
            case 3:
                //_field[(numberY * _ColumnCount) + numberX].GetComponent<Image>().sprite = IFilledCell;
                break;
        }
    }

    public void UpdateField(int x, int y, int dir)
    {
        if (_dirMoves.Count == 0)
        {
            switch (dir)
            {
                case 1:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellStart[dir - 1];
                    _field[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = IFilledCellState[1];
                    break;
                case 2:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellStart[dir - 1];
                    _field[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = IFilledCellState[0];
                    break;
                case 3:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellStart[dir - 1];
                    _field[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[3];
                    break;
                case 4:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellStart[dir - 1];
                    _field[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[2];
                    break;
            }
        }
        else
        {
            switch((int)_dirMoves.Peek())
            {
                case 1:
                    switch(dir)
                    {
                        case 1:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[0];
                            _field[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = IFilledCellState[1];
                            break;
                        case 3:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[4];
                            _field[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[3];
                            break;
                        case 4:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[5];
                            _field[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[2];
                            break;
                    }
                    break;
                case 2:
                    switch (dir)
                    {
                        case 2:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[0];
                            _field[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = IFilledCellState[0];
                            break;
                        case 3:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[2];
                            _field[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[3];
                            break;
                        case 4:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[3];
                            _field[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[2];
                            break;
                    }
                    break;
                case 3:
                    switch (dir)
                    {
                        case 1:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[3];
                            _field[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = IFilledCellState[1];
                            break;
                        case 2:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[5];
                            _field[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = IFilledCellState[0];
                            break;
                        case 3:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[1];
                            _field[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[3];
                            break;
                    }
                    break;
                case 4:
                    switch (dir)
                    {
                        case 1:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[2];
                            _field[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = IFilledCellState[1];
                            break;
                        case 2:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[4];
                            _field[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = IFilledCellState[0];
                            break;
                        case 4:
                            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCell[1];
                            _field[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = IFilledCellState[2];
                            break;
                    }
                    break;
            }
        }
        _dirMoves.Push(dir);
    }
    public void UndoToStep(int lastX, int lastY, int x, int y)
    {
        _dirMoves.Pop();
        if (_dirMoves.Count == 0)
        {
            _field[(lastY * _ColumnCount) + lastX].GetComponent<Image>().sprite = IEmptyCell;
            _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellState[4];
        }
        else
        {
            _field[(lastY * _ColumnCount) + lastX].GetComponent<Image>().sprite = IEmptyCell;
            switch ((int)_dirMoves.Peek())
            {
                case 1:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellState[1];
                    break;
                case 2:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellState[0];
                    break;
                case 3:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellState[3];
                    break;
                case 4:
                    _field[(y * _ColumnCount) + x].GetComponent<Image>().sprite = IFilledCellState[2];
                    break;
            }
        }
    }

    public void CreateField(int[,] _field)
    {
        _CellSize = Mathf.Min((_FieldSizeX - this.GetComponent<GridLayoutGroup>().padding.left * (_ColumnCount + 1)) / _ColumnCount, (_FieldSizeY - this.GetComponent<GridLayoutGroup>().padding.top * (_RowCount + 1)) / _RowCount);
        this.GetComponent<GridLayoutGroup>().cellSize = HintField.GetComponent<GridLayoutGroup>().cellSize = new Vector2(_CellSize, _CellSize);
        this.GetComponent<GridLayoutGroup>().constraintCount = HintField.GetComponent<GridLayoutGroup>().constraintCount = _ColumnCount;

        Contour.SetActive(true);

        for (int i = 0; i < _RowCount; i++)
        {
            for (int j = 0; j < _ColumnCount; j++)
            {
                CreateCell(_field[i, j]);
                CreateHintCell(_field[i, j]);
            }
        }
    }
    private void CreateCell(int status)
    {
        _field.Add((GameObject)Instantiate(Cells));
        _field[_field.Count - 1].transform.SetParent(this.transform);
        _field[_field.Count - 1].transform.localScale = Vector3.one;
        _field[_field.Count - 1].transform.transform.localPosition = Vector3.zero;
        switch(status)
        {
            case 0:
                _field[_field.Count - 1].GetComponent<Image>().sprite = IEmptyCell;
                break;
            case 1:
                _field[_field.Count - 1].GetComponent<Image>().sprite = IInvalidCell;
                break;
            case 2:
                _field[_field.Count - 1].GetComponent<Image>().sprite = IFilledCellState[4];
                break;
            case 3:
                //_field[_field.Count - 1].GetComponent<Image>().sprite = IFilledCell;
                break;
        }
    }

    public void UpdateHintField(int x, int y, int dir, int lastDir)
    {
        if (HintController.StepX.Count == 1)
        {
            switch (dir)
            {
                case 1:
                    _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCellStart[dir - 1];
                    _hintField[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = HintIFilledCellState[1];
                    break;
                case 2:
                    _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCellStart[dir - 1];
                    _hintField[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = HintIFilledCellState[0];
                    break;
                case 3:
                    _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCellStart[dir - 1];
                    _hintField[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[3];
                    break;
                case 4:
                    _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCellStart[dir - 1];
                    _hintField[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[2];
                    break;
            }
        }
        else
        {
            switch (lastDir)
            {
                case 1:
                    switch (dir)
                    {
                        case 1:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[0];
                            _hintField[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = HintIFilledCellState[1];
                            break;
                        case 3:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[4];
                            _hintField[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[3];
                            break;
                        case 4:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[5];
                            _hintField[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[2];
                            break;
                    }
                    break;
                case 2:
                    switch (dir)
                    {
                        case 2:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[0];
                            _hintField[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = HintIFilledCellState[0];
                            break;
                        case 3:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[2];
                            _hintField[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[3];
                            break;
                        case 4:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[3];
                            _hintField[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[2];
                            break;
                    }
                    break;
                case 3:
                    switch (dir)
                    {
                        case 1:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[3];
                            _hintField[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = HintIFilledCellState[1];
                            break;
                        case 2:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[5];
                            _hintField[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = HintIFilledCellState[0];
                            break;
                        case 3:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[1];
                            _hintField[((y - 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[3];
                            break;
                    }
                    break;
                case 4:
                    switch (dir)
                    {
                        case 1:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[2];
                            _hintField[((y) * _ColumnCount) + (x - 1)].GetComponent<Image>().sprite = HintIFilledCellState[1];
                            break;
                        case 2:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[4];
                            _hintField[((y) * _ColumnCount) + (x + 1)].GetComponent<Image>().sprite = HintIFilledCellState[0];
                            break;
                        case 4:
                            _hintField[(y * _ColumnCount) + x].GetComponent<Image>().sprite = HintIFilledCell[1];
                            _hintField[((y + 1) * _ColumnCount) + (x)].GetComponent<Image>().sprite = HintIFilledCellState[2];
                            break;
                    }
                    break;
            }
        }
    }
    private void CreateHintCell(int status)
    {
        _hintField.Add((GameObject)Instantiate(Cells));
        _hintField[_hintField.Count - 1].transform.SetParent(HintField.transform);
        _hintField[_hintField.Count - 1].transform.localScale = Vector3.one;
        _hintField[_hintField.Count - 1].transform.transform.localPosition = Vector3.zero;
        switch (status)
        {
            case 0:
                _hintField[_hintField.Count - 1].GetComponent<Image>().sprite = HintIEmptyCell;
                break;
            case 1:
                _hintField[_hintField.Count - 1].GetComponent<Image>().sprite = HintIInvalidCell;
                break;
            case 2:
                _hintField[_hintField.Count - 1].GetComponent<Image>().sprite = HintIFilledCellState[4];
                break;
            case 3:
                //_hintField[_hintField.Count - 1].GetComponent<Image>().sprite = HintIFilledCell;
                break;
        }
    }

    public void ClearGame()
    {
        Contour.SetActive(false);

        for (int i = 0; i<_field.Count; i++)
        {
            Destroy(_field[i]);
            _field[i] = null;
        }
        _field.Clear();
        _dirMoves.Clear();
    }

    public void ClearHintField()
    {
        for(int i = 0; i< _hintField.Count; i++)
        {
            Destroy(_hintField[i]);
            _hintField[i] = null;
        }
        _hintField.Clear();
    }
}
