using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoApp2.Model;
using ToDoApp2.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace ToDoApp2.ViewModel
{
    public class CountryViewModel : BindableObject
    {
        // Переменная для хранения состояния
        // выбранного элемента коллекции
        public Country _selectedItem;
        // Объект с логикой по извлечению данных
        // из источника
        CountryServise countryService = new();

        // Коллекция извлекаемых объектов
        public ObservableCollection<Country> Countries { get; } = new();
        // Конструктор с вызовом метода
        // получения данных
        public CountryViewModel()
        {
            GetCountriesAsync();
        }
        // Публичное свойство для представления
        // описания выбранного элемента из коллекции
        public string Desc { get; set; }
        // Свойство для представления и изменения
        // состояния выбранного объекта
        public Country SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                Desc = value?.Description;
                // Метод отвечает за обновление данных
                // в реальном времени
                OnPropertyChanged(nameof(Desc));
            }
        }
        // Команда для добавления нового элемента
        // в коллекцию
        public ICommand AddItemCommand => new Command(() => AddNewItem());

        // Метод для создания нового элемента
        private void AddNewItem()
        {
            Countries.Add(new Country
            {
                Id = Countries.Count + 1,
                Name = "Title " + Countries.Count,
                Description = "Description",
                Area = "Area"
            });
        }

        // Метод получения коллекции объектов
        public async Task GetCountriesAsync()
        {
            try
            {
                var countries = await CountryServise.GetCountry();

                if (Countries.Count != 0)
                    Countries.Clear();

                foreach (var country in countries)
                {
                    Countries.Add(country);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!",
                    $"Что-то пошло не так: {ex.Message}", "OK");            
            }
        }
    }
}
