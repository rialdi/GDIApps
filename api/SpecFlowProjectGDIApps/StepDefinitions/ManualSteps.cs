using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding,Scope(Tag ="manual")]
    public class ManualSteps
    {
        [Given(".*"), When(".*"), Then(".*")]
        public void EmptyStep()
        {
        }

        [Given(".*"), When(".*"), Then(".*")]
        public void EmptyStep(string multiLineStringParam)
        {
        }

        [Given(".*"), When(".*"), Then(".*")]
        public void EmptyStep(Table tableParam)
        {
        }
    }
}
