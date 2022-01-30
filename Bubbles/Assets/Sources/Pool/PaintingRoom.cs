using System;
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
    public void GetColor(Comet comet)
    {
        do
        {
            _randomIndex = (int)floatRange.RandomValueInRange;
        } while (_randomIndex == _previosColorIndex || _colors[_randomIndex] == comet.Color);
        _previosColorIndex = _randomIndex;
        comet.Painting(_colors[_randomIndex]);
    }
}