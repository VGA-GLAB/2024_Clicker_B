using UnityEngine;

public class GameManager : MonoBehaviour
{

    CookieDate _cookieDate;

    private void Start()
    {
        _cookieDate = new CookieDate();
    }

    private void Update()
    {
        // ToDO : 施設のCookie増加の処理をここに書き込む
    }

    public void CookieClicked()
    {
        _cookieDate.Cookies += _cookieDate.IncreaseCookie;
        Debug.Log("Click");
    }
}
