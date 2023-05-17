namespace DataAccessLayer.Entities
{
    public class BaseEntity: BaseVM
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged(nameof(Id)); }
        }
    }
}