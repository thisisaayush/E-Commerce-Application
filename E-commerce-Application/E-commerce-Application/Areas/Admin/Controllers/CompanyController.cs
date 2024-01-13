using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace E_commerce_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CompanyController(IUnitOfWork unitofWork)
        {
            _unitofwork = unitofWork;
        }
        public IActionResult Index() //goes to index.cshtml class in Views/Company folder.
        {
            List<Company> objCompanyList = _unitofwork.Company.GetAll().ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                //Insert-Create
            {
                return View(new Company());
            }
            else //Update
            {
                Company companyObj = _unitofwork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
           
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if(CompanyObj.Id == 0)
                {
                    //Add Id, Company.
                    _unitofwork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitofwork.Company.Update(CompanyObj);
                }
                
                _unitofwork.Save();
                TempData["success"] = "Company created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
        }
       
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitofwork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitofwork.Company.Get(u => u.Id == id);

            if(CompanyToBeDeleted == null)
            {
                return Json(new { success=false, message="Error while deleting"});
            }

            _unitofwork.Company.Remove(CompanyToBeDeleted);
            _unitofwork.Save();

            return Json(new { success = true, message = "Delete Successful." });
        }
        #endregion
    }
}
