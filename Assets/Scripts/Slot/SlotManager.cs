using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPanel;
    [SerializeField] private Text betAmountText;
    [SerializeField] private Text resourceText;
    [SerializeField] private NewButton startStopButton;
    [SerializeField] private NewButton increaseBetButton;
    [SerializeField] private NewButton decreaseBetButton;
    [SerializeField] private NewButton closeButton;
    [SerializeField] private Text messageText;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text startStopButtonText;
    [SerializeField] private SlotAnimeSystem slotAnimeSystem;
    [SerializeField] private FacilityData facilityData; // FacilityDataへの参照を追加

    private int currentBet = 0;
    private bool isSpinning = false;

    [SerializeField] private bool isDebugMode = false;

    public enum SymbolType
    {
        Coin,
        Meat,
        CoinBag,
        Bank,
        Box,
        Bolt,
        Clover,
        Trophy,
        Miss
    }

    private static readonly (SymbolType, float, int)[] symbolData = new[]
    {
        (SymbolType.Bolt, 0.05f, 0),
        (SymbolType.Box, 0.001f, 10000),
        (SymbolType.Trophy, 0.005f, 1000),
        (SymbolType.Bank, 0.014f, 500),
        (SymbolType.CoinBag, 0.03f, 200),
        (SymbolType.Coin, 0.05f, 100),
        (SymbolType.Meat, 0.1f, 10),
        (SymbolType.Clover, 0.3f, 1),
        (SymbolType.Miss, 0.45f, 0)
    };

    private void Start()
    {
        UpdateUI();
        startStopButton.OnClick.AddListener(StartStopButtonPressed);
        increaseBetButton.OnClick.AddListener(IncreaseBet);
        decreaseBetButton.OnClick.AddListener(DecreaseBet);
        closeButton.OnClick.AddListener(CloseSlotPanel);
        UpdateStartStopButtonText();
    }

    public void IncreaseBet()
    {
        if (facilityData.TotalResource >= currentBet + 1)
        {
            currentBet++;
            UpdateUI();
        }
        else
        {
            ShowWarning("リソースが足りません！");
        }
    }


    public void DecreaseBet()
    {
        if (currentBet > 0)
        {
            currentBet--;
            UpdateUI();
        }
    }

    private void StartStopButtonPressed()
    {
        if (!isSpinning)
        {
            StartSlot();
        }
    }

    public void StartSlot()
    {
        if (currentBet == 0)
        {
            ShowWarning("ベット額を設定してください！");
            return;
        }

        if (!isDebugMode && facilityData.TotalResource < currentBet)
        {
            ShowWarning("リソースが足りません！");
            return;
        }

        isSpinning = true;
        if (!isDebugMode)
        {
            facilityData.TotalResource -= currentBet;
        }
        UpdateUI();

        SlotSystem.ResultEnum result = GenerateResult();
        slotAnimeSystem.Slot(result);
        StartCoroutine(WaitForSpinCompletion());
    }


    private IEnumerator WaitForSpinCompletion()
    {
        float timeout = 10f; // タイムアウト時間（秒）
        float elapsedTime = 0f;

        while (slotAnimeSystem.IsSpinning)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            // タイムアウト処理
            if (elapsedTime > timeout)
            {
                Debug.LogWarning("Slot spin timed out");
                break;
            }
        }

        ProcessResult();
    }

    private SlotSystem.ResultEnum GenerateResult()
    {
        float random = UnityEngine.Random.value;
        float cumulativeProbability = 0f;

        foreach (var (symbol, probability, _) in symbolData)
        {
            cumulativeProbability += probability;
            if (random <= cumulativeProbability)
            {
                return (SlotSystem.ResultEnum)symbol;
            }
        }

        return SlotSystem.ResultEnum.Miss;
    }

    private void ProcessResult()
    {
        isSpinning = false;
        // ここで結果に基づいて報酬を計算し、リソースを更新
        UpdateUI();
    }


    private void UpdateUI()
    {
        betAmountText.text = currentBet.ToString();
        resourceText.text = facilityData.TotalResource.ToString("F2"); // 小数点以下2桁まで表示
        SetButtonEnabled(startStopButton, facilityData.TotalResource >= currentBet);
        UpdateStartStopButtonText();
    }

    private void UpdateStartStopButtonText()
    {
        startStopButtonText.text = isSpinning ? "STOP" : "START";
    }

    private void SetButtonEnabled(NewButton button, bool isEnabled)
    {
        button.enabled = isEnabled;
        var canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isEnabled ? 1f : 0.5f;
            canvasGroup.interactable = isEnabled;
            canvasGroup.blocksRaycasts = isEnabled;
        }
    }

    private void ShowWarning(string message)
    {
        messageText.text = message;
        messageText.color = Color.yellow;
        messagePanel.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(2f));
    }

    private void ShowWinMessage(string message)
    {
        messageText.text = message;
        messageText.color = Color.green;
        messagePanel.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(3f));
    }

    private void ShowLoseMessage(string message)
    {
        messageText.text = message;
        messageText.color = Color.red;
        messagePanel.SetActive(true);
        StartCoroutine(HideMessageAfterDelay(2f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePanel.SetActive(false);
    }

    public void CloseSlotPanel()
    {
        slotPanel.SetActive(false);
    }
}
