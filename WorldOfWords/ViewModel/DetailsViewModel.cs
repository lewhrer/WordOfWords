using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public delegate void Func();

    public class DetailsViewModel
    {
        public string Message { get; set; }
        private Func func;

        public DetailsViewModel(string message, Func func)
        {
            Message = message;
            this.func = func;
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ??
                  (okCommand = new RelayCommand(obj =>
                  {
                      func.Invoke();
                      Settings.Details.NavigationService.Source = null;
                  }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
                      Settings.Details.NavigationService.Source = null;
                  }));
            }
        }
    }
}
