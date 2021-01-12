using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MesBase.Mvvm;

namespace HumanResource
{
    public abstract class Organization : ObservableObject, IOrganization, ILegalPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rights { get; set; }
        public string Responsibility { get; set; }
        public abstract string Type { get; }
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

        private readonly ICollection<IPerson> _people;
        public Organization(string id, string name) 
        {
            this.Id = id;
            this.Name = name;
            _people = new List<IPerson>();
        }
        public Organization(string id, string name, ICollection<IPerson> people)
        {
            this.Id = id;
            this.Name = name;
            this._people = people;
        }

        public void AddMember(IPerson person)
        {
            _people.Add(person);
            person.Organization = this;
            Count++;
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
            _people.Remove(person);
            Count--;
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
    }
}
