using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Item", order = 0)]
public class Data : ScriptableObject {
   [SerializeField] float volumen;
   [SerializeField] int puntajeP1;
   [SerializeField] int puntajeP2;

   public int PuntajeP1{
        get{
            return puntajeP1;
        }
        set{
            puntajeP1 = value;
        }
   }
   
    public int PuntajeP2{
        get{
            return puntajeP2;
        }
        set{
            puntajeP2 = value;
        }

   }
    public float Volumen{
        get{
            return volumen;
            }

        set{
            volumen = value;
        }
    }

}
