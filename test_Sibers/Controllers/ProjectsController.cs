using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test_Sibers.DB;

namespace test_Sibers.Controllers
{
    public class ProjectsController : Controller
    {
        private Test_ProjectsDBEntities db = new Test_ProjectsDBEntities();

        // GET: Projects
        public ActionResult Index()
        {
            var _model = db.Project.Include(ppppp => ppppp.ProjectWorker);
            _model = AddInfoToProjectModel(_model);
            return View(_model);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Include(ppppp => ppppp.ProjectWorker).SingleOrDefault(x => x.ID == id);
            var _workers = db.Worker.ToList();
            var _companies = db.Company.ToList();
            project.Workers = _workers;
            project.Companies = _companies;
            project = AddInfoToProjectModel(project);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            Project project = new Project();
            var _workers = db.Worker.ToList();
            var _companies = db.Company.ToList();
            project.Workers = _workers;
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now;
            project.Companies = _companies;
            project = AddInfoToProjectModel(project);
            return View(project);
        }
        public Project AddInfoToProjectModel(Project _model)
        {
            try
            {
                _model.Leader = db.Worker.Where(w => w.ID == _model.LeaderID).First<Worker>();
            }
            catch (Exception)
            {
                _model.Leader = new Worker() { SecondName = "Не назначен", FirstName = "" };
            }
            try
            {
                _model.Customer = db.Company.Where(w => w.ID == _model.CustomerID).First<Company>();
            }
            catch (Exception)
            {
                _model.Customer = new Company() { Name = "Не назначен" };
            }
            try
            {
                _model.Executor = db.Company.Where(w => w.ID == _model.ExecutorID).First<Company>();
            }
            catch (Exception)
            {
                _model.Executor = new Company() { Name = "Не назначен" };
            }
            

            return _model;
        }
        
            public IQueryable<Project> AddInfoToProjectModel(IQueryable<Project> _model)
        {
            foreach (var item in _model)
            {
                try
                {
                    item.Leader = db.Worker.Where(w => w.ID == item.LeaderID).First<Worker>();
                }
                catch (Exception)
                {
                    item.Leader = new Worker() { SecondName = "Не назначен", FirstName = "" };
                }
                try
                {
                    item.Customer = db.Company.Where(w => w.ID == item.CustomerID).First<Company>();
                }
                catch (Exception)
                {
                    item.Customer = new Company() { Name = "Не назначен" };
                }
                try
                {
                    item.Executor = db.Company.Where(w => w.ID == item.ExecutorID).First<Company>();
                }
                catch (Exception)
                {
                    item.Executor = new Company() { Name = "Не назначен" };
                }

            }
            return _model;
        }
        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CustomerID,ExecutorID,LeaderID,StartDate,EndDate,Priority_,Comment,WorkersIDs")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Project.Add(project);
                db.SaveChanges();
                var _last_id = project.ID;
                //inseret into ProjectWorker
                
                if (project.WorkersIDs!=null)
                {
                    try
                    {
                        foreach (var item in project.WorkersIDs)
                        {
                            db.ProjectWorker.Add(new ProjectWorker { ProjectID = _last_id, WorkerID = item });
                        }
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }
                   
                }
               
                return RedirectToAction("Index");
            }

            return View(project);
        }
        public ActionResult SortProject(string _sortField, string _fromTo)
        {
            var _model = db.Project.Include(ppppp => ppppp.ProjectWorker);
            _model = AddInfoToProjectModel(_model);
            var _fields = _model.First();
            if (_fromTo=="asc")
            {
                switch (_sortField)
                {
                    case ("Name"):
                        _model = _model.OrderBy(m => m.Name);
                        break;
                    case ("StartDate"):
                        _model = _model.OrderBy(m => m.StartDate);
                        break;
                    case ("Priority_"):
                        _model = _model.OrderBy(m => m.Priority_);
                        break;
                    default:
                        break;
                }
            }
            if(_fromTo=="desc")
            {
                switch (_sortField)
                {
                    case ("Name"):
                        _model = _model.OrderByDescending(m => m.Name);
                        break;
                    case ("StartDate"):
                        _model = _model.OrderByDescending(m => m.StartDate);
                        break;
                    case ("Priority_"):
                        _model = _model.OrderByDescending(m => m.Priority_);
                        break;
                    default:
                        break;
                }
            }
           

            return View("Index",_model);
        }
        
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Include(ppppp => ppppp.ProjectWorker).SingleOrDefault(x => x.ID == id);
            var _workers = db.Worker.ToList();
            var _companies = db.Company.ToList();
            project.Workers = _workers;
            project.Companies = _companies;
            project = AddInfoToProjectModel(project);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CustomerID,ExecutorID,LeaderID,StartDate,EndDate,Priority_,Comment,ProjectsIDs,WorkersIDs")] Project project, string param)
        {
            //делит и едит отдельно
            if (ModelState.IsValid)
            {
                if (param == "Del"&&project.ProjectsIDs!=null)
                {
                    foreach (var item in project.ProjectsIDs)
                    {
                        var _what2del = db.ProjectWorker.Where(x => x.ID == item).FirstOrDefault();
                        if (_what2del!=null)
                        {
                            db.ProjectWorker.Remove(_what2del);
                        }                        
                    }                 
                }
                if (param=="Save")
                {
                    db.Entry(project).State = EntityState.Modified;
                }
                if (param=="Add")
                {
                    foreach (var item in project.WorkersIDs)
                    {
                        db.ProjectWorker.Add(new ProjectWorker { WorkerID = item, ProjectID = project.ID });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Include(ppppp => ppppp.ProjectWorker).SingleOrDefault(x => x.ID == id);
            var _workers = db.Worker.ToList();
            var _companies = db.Company.ToList();
            project.Workers = _workers;
            project.Companies = _companies;
            project = AddInfoToProjectModel(project);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
