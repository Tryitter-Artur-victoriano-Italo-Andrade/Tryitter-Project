// using Flunt.Notifications;
// using Flunt.Validations;

// namespace Tryitter_Project.Models;

// public class StudentRequest : Notifiable<Notification>
// {
//   public string Username { get; set; }
//   public string Module { get; set; }
//   public string Status { get; set; }
//   public string Password { get; set; }
//   public string Email { get; set; }

//   public StudentRequest(string username, string email, string password, string module, string status)
//   {

//     var contract = new Contract<StudentRequest>()
//     .IsNotNull(username, "Username");

//     AddNotifications(contract);

//     Username = username;
//     Email = email;
//     Password = password;
//     Module = module;
//     Status = status;
//   }

//   public Student CreateStudent()
//   {
//     return new Student(Username, Email, Password, Module, Status);
//   }
// }
