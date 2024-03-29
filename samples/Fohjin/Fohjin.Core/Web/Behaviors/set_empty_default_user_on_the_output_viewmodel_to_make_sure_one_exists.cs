using Fohjin.Core.Domain;
using FubuMVC.Core.Behaviors;

namespace Fohjin.Core.Web.Behaviors
{
    public class set_empty_default_user_on_the_output_viewmodel_to_make_sure_one_exists : behavior_base_for_convenience
    {
        public void UpdateModel(ViewModel model)
        {
            if (model == null) return;

            model.CurrentUser = new User
                                {
                                    IsAuthenticated = false,
                                    UserRole = UserRoles.NotAuthenticated,
                                    DisplayName = "",
                                    Email = "",
                                    HashedEmail = "",
                                    Password = "",
                                    PasswordSalt = "",
                                    Remember = false,
                                };
        }

        public override void PrepareInput<INPUT>(INPUT input)
        {
            UpdateModel(input as ViewModel);
        }
    }
}