using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ContourDetector.Annotations;
using DetectingAlgorithms;

namespace ContourDetector.ViewModel
{
    public class AlgorithmViewModel: INotifyPropertyChanged
    {    
        public event PropertyChangedEventHandler PropertyChanged;

        public AlgorithmViewModel(IList<AbstractOperator> algorithms)
        {
            this.Algorithms = new CollectionView(algorithms);
        }

        IDetectingAlgorithm _selectedAlgorithm;
        public IDetectingAlgorithm SelectedAlgorithm
        {
            get
            {
                return _selectedAlgorithm;
            }
            set
            {
                _selectedAlgorithm = value;
                OnPropertyChanged(nameof(SelectedAlgorithm));
            }
        }

        public CollectionView Algorithms { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
