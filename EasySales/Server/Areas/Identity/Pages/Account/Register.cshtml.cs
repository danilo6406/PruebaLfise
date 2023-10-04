// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using EasySales.Client.Pages;
using EasySales.Server.Areas.Identity.Data;
using EasySales.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;


namespace EasySales.Server.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EasySalesServerUser> _signInManager;
        private readonly UserManager<EasySalesServerUser> _userManager;
        private readonly IUserStore<EasySalesServerUser> _userStore;
        private readonly IUserEmailStore<EasySalesServerUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<EasySalesServerUser> userManager,
            IUserStore<EasySalesServerUser> userStore,
            SignInManager<EasySalesServerUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [Display(Name = "Nombre Completo")]
            public string NombreCompleto { get; set; }

            [Required]
            [Display(Name = "Numero de Identificación")]
            public string NumeroIdentificacion { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres y un maximo de {1} caracteres de largo.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirma contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                #region Propiedades extendidas

                user.NombreCompleto = Input.NombreCompleto.ToUpper();
                user.NumeroIdentificacion = Input.NumeroIdentificacion.ToUpper();
                user.FechaCreacion = DateTime.Now;
                user.UsuarioCreacion = "Sistema";
                user.Activo = true;
                user.UsuarioModificacion = null;
                user.TipoModificacionId = 1;

                #endregion

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirma tu direccion de correo - EasySales",
                    //    $"Por favor confirma tu cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>dando click aquí!</a>.");

                    var bodyHtmlCorreoConfirmacion = $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n  <meta charset=\"utf-8\">\r\n  <meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">\r\n  <title>Confirmación de correo electrónico</title>\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n  <style type=\"text/css\">\r\n    body {{\r\n      font-family: sans-serif;\r\n      font-size: 16px;\r\n      margin: 0;\r\n      padding: 0;\r\n    }}\r\n\r\n    h1 {{\r\n      font-size: 24px;\r\n      margin-top: 0;\r\n    }}\r\n\r\n    p {{\r\n      margin-bottom: 10px;\r\n    }}\r\n\r\n    .button {{\r\n      background-color: #007bff;\r\n      color: white;\r\n      padding: 10px 20px;\r\n      border-radius: 5px;\r\n      cursor: pointer;\r\n    }}\r\n\r\n    .button:hover {{\r\n      background-color: #0062cc;\r\n    }}\r\n  </style>\r\n</head>\r\n<body>\r\n  <h1>Confirmación de correo electrónico</h1>\r\n\r\n  <p>Hola {Input.NombreCompleto.ToUpper()},</p>\r\n\r\n  <p>Hicimos una solicitud para confirmar su dirección de correo electrónico. Haga clic en el botón de abajo para confirmar su dirección de correo electrónico.</p>\r\n\r\n  <a style=\"color:#FFFFFF;\" href=\"{HtmlEncoder.Default.Encode(callbackUrl)}\" class=\"button\">Confirmar correo electrónico</a>\r\n\r\n  <p>Si no hizo esta solicitud, ignore este correo electrónico.</p>\r\n\r\n  <p>¡Gracias por usar nuestro servicio!</p>\r\n\r\n  <p>El equipo de EasySales</p>\r\n</body>\r\n</html>\r\n";

                    await _emailSender.SendEmailAsync(Input.Email, "Confirma tu direccion de correo - EasySales",bodyHtmlCorreoConfirmacion);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private EasySalesServerUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<EasySalesServerUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(EasySalesServerUser)}'. " +
                    $"Ensure that '{nameof(EasySalesServerUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<EasySalesServerUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<EasySalesServerUser>)_userStore;
        }
    }
}
