namespace DonsDuSangApp.Services
{
    public partial class DialogService : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
			if (Shell.Current is null)
            {
                throw new NotSupportedException($"This method is currently supported only with a Shell-enabled application.");
            }
            
            return Shell.Current.DisplayAlert(title, message, cancel);
        }

        public Task DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
			if (Shell.Current is null)
            {
                throw new NotSupportedException($"This method is currently supported only with a Shell-enabled application.");
            }

			return Shell.Current.DisplayAlert(title, message, accept, cancel);
        }
    }
}
