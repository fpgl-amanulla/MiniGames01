using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base
{
    public static class Constants
    {

        public static Dictionary<int, string> SceneName = new Dictionary<int, string>{

                {1, "FourInARow"},
                {2, "TicTakToe"},
                {3, "UnlockPuzzle"},
                {4, "RollerSplat"}

            };

        internal static string GetSceneName(int _key)
        {
            for (int i = 0; i < SceneName.Count; i++)
            {
                if(SceneName.ElementAt(i).Key == _key)
                {
                    return SceneName.ElementAt(i).Value;
                }
            }
            return null;
        }
    }
}
