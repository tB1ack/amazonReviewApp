// POST: Campaigns/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include = "CampaignID,ASIN,Name,ImageUrL,Description,RetailPrice,SalePrice,StartDate,CloseDate,ExpireDate,CalculatedDiscount,VendorsPurchaseInstructions,SellerID")] Campaign campaign)
{
    try
    {
        if (ModelState.IsValid)
        {
            campaign.VendorsPurchaseURL = String.Format("https://www.amazon.com/dp/{0}",campaign.ASIN);
            campaign.OpenCampaign = false;
            db.Campaigns.Add(campaign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    catch (DataException /* dex */)
    {
        //Log the error (uncomment dex variable name and add a line here to write a log.
        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem perists, see your system administrator.");
    }

    ViewBag.SellerID = new SelectList(db.Sellers, "SellerID", "FirstName", campaign.SellerID);
    return View(campaign);
}


// POST: Campaigns/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost, ActionName("Edit")]
[ValidateAntiForgeryToken]
public ActionResult EditPost(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    var campaignToUpdate = db.Campaigns.Find(id);
    if (TryUpdateModel(campaignToUpdate, "",
        new string[] { "ASIN", "Name", "OpenCampaign", "ImageURL",
        "Description", "RetailPrice", "SalePrice", "StartDate", "CloseDate",
        "ExpireDate", "CalculatedDiscount", "VendorsPurchaseInstructions",
        "VendorsPurchaseURL", "SellerID"}))
    {
        try
        {
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        catch (DataException /* dex */)
        {
            //Log the error (uncomment dex variable name and add a line here to write a log.
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        }
    }
    return View(campaignToUpdate);
}
