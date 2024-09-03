
public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}
