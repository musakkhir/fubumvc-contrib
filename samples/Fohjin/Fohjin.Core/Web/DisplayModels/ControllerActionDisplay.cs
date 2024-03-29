using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Controller.Config;

namespace Fohjin.Core.Web.DisplayModels
{
    public class ControllerActionDisplay
    {
        public ControllerActionDisplay(ControllerActionConfig controllerActionConfig)
        {
            PrimaryUrl = controllerActionConfig.PrimaryUrl;
            ControllerType = StripAllUpToAndIncludingTheLastDot(controllerActionConfig.ControllerType.ToString());
            ActionFunc = controllerActionConfig.ActionMethod.Name;
            InputType = StripAllUpToAndIncludingTheLastDot(controllerActionConfig.ActionMethod.ToString());
            InputType = InputType.Substring(0, InputType.IndexOf(")"));
            OutputType = StripAllUpToAndIncludingTheLastDot(controllerActionConfig.ActionMethod.ReturnType.ToString());

            var actionName = StripAllUpToAndIncludingTheLastDot(ActionFunc);
            var parenLoc = actionName.IndexOf("(");
            if (parenLoc >= 0)
            {
                actionName = actionName.Substring(0, parenLoc);
            }
            MethodSignature = "public {0} {1}({2} object);".ToFormat(OutputType, actionName, InputType);


            var behaviors = new List<DebugSingleLineDisplay>();
            controllerActionConfig.GetBehaviors().Each(b => behaviors.Add(new DebugSingleLineDisplay(b.ToString())));
            Behaviors = behaviors;

            var otherUrls = new List<DebugSingleLineDisplay>();
            controllerActionConfig.GetOtherUrls().Each(u => otherUrls.Add(new DebugSingleLineDisplay(u)));
            OtherUrls = otherUrls;
            ShowOtherUrls = (OtherUrls.Count != 0);
        }

        public string OutputType { get; set; }
        public string InputType { get; set; }
        public string ActionFunc { get; set; }
        public string ControllerType { get; set; }
        public string PrimaryUrl { get; set; }

        public string MethodSignature { get; set; }
        public IList<DebugSingleLineDisplay> Behaviors { get; set; }
        public IList<DebugSingleLineDisplay> OtherUrls { get; set; }
        public bool ShowOtherUrls { get; set; }

        private static string StripAllUpToAndIncludingTheLastDot(string input)
        {
            return input.Substring(input.LastIndexOf(".") + 1);
        }
    }
}