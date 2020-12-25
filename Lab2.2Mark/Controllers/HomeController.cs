
using Lab2._2Mark.Filters;
using Lab2._2Mark.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using System.Text;

namespace Lab2._2Mark.Controllers
{
    [Log]
    public class HomeController : Controller
    {
        //Передаём бд в контроллер
        ImageContext db = new ImageContext();
        //К представлению картинок
        [MyAuth]
        public ActionResult Index()
        {
            // Создаём перечислитель с типом Image
            IEnumerable<Image> image = db.Images;
            ViewBag.Images = image;
            return View();
        }

        
        [HttpGet]
        public ActionResult EditImage(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Image image = db.Images.Find(id);
            if (image != null)
            {
                return View(image);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditImage(Image image)
        {
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Image image)
        {
            db.Images.Add(image);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // Получаем удаляемы элемент

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Image b = db.Images.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        //Удаляем элемент
        public ActionResult DeleteConfirmed(int id)
        {
            Image b = db.Images.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Images.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UserIndex(int page = 1)
        {
            //Создаём перечислитель, содержащий картинки (Images)
            IEnumerable<Image> image = db.Images;
            int pageSize = 1; // количество объектов на страницу
            // Перечислитель, содержащий картинки, которые будут отображаться на одой странице
            IEnumerable<Image> ImagesPerPages = image.Skip((page - 1) * pageSize).Take(pageSize);
            // Собирем информацию о странице((номер страницы, размер страници, количество картинок), перечилитель с картинками)
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = image.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Images = ImagesPerPages };
            // Передаём переменную типа IndexViewModel в представление
            return View(ivm);
        }
        
        // Метод для получения лайка или дизлайка от UserIndex
        [HttpPost]
        public ActionResult UserIndex(int id, string buttonValue)
        {
            Image image = db.Images.Find(id);
            if(image.Rating==null)
            {
                if(buttonValue == "Нравится")
                {
                    image.Rating = 1;
                }
                else
                {
                    image.Rating = -1;
                }
            }
            else
            {
                if (buttonValue == "Нравится")
                {
                    image.Rating += 1;
                }
                else
                {
                    image.Rating -= 1;
                }

            }
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserIndex");


        }

        // Путь к файлу с протоколированием
        string docPath = "C:/Users/Марк/source/repos/Lab2.2Mark/Lab2.2Mark/History/";

        public ActionResult Log()
        {
            var visitors = new List<Visitor>();
            using (LogContext db = new LogContext())
            {
                visitors = db.Visitors.ToList();
            }

            // Append text to an existing file named "history.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "history.txt"), true))
            {
                string table1;
                foreach(var item in visitors)
                {
                    table1 = "";
                    table1 += item.Login.ToString() + " ";
                    table1 += item.Ip.ToString() + " ";
                    table1 += item.Url.ToString() + " ";
                    table1 += item.Date.ToString() + " ";
                    outputFile.WriteLine(table1);
                }
            }
            return View(visitors);
                
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}