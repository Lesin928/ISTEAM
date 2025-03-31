using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드의 버튼을 눌렀을 때 비활성화(배경, 텍스트, 볼륨) 및 카드(3가지) 삭제
/// </summary>
public class LvUP_CardDestroy : MonoBehaviour
{
    public void RemoveAllInstances()
    {
        UI_CardManager cardManager = UI_CardManager.Instance;

        if (cardManager != null)
        {
            foreach (GameObject card in cardManager.GetCardInstances())
            {
                Destroy(card);
            }

            if (cardManager.GetBackgroundInstance() != null)
            {
                cardManager.GetBackgroundInstance().SetActive(false);
            }

            if (cardManager.GetTextInstance() != null)
            {
                cardManager.GetTextInstance().SetActive(false);
            }

            if (cardManager.GetVolumeInstance() != null)
            {
                cardManager.GetVolumeInstance().SetActive(false);
            }

            cardManager.GetCardInstances().Clear(); 

            GameManager.Instance.ContinueGame(); // 게임 재개
            UI_GameManager.instance.SetExp();
        }
    }
}
