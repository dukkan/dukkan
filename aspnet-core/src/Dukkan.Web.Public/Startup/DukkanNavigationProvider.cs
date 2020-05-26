using Abp.Application.Navigation;
using Abp.Localization;

namespace Dukkan.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class DukkanNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DukkanConsts.LocalizationSourceName);
        }
    }
}
