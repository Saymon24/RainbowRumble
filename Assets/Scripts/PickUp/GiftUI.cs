using UnityEngine;
using TMPro;

public class GiftUI : MonoBehaviour
{

    [SerializeField] private GameObject giftUI;
    [SerializeField] private TMP_Text keyBindText;

    private void Start()
    {
        giftUI.SetActive(false);
    }

    public void UpdateKeyBindText()
    {
        return;
    }

    public GameObject GetReferenceToObj()
    {
        return giftUI;
    }

}
