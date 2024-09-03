namespace DonsDuSangApp.Services
{
    public partial class NavigationService : INavigationService
    {
        public Task GoToAsync(string route, object? paramValue = null)
        {
            if (Shell.Current is null)
            {
                throw new NotSupportedException($"Navigation with the '{nameof(GoToAsync)}' method is currently supported only with a Shell-enabled application.");
            }

            Dictionary<string, object?> parameters = [];
            if (paramValue != null)
            {
                parameters.Add("data", paramValue);
            }

            return Shell.Current.GoToAsync(new ShellNavigationState(route), parameters);
        }

        public Task GoBackAsync()
        {
            if (Shell.Current is null)
            {
                throw new NotSupportedException($"Navigation with the '{nameof(GoBackAsync)}' method is currently supported only with a Shell-enabled application.");
            }

            return Shell.Current.GoToAsync(new ShellNavigationState(".."));
        }
    }
}
