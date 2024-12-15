using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VillageClickSystem : MonoBehaviour
{
    public static VillageClickSystem instance;

    private Camera _camera;
    private Transform _cameraT;

    [SerializeField]
    private RectTransform _rect;

    private GameObject _rectObj;

    [SerializeField]
    private NewButton _xButton;

    [SerializeField]
    private Text _text;

    void Awake()
    {
        instance = this;

        _camera = Camera.main;
        _cameraT = _camera.transform;

        _rectObj = _rect.gameObject;
    }
    private void Start()
    {
        _rectObj.SetActive(false);

        _xButton.OnClick.AddListener(() => _rectObj.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButtonDown(0))
            return;

        if (CoinManager.Instance._panelState is not CoinManager.PanelState.Village)
            return;


        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return;

        if(hit.transform.CompareTag("Untagged"))
            return;

        _rectObj.SetActive(true);
        var pos = Input.mousePosition + new Vector3(_rect.sizeDelta.x / 2, _rect.sizeDelta.x / -2);
        pos.x = Mathf.Clamp(pos.x, _rect.sizeDelta.x / 2, Screen.width - _rect.sizeDelta.x / 2);
        pos.y = Mathf.Clamp(pos.y, _rect.sizeDelta.y / 2, Screen.height - _rect.sizeDelta.y / 2);
        _rect.position = pos;

        var data = ToBuildingStatus(hit.transform.tag);

        string str = $"{ToBuildingName(hit.transform.tag)}\nLevel " + (data.level is 3 ? "Max" : data.level) + "\n\n";

        if (data.CoinPerClick[data.level] is not 0)
            str += data.CoinPerClick[data.level] + " PerClick\n";

        if(data.CoinPerSec[data.level] is not 0)
            str += data.CoinPerSec[data.level] + " PerSec\n";

        if (data.level is not 3)
            str += "\nLvƒAƒbƒvƒRƒXƒg " + data.Cost[data.level] + "G";

        _text.text = str;
        Debug.Log(hit.transform.tag);
    }

    public void CloseTab()
    {
        _rectObj.SetActive(false);
    }
    string ToBuildingName(string tagName)
        => tagName switch
        {
            "House" => "‰Æ",
            "Factory" => "Hê",
            "Ruins" => "ˆâÕ",
            "Amusement" => "—V‹Zê",
            "Castle" => "é",
            _ => "‰½‚±‚ê?????"
        };
    Building ToBuildingStatus(string tagName)
        => tagName switch
        {

            "House" => CoinManager.Instance.Buildings.Where(x => x.Name is "‰Æ").ToArray()[0],
            "Factory" => CoinManager.Instance.Buildings.Where(x => x.Name is "Hê").ToArray()[0],
            "Ruins" => CoinManager.Instance.Buildings.Where(x => x.Name is "ˆâÕ").ToArray()[0],
            "Amusement" => CoinManager.Instance.Buildings.Where(x => x.Name is "—V‹Zê").ToArray()[0],
            "Castle" => CoinManager.Instance.Buildings.Where(x => x.Name is "é").ToArray()[0],
        };
}
