using System.Collections;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reels")]
    public SlotReel[] reels;

    [Header("Spin Settings")]
    public float spinDuration = 2.2f;
    public float stopDelayBetweenReels = 0.35f;

    [Header("Reward")]
    public CoinPayout coinPayout;
    public int rewardCoinCount = 12;

    private bool isSpinning = false;

    public void PullLever()
    {
        if (isSpinning) return;
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        isSpinning = true;

        foreach (SlotReel reel in reels)
        {
            reel.StartSpin();
        }

        yield return new WaitForSeconds(spinDuration);

        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].StopSpin();
            yield return new WaitForSeconds(stopDelayBetweenReels);
        }

        yield return new WaitForSeconds(0.2f);

        bool win = CheckWin();

        if (win)
        {
            Debug.Log("WIN!");
            if (coinPayout != null)
            {
                coinPayout.Payout(rewardCoinCount);
            }
        }
        else
        {
            Debug.Log("LOSE!");
        }

        isSpinning = false;
    }

    bool CheckWin()
    {
        if (reels == null || reels.Length < 3) return false;

        int first = reels[0].CurrentSymbol;
        for (int i = 1; i < reels.Length; i++)
        {
            if (reels[i].CurrentSymbol != first)
                return false;
        }

        return true;
    }
}