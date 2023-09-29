using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDBContext _db;
        public PaymentController(AppDBContext db)
        {
           _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Payment> objPaymentList = _db.Payments;
            return View(objPaymentList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment obj)
        {
            if (ModelState.IsValid) {

                if (!_db.Payments.Any(p => p.PaymentName == obj.PaymentName))
                {
                    _db.Payments.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("PaymentName", "A Payment with the same name already exists.");
                }
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var paymentFromDb = _db.Payments.Find(id);
            if (paymentFromDb == null)
            {
                return NotFound();
            }
            return View(paymentFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Payment obj)
        {
            if (ModelState.IsValid)
            {
                _db.Payments.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Payment updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var PaymentFromDb = _db.Payments.Find(id);

            if (PaymentFromDb == null)
            {
                return NotFound();
            }

            return View(PaymentFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Payments.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Payments.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Payment deleted";
            return RedirectToAction("Index");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBulk(List<int> selectedIds)
        {
            foreach (var id in selectedIds)
            {
                var recordToDelete = _db.Payments.Find(id);
                if (recordToDelete != null)
                {
                    _db.Payments.Remove(recordToDelete);
                }
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
