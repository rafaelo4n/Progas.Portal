using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Application.Services.Implementations;
using Progas.Portal.UI.Controllers;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Progas.Portal.UI.App_Start
{
    public class ControllerRegistry:  Registry
    {
        public ControllerRegistry()
        {
           
        }
    }
}
