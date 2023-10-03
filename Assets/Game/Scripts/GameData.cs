using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="new Data",fileName ="New Data")]
public class GameData : ScriptableObject
{
    public string nameData;
    public List<Sprite> listPhotos = new List<Sprite>();
}
