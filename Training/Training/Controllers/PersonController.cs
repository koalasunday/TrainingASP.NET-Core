using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;
using Training.Models;

namespace Training.Controllers
{
    public class PersonController : Controller
    {
        private readonly MyContext myContext;
        public PersonController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var persons = myContext.Peoples.ToList();
            return View(persons);
        }
        public IActionResult Create() //defaultnya get [httpget]
        {
            return View();
        }

        //konsep ...
        [HttpPost]
        //[ValidateAntiForgeryToken] //
        public IActionResult Create(Person person)
        {
            myContext.Peoples.Add(person);
            var result = myContext.SaveChanges(); //diberi variabel untuk menampung nilai dari savechange

            //untuk mengetahui apkah savecg=hanges sukses, jika nilainya > 0 berartikondisinya 
            if (result > 0)
                return RedirectToAction("Index"); //nameof(Index) //cara lain
            return View();

        }

    }
}
