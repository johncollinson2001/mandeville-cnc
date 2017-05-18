A simple, promotional website for a joinery company.

The site uses a really neat responsive CSS library, that is styled on Google's material design principles and built ontop of Bootstrap. The site uses asp.net core as the server back end.

It has some simple email support which must be configured before it'll work. It uses SendGrid, which allows you to send a number of free emails via their services. This was used due to its integration with Azure. Set yourself up a SendGrid account, and get an api key. The api key must then be added as an environment variable, which by default is called SENDGRID_APIKEY, although this can be configured in appsettings.json. Writing a different email service using another api e.g. Mailkit is simple, just edit the EmailHelper.cs class.

The forms are secured with a recaptcha, which you'll also need to configure before it'll work on your own site. The site key/secret is set to work on localhost only. Create yourself a key/secret for your domain and update the configuration in appsettings.json.

View an online demo here:

http://mandeville.azurewebsites.net/