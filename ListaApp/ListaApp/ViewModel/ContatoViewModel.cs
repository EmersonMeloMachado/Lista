using ListaApp.Model;
using ListaApp.Service;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ListaApp.ViewModel
{
    public class ContatoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if(EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public ObservableCollection<Contato> lContato { get; }

        private int _contator;
        public int ContatoCount
        {
            get => _contator;
            set => SetProperty(ref _contator, value);
        }

        public string Telefone { get; private set; } = "+55 (11) 9807-0055";

        public ContatoViewModel()
        {
            lContato = new ObservableCollection<Contato>();
            ListaContato();

            ContatoCount = lContato.Count;
        }

        public async void ListaContato()
        {
            var repo = new ContatoService();
            ObservableCollection<Contato> l = await repo.ObterTodosContatos();
            foreach(var item in l)
            {
                lContato.Add(item);
            }
        }
    }
}
