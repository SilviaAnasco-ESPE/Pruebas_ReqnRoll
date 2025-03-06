using Reqnroll;
using ReqnrollTestProject.Reports;

namespace ReqnrollTestProject.Hocks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportManager.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext) 
        {
            ExtentReportManager.StartTest(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext) 
        {
            var stepInfo = scenarioContext.StepContext.StepInfo.Text;
            bool isSuccess = scenarioContext.TestError == null;

            ExtentReportManager.Logstep(isSuccess, isSuccess ? $"Paso exitosos {stepInfo}" : $"Error: {scenarioContext.TestError.Message}");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();
        }
    }
}