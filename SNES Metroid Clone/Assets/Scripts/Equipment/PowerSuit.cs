using ScriptableObjects;
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

        [SerializeField] PowerSuitData _powerSuitData;

        public void Activate(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    _powerSuitData.xrayVisorEnabled = true;
                    break;
                case Upgrade.MorphBall:
                    _powerSuitData.morphballEnabled = true;
                    break;
                case Upgrade.Bomb:
                    _powerSuitData.bombEnabled = true;
                    break;
                case Upgrade.PowerBomb:
                    _powerSuitData.powerBombEnabled = true;
                    break;
                case Upgrade.SpringBall:
                    _powerSuitData.springBallEnabled = true;
                    break;
                case Upgrade.HighJump:
                    _powerSuitData.highJumpEnabled = true;
                    break;
                case Upgrade.SpeedBoost:
                    _powerSuitData.speedboostEnabled = true;
                    break;
                case Upgrade.GrappleBeam:
                    _powerSuitData.grappleBeamEnabled = true;
                    break;
                case Upgrade.SpaceJump:
                    _powerSuitData.spaceJumpEnabled = true;
                    break;
                case Upgrade.ChargeBeam:
                    _powerSuitData.chargeBeamEnabled = true;
                    break;
                case Upgrade.SpazerBeam:
                    _powerSuitData.spazerBeamEnabled = true;
                    break;
                case Upgrade.IceBeam:
                    _powerSuitData.iceBeamEnabled = true;
                    break;
                case Upgrade.WaveBeam:
                    _powerSuitData.waveBeamEnabled = true;
                    break;
                case Upgrade.PlasmaBeam:
                    _powerSuitData.plasmaBeamEnabled = true;
                    break;
            }
        }

        public bool IsEnabled(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    return _powerSuitData.xrayVisorEnabled;
                case Upgrade.MorphBall:
                    return _powerSuitData.morphballEnabled;
                case Upgrade.Bomb:
                    return _powerSuitData.bombEnabled;
                case Upgrade.PowerBomb:
                    return _powerSuitData.powerBombEnabled;
                case Upgrade.SpringBall:
                    return _powerSuitData.springBallEnabled;
                case Upgrade.HighJump:
                    return _powerSuitData.highJumpEnabled;
                case Upgrade.SpeedBoost:
                    return _powerSuitData.speedboostEnabled;
                case Upgrade.GrappleBeam:
                    return _powerSuitData.grappleBeamEnabled;
                case Upgrade.SpaceJump:
                    return _powerSuitData.spaceJumpEnabled;
                case Upgrade.ChargeBeam:
                    return _powerSuitData.chargeBeamEnabled;
                case Upgrade.SpazerBeam:
                    return _powerSuitData.spazerBeamEnabled;
                case Upgrade.IceBeam:
                    return _powerSuitData.iceBeamEnabled;
                case Upgrade.WaveBeam:
                    return _powerSuitData.waveBeamEnabled;
                case Upgrade.PlasmaBeam:
                    return _powerSuitData.plasmaBeamEnabled;
                default:
                    return false;
            }
        }

        public GameObject GetSelectedWeapon()
        {
            return _powerSuitData.PowerBeam;
        }
    }
}
