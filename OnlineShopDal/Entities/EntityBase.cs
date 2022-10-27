namespace OnlineShopDal.Entities
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Primary key for table
        /// </summary>
        TKey Id { get; set; }
    }
}
