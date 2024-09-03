using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DonsDuSangApp.ViewModels
{
    public partial class QuestionnaireViewModel(IDialogService dialogService, INavigationService navigationService)
        : BaseViewModel(dialogService, navigationService)
    {

    }
}
