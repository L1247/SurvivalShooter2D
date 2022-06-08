using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zenject.SpaceFighter
{
    public class PlayerShootHandler : ITickable
    {
        readonly AudioPlayer      _audioPlayer;
        readonly Player           _player;
        readonly Settings         _settings;
        readonly Bullet.Factory   _bulletFactory;
        readonly PlayerInputState _inputState;

        float         _lastFireTime;
        private int[] angels = { -50 , -40 , -30 , -20 , -10 , 0 , 10 , 20 , 30 , 40 ,50 };

        public PlayerShootHandler(
            PlayerInputState inputState ,
            Bullet.Factory   bulletFactory ,
            Settings         settings ,
            Player           player ,
            AudioPlayer      audioPlayer)
        {
            _audioPlayer   = audioPlayer;
            _player        = player;
            _settings      = settings;
            _bulletFactory = bulletFactory;
            _inputState    = inputState;
        }

        public void Tick()
        {
            if (_player.IsDead)
            {
                return;
            }

            if (_inputState.IsFiring && Time.realtimeSinceStartup - _lastFireTime > _settings.MaxShootInterval)
            {
                _lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }


        void Fire()
        {
            // _audioPlayer.Play(_settings.Laser, _settings.LaserVolume);

            var bulletCount = angels.Length;
            for (int i = 0 ; i < bulletCount ; i++)
            {
                float angel = angels[i];
                var bullet = _bulletFactory.Create(
                    _settings.BulletSpeed , _settings.BulletLifetime , BulletTypes.FromPlayer);

                bullet.transform.position = _player.Position + _player.LookDir * _settings.BulletOffsetDistance;
                bullet.transform.rotation = _player.Rotation * Quaternion.Euler(0 , 0 , angel);
            }
        }

        [Serializable]
        public class Settings
        {
            public AudioClip Laser;
            public float     LaserVolume = 1.0f;

            public float BulletLifetime;
            public float BulletSpeed;
            public float MaxShootInterval;
            public float BulletOffsetDistance;
        }
    }
}