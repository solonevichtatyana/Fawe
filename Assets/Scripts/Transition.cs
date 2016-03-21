using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
    public void StartGame()
    { 
        Application.LoadLevel("HardScene");
    }
   }