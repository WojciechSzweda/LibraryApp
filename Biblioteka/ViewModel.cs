using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Biblioteka
{
    [ImplementPropertyChanged]
    public class ViewModel
    {
        public ObservableCollection<KeyValuePair<string, int>> ValueList { get; set; }

        public ViewModel()
        {
            this.ValueList = new ObservableCollection<KeyValuePair<string, int>>();
        }

        public void Add(List<KeyValuePair<string, int>> list)
        {
            if (list == null)
                return;

            foreach (var item in list)
            {
                this.ValueList.Add(item);
            }
        }
    }

    [ImplementPropertyChanged]
    public class LoginViewModel
    {
        public bool ErrorVisibility { get; set; }
    }
}
