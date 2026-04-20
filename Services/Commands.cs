using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;
using W04.Models;

namespace W04.Services
{

    public class AttackCommand : ICommand
    {
        private IEntity _attacker;
        private IEntity _target;
        public AttackCommand(IEntity attacker, IEntity target)
        {
            _attacker = attacker;
            _target = target;
        }
        public void Execute()
        {
            _attacker.Attack(_target);
        }
    }

    public class MoveCommand : ICommand
    {
        private IEntity _entity;
        public MoveCommand(IEntity entity)
        {
            _entity = entity;
        }
        public void Execute()
        {
            _entity.Move();
        }
    }
    public class FlyCommand : ICommand
    {
        private IFlyable _entity;
        public FlyCommand(IFlyable entity)
        {
            _entity = entity;
        }
        public void Execute()
        {
            _entity.Fly();
        }
    }

        public class ShootCommand : ICommand
        {
            private IShootable _entity;
            public ShootCommand(IShootable entity)
            {
                _entity = entity;
            }
            public void Execute()
            {
                _entity.Shoot();
            }
        }

        public class FirebreathingCommand : ICommand
        {
            private IFireBreathing _entity;
            public FirebreathingCommand(IFireBreathing entity)
            {
                _entity = entity;
            }
            public void Execute()
            {
                _entity.FireBreathing();
            }

        }
   }   