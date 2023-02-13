using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private void Awake()
    {
        graphContainer = transform.Find("Graph Container").GetComponent<RectTransform>();
        CreateCircle(new Vector2(100, 100));

        // List<int> valueList = new List<int>() {10, 5, 25, 20};
        // ShowGraph(valueList);
    }

    private void CreateCircle(Vector2 anchoredPosition) {
        GameObject gmeObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

    }

}