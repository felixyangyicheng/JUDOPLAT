
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API_JUDOPLAT;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.Configure<ConnectionStringModel>(
builder.Configuration.GetSection("MongoDatabase"));

#if DEBUG
        builder.Services.Configure<BaseAddress>(
        builder.Configuration.GetSection("BaseAddress").GetRequiredSection("dev"));

#else
             builder.Services.Configure<BaseAddress>(
            builder.Configuration.GetSection("BaseAddress").GetRequiredSection("prod"));
#endif

        builder.Services.Configure<Endpoints>(
        builder.Configuration.GetSection("Endpoints"));

        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                  new[] { "application/octet-stream" });
        });

        var connString = builder.Configuration.GetConnectionString("Account");
        builder.Services.AddDbContextPool<JudoDbContext>(options =>
        {
            options.UseNpgsql(connString);
        }
        //ServiceLifetime.Transient
        );

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<HttpContextAccessor>();
        builder.Services.AddMemoryCache();
        builder.Services.AddIdentityCore<ApiUser>(opt =>
        {
            opt.Lockout.AllowedForNewUsers = false;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;
        })
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<JudoDbContext>()
                        .AddSignInManager()
                        //.AddDefaultTokenProviders();
                        .AddTokenProvider(TokenOptions.DefaultProvider, typeof(DataProtectorTokenProvider<ApiUser>))
                        .AddTokenProvider(TokenOptions.DefaultEmailProvider, typeof(EmailTokenProvider<ApiUser>))
                        .AddTokenProvider(TokenOptions.DefaultPhoneProvider, typeof(PhoneNumberTokenProvider<ApiUser>))
                        .AddTokenProvider(TokenOptions.DefaultAuthenticatorProvider, typeof(AuthenticatorTokenProvider<ApiUser>));
        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
        builder.Services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
            };
        });
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

