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

        public bool _xrayVisorEnabled = false;
        
        public bool _morphballEnabled = false;
        public bool _bombEnabled = false;
        public bool _powerBombEnabled = false;
        public bool _springBallEnabled = false;

        public bool _highJumpEnabled = false;
        public bool _speedboostEnabled = false;
        public bool _grappleBeamEnabled = false;
        public bool _spaceJumpEnabled;
        
        public bool _chargeBeamEnabled = false;
        
        public bool _spazerBeamEnabled = false;
        public bool _iceBeamEnabled = false;
        public bool _waveBeamEnabled = false;
        public bool _plasmaBeamEnabled = false;

        private bool _isVariaSuit = false;
        private bool _isGravitySuit = false;
        public bool _screwAttackEnabled = false;

        public GameObject PowerBeam;
        
        public void Activate(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    _xrayVisorEnabled = true;
                    break;
                case Upgrade.MorphBall:
                    _morphballEnabled = true;
                    break;
                case Upgrade.Bomb:
                    _bombEnabled = true;
                    break;
                case Upgrade.PowerBomb:
                    _powerBombEnabled = true;
                    break;
                case Upgrade.SpringBall:
                    _springBallEnabled = true;
                    break;
                case Upgrade.HighJump:
                    _highJumpEnabled = true;
                    break;
                case Upgrade.SpeedBoost:
                    _speedboostEnabled = true;
                    break;
                case Upgrade.GrappleBeam:
                    _grappleBeamEnabled = true;
                    break;
                case Upgrade.SpaceJump:
                    _spaceJumpEnabled = true;
                    break;
                case Upgrade.ChargeBeam:
                    _chargeBeamEnabled = true;
                    break;
                case Upgrade.SpazerBeam:
                    _spazerBeamEnabled = true;
                    break;
                case Upgrade.IceBeam:
                    _iceBeamEnabled = true;
                    break;
                case Upgrade.WaveBeam:
                    _waveBeamEnabled = true;
                    break;
                case Upgrade.PlasmaBeam:
                    _plasmaBeamEnabled = true;
                    break;
            }
        }

        public bool IsEnabled(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.XrayVisor:
                    return _xrayVisorEnabled;
                case Upgrade.MorphBall:
                    return _morphballEnabled;
                case Upgrade.Bomb:
                    return _bombEnabled;
                case Upgrade.PowerBomb:
                    return _powerBombEnabled;
                case Upgrade.SpringBall:
                    return _springBallEnabled;
                case Upgrade.HighJump:
                    return _highJumpEnabled;
                case Upgrade.SpeedBoost:
                    return _speedboostEnabled;
                case Upgrade.GrappleBeam:
                    return _grappleBeamEnabled;
                case Upgrade.SpaceJump:
                    return _spaceJumpEnabled;
                case Upgrade.ChargeBeam:
                    return _chargeBeamEnabled;
                case Upgrade.SpazerBeam:
                    return _spazerBeamEnabled;
                case Upgrade.IceBeam:
                    return _iceBeamEnabled;
                case Upgrade.WaveBeam:
                    return _waveBeamEnabled;
                case Upgrade.PlasmaBeam:
                    return _plasmaBeamEnabled;
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
