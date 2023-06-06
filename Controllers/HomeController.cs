using Kursovaya_BD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Kursovaya_BD.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarContext db;
        public string NewName, NewName1;



        public HomeController(CarContext db)
        {
            this.db = db;
        }


        public IActionResult Index(int? modId, int? markId)
        {
            IQueryable<Tovar> tovars = db.Tovars.Include(x => x.Mod) //
                                        .Include(x => x.Mark);

            // 1. Если выбран тип устройства
            if (modId != null && modId != 0)
            {
                tovars = tovars.Where(x => x.modId == modId);
            }

            // 2. Если выбрана фирма-производитель
            if (markId != null && markId != 0)
            {
                tovars = tovars.Where(x => x.markId == markId);
            }

            // Передаем список товаров в представление через ViewBag
            ViewBag.Tovars = tovars.ToList();

            //====================================================
            // Формируем список типов устройств
            //====================================================
            var modList = db.Models.ToList();

            // Id и DevName - названия полей таблицы Devices
            modList.Insert(0, new Model { Id = 0, modName = "Все модели" });
            SelectList models = new SelectList(modList, "Id", "modName", modId);

            // Передаем список устройств в представление через ViewBag
            ViewBag.Models = models;

            //====================================================
            // Формируем список фирм-производителей
            //====================================================
            IQueryable<Marka> marka = db.Markas;

            // Фильтрация таблицы Firms  по типу устройств
            if (markId != null && markId != 0)
            {
                marka = marka.Where(x => x.ModId == markId);
            }

            var markaList = marka.ToList();

            // Id и FirmName - названия полей таблицы Firms
            markaList.Insert(0, new Marka { Id = 0, markName = "Все марки" });
            SelectList markas = new SelectList(markaList, "Id", "markName", markId);

            // Передаем список фирм в представление через ViewBag
            ViewBag.Markas = markas;

            return View();

        }

        public ActionResult Korzina(int? id)
        {
            Tovar tovar = db.Tovars.FirstOrDefault(p => p.Id == id);

            Korzina korzina = new Korzina()
            {
                // Если поле Id не определен атрибутом Identity
                //Id = tovar.Id, 

                Name = tovar.Name,
                modId = tovar.modId,
                markId = tovar.markId,
                Price = tovar.Price,
                Kol = tovar.Kol
            };

            db.Korzinas.Add(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("Index");
        }

        
        public ActionResult Order(int? id)
        {
            
            Korzina korzina = db.Korzinas.FirstOrDefault(p => p.Id == id);
            

            //Customer customer = db.Customers.FirstOrDefault(p => p.Id == NewID);
            Order order = new Order()
            {
                // Если поле Id не определен атрибутом Identity
                Id = korzina.Id,
                CustomerName = "Иван",
                Name = korzina.Name,
                modId = korzina.modId,
                markId = korzina.markId,
                Price = korzina.Price,
                Kol = korzina.Kol
            };

            db.Orders.Add(order);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction();
        }


        public ActionResult GoToOrder()
        {
            // Переход на главную страницу приложения
            // Получаем из БД все записи таблицы Person
            IEnumerable<Korzina> korzinas = db.Korzinas;

            // Передаем все объекты записи в ViewBag
            ViewBag.Korzinas = korzinas;


            // Переход на главную страницу приложения
            // Получаем из БД все записи таблицы Person
            IEnumerable<Customer> customers = db.Customers;

            // Передаем все объекты записи в ViewBag
            ViewBag.Customers = customers;


            // Переход на главную страницу приложения
            // Получаем из БД все записи таблицы Person
            IEnumerable<Order> orders = db.Orders;

            // Передаем все объекты записи в ViewBag
            ViewBag.Orders = orders;

            // Возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult GoToOrder2(int? Id)
        {
            // Переход на главную страницу приложения
            // Получаем из БД все записи таблицы Person
            IEnumerable<Korzina> korzinas = db.Korzinas;

            // Передаем все объекты записи в ViewBag
            ViewBag.Korzinas = korzinas;

            // Переход на главную страницу приложения
            // Получаем из БД все записи таблицы Person
            IEnumerable<Order> orders = db.Orders;

            // Передаем все объекты записи в ViewBag
            ViewBag.Orders = orders;

            //IEnumerable<Customer> customers = db.Customers;

            //Customer customer1 = db.Customers.FirstOrDefault(d => d.Id == Id);

            //string customer2 = customer1.Name;

            //ViewBag.Name = customer2;

            //NewName = customer2;


            // Возвращаем представление
            return View();
        }









        public ActionResult ViewKorzina()
        {
            // Получаем из БД все записи таблицы Person
            IEnumerable<Korzina> korzinas = db.Korzinas;

            // Передаем все объекты записи в ViewBag
            ViewBag.Korzinas = korzinas;

            // Возвращаем представление
            return View();
        }

        public ActionResult Cancel(int? id)
        {
            Korzina korzina = db.Korzinas.FirstOrDefault(p => p.Id == id);

            db.Korzinas.Remove(korzina);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("ViewKorzina");
        }

        public ActionResult Cancel1(int? id)
        {
            Order order = db.Orders.FirstOrDefault(p => p.Id == id);

            db.Orders.Remove(order);
            db.SaveChanges();

            // Переход на главную страницу приложения
            return RedirectToAction("GoToOrder2");
        }
        public ActionResult BackToTovars()
        {
            // Переход на главную страницу приложения
            return RedirectToAction("Index");
        }
        public ActionResult BackToKorzinas()
        {
            // Переход на главную страницу приложения
            return RedirectToAction("ViewKorzina");
        }
    }
}