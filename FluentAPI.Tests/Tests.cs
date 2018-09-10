using System;
using FluentAPI.EF;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllProjects()
        {
            Model model = new Model();
            var result = model.Projects.ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void AddNewProject()
        {
            Model model = new Model();
            Project p = new Project();
            p.Name = "Something";
            p.Description = "A lot of something";
            model.Projects.Add(p);
            int count = model.Projects.ToList().Count;
            model.SaveChanges();
            int newCount = model.Projects.ToList().Count;
            Assert.AreEqual(newCount, count + 1);
        }

        [TestMethod]
        public void UpdateProject()
        {
            Model model = new Model();
            Project project = model.Projects.Find(1);
            string oldDescription = project.Description;
            project.Description = ((new Random()).Next(0,Int32.MaxValue)).ToString();
            model.SaveChanges();
            var newP = model.Projects.Find(1);
            Assert.AreNotEqual(oldDescription, newP.Description);
        }

        [TestMethod]
        public void CreateUnaffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "Awesome Team";
            Model model = new Model();
            model.Teams.Add(t);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateAffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "A Team";
            Project p = new Project();
            p.Name = "The A Team Project";
            List<Team> demTeams = new List<Team>();
            demTeams.Add(t);
            p.Teams = demTeams;
            Model model = new Model();
            model.Projects.Add(p);
            model.SaveChanges();

            Team team = model.Teams.Where(someteam => someteam.Name == "A Team").FirstOrDefault();
            Project affiliatedProject = team.Project;
            bool projectHasTeam = team.Name == t.Name && affiliatedProject.Name == p.Name;

            Assert.IsTrue(projectHasTeam);
        }
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        [TestMethod]
        public void InvalidOperationException()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "mara@aspit.dk";
            contactInfo.Phone = "12345678";
            Model model = new Model();
            model.ContactInfos.Add(contactInfo);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateEmployeeWithoutContactInfo()
        {
            Employee employee = new Employee();
            employee.Name = "TestName";
            Model model = new Model();
            model.Employees.Add(employee);
            model.SaveChanges();
            Employee savedEmployee = model.Employees.Where(someEmployee => someEmployee.Name == "TestName").FirstOrDefault();
            Assert.IsNotNull(savedEmployee);
        }

        [TestMethod]
        public void CreateEmployeeWithContactInfo()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "mara@aspit.dk";
            contactInfo.Phone = "12345678";
            Employee employee = new Employee();
            employee.Name = "TestName";
            employee.ContactInfo = contactInfo;
            Model model = new Model();
            model.Employees.Add(employee);
            model.SaveChanges();
            Employee savedEmployee = model.Employees.Where(someEmployee => someEmployee.Name == "TestName").FirstOrDefault();
            Assert.IsNotNull(savedEmployee);
        }
    }
}
