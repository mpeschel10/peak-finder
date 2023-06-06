using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineLayerColors : MonoBehaviour
{
    public string[] names = { "can-drag", "can-click" };
    public Color[] colors = { Color.blue, Color.green };

    public OutlineLayerColor[] array;
    public Dictionary<string, OutlineLayerColor> dictionary;
    void Start()
    {
        if (names.Length != colors.Length)
            throw new System.Exception("OutlineLayerColors: length of names " + names.Length + " does not match length of colors " + colors.Length + ".");
        
        array = new OutlineLayerColor[names.Length];
        dictionary = new Dictionary<string, OutlineLayerColor>();
        for (int i = 0; i < names.Length; i++)
        {
            string name = names[i];
            if (dictionary.ContainsKey(name)) throw new System.Exception("OutlineLayerColors: duplicate name " + name + ".");
            
            OutlineLayerColor layer = new OutlineLayerColor(i, name, colors[i]);
            array[i] = layer;
            dictionary[name] = layer;
        }
    }

    public class OutlineLayerColor
    {
        public int index; public string name; public Color color;
        public OutlineLayerColor(int index, string name, Color color)
        {
            this.index = index; this.name = name; this.color = color;
        }
    }
}
