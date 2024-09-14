using Demo_DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Demo_PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smpt.gmail.com",587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("m.hussein999m@gmil.com","");
			client.Send("m.hussein999m@gmil.com",email.To,email.Subject,email.Body);
		} 
	}
}
