using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext db, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            // Veritabanını kontrol et, yoksa oluştur(Migrationları gerçekleştirmez.)
            //await db.Database.EnsureCreatedAsync();

            //Veritabanını kontrol et, yoksa oluştur, migration Yap
            await db.Database.MigrateAsync();

            // Admin Rolü
            if (!await roleManager.RoleExistsAsync(AuthorizationConstants.Roles.ADMINISTRATOR))
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATOR));
            }

            // Admin Kullanıcısı
            //var adminUser = await userManager.FindByEmailAsync(AuthorizationConstants.DEFAULT_ADMIN_USER);
            if (!await userManager.Users.AnyAsync(u => u.Email == AuthorizationConstants.DEFAULT_ADMIN_USER))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = AuthorizationConstants.DEFAULT_ADMIN_USER,
                    Email = AuthorizationConstants.DEFAULT_ADMIN_USER,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATOR);
            }

            // Demo Kullanıcısı
            //var demoUser = await userManager.FindByEmailAsync(AuthorizationConstants.DEFAULT_DEMO_USER);
            if (!await userManager.Users.AnyAsync(u => u.Email == AuthorizationConstants.DEFAULT_DEMO_USER))
            {
                var demoUser = new ApplicationUser
                {
                    UserName = AuthorizationConstants.DEFAULT_DEMO_USER,
                    Email = AuthorizationConstants.DEFAULT_DEMO_USER,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(demoUser, AuthorizationConstants.DEFAULT_PASSWORD);

                // !! NOT : Burda var olup olmadığını kontrol ederken mail adresni kullanıyor olmamız ileride mail
                // adresini değiştrimesi durumunda güvenlik açığı oluşturabilir.
            }
        }
    }
}
