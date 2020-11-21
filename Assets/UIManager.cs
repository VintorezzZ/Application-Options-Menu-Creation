using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("___________MUSIC")]
    public Slider musicSlider;
    public TextMeshProUGUI musicPercentText;

    [Header("___________SOUNDS")]
    public Slider soundsSlider;
    public TextMeshProUGUI soundsPercentText;

    [Header("___________VIBRATION")] 
    public Toggle vibroToggle;
    public Text vibroOnOffText;

    [Header("___________FONTS")]
    public List<TMP_FontAsset> allFonts = new List<TMP_FontAsset>();
    public TMP_Dropdown dropDownFontsList;
    private TextMeshProUGUI[] allText;
    
    [Header("___________FONTS COLOR")]
    public Color[] colors = new []{Color.white, Color.black, Color.red, Color.green, Color.blue};
    public TMP_Dropdown dropDownColorList;

    [Header("___________DARK MODE")] 
    public Color darkColor;
    public Color lightColor;
    public Toggle darkModeToggle;
    public Image background;
    private bool isDarkMode = false;

    [Space] [Space] [Header("Test")] [Space] [Space]
    public Canvas canvas;
    public GameObject buttonToDarg;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { UpdateMusicPercentText(); });
        soundsSlider.onValueChanged.AddListener(delegate { UpdateSoundsPercentText(); });
        vibroToggle.onValueChanged.AddListener(delegate { UpdateToggleText(); });
        darkModeToggle.onValueChanged.AddListener(delegate { DarkModeOnOff(); });
        dropDownFontsList.onValueChanged.AddListener(delegate { ChangeTextFont(); });
        dropDownColorList.onValueChanged.AddListener(delegate { ChangeTextColor(); });
    
        ChangeTextFont();
        ChangeTextColor();
        
        UpdateMusicPercentText();
        UpdateSoundsPercentText();
        
    }

    public void UpdateMusicPercentText()
    {
        musicPercentText.text = Mathf.RoundToInt(musicSlider.value * 100).ToString() + "%";
    }

    public void UpdateSoundsPercentText()
    {
        soundsPercentText.text = Mathf.RoundToInt(soundsSlider.value * 100).ToString() + "%";
    }

    public void UpdateToggleText()
    {
        vibroOnOffText.text = vibroToggle.isOn ? "On" : "Off";
    }

    private void FindAllTextGameObjects()
    {
        allText = GameObject.FindObjectsOfType<TextMeshProUGUI>();
    }

    public void ChangeTextFont()
    {
        FindAllTextGameObjects();

        int fontIndex = dropDownFontsList.value;
        
        foreach (TextMeshProUGUI i in allText)
        {
            i.font = allFonts[fontIndex];
        }
    }

    public void ChangeTextColor()
    {
        FindAllTextGameObjects();

        int colorIndex = dropDownColorList.value;

        foreach (TextMeshProUGUI i in allText)
        {
            if (colorIndex == 0 && i.gameObject.name == "Label")
            {
                i.color = colors[1];
                continue;
            }
            else if (colorIndex == 1 && i.gameObject.name == "Label")
            {
                i.color = colors[1];
                continue;
            }
            i.color = colors[colorIndex];
        }
    }

    public void DarkModeOnOff()
    {
        if (!isDarkMode)
        {
            background.color = darkColor;
            isDarkMode = true;
        }
        else
        {
            background.color = lightColor;
            isDarkMode = false;
        }
        
    }
    
    
    

    #region Test

    public void OnHover()
    {
        DarkModeOnOff();
    }

    public void OnSelect()
    {
        vibroOnOffText.text = "OnSelect";
    }  
    public void OnDeSelect()
    {
        vibroOnOffText.text = "OnDeSelect";
        print("Deselect");
    }

    public void OnClick()
    {
        vibroOnOffText.text = "OnClick";
    }

    public void OnDrag()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera, out position);
        buttonToDarg.transform.position = canvas.transform.TransformPoint(position);
    }
    
    
    #endregion
}
