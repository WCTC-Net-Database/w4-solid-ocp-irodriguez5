using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Services
{
    internal class GameEngine
    {
        private List<IEntity> _entities = new List<IEntity>();
        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
        }

        public void Run()
        {
            foreach (var entity in _entities)
            {
                entity.Move();

                if (entity is IFlyable flyer)
                {
                    flyer.Fly();
                }

                if (entity is IShootable shooter)
                {
                    shooter.Shoot();
                }

                if (entity is IFireBreathing fireBreather)
                {
                    fireBreather.FireBreathing();
                }
            }
        }
    }
}
