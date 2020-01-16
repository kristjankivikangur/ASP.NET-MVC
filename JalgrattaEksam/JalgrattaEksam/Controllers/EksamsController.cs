using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JalgrattaEksam.Models;

namespace JalgrattaEksam.Controllers
{
 public class EksamsController : Controller
 {
  private ApplicationDbContext db = new ApplicationDbContext();
  public ActionResult Registreeri()
  {
   return View();
  }
  [HttpPost]
  public ActionResult Registreeri([Bind(Include = "Eesnimi,Perenimi")] Eksam eksam)
  {
   if (ModelState.IsValid)
   {
    db.Eksams.Add(eksam);
    db.SaveChanges();
    return RedirectToAction("Index");
   }

   return View(eksam);
  }

  public ActionResult Teooria()
  {
   var model = db.Eksams
                 .Where(u => u.Teooria == -1)
                 .ToList();
   return View(model);
  }
  public ActionResult TeooriaTulemus(int? id)
  {
   if (id == null)
   {
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
   }
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   return View(eksam);
  }
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult TeooriaTulemus([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Tee,Luba")] Eksam eksam)
  {
   if (ModelState.IsValid)
   {
    db.Entry(eksam).State = EntityState.Modified;
    db.SaveChanges();
    return RedirectToAction("Index");
   }
   return View(eksam);
  }

  public ActionResult Slaalom()
  {
   var model = db.Eksams
              .Where(u => u.Teooria >= 9 && u.Slaalom == -1)
              .ToList();
   return View(model);
  }

  public ActionResult SlaalomiTulemus(int id, int score)
  {
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   eksam.Slaalom = score;
   db.Entry(eksam).State = EntityState.Modified;
   db.SaveChanges();
   return RedirectToAction("Slaalom");
  }
  public ActionResult Ring()
  {
   var model = db.Eksams
              .Where(u => u.Teooria >= 9 && u.Ringtee == -1)
              .ToList();
   return View(model);
  }

  public ActionResult RingiTulemus(int id, int score)
  {
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   eksam.Ringtee = score;
   db.Entry(eksam).State = EntityState.Modified;
   db.SaveChanges();
   return RedirectToAction("Ring");
  }
  public ActionResult Tee()
  {
   var model = db.Eksams
              .Where(u => u.Slaalom == 1 && u.Ringtee == 1 && u.Tee ==-1)
              .ToList();
   return View(model);
  }

  public ActionResult TeeTulemus(int id, int score)
  {
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   eksam.Tee = score;
   db.Entry(eksam).State = EntityState.Modified;
   db.SaveChanges();
   return RedirectToAction("Tee");
  }
  public ActionResult Luba()
  {
   string[] valikud = { "","", "korras", "ebaõnnestunud" };
   var model = db.Eksams.Select(u => new LubaViewModel
   {
    Id = u.Id,
    Eesnimi = u.Eesnimi,
    Perenimi = u.Perenimi,
    Teooria = u.Teooria,
    Slaalom = u.Slaalom==-1?"":u.Slaalom == 1?"korras":"ebaõnnestunud",
    Ringtee = u.Ringtee==-1?"":u.Ringtee == 1?"korras":"ebaõnnestunud",
    Tee = u.Tee == -1 ? "" : u.Tee == 1 ? "korras" : "ebaõnnestunud",
    Luba = u.Luba
   }).ToList();

   return View(model);
  }
  public ActionResult Vormista(int id)
  {
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   eksam.Luba = 1;
   db.Entry(eksam).State = EntityState.Modified;
   db.SaveChanges();
   return RedirectToAction("Luba");
  }
  // GET: Eksams
  public ActionResult Index()
  {
   return View(db.Eksams.ToList());
  }

  // GET: Eksams/Details/5
  public ActionResult Details(int? id)
  {
   if (id == null)
   {
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
   }
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   return View(eksam);
  }

  // GET: Eksams/Create
  public ActionResult Create()
  {
   return View();
  }

  // POST: Eksams/Create
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Create([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Tee,Luba")] Eksam eksam)
  {
   if (ModelState.IsValid)
   {
    db.Eksams.Add(eksam);
    db.SaveChanges();
    return RedirectToAction("Index");
   }

   return View(eksam);
  }

  // GET: Eksams/Edit/5
  public ActionResult Edit(int? id)
  {
   if (id == null)
   {
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
   }
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   return View(eksam);
  }

  // POST: Eksams/Edit/5
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Tee,Luba")] Eksam eksam)
  {
   if (ModelState.IsValid)
   {
    db.Entry(eksam).State = EntityState.Modified;
    db.SaveChanges();
    return RedirectToAction("Index");
   }
   return View(eksam);
  }

  // GET: Eksams/Delete/5
  public ActionResult Delete(int? id)
  {
   if (id == null)
   {
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
   }
   Eksam eksam = db.Eksams.Find(id);
   if (eksam == null)
   {
    return HttpNotFound();
   }
   return View(eksam);
  }

  // POST: Eksams/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public ActionResult DeleteConfirmed(int id)
  {
   Eksam eksam = db.Eksams.Find(id);
   db.Eksams.Remove(eksam);
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
