using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookPage
{
    public string fishDescription;
    public Sprite fishSprite;
    public string fishName;
    
    public BookPage(string fishDescription, Sprite fishSprite, string fishName)
    {
        this.fishDescription = fishDescription;
        this.fishSprite = fishSprite;
        this.fishName = fishName;
    }
}

public class AchievmentBook : MonoBehaviour
{
    private Dictionary<int, BookPage> bookPages = new Dictionary<int, BookPage>();
    private int currentPage = 0;
    private int currentIndex = 0;
    public TextMeshProUGUI fishDescription;
    public Image fishSprite;
    public TextMeshProUGUI fishName;

    public void OnPageOpen()
    {
        if (bookPages.ContainsKey(currentPage))
        {
            BookPage bookPage = bookPages[currentPage];
            fishDescription.text = bookPage.fishDescription;
            fishSprite.sprite = bookPage.fishSprite;
            fishName.text = bookPage.fishName;
        }
    }
    
    public void OnNextPage()
    {
        if (currentPage < bookPages.Count - 1)
        {
            currentPage++;
            OnPageOpen();
        }
    }
    
    
    public void OnAchievmentUnlocked(Fish fish)
    {
        CreateBookPage(fish);
    }
    
    private void CreateBookPage(Fish fish)
    {
        BookPage bookPage = new BookPage(fish.FishName, null, fish.FishName);
        bookPages.Add(currentIndex, bookPage);
        currentIndex++;
    }
}
