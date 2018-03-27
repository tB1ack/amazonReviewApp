public class Buyer
{
    [Key]
    public int BuyerId { get; set; }

    public string FirstName { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime JoinDate { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

}