using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{

    public class Rnd
    {
        public static System.Random rnd = new System.Random ();


        public static void SetSeed(int seed)
        {
            rnd = new System.Random(seed);
        }
     
        public static float Triangular (float low=0f, float high=1f, float? mode=null)
        {
            var u = Value;
            var c = mode.HasValue ? (mode.Value - low) / (high - low) : 0.5f;
            if (u > c) {
                u = 1f - u;
                c = 1f - c;
                var t = low;
                low = high;
                high = t;
            }
            return Mathf.Pow (low + (high - low) * (u * c), 0.5f);
        }

        public static float Value {
            get {
                return (float)rnd.NextDouble ();
            }
        }

        public static float Range (float min, float max)
        {
            return ((max - min) * Value) + min;
        }

        public static int Range (int min, int max)
        {
            return (int)(((max - min) * Value) + min);
        }

    }


}




