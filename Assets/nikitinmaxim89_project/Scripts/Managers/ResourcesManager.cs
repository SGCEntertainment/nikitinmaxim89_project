using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class ResourcesManager : MonoBehaviour
{
    SimpleScrollSnap snap;

    [SerializeField] GameObject containerPrefab;
    [SerializeField] Transform parent;

    private void Start()
    {
        snap = FindObjectOfType<SimpleScrollSnap>();
        Object[] sprites = Resources.LoadAll("FakeFoto", typeof(Sprite));

        for(int i = 0; i < sprites.Length; i++)
        {
            GameObject container = Instantiate(containerPrefab, parent);

            Sprite spriteTmp = (Sprite)sprites[i];
            Image imgTmp = container.transform.GetChild(0).GetComponent<Image>();

            float x = 480.0f;
            float y = x * spriteTmp.texture.height / spriteTmp.texture.width;

            Vector2 size = new Vector2((int)x, (int)y);
            imgTmp.rectTransform.sizeDelta = size;

            imgTmp.sprite = spriteTmp;
        }

        snap.enabled = true;
    }
}
