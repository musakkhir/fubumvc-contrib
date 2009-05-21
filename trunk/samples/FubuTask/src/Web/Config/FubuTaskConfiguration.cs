using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Conventions.ControllerActions;
using FubuMVC.Framework.Presentation.Behaviors;
using FubuTask.Presentation;

namespace FubuTask.Config
{
    public class MvcConfiguration
    {
        public static void Configure()
        {
            ControllerConfig.Configure = x =>
            {
                x.UsingConventions(conv =>
                {
                    conv.DefaultPathToViewForAction = config =>
                    {
                        var controllerName = conv.CanonicalControllerName(config.ControllerType);
                        var actionName = config.ActionName;
                        return "{0}/{1}/{2}.spark".ToFormat(conv.ViewFileBasePath, controllerName, actionName);
                    };

                });

                // Default Behaviors for all actions -- ordered as they're executed
                /////////////////////////////////////////////////
                x.ByDefault.EveryControllerAction(d => d
                    .Will<access_the_database_through_a_unit_of_work>()
                    .Will<load_the_current_principal>()
                    .Will<execute_the_result>()
                    .Will<OutputAsRssOrAtomFeed>()
                    .Will<output_as_json_if_requested>()
                    .Will<copy_viewmodel_from_input_to_output<ViewModel>>()
                );

                x.ActionConventions(custom =>
                {
                    custom.Add<wire_up_JSON_URL>();
                    custom.Add<wire_up_RSS_and_ATOM_URLs_if_required>();
                    custom.Add<wire_up_404_handler_URL>();
                });

                x.AddControllerActions(a => a
                    .UsingTypesInTheSameAssemblyAs<ViewModel>(s =>
                        s.SelectTypes(t =>
                            t.Namespace.EndsWith("Presentation.Controllers") &&
                            t.Name.EndsWith("Controller")
                        )
                    )
                );
            };
        }
    }
}