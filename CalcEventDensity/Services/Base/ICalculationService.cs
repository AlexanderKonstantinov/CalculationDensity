using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcEventDensity.Services.Base
{
    interface ICalculationService
    {
        event Action OnCalculationStart;
        event Action OnCalculationEnd;

        void Calculate();
    }
}
