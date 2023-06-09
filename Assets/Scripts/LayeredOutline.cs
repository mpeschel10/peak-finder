using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class LayeredOutline : MonoBehaviour
{
    OutlineLayerColors layerColors;
    int[] layerCounts;
    Outline outline;
    void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        if (gameController == null) throw new System.Exception("Could not find GameController tagged object.");
        layerColors = gameController.GetComponent<OutlineLayerColors>();
        if (layerColors == null) throw new System.Exception("Could not find GameController tagged object with OutlineLayerColors component.");
        layerCounts = new int[layerColors.names.Length];

        outline = GetComponent<Outline>();
    }

  public void AddLayer(string layerName)
  {
    OutlineLayerColors.OutlineLayerColor layerColor = layerColors.dictionary[layerName];
    layerCounts[layerColor.index]++;
    if (layerCounts[layerColor.index] == 1)
    {
      ShowFirstLayer();
    }
  }

  public void SubtractLayer(string layerName)
  {
    OutlineLayerColors.OutlineLayerColor layerColor = layerColors.dictionary[layerName];
    if (layerCounts[layerColor.index] <= 0) throw new System.Exception("Cannot remove Outline layer " + layerName + "; there are none left.");
    layerCounts[layerColor.index]--;
    if (layerCounts[layerColor.index] == 0)
    {
      ShowFirstLayer();
    }
  }

  void ShowFirstLayer()
  {
    for(int i = 0; i < layerCounts.Length; i++)
    {
      if (layerCounts[i] > 0)
      {
        outline.enabled = true;
        outline.OutlineColor = layerColors.array[i].color;
        return;
      }
    }
    outline.enabled = false;
  }
}
