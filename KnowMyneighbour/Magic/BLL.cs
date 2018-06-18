using KnowMyneighbour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KnowMyneighbour.Magic
{

    public class BLL
    {

        public static bool hasCurrentUserHasPaidForThisNetwork(string user, int? tripNtwkID)
        {
            bool answer = true;
            DAL.GeneralLogic dg = new DAL.GeneralLogic();
            answer = dg.CheckIfUserHasPaid(user,tripNtwkID);
            return answer;
        }

        internal static bool IsCurrentUserTripAdmin(string user, string LoggedInUser)
        {
            
            if (user==LoggedInUser)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static void RejectRequest(int? id)
        {
            DAL.TripNetwk d = new DAL.TripNetwk();
            var Tripnetwork = d.Details(id);
            Tripnetwork.DateTripAdminAcceptedOrRejected = DateTime.Now;
            Tripnetwork.AcceptedOrRejectedByAdmin = false;
            d.EditTripNetwork(Tripnetwork);
          
        }

        internal static void AcceptRequest(int? id)
        {
            DAL.TripNetwk d = new DAL.TripNetwk();
            var Tripnetwork = d.Details(id);
            Tripnetwork.DateTripAdminAcceptedOrRejected = DateTime.Now;
            Tripnetwork.AcceptedOrRejectedByAdmin = true;
            d.EditTripNetwork(Tripnetwork);
        }

        public static void SendMail(string receiverEmailAddress, string emailType)
        {
            string senderEmail = "loladeking@gmail.com";
            using (MailMessage mm = new MailMessage(senderEmail, receiverEmailAddress))
            {
                mm.Subject = EmailSubjectByType(emailType);
                mm.Body = EmailBodyByType(emailType,receiverEmailAddress);
             
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(senderEmail, "technololy1986google");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
             
            }
        }

        private static string EmailBodyByType(string emailType, string sender)
        {
            throw new NotImplementedException();
        }

        private static string EmailSubjectByType(string emailType)
        {
            string subject = "";
            switch (emailType)
            {
                case "payment":
                    subject= "Payment on KnowMyNeighbour App";
                    break;
                case "Accept":
                    subject = "You have been joined to a network on KnowMyNeighbour App";
                    break;
                default:
                    break;                   
            }
            return subject;
        }
    }
}