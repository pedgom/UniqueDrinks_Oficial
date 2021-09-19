using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using teste.Data;
using teste.Models;

namespace teste.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _caminho;
        private readonly Teste _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment caminho,
            Teste context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _caminho = caminho;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        

        public class InputModel
        {
            [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
            [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
            [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                                ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório. Lembrar que a password deve ter no mínimo 1 letra minúscula, 1 letra maiúscula, 1 número e 1 caractere especial.")]
            [StringLength(100, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "As password´s não coincidem.")]
            public string ConfirmPassword { get; set; }

            
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "Data de Nascimento")]
            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            public Nullable<System.DateTime> Datanasc { get; set; }

            public string Fotografia { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
           
        }

        public async Task<IActionResult> OnPostAsync(IFormFile fotoCliente, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (ModelState.IsValid)
            {

               
                string caminhoCompleto = "";
                string nomeFoto = "";
                bool hImagem = false;

                if (fotoCliente == null) { nomeFoto = "../noUser.jpg"; }
                else
                {
                    if (fotoCliente.ContentType == "image/jpeg" || fotoCliente.ContentType == "image/jpg" || fotoCliente.ContentType == "image/png")
                    {
                 
                        Guid g;
                        g = Guid.NewGuid();
                        string extensao = Path.GetExtension(fotoCliente.FileName).ToLower();
                        string nome = g.ToString() + extensao;

                        
                        caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens/Clientes", nome);

                       
                        nomeFoto = nome;

                       
                        hImagem = true;
                    }
                    else
                    {
                       
                        nomeFoto = "../noUser.png";
                    }
                }

                
                var cliente = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Nome = Input.Nome,
                    Fotografia = nomeFoto,
                    RegisterTime = DateTime.Now
                };

                var utilizador = new Clientes
                {
                    Nome = Input.Nome,
                    Email = Input.Email,
                    Fotografia = nomeFoto,
                    Username = cliente.Id
                };

                _context.Add(utilizador);
                await _context.SaveChangesAsync();


                var result = await _userManager.CreateAsync(cliente, Input.Password);

                if (result.Succeeded)
                {
                    
                    if (hImagem)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fotoCliente.CopyToAsync(stream);
                    }




                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(cliente);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = cliente.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(cliente, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    Console.WriteLine(result.Errors);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}