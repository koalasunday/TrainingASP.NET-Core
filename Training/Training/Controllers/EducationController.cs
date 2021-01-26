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
    public class EducationController : Controller
    {
        private readonly MyContext myContext;
        public EducationController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            List<Education> educations = myContext.Educations.ToList();
            List<University> universities = myContext.Universities.ToList();

            var educationList = from e in educations
                                  join u in universities on e.University_Id equals u.Id into table1
                                  from u in table1.ToList()
                                  select new ViewModel
                                  {
                                      education = e,
                                      university = u
                                  };
            return View(educationList);
        }

        #region Create New Data
        public IActionResult Create()
        {
            List<University> university = new List<University>();
            university = (from c in myContext.Universities select c).Where(x => x.IsAvailable == true).ToList();
            university.Insert(0, new University { Id = 0, Name = "--Select University--" });
            ViewBag.message = university;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(ViewModel education_ViewModel)
        {
            Education education = new Education();
            education.GPA = education_ViewModel.GPA;
            education.Degree = education_ViewModel.Degree;
            education.University_Id = education_ViewModel.UniversityId;
            myContext.Educations.Add(education);
            var result = myContext.SaveChanges();

            if (result > 0)
                return RedirectToAction("Index"); //nameof(Index) //cara lain
            return View();

        }

        #endregion

        #region Edit Data
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var result = myContext.Educations.Find(id); //cara lain myContext.Universities.Find(id);
            if (result == null)
            {
                return View();
            }

            List<University> university = new List<University>();
            university = (from c in myContext.Universities select c).Where(x => x.IsAvailable == true).ToList();
            university.Insert(0, new University { Id = 0, Name = "--Select University--" });
            ViewBag.message2 = university;

            //List<University> university = new List<University>();
            //university = (from c in myContext.Universities select c).ToList();
            //university.Insert(result.University_Id, new University { Id = 0, Name = "--Select University--" });
            //ViewBag.message = university;

            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Education education)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                get.GPA = education.GPA;
                get.Degree = education.Degree;
                get.University_Id = education.University_Id;
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
        public IActionResult Delete(int id)
        {
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                myContext.Educations.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }
            return Json(0);
        }
        #endregion
    }
}
