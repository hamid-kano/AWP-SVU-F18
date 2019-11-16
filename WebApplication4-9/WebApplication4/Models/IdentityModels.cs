using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //حقول الداتا ماينينغ ماعدا اول حقل معرف بطاقة الائتمان
        public int Credit_ID { get; set; }
        public int Age { get; set; }
        public string EducationLevel { get; set; }
        public string Gender { get; set; }
        public string HomeOwnerShaip { get; set; }
        public string InternetConnections { get; set; }
        public string MaritalStatus { get; set; }
        public string MovieSelector { get; set; }
        public int NumBathrooms { get; set; }
        public int NumBedrooms { get; set; }
        public int NumCars { get; set; }
        public int NumChildren { get; set; }
        public int NumTVs { get; set; }
        public string PPV_Freq { get; set; }
        public string Buying_Freq { get; set; }
        public string Format { get; set; }
        public string RentingFreq { get; set; }
        public string ViewigFreq { get; set; }
        public string TheaterFreq { get; set; }
        public string TV_MovieFreq { get; set; }
        public string TV_Signal { get; set; }
        public string Channel { get; set; }
        public string Criteria { get; set; }
        public string Technology { get; set; }
        public string Hobbies { get; set; }

        // نهاية حقول الداتا ماينينغ  

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication4.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Director> Directors { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Movie> Movies { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Actors> Actors { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.MoviesToActors> MoviesToActors { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.CommentsMovies> CommentsMovies { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Profits> Profits { get; set; }
    }
}