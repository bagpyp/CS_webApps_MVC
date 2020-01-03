namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        //generally would be just ID
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        //again, navigation property made virtual
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}