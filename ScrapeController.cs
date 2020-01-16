using Microsoft.Graph;
using GRAPHScrap.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GRAPHScrap.Controllers
{
    public class ScrapeController : Controller
    {

        public async Task<ActionResult> Index()
        {

            var client = new GraphServiceClient(new DelegateAuthenticationProvider(async request =>
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6InRmWDh0RG1qc1d3VXlxWGIzQ1g4dzI4cEdnX0VNemh0YnpWTm51QW9rVjgiLCJhbGciOiJSUzI1NiIsIng1dCI6InBpVmxsb1FEU01LeGgxbTJ5Z3FHU1ZkZ0ZwQSIsImtpZCI6InBpVmxsb1FEU01LeGgxbTJ5Z3FHU1ZkZ0ZwQSJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xMzlmMDY2OS1jZWJiLTQyZGYtYjllMy00MTUwMDlkZGI2OTAvIiwiaWF0IjoxNTc5MTg1MTU3LCJuYmYiOjE1NzkxODUxNTcsImV4cCI6MTU3OTE4OTA1NywiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IjQyTmdZUENQWFh6cWk0dVlsTVNUbjJ6UExRTkNHTG15MW55bzgxeHppZXVnR29QWUlVMEEiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkdyYXBoIGV4cGxvcmVyIiwiYXBwaWQiOiJkZThiYzhiNS1kOWY5LTQ4YjEtYThhZC1iNzQ4ZGE3MjUwNjQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IkxhbmdlciIsImdpdmVuX25hbWUiOiJNYXRoZXVzIiwiaXBhZGRyIjoiMjAwLjE3NC40My4xOTQiLCJuYW1lIjoiTWF0aGV1cyBMYW5nZXIiLCJvaWQiOiIyNjBlZTUzMy03ZGVjLTRmNTktOGUwZS0yNTE0ZTc2MzczYjMiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMTIwMjY2MDYyOS03MjUzNDU1NDMtMTE3NzIzODkxNS00NjI3MSIsInBsYXRmIjoiMyIsInB1aWQiOiIxMDAzMjAwMDc2ODQzN0VDIiwic2NwIjoiQ2FsZW5kYXJzLlJlYWRXcml0ZSBDb250YWN0cy5SZWFkV3JpdGUgRmlsZXMuUmVhZFdyaXRlLkFsbCBNYWlsLlJlYWRXcml0ZSBOb3Rlcy5SZWFkV3JpdGUuQWxsIG9wZW5pZCBQZW9wbGUuUmVhZCBwcm9maWxlIFNpdGVzLlJlYWRXcml0ZS5BbGwgVGFza3MuUmVhZFdyaXRlIFVzZXIuUmVhZEJhc2ljLkFsbCBVc2VyLlJlYWRXcml0ZSBlbWFpbCIsInN1YiI6Ims2bEJZcHlUZFdNZEVnWXFxLUFIMjk5aklYal9WUGxDNE5jRGxmakFtVG8iLCJ0aWQiOiIxMzlmMDY2OS1jZWJiLTQyZGYtYjllMy00MTUwMDlkZGI2OTAiLCJ1bmlxdWVfbmFtZSI6Im1hdGhldXMubGFuZ2VyQHZhbHRlY2guY29tIiwidXBuIjoibWF0aGV1cy5sYW5nZXJAdmFsdGVjaC5jb20iLCJ1dGkiOiIwOWxkTjJlaXUwcXo4bmRUdWM4ZkFBIiwidmVyIjoiMS4wIiwieG1zX3N0Ijp7InN1YiI6Ii1fbVVMN0lLMmFCVWprdnFseTVQc0xJTUpsak1hYmZURXhGVVhqcEpfX0EifSwieG1zX3RjZHQiOjEzMTc5NjM3OTB9.b0wEiqbFVrkV2ftyX-onVyDzX-I7f9yf7tKK3ZqkmVLoLlB86qi5WnAxuG2kCE9VRIKG7liPJ9uvxBZnHf1c_OPObJtyZQVib-2GDHX9JG2VUdLcnB6DGMfBz7OdR-KTBxLNfLBqQN3HFPH_2zqQpoJPjCO86szWcFShZABWAHZxLJI9rXi5WHb7pnxs6Iqi5BPhAeBvqEwsB_vLtsdKby7WTfTabWRu7bG4hPea8O4CyhrXVv4jF8zyIa1veCTggvN5pfIzIj8WPCV5YnnVggtd1VxvjWlHj2xv4yZJIV_J53Lg3AnH7bkGzBo5awvd_r7aqCLSNdkKspQBgxdIzg");
            }));


            var messages = await client.Me.Messages.Request()
                .Top(10)
                .GetAsync();


            List<Mail> mailList = new List<Mail>();

            foreach (var item in messages)
            {
                var email = new Mail()
                {
                    Id = item.Sender.EmailAddress.Address,
                    Message = item.Body.Content,
                    Subject = item.Subject
                };
                mailList.Add(email);

            }

            return View(mailList);
        }
    }
}