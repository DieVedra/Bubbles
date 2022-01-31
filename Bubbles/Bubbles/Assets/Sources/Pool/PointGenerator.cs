using UnityEngine;

public class PointGenerator
{
    private readonly Vector3 _leftBorderPoint;
    private readonly Vector3 _rightBorderPoint;
    private FloatRange _floatRange;
    private readonly float _radiusMultiplier = 0.5f;
    private readonly float _subtractFromYPool = 0.5f;
    private float _minBorder;
    private float _maxBorder;
    private float _x;
    private float _y;
    private float _z => _leftBorderPoint.z;
    public PointGenerator(Vector3 leftBorderPoint, Vector3 rightBorderPoint)
    {
        _leftBorderPoint = leftBorderPoint;
        _rightBorderPoint = rightBorderPoint;
    }
    public Vector3 GetStartPoint(Vector2 scaleBall)
    {
        CalculatesEdges(scaleBall);
        CalculatesRange(_minBorder, _maxBorder);
        _x = _floatRange.RandomValueInRange;
        _y = _leftBorderPoint.y;

        return new Vector3(_x, _y, _z);
    }
    public Vector3 GetPointToJump(Vector2 scaleBall, Vector2 startPoint, Vector2 currentPosition)
    {
        CalculatesEdges(scaleBall);
        if (startPoint.x >= 0f)
        {
            CalculatesRange(_minBorder, 0f);
            _x = _floatRange.RandomValueInRange;
        }
        else if (startPoint.x < 0f)
        {
            CalculatesRange(0f, _maxBorder);
            _x = _floatRange.RandomValueInRange;
        }
        float point = startPoint.y -= (_subtractFromYPool + scaleBall.y * _radiusMultiplier);
        CalculatesRange(point, currentPosition.y);
        _y = _floatRange.RandomValueInRange;
        return new Vector3(_x, _y, _z);
    }
    private void CalculatesEdges(Vector2 scaleBall)
    {
        _minBorder = _leftBorderPoint.x + scaleBall.x * _radiusMultiplier;
        _maxBorder = _rightBorderPoint.x - scaleBall.x * _radiusMultiplier;
    }
    private void CalculatesRange(float min, float max)
    {
        _floatRange = new FloatRange(min, max);
    }
}
