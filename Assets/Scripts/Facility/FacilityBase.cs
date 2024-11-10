// FacilityBase.cs
public class FacilityBase
{
    private int _facilityLevel = 1;
    private int _increaseAmount = 1;
    private float _interval;
    private float _timeElapsed;
    private int _cost;

    public FacilityBase(int initialIncreaseAmount, float interval, int initialCost)
    {
        _increaseAmount = initialIncreaseAmount;
        _interval = interval;
        _cost = initialCost;
    }

    // 施設のアップグレード
    public void Upgrade()
    {
        _facilityLevel++;
        _increaseAmount += 2;
        _cost += _facilityLevel * 10;  // コストを増加
    }

    // 自動クリックが可能かを判定
    public bool CanAutoClick(float deltaTime)
    {
        _timeElapsed += deltaTime;
        if (_timeElapsed >= _interval)
        {
            _timeElapsed = 0;
            return true;
        }
        return false;
    }

    // 施設の増加量取得
    public int GetIncreaseAmount() => _increaseAmount * _facilityLevel;

    // 施設のコスト
    public int Cost => _cost;
}
