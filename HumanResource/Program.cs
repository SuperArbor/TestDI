using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResource
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            var school = new School(Guid.NewGuid().ToString(), "Zhedafuzhong");

            var company = new Company(Guid.NewGuid().ToString(), "Huawei");

            sc.AddSingleton<IOrganization, School>(sp => school);
            sc.AddSingleton<IOrganization, Company>(sp => company);
            var sp = sc.BuildServiceProvider();

            school.AddMember(new Student(Guid.NewGuid().ToString(), "Lin", "female", 22));
            company.AddMember(new Worker(Guid.NewGuid().ToString(), "Yang", "male", 18));
            company.AddMember(new Worker(Guid.NewGuid().ToString(), "Feng", "male", 14));

            school.Introduce();
            company.Introduce();

            school.Live();
            company.Live();
        }
    }
}
