using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Services;

namespace W04.Interfaces
{
    public interface IEntity 
    {

        void Attack(IEntity target);
        void Move();
        string Name { get; set; }
    }
}
