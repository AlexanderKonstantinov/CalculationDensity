using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcEventDensity.Models;

namespace CalcEventDensity.Services.Base
{
    interface ICalculationService
    {
        void Calculate(ref string pathToNewFile);

        event Action OnCalculationEnd;
    }
}
