using ListaApp.Model;
using ListaApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public ObservableCollection<Contato> _contato;

        public int ContatoCount { get; private set; }
        public string Telefone { get; private set; } = "+55 (11) 9807-0055";

        public ContatoViewModel()
        {
            ListaContato().Wait();
        }

        public async Task ListaContato()
        {
            var repo = new ContatoService();
            _contato = await repo.ObterTodosContatos();
        }
    }
}
