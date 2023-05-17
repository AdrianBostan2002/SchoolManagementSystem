namespace DataAccessLayer.Entities
{
    public class TeachingMaterial : BaseEntity
    {
        private int teacherId;
        public int TeacherId
        {
            get { return teacherId; }
            set { teacherId = value; NotifyPropertyChanged(nameof(TeacherId)); }
        }
        public Teacher Teacher { get; set; }

        private int classId;
        public int ClassId
        {
            get { return classId; }
            set { classId = value; NotifyPropertyChanged(nameof(ClassId)); }
        }
        public Class Class { get; set; }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; NotifyPropertyChanged(nameof(Content)); }
        }
    }
}