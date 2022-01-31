using UnityEngine;

public class PaintingRoom
{
    private readonly Color[] _colors;
    private readonly FloatRange floatRange;
    private int _previosColorIndex = 0;
    private int _randomIndex;
    public PaintingRoom(params Color[] colors)
    {
        _colors = colors;
        floatRange = new FloatRange(0, _colors.Length);
    }
    public Color GetColor(Color ballColor)
    {
        do
        {
            _randomIndex = (int)floatRange.RandomValueInRange;
        } while (_randomIndex == _previosColorIndex || _colors[_randomIndex] == ballColor);
        _previosColorIndex = _randomIndex;
        return _colors[_randomIndex];
    }
}