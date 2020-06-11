
using UnityEngine;

namespace Equipment
{
    public class PowerSuit : MonoBehaviour
    {
        public enum SuitType
        {
            PowerSuit,
            VariaSuit,
            GravitySuit
        };

        public SuitType _suitType = SuitType.PowerSuit;
        public enum Upgrade
        {
            XrayVisor,
            MorphBall,
            Bomb,
            PowerBomb,
            SpringBall,
            HighJump,
            SpeedBoost,
            GrappleBeam,
            SpaceJump,
            ChargeBeam,
            SpazerBeam,
            IceBeam,
            WaveBeam,
            PlasmaBeam,
            VariaSuit,
            GravitySuit,
            ScrewAttack
        };

        public int initialEnergy = 99;
        public int initialNumEnergyTanks = 0;

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

        public void Activate(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    xrayVisorEnabled = true;
                    break;
                case Upgrade.MorphBall:
                    morphballEnabled = true;
                    break;
                case Upgrade.Bomb:
                    bombEnabled = true;
                    break;
                case Upgrade.PowerBomb:
                    powerBombEnabled = true;
                    break;
                case Upgrade.SpringBall:
                    springBallEnabled = true;
                    break;
                case Upgrade.HighJump:
                    highJumpEnabled = true;
                    break;
                case Upgrade.SpeedBoost:
                    speedboostEnabled = true;
                    break;
                case Upgrade.GrappleBeam:
                    grappleBeamEnabled = true;
                    break;
                case Upgrade.SpaceJump:
                    spaceJumpEnabled = true;
                    break;
                case Upgrade.ChargeBeam:
                    chargeBeamEnabled = true;
                    break;
                case Upgrade.SpazerBeam:
                    spazerBeamEnabled = true;
                    break;
                case Upgrade.IceBeam:
                    iceBeamEnabled = true;
                    break;
                case Upgrade.WaveBeam:
                    waveBeamEnabled = true;
                    break;
                case Upgrade.PlasmaBeam:
                    plasmaBeamEnabled = true;
                    break;
            }
        }

        public bool IsEnabled(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    return xrayVisorEnabled;
                case Upgrade.MorphBall:
                    return morphballEnabled;
                case Upgrade.Bomb:
                    return bombEnabled;
                case Upgrade.PowerBomb:
                    return powerBombEnabled;
                case Upgrade.SpringBall:
                    return springBallEnabled;
                case Upgrade.HighJump:
                    return highJumpEnabled;
                case Upgrade.SpeedBoost:
                    return speedboostEnabled;
                case Upgrade.GrappleBeam:
                    return grappleBeamEnabled;
                case Upgrade.SpaceJump:
                    return spaceJumpEnabled;
                case Upgrade.ChargeBeam:
                    return chargeBeamEnabled;
                case Upgrade.SpazerBeam:
                    return spazerBeamEnabled;
                case Upgrade.IceBeam:
                    return iceBeamEnabled;
                case Upgrade.WaveBeam:
                    return waveBeamEnabled;
                case Upgrade.PlasmaBeam:
                    return plasmaBeamEnabled;
                default:
                    return false;
            }
        }

        public GameObject GetSelectedWeapon()
        {
            return PowerBeam;
        }
    }
}
