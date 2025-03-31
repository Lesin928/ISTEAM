using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī���� ��ư�� ������ �� ��Ȱ��ȭ(���, �ؽ�Ʈ, ����) �� ī��(3����) ����
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

            GameManager.Instance.ContinueGame(); // ���� �簳
            UI_GameManager.instance.SetExp();
        }
    }
}
