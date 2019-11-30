using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{
    public class GameState
    {
        private static bool _controllable;
        public static int characterPersona;
        public static int nowMission = 0;
        public static bool controllable {
            set { 
                _controllable = value; 
                UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl.disabled = !controllable;
                Debug.Log(value);
            }
            get {
                return _controllable;
            }
        }
    }
    
}
