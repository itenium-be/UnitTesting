using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Itenium.Interview
{
    public class AppModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AppContext : System.Data.Entity.DbContext
    {
        public virtual ICollection<AppModel> Models { get; set; }
    }


    /// <summary>
    /// How to UnitTest this class?
    /// </summary>
    public class PoorlyWrittenCodeTest
    {
        public void PoorlyWrittenCode(string relevantIds = "1,2,5,9,11,15,18,21")
        {
            var context = new AppContext();
            var audit = "";

            foreach (int relevantId in relevantIds.Split(',').Select(int.Parse))
            {
                var model = context.Models.Single(x => x.Id == relevantId);
                model.Id += 1000; // ATTN: This is the ONLY line that is guaranteed to be 100% correct
                audit += "\nUPDATED AppModel " + relevantId;
                audit += " on " + DateTime.UtcNow.ToString("G") + " (batch operation)";
            }

            context.SaveChanges();
            File.WriteAllText("c:\\temp\\batch-log.txt", audit);
        }
    }
}
