namespace RowinPt.Business.MailTemplates
{
    internal static class AccountActivationMailTemplates
    {
        internal static string GetTemplateRowinPtPlain(
            string recipient, string activationUri) 
            => string.Format(RowinPtPlainText, recipient, activationUri);

        internal static string GetTemplateRowinPtHtml(
            string recipient, string activationUri)
            => string.Format(RowinPtHtml, recipient, activationUri);

        internal static string GetTemplateGeldersportPlain(
            string recipient, string activationUri)
            => string.Format(GeldersportPlainText, recipient, activationUri);

        internal static string GetTemplateGeldersportHtml(
            string recipient, string activationUri)
            => string.Format(GeldersportHtml, recipient, activationUri);

        private const string RowinPtPlainText =
@"Beste {0},


Er is een Rowin Enckhof Personal Training account voor je aangemaakt.
Plak de link hieronder in je webbrowser om je account te activeren en je nieuwe wachtwoord in te stellen.
LET OP! De activatie code vervalt na 7 dagen vanaf het moment dat deze mail verzonden is.

{1}


Met vriendelijke groeten,

Team Rowin Enckhof Personal Training";

        private const string RowinPtHtml =
@"<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      @import url(""//fonts.googleapis.com/css?family=Lato|Source+Sans+Pro|Open+Sans"");
    </style>
  </head>
  <body style=""font-family:'Source Sans Pro', sans-serif;font-size:1.2rem;color:#666666;"">
    <p style=""margin-bottom:3rem;"">Beste {0},</p>
    <p>Er is een Rowin Enckhof Personal Training account voor je aangemaakt.</p>
    <p>Klik hieronder om je account te activeren en je nieuwe wachtwoord in te stellen.</p>
    <p><strong>Let op!</strong> De activatie code vervalt na 7 dagen vanaf het moment dat deze mail verzonden is.</p>
    <p style=""margin-bottom:3rem;""></p>
    <a href=""{1}"" style=""border:2px solid #23d05f;color:#23d05f;padding:0.8rem 1.4rem; text-decoration:none; margin-left:1rem; letter-spacing:2px; font-size:1rem;"">ACTIVEREN</a>
    <p style=""margin-top:3em;margin-bottom:0.5rem;""><small>Als de knop hierboven niet werkt, plak dan de onderstaande link in je webbrowser om je account te activeren.</small></p>
    <pre style=""max-width:40rem;word-wrap:break-word !important;font-size:1rem;margin:0;padding:0;white-space: pre-wrap;"">{1}</pre>
    <p style=""margin-top:3rem;"">Met vriendelijke groeten,</p>
    <p>Team Rowin Enckhof Personal Training</p>
  </body>
</html>";

        private const string GeldersportPlainText =
@"Beste {0},


Er is een Geldersport account voor je aangemaakt.
Plak de link hieronder in je webbrowser om je account te activeren en je nieuwe wachtwoord in te stellen.
LET OP! De activatie code vervalt na 7 dagen vanaf het moment dat deze mail verzonden is.

{1}


Met vriendelijke groeten,

Team Geldersport";

        private const string GeldersportHtml =
@"<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      @import url(""//fonts.googleapis.com/css?family=Lato|Source+Sans+Pro|Open+Sans"");
    </style>
  </head>
  <body style=""font-family:'Source Sans Pro', sans-serif;font-size:1.2rem;color:#858484;"">
    <p style=""margin-bottom:3rem;"">Beste {0},</p>
    <p>Er is een Geldersport account voor je aangemaakt.</p>
    <p>Klik hieronder om je account te activeren en je nieuwe wachtwoord in te stellen.</p>
    <p><strong>Let op!</strong> De activatie code vervalt na 7 dagen vanaf het moment dat deze mail verzonden is.</p>
    <p style=""margin-bottom:3rem;""></p>
    <a href=""{1}"" style=""border:2px solid #9bc31a;color:#9bc31a;padding:0.8rem 1.4rem; text-decoration:none; margin-left:1rem; letter-spacing:2px; font-size:1rem;"">ACTIVEREN</a>
    <p style=""margin-top:3em;margin-bottom:0.5rem;""><small>Als de knop hierboven niet werkt, plak dan de onderstaande link in je webbrowser om je account te activeren.</small></p>
    <pre style=""max-width:40rem;word-wrap:break-word !important;font-size:1rem;margin:0;padding:0;white-space: pre-wrap;"">{1}</pre>
    <p style=""margin-top:3rem;"">Met vriendelijke groeten,</p>
    <p>Team Geldersport</p>
  </body>
</html>";
    }
}
