/// <summary>
/// 施設の情報を一括管理するためのクラスです
/// </summary>
public class FacilityData
{
    public int Level { get; set; } //現在のレベル
    public float ResourcePerSecond { get; set; } //毎秒獲得できるリソース数
    public float TotalResource { get; set; } //今までの総取得数

    public FacilityData(){}
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FacilityData(int level, float resourcePerSecond, float totalResource)
    {
        Level = level;
        ResourcePerSecond = resourcePerSecond;
        TotalResource = totalResource;
    }
}
