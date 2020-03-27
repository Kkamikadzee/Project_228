#define DEBUG
#undef DEBUG
using UnityEngine;

public class Game : MonoBehaviour
{
    private int[,] _Field;
    private int _emptyCells = -1;
    private int _height;
    private int _width;
    private int _playerPositionX;
    private int _playerPositionY;
    public System.Collections.Stack _stepX = new System.Collections.Stack();
    public System.Collections.Stack _stepY = new System.Collections.Stack();
    public System.Collections.Stack _stepCells = new System.Collections.Stack();

    private int[,] _clearField;
    public int _startPlayerPositionX, _startPlayerPositionY;

    public int[,] Field
    {
        get { return _Field; }
    }
    public int Height
    {
        get { return _height; }
    }
    public int Width
    {
        get { return _width; }
    }
    public int PlayerPositionX
    {
        get { return _playerPositionX; }
    }
    public int PlayerPositionY
    {
        get { return _playerPositionY; }
    }
    public int EmptyCells
    {
        get { return _emptyCells; }
    }

    public void LevelInitialization(int h, int w)
    {
        if (h < 1)
            throw new System.Exception("Height < 1");
        if (w < 1)
            throw new System.Exception("Width < 1");
        _height = h;
        _width = w;
        _Field = new int[_height, _width];
        LevelGenerator();
    }
    public void LevelInitialization(int number)
    {
        _clearField = GLevelReader(number);
        _Field = new int[_height, _width];
        for (int i = 0; i < _height; i++)
            for (int j = 0; j < _width; j++)
                _Field[i, j] = _clearField[i, j];
    }

    private bool LevelGenerator()
    {
        return true;
    }

    private int[,] GLevelReader(int number)
    {
        int[,] array = new int[LevelReader.levelList.level[number].height, LevelReader.levelList.level[number].width];

        int[,] tempArray = LevelReader.levelList.level[number].ToIntArray();
        for (int i = 0; i< LevelReader.levelList.level[number].height; i++)
        {
            for (int j = 0; j < LevelReader.levelList.level[number].width; j++)
            {
                array[i, j] = tempArray[i, j];
            }
        }
        _emptyCells = LevelReader.levelList.level[number].emptyCells;
        _playerPositionX = _startPlayerPositionX = LevelReader.levelList.level[number].playerPositionX;
        _playerPositionY = _startPlayerPositionY = LevelReader.levelList.level[number].playerPositionY;
        _height = LevelReader.levelList.level[number].height;
        _width = LevelReader.levelList.level[number].width;
        return array;
    }

    public bool StepUp()
    {
        if (_playerPositionY != 0)
        {
            switch (_Field[_playerPositionY - 1, _playerPositionX])
            {
                case 0:
                    _stepX.Push(_playerPositionX);
                    _stepY.Push(_playerPositionY);
                    _stepCells.Push(0);
                    _Field[_playerPositionY, _playerPositionX] = 3;
                    _Field[_playerPositionY - 1, _playerPositionX] = 2;
                    _playerPositionY--;
                    _emptyCells--;
                    return true;
                default:
#if DEBUG
                    throw new System.Exception("It is impossible to go up");
#endif
                    break;
            }
        }
        else
        {
#if DEBUG
            throw new System.Exception("It is impossible to go up");
#endif
        }
        return false;
    }
    public bool StepDown()
    {
        if (_playerPositionY != _height - 1)
        {
            switch (_Field[_playerPositionY + 1, _playerPositionX])
            {
                case 0:
                    _stepX.Push(_playerPositionX);
                    _stepY.Push(_playerPositionY);
                    _stepCells.Push(0);
                    _Field[_playerPositionY, _playerPositionX] = 3;
                    _Field[_playerPositionY + 1, _playerPositionX] = 2;
                    _playerPositionY++;
                    _emptyCells--;
                    return true;
                default:
#if DEBUG
                    throw new System.Exception("It is impossible to go down");
#endif
                    break;
            }
        }
        else
        {
#if DEBUG
            throw new System.Exception("It is impossible to go down");
#endif
        }
        return false;
    }
    public bool StepLeft()
    {
        if (_playerPositionX != 0)
        {
            switch (_Field[_playerPositionY, _playerPositionX - 1])
            {
                case 0:
                    _stepX.Push(_playerPositionX);
                    _stepY.Push(_playerPositionY);
                    _stepCells.Push(0);
                    _Field[_playerPositionY, _playerPositionX] = 3;
                    _Field[_playerPositionY, _playerPositionX - 1] = 2;
                    _playerPositionX--;
                    _emptyCells--;
                    return true;
                default:
#if DEBUG
                    throw new System.Exception("It is impossible to go left");
#endif
                    break;
            }
        }
        else
        {
#if DEBUG
            throw new System.Exception("It is impossible to go left");
#endif
        }
        return false;
    }
    public bool StepRight()
    {
        if (_playerPositionX != _width - 1)
        {
            switch (_Field[_playerPositionY, _playerPositionX + 1])
            {
                case 0:
                    _stepX.Push(_playerPositionX);
                    _stepY.Push(_playerPositionY);
                    _stepCells.Push(0);
                    _Field[_playerPositionY, _playerPositionX] = 3;
                    _Field[_playerPositionY, _playerPositionX + 1] = 2;
                    _playerPositionX++;
                    _emptyCells--;
                    return true;
                default:
#if DEBUG
                    throw new System.Exception("It is impossible to go right");
#endif
                    break;
            }
        }
        else
        {
#if DEBUG
            throw new System.Exception("It is impossible to go right");
#endif
        }
        return false;
    }
    public bool UndoToStep()
    {
        if (_stepCells.Count != 0)
        {
            if (System.Convert.ToInt32(_stepCells.Peek()) == 0)
                _emptyCells++;
            _Field[_playerPositionY, _playerPositionX] = System.Convert.ToInt32(_stepCells.Pop());
            _playerPositionY = System.Convert.ToInt32(_stepY.Pop());
            _playerPositionX = System.Convert.ToInt32(_stepX.Pop());
            _Field[_playerPositionY, _playerPositionX] = 2;
            return true;
        }
        else
        {
#if DEBUG
            throw new System.Exception("There are no moves that can be undone");
#endif
        }
        return false;
    }

    public bool EndGame()
    {
        if (_emptyCells == 0)
            return true;
        else
            return false;
    }

    public void ClearField()
    {
        _emptyCells = 0;
        for (int i = 0; i < _height; i++)
            for (int j = 0; j < _width; j++)
            {
                _Field[i, j] = _clearField[i, j];
                if (_clearField[i, j] == 0)
                    _emptyCells++;
            }
        _playerPositionY = _startPlayerPositionY;
        _playerPositionX = _startPlayerPositionX;
        _stepX.Clear();
        _stepY.Clear();
        _stepCells.Clear();
    }

    public void ClearGame()
    {
        _Field = null;
        _emptyCells = -1;
        _height = 0;
        _width = 0;
        _playerPositionX = 0;
        _playerPositionY = 0;
        _stepX.Clear();
        _stepY.Clear();
        _stepCells.Clear();
        _clearField = null;
        _startPlayerPositionX = 0;
        _startPlayerPositionY = 0;
    }
}
