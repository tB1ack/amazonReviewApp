// POST: Sellers/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include = "SellerID,FirstName,LastName,Email,JoinDate")] Seller seller)
{
    try
    {
        if (ModelState.IsValid)
        {
            seller.SellerID = Guid.NewGuid();
            seller.JoinDate = DateTime.Today;
            db.Sellers.Add(seller);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    catch (DataException)
    {
        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
    }
    return View(seller);
}


// POST: Sellers/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost, ActionName("Edit")]
[ValidateAntiForgeryToken]
public ActionResult EditPost(Guid? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Seller sellerToUpdate = db.Sellers.Find(id);
    if (TryUpdateModel(sellerToUpdate, "",
        new string [] { "FirstName", "LastName", "Email", "JoinDate" }))
    {
        try
        {
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (DataException)
        {
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator");
        }
    }
    return View(sellerToUpdate);
}