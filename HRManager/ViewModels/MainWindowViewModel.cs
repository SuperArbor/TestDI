using System;
using System.Collections.Generic;
using System.Text;
using MesBase.Mvvm;
using HumanResource;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HRManager.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private IHumanCenter HumanCenter { get; set; }
        private IPersonFactory PersonFactory { get; set; }
        public MainWindowViewModel(IServiceProvider sp)
        {
            this.HumanCenter = sp.GetService(typeof(IHumanCenter)) as IHumanCenter;
            this.PersonFactory = sp.GetService(typeof(IPersonFactory)) as IPersonFactory;
            this.Organizations = new ObservableCollection<Organization>();
            HumanCenter.GetOrganizations().ForEach(org => Organizations.Add(org as Organization));
            var names = new List<string>();
            foreach (var o in Organizations) names.Add(o.Name);
            this.OrganizationNames = new ObservableCollection<string>(names);
        }

        #region ObservableObjects
        public ObservableCollection<Organization> Organizations { get; set; }
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
        private Organization _currentOrganization;
        public Organization CurrentOrganization
        {
            get => _currentOrganization;
            set => SetProperty(ref _currentOrganization, value);
        }
        private IPerson _currentPerson;
        public IPerson CurrentPerson
        {
            get => _currentPerson;
            set => SetProperty(ref _currentPerson, value);
        }
        public ICommand AddPersonCommand => new RelayCommand(() =>
        {
            var currentOrganization = HumanCenter.GetOrganizationByName(OrganizationName);
            if(currentOrganization != null)
            {
                var person = PersonFactory.CreatePerson(PersonType, PersonName, PersonGender, PersonAge);
                currentOrganization.AddMember(person);
            }
        });
        public ICommand RemovePersonCommand => new RelayCommand(() =>
        {
            if (CurrentOrganization != null)
            {
                CurrentOrganization.RemoveMember(CurrentPerson);
            }
        });
        #endregion
    }
}
