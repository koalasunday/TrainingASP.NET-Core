using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;
using Training.Models;
using Training.ViewModels;

namespace Training.Controllers
{
    public class ProfilingController : Controller
    {
        private readonly MyContext myContext;
        public ProfilingController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {

            List<Profiling> profilings = myContext.Profilings.ToList();
            List<Education> educations = myContext.Educations.ToList();
            List<Person> people = myContext.Peoples.ToList();
            List<University> universities = myContext.Universities.ToList();
            List<Account> accounts = myContext.Accounts.ToList();

            var profilingList = from p in profilings
                                join e in educations on p.Education_Id equals e.Id into table1
                                from e in table1.ToList()
                                join u in universities on e.University_Id equals u.Id into table2
                                from u in table2.ToList()
                                join a in accounts on p.NIK equals a.NIK into table3
                                from a in table3.ToList()
                                join l in people on a.NIK equals l.NIK into table4
                                from l in table4.ToList()

                                select new ViewModel
                                {
                                    profiling = p,
                                    education = e,
                                    university = u,
                                    account = a,
                                    person = l
                                };
            return View(profilingList);
        }

        #region Create Data 
        public IActionResult Create()
        {
            List<Education> educations = new List<Education>();
            educations = (from c in myContext.Educations select c).ToList();
            educations.Insert(0, new Education { Id = 0, Degree = "--Select University--" });
            ViewBag.message = educations;

            List<Person> people = new List<Person>();
            people = (from c in myContext.Peoples select c).ToList();
            people.Insert(0, new Person { NIK = "", FirstName = "", LastName = "" });
            ViewBag.message2 = people;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ViewModel viewModel)
        {
            Person person = new Person();
            person.NIK = viewModel.NIK;
            person.FirstName = viewModel.FirstName;
            person.LastName = viewModel.LastName;
            person.Phone = viewModel.Phone;
            person.BirthDate = viewModel.BirthDate;
            myContext.Peoples.Add(person);
            var result = myContext.SaveChanges();

            if (result > 0)
            {
                Account account = new Account();
                account.NIK = viewModel.NIK;
                account.Password = "123456";
                myContext.Accounts.Add(account);
                var result2 = myContext.SaveChanges();
                if (result2 > 0)
                {
                    Profiling profiling = new Profiling();
                    profiling.NIK = viewModel.NIK;
                    profiling.Education_Id = viewModel.EducationId;
                    myContext.Profilings.Add(profiling);
                    var result3 = myContext.SaveChanges();
                }
            }

            return View();

        }

        #endregion

                #region Edit Data
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return View();
            }
            var result = myContext.Profilings.Find(id); 
            if (result == null)
            {
                return View();
            }

            List<Education> educations = new List<Education>();
            educations = (from c in myContext.Educations select c).ToList();
            educations.Insert(0, new Education { Id = 0, Degree = "--Select University--" });
            ViewBag.message2 = educations;



            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(string id, Profiling profiling)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Profilings.Find(id);
            if (get != null)
            {
                get.Education_Id = profiling.Education_Id;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction(nameof(Index));//View("Index");
                return View();
            }
            return View();
        }

        #endregion

        #region Delete Data
        public IActionResult Delete(string id)
        {
            var get = myContext.Profilings.Find(id);

            if (get != null)
            {
                myContext.Profilings.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }
            return Json(0);
        }
        #endregion

    }
}
