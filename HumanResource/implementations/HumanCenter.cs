using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class HumanCenter : IHumanCenter
    {
        private List<IOrganization> _organizations = new List<IOrganization>();
        public void AddOrganization(IOrganization org)
        {
            if (_organizations.Find(o => o.Id == org.Id) != null)
            {
                throw new InvalidOperationException();
            }
            else
                _organizations.Add(org);
        }

        public IOrganization GetOrganizationById(string id)
        {
            var result = _organizations.Find(o => o.Id == id);
            return result;
        }

        public IOrganization GetOrganizationByName(string name)
        {
            var result = _organizations.Find(o => o.Name == name);
            return result;
        }

        public List<IOrganization> GetOrganizations()
        {
            return _organizations.ToList();
        }

        public void RemoveOrganization(string id)
        {
            var query = from org in _organizations
                        where org.Id == id
                        select org;
            if (query.Any())
            {
                var result = query.Single();
                _organizations.Remove(result);
            }
            else
                throw new InvalidOperationException();
        }
    }
}
