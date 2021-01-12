using System;
using System.Collections.Generic;
using System.Text;
using MesBase.Mvvm;
using HumanResource;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace HRManager.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IHumanCenter HumanCenter { get; set; }
        private IPersonFactory PersonFactory { get; set; }
        public MainWindowViewModel(IServiceProvider sp)
        {
            this.ServiceProvider = sp;
            this.HumanCenter = sp.GetService(typeof(IHumanCenter)) as IHumanCenter;
            this.PersonFactory = sp.GetService(typeof(IPersonFactory)) as IPersonFactory;
            Organizations = new ObservableCollection<IOrganization>(HumanCenter.GetOrganizations());
            var names = new List<string>();
            foreach (var o in Organizations) names.Add(o.Name);
            OrganizationNames = new ObservableCollection<string>(names);
        }

        #region ObservableObjects
        public ObservableCollection<IOrganization> Organizations { get; set; }
        public ObservableCollection<string> OrganizationNames { get; set; }
        private string _organizationName;
        public string OrganizationName
        {
            get => _organizationName;
            set => SetProperty(ref _organizationName, value);
        }
        private string _personType = "Student";
        public string PersonType
        {
            get => _personType;
            set => SetProperty(ref _personType, value);
        }
        private string _personName = "Wang";
        /// <summary>
        /// Name of the current person in editing
        /// </summary>
        public string PersonName
        {
            get => _personName;
            set => SetProperty(ref _personName, value);
        }
        private string _personGender = "Male";
        public string PersonGender
        {
            get => _personGender;
            set => SetProperty(ref _personGender, value);
        }
        private int _personAge = 18;
        public int PersonAge
        {
            get => _personAge;
            set => SetProperty(ref _personAge, value);
        }
        
        public RelayCommand AddPersonCommand => new RelayCommand(() =>
        {
            var currentOrganization = HumanCenter.GetOrganizationByName(OrganizationName);
            var person = PersonFactory.CreatePerson(PersonType, PersonName, PersonGender, PersonAge);
            currentOrganization.AddMember(person);
        });
        #endregion
    }
}
