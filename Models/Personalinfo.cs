namespace iMate.Models
{
    class PersonalInfo
    {
        public PersonalInfo(string username, string fullName, int age, string gender)
        {
            Username = username;
            FullName = fullName;
            Age = age;
            Gender = gender;
        }

        public string Username { get; set; }

        public string FullName{ get; set; }

        public int Age { get; set; }

        public string Gender {  get; set; }
    }
}
