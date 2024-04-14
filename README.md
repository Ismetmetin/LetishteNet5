        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            CreateRole(serviceProvider).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlightManagerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

            })
                .AddEntityFrameworkStores<FlightManagerDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        private async Task CreateRole(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roles = { "Admin", "Employee" };
            IdentityResult result;

            //Checks if roles Admin and Employee exist. If not - they get created
            foreach (var role in roles)
            {
                var check = await RoleManager.RoleExistsAsync(role);
                if (!check)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }
            // Add admin user
            var admin = new User
            {
                UserName = "Admin@admin.bg",
                FirstName = "Admin",
                LastName = "Admin",
                EGN = "999999999",
                Address = "AdminAdress",
                Email = "Admin@admin.bg",
                PhoneNumber = "1234567890",
                Id = Guid.NewGuid().ToString()
            };

            string passwordUser = "Pass_23";

            //Checks if admin user exists. If it doesn't, it creates a admin user with role ,,Admin''
            var _userAdmin = await UserManager.FindByNameAsync(admin.UserName);
            if (_userAdmin == null)
            {
                IdentityResult checkUser = await UserManager.CreateAsync(admin, passwordUser);
                if (checkUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }

            var emp = new User
            {
                UserName = "Emp@emp.bg",
                FirstName = "Employee",
                LastName = "Employee",
                EGN = "111111111",
                Address = "EmployeeAdress",
                Email = "Emp@emp.bg",
                PhoneNumber = "0987654321",
                Id = Guid.NewGuid().ToString()
            };

            string passwordEmp = "Pass_23";

            var _userEmployee = await UserManager.FindByNameAsync(emp.UserName);
            if (_userEmployee == null)
            {
                IdentityResult checkUser = await UserManager.CreateAsync(emp, passwordEmp);
                if (checkUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(emp, "Employee");
                }
            }
        }
    }
}
