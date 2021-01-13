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
            //var school = new School(Guid.NewGuid().ToString(), "ZJU");
            //var company = new Company(Guid.NewGuid().ToString(), "Huawei");

            var school = new School(Guid.NewGuid().ToString(), "ZJU", 
                (org, p) => 
                {
                    p.Organization = org.Name;
                    Console.WriteLine($"{org.Name}欢迎新生{p.Name}");
                    p.SelfIntroduce();
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                },
                (org, p) =>
                {
                    p.Organization = "None";
                    Console.Write($"{org.Name}送别毕业生{p.Name}。");
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                });
            var huawei = new Company(Guid.NewGuid().ToString(), "Huawei",
                (org, p) =>
                {
                    p.Organization = org.Name;
                    Console.Write($"{org.Name}欢迎新同事{p.Name}。");
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                    Console.WriteLine($"{p.Name}签署了《奋斗者协议》");
                },
                (org, p) =>
                {
                    p.Organization = org.Name;
                    Console.Write($"{org.Name}送别同事{p.Name}。");
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                    Console.WriteLine($"{p.Name}向{org.Name}索要赔偿N+2");
                });
            var ali = new Company(Guid.NewGuid().ToString(), "Alibaba",
                (org, p) =>
                {
                    p.Organization = org.Name;
                    Console.Write($"{org.Name}欢迎新同事{p.Name}。");
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                },
                (org, p) =>
                {
                    p.Organization = org.Name;
                    Console.Write($"{org.Name}送别同事{p.Name}。");
                    Console.WriteLine($"现在{org.Name}的人数为{org.Count}");
                    Console.WriteLine($"{p.Name}向{org.Name}索要赔偿N+3");
                });

            var dong = new Worker(Guid.NewGuid().ToString(), "Dong", "male", 14);
            school.AddMember(new Student(Guid.NewGuid().ToString(), "Gao", "female", 22));
            huawei.AddMember(new Worker(Guid.NewGuid().ToString(), "Liu", "male", 18));
            huawei.AddMember(new Worker(Guid.NewGuid().ToString(), "Hao", "female", 18));
            huawei.AddMember(dong);
            huawei.RemoveMember(dong);
            ali.AddMember(dong);

            //school.Live();
            //huawei.Live();
            //ali.Live();
        }
    }
}
