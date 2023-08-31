using SampleHierarchies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Services
{
    public interface IScreenDefinitionService
    {
        ScreenDefinition Load(string jsonFileName);
        bool Save(ScreenDefinition screenDefinition, string jsonFileName);
    } 
}
