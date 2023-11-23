using UnityEngine;
using TMPro;
using System.Collections;

public class GiftUI : MonoBehaviour
{

    [SerializeField] private GameObject giftUI;
    [SerializeField] private GameObject SpawnName;
    [SerializeField] private TMP_Text keyBindText;
    [SerializeField] private TMP_Text giftSpawnPlace;
    [SerializeField] private float fadeTime;

    private float timeElapse;
    private float timer;

    private void Start()
    {
        giftUI.SetActive(false);
        SpawnName.SetActive(false);
        timeElapse = fadeTime;
    }

    private void Update()
    {
        if (SpawnName.activeSelf)
        {
            StartCoroutine(FadeTextToZeroAlpha());
            if (giftSpawnPlace.color.a <= 0.05f)
            {
                giftSpawnPlace.color = new Color(giftSpawnPlace.color.r, giftSpawnPlace.color.g, giftSpawnPlace.color.b, 1);
                SpawnName.SetActive(false);
            }
        }
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        while (giftSpawnPlace.color.a > 0.0f)
        {
            giftSpawnPlace.color = new Color(giftSpawnPlace.color.r, giftSpawnPlace.color.g, giftSpawnPlace.color.b, giftSpawnPlace.color.a - (Time.deltaTime / fadeTime));
            print("New color: " + giftSpawnPlace.color.ToString());
            print(Time.deltaTime.ToString());
            yield return null;
        }
    }

    public void UpdateKeyBindText()
    {
        return;
    }

    public GameObject GetReferenceToObj()
    {
        return giftUI;
    }

    public void SetGiftSpawnPlace(string place)
    {

        print("Je set en active l'objet text");

        SpawnName.SetActive(true);
        giftSpawnPlace.text = "A gift has spawn in: " + place;
        timer = Time.time;
    }

}
