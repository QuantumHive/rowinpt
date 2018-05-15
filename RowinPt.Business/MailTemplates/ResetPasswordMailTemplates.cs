namespace RowinPt.Business.MailTemplates
{
    internal static class ResetPasswordMailTemplates
    {
        internal static string GetTemplateRowinPtPlain(
            string recipient, string resetUri) 
            => string.Format(RowinPtPlainText, recipient, resetUri);

        internal static string GetTemplateRowinPtHtml(
            string recipient, string resetUri)
            => string.Format(RowinPtHtml, recipient, resetUri);

        internal static string GetTemplateGeldersportPlain(
            string recipient, string resetUri)
            => string.Format(GeldersportPlainText, recipient, resetUri);

        internal static string GetTemplateGeldersportHtml(
            string recipient, string resetUri)
            => string.Format(GeldersportHtml, recipient, resetUri);

        internal static string GetTemplateRowinsportsPlain(
            string recipient, string resetUri)
            => string.Format(RowinsportsPlainText, recipient, resetUri);

        internal static string GetTemplateRowinsportsHtml(
            string recipient, string resetUri)
            => string.Format(RowinsportsHtml, recipient, resetUri);

        private const string RowinPtPlainText =
@"Beste {0},

Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Rowin Enckhof Personal Training account.
LET OP! Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!

Plak de link hieronder in je webbrowser om je wachtwoord opnieuw in te stellen.

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
    <p>Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Rowin Enckhof Personal Training account.</p>
    <p><strong>Let op!</strong> Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!</p>
    <p style=""margin-bottom:3rem;""></p>
    <p>Klik hieronder om je wachtwoord opnieuw in te stellen.</p>
    <p style=""margin-bottom:3rem;""></p>
    <a href=""{1}"" style=""border:2px solid #23d05f;color:#23d05f;padding:0.8rem 1.4rem; text-decoration:none; margin-left:1rem; letter-spacing:2px; font-size:1rem;"">RESET</a>
    <p style=""margin-top:3em;margin-bottom:0.5rem;""><small>Als de knop hierboven niet werkt, plak dan de onderstaande link in je webbrowser om je account te activeren.</small></p>
    <pre style=""max-width:40rem;word-wrap:break-word !important;font-size:1rem;margin:0;padding:0;white-space: pre-wrap;"">{1}</pre>
    <p style=""margin-top:3rem;"">Met vriendelijke groeten,</p>
    <p>Team Rowin Enckhof Personal Training</p>
  </body>
</html>";

        private const string GeldersportPlainText =
@"Beste {0},

Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Geldersport account.
LET OP! Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!

Plak de link hieronder in je webbrowser om je wachtwoord opnieuw in te stellen.

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
    <p>Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Rowin Enckhof Personal Training account.</p>
    <p><strong>Let op!</strong> Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!</p>
    <p style=""margin-bottom:3rem;""></p>
    <p>Klik hieronder om je wachtwoord opnieuw in te stellen.</p>
    <p style=""margin-bottom:3rem;""></p>
    <a href=""{1}"" style=""border:2px solid #9bc31a;color:#9bc31a;padding:0.8rem 1.4rem; text-decoration:none; margin-left:1rem; letter-spacing:2px; font-size:1rem;"">RESET</a>
    <p style=""margin-top:3em;margin-bottom:0.5rem;""><small>Als de knop hierboven niet werkt, plak dan de onderstaande link in je webbrowser om je account te activeren.</small></p>
    <pre style=""max-width:40rem;word-wrap:break-word !important;font-size:1rem;margin:0;padding:0;white-space: pre-wrap;"">{1}</pre>
    <p style=""margin-top:3rem;"">Met vriendelijke groeten,</p>
    <p>Team Geldersport</p>
  </body>
</html>";

        private const string RowinsportsPlainText =
@"Beste {0},

Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Rowinsports account.
LET OP! Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!

Plak de link hieronder in je webbrowser om je wachtwoord opnieuw in te stellen.

{1}


Met vriendelijke groeten,

Team Rowinsports";

        private const string RowinsportsHtml =
@"<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      @import url(""//fonts.googleapis.com/css?family=Lato|Source+Sans+Pro|Open+Sans"");
    </style>
  </head>
  <body style=""font-family:'Source Sans Pro', sans-serif;font-size:1.2rem;color:#666666;"">
    <p style=""margin-bottom:3rem;"">Beste {0},</p>
    <p>Je hebt een verzoek gedaan om je wachtwoord opnieuw in te stellen voor je Rowinsports account.</p>
    <p><strong>Let op!</strong> Heb je geen verzoek gedaan om je wachtwoord opnieuw in te stellen? Negeer en verwijder dan onmiddelijk deze mail!</p>
    <p style=""margin-bottom:3rem;""></p>
    <p>Klik hieronder om je wachtwoord opnieuw in te stellen.</p>
    <p style=""margin-bottom:3rem;""></p>
    <a href=""{1}"" style=""border:2px solid #23d05f;color:#23d05f;padding:0.8rem 1.4rem; text-decoration:none; margin-left:1rem; letter-spacing:2px; font-size:1rem;"">RESET</a>
    <p style=""margin-top:3em;margin-bottom:0.5rem;""><small>Als de knop hierboven niet werkt, plak dan de onderstaande link in je webbrowser om je account te activeren.</small></p>
    <pre style=""max-width:40rem;word-wrap:break-word !important;font-size:1rem;margin:0;padding:0;white-space: pre-wrap;"">{1}</pre>
    <p style=""margin-top:3rem;"">Met vriendelijke groeten,</p>
    <p>Team Rowinsports</p>
  </body>
</html>";
    }
}
