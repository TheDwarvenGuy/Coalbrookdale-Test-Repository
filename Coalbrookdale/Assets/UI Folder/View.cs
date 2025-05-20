using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public ViewSO viewData;

    public GameObject containerTop;
    public GameObject containerMiddle;
    public GameObject containerBottom;
    
    private Image imageTop;
    private Image imageMiddle;
    private Image imageBottom;

    private VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Setup();
        Configure();
    }

    public void Setup()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        imageTop = containerTop.GetComponent<Image>();
        imageMiddle = containerMiddle.GetComponent<Image>();
        imageBottom = containerBottom.GetComponent<Image>();

    }

    public void Configure()
    {
        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;

        imageTop.color = viewData.theme.colorTop;
        imageMiddle.color = viewData.theme.colorMiddle;
        imageBottom.color = viewData.theme.colorBottom;
    }

    private void OnValidate()
    {
        Init();
    }
}
