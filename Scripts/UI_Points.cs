using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Points : MonoBehaviour
{
   Text textComp;
   GameManager gm;
   void Start()
   {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();
   }
   
   void Update()
   {
       textComp.text = $"POINTS: {gm.points}";
   }
}
