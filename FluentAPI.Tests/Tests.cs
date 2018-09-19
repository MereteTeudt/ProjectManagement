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
            p.StartDate = new DateTime(1990, 10, 10);
            p.EndDate = new DateTime(2000, 10, 10);
            p.Budget = 100.00m;
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
            Project project = model.Projects.Where(p => p.Name == "Something").FirstOrDefault();
            string oldDescription = project.Description;
            project.Description = ((new Random()).Next(0,Int32.MaxValue)).ToString();
            model.SaveChanges();
            Assert.AreNotEqual(oldDescription, project.Description);
        }

        [TestMethod]
        public void CreateUnaffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "Indigo";
            t.Description = "I am fancy, but Im still blue";
            t.StartDate = new DateTime(2017, 12, 12);
            t.EndDate = new DateTime(2018, 12, 12);
            Model model = new Model();
            model.Teams.Add(t);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateAffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "Orange";
            t.Description = "I am not a fruit";
            t.StartDate = new DateTime(2016, 12, 12);
            t.EndDate = new DateTime(2017, 12, 12);

            Project p = new Project();
            p.Name = "Tango";
            p.Description = "I am not a dance";
            p.StartDate = new DateTime(1990, 10, 10);
            p.EndDate = new DateTime(2000, 10, 10);
            p.Budget = 100.00m;

            List<Team> demTeams = new List<Team>();
            demTeams.Add(t);
            p.Teams = demTeams;
            Model model = new Model();
            model.Projects.Add(p);
            model.SaveChanges();

            Team team = model.Teams.Where(someteam => someteam.Name == "Orange").FirstOrDefault();
            Project affiliatedProject = team.Project;
            bool projectHasTeam = team.Name == t.Name && affiliatedProject.Name == p.Name;

            Assert.IsTrue(projectHasTeam);
        }
        /// <summary>
        /// Verify that contactinfo cannot be created without an employee because contactinfo Id is a foreign key to an employee
        /// </summary>
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
            Employee employee = new Employee("Test", "Name", "2222222222", new DateTime(1980, 01, 01), new DateTime(2010, 01, 01), 30000.00m);
            Model model = new Model();
            model.Employees.Add(employee);
            model.SaveChanges();
            Employee savedEmployee = model.Employees.Where(someEmployee => someEmployee.FirstName == "Test").FirstOrDefault();
            Assert.IsNotNull(savedEmployee);
        }

        [TestMethod]
        public void CreateEmployeeWithContactInfo()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "test@test.dk";
            contactInfo.Phone = "12345678";
            Employee employee = new Employee("Test", "Name", "2222222222", new DateTime(1980, 01, 01), new DateTime(2010, 01, 01), 30000.00m);
            employee.ContactInfo = contactInfo;
            Model model = new Model();
            model.Employees.Add(employee);
            model.SaveChanges();
            Employee savedEmployee = model.Employees.Where(someEmployee => someEmployee.FirstName == "Test").FirstOrDefault();
            Assert.IsNotNull(savedEmployee);
        }
    }
}
