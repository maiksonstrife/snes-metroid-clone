using System;
using Equipment;
using UnityEngine;
using Equipment;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PowerSuit")]
    public class PowerSuitData : ScriptableObject, ISerializationCallbackReceiver
    {

        public int initialEnergy = 99;
        public int initialNumEnergyTanks = 0;

        [NonSerialized] public int Energy;
        [NonSerialized] public int NumEnergyTanks;



        public PowerSuit.SuitType suitType = PowerSuit.SuitType.PowerSuit;

        public bool xrayVisorEnabled = false;
        
        public bool morphballEnabled = false;
        public bool bombEnabled = false;
        public bool powerBombEnabled = false;
        public bool springBallEnabled = false;

        public bool highJumpEnabled = false;
        public bool speedboostEnabled = false;
        public bool grappleBeamEnabled = false;
        public bool spaceJumpEnabled;
        
        public bool chargeBeamEnabled = false;
        
        public bool spazerBeamEnabled = false;
        public bool iceBeamEnabled = false;
        public bool waveBeamEnabled = false;
        public bool plasmaBeamEnabled = false;

        public bool isVariaSuit = false;
        public bool isGravitySuit = false;
        public bool screwAttackEnabled = false;

        public GameObject PowerBeam;
        public GameObject PowerBeamCharging;
        public GameObject PowerBeamCharged;

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            Energy = initialEnergy;
            NumEnergyTanks = initialNumEnergyTanks;
        }
    }
}
