using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using MesBase.Mvvm;

namespace HumanResource
{
    public abstract class Organization : ObservableObject, IOrganization, ILegalPerson
    {
        private Action<IPerson> _onMemberEntered;
        private Action<IPerson> _onMemberLeft;
        private ICollection<IPerson> _people;

        public string Id { get; set; }
        public string Name { get; set; }

        #region ILegalPerson
        public string Rights { get; set; }
        public string Responsibility { get; set; }
        #endregion
        public abstract string Type { get; }
        #region ObservableObjects
        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private ObservableCollection<IPerson> _peopleToShow;
        public ObservableCollection<IPerson> PeopleToShow 
        {
            get => _peopleToShow;
            set => SetProperty(ref _peopleToShow, value);
        }
        #endregion

        public Organization(string id, string name, 
            Action<IOrganization, IPerson> onMemberEntered = null,
            Action<IOrganization, IPerson> onMemberLeft = null)
        {
            this.Id = id;
            this.Name = name;
            _people = new List<IPerson>();

            if(onMemberEntered != null)
                this._onMemberEntered = new Action<IPerson>(person =>
                {
                    onMemberEntered.Invoke(this, person);
                });

            if (onMemberLeft != null)
                this._onMemberLeft = new Action<IPerson>(person =>
                {
                    onMemberLeft.Invoke(this, person);
                });
        }

        public void AddMember(IPerson person)
        {
            if (ContainsMember(person))
                throw new InvalidOperationException();
            _people.Add(person);
            Count++;
            _onMemberEntered?.Invoke(person);
            Description = this.ToString();
        }

        public IPerson GetMember(string id)
        {
            var query = from p in _people
                        where p.Id == id
                        select p;
            return query.FirstOrDefault();
        }

        public void RemoveMember(IPerson person)
        {
            if (!ContainsMember(person))
                throw new InvalidOperationException();
            _people.Remove(person);
            Count--;
            _onMemberLeft?.Invoke(person);
            Description = this.ToString();
        }

        public void Live()
        {
            foreach (var m in _people)
                m.Live();
        }
        public void Introduce()
        {
            Console.WriteLine($"The organization name:{Name}. We have {_people.Count} members");
            foreach (var m in _people)
                m.SelfIntroduce();
        }
        public override string ToString()
        {
            var s = $"Type:{Type},\tName:{Name},\tCount:{Count},\tList:\n";
            foreach (var m in _people)
                s += $"{m.Name} ";
            return s;
        }

        public bool ContainsMember(IPerson person)
        {
            var result = _people.ToList().Find(p => p.Id == person.Id);
            return result != null;
        }

        public List<IPerson> GetMembers()
        {
            return _people.ToList();
        }
    }
}
