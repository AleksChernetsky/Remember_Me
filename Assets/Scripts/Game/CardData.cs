using System;
using System.Collections.Generic;

[Serializable]
public class CardData
{
    public List<CardInfo> cards;
}

[Serializable]
public class CardInfo
{
    public int id;
    public string imageUrl;
}