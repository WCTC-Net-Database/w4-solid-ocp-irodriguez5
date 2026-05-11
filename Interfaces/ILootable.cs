using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04.Interfaces
{
    public interface ILootable
    {
        List<string>? Treasure { get; set; }
    }
}
