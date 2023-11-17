
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
        // builder.WebHost.UseUrls(new[] { "http://*:8081" });
        builder.WebHost.ConfigureKestrel((context, options) =>
    {
        options.ListenAnyIP(8081, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            listenOptions.UseHttps();
        });
    });

        builder.Serivices.AddServiceDiscovery(o => o.UseEureka());
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
        builder.Services.AddSignalR();
        var connString = builder.Configuration.GetConnectionString("Account");
        builder.Services.AddDbContext<JudoDbContext>(options =>
        {
            options.UseNpgsql(connString);
        },
              ServiceLifetime.Transient
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

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
        });
        builder.Services.AddAutoMapper(typeof(MapperConfig));
        builder.Services.AddControllers();
        builder.Services.AddScoped<JwtSecurityTokenHandler>();
            var emailConfig = builder.Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<SignInManager<ApiUser>>();
            builder.Services.AddScoped<IPdfRepo, PdfService>();
            builder.Services.AddScoped<IAuthRepo, AuthService>();
            builder.Services.AddScoped<INewsRepo, NewsService>();
            builder.Services.AddScoped<INewsRepo, NewsService>();
            builder.Services.AddScoped<IIndexMarkdownRepo, IndexMarkdownService>();
            builder.Services.AddScoped<IPostRepo, PostService>();
            builder.Services.AddScoped<ICommandRepo, CommandService>();
            builder.Services.AddScoped<ICommentRepo, CommentService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<ITokenRepo, TokenService>();
            builder.Services.AddSingleton<IChatRoomService, InMemoryChatRoomService>();
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

            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

         

            // app.Use((context, next) =>
            // {
            //     // Http Context
            //     var counter = Metrics.CreateCounter
            //     ("PathCounter", "Count request",
            //     new CounterConfiguration
            //     {
            //         LabelNames = new[] { "method", "endpoint" }
            //     });
            //     // method: GET, POST etc.
            //     // endpoint: Requested path
            //     counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            //     return next();
            // });

            app.UseEndpoints(e =>
            {

      
                e.MapHub<ChatHub>("/chathub");
                e.MapControllers();
                e.MapHub<NotifHub>("/notifhub");
         
            });

        app.Run();
    }
}

