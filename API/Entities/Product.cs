namespace API.Entities
{
  public class Product
  {

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public long Price { get; set; }

    public string PictureUrl { get; set; }

    public string Type { get; set; }

    public string Brand { get; set; }

    public int QuantityInStock { get; set; }

    public override String ToString()
    {
      return "Id:" + Id + "\n " +
             "Name:" + Name + "\n " +
             "Description:" + Description + "\n " +
             "Price:" + Price + "\n " +
             "PictureUrl:" + PictureUrl + "\n " +
             "Type:" + Type + "\n " +
             "Brand:" + Brand + "\n " +
             "QuantityInStock:" + QuantityInStock + "\n ";
    }

  }
}