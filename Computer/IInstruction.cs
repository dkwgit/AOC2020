using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Computer
{
    public interface IInstruction
    {
        int Amount { get; }
        string Name { get;}
    }
}
