namespace TestOkur.Sabit.Models
{
    public class ErrorModel
    {
        public ErrorModel(
            string userEmail,
            string userPhone,
            int reporterUserId,
            int examId,
            string examName,
            string image1FilePath,
            string image2FilePath,
            string image3FilePath,
            string description)
        {
            UserEmail = userEmail;
            UserPhone = userPhone;
            ReporterUserId = reporterUserId;
            ExamId = examId;
            ExamName = examName;
            Image1FilePath = image1FilePath;
            Image2FilePath = image2FilePath;
            Image3FilePath = image3FilePath;
            Description = description;
        }

        private ErrorModel()
        {
        }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public int ReporterUserId { get; set; }

        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public string Description { get; set; }

        public string Image1FilePath { get; set; }

        public string Image2FilePath { get; set; }

        public string Image3FilePath { get; set; }
    }
}