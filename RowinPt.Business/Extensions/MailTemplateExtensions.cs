using AlperAslanApps.Core;
using RowinPt.Business.MailTemplates;
using System;

namespace RowinPt.Business
{
    internal static class MailTemplateExtensions
    {
        internal static string AccountActivationTemplatePlainText(
            this ICompanyContext context, string name, string activationUri)
        {
            if (context.CompanyId == CompanyIds.RowinPt)
            {
                return AccountActivationMailTemplates.GetTemplateRowinPtPlain(name, activationUri);
            }

            if (context.CompanyId == CompanyIds.Geldersport)
            {
                return AccountActivationMailTemplates.GetTemplateGeldersportPlain(name, activationUri);
            }

            if (context.CompanyId == CompanyIds.Rowinsports)
            {
                return AccountActivationMailTemplates.GetTemplateRowinsportsPlain(name, activationUri);
            }

            throw new ArgumentException("No matching Company Id has been found in the business layer for the consuming app");
        }

        internal static string AccountActivationTemplateHtml(
            this ICompanyContext context, string name, string activationUri)
        {
            if (context.CompanyId == CompanyIds.RowinPt)
            {
                return AccountActivationMailTemplates.GetTemplateRowinPtHtml(name, activationUri);
            }

            if (context.CompanyId == CompanyIds.Geldersport)
            {
                return AccountActivationMailTemplates.GetTemplateGeldersportHtml(name, activationUri);
            }

            if (context.CompanyId == CompanyIds.Rowinsports)
            {
                return AccountActivationMailTemplates.GetTemplateRowinsportsHtml(name, activationUri);
            }

            throw new ArgumentException("No matching Company Id has been found in the business layer for the consuming app");
        }

        internal static string ResetPasswordTemplatePlainText(
            this ICompanyContext context, string name, string resetUri)
        {
            if (context.CompanyId == CompanyIds.RowinPt)
            {
                return ResetPasswordMailTemplates.GetTemplateRowinPtPlain(name, resetUri);
            }

            if (context.CompanyId == CompanyIds.Geldersport)
            {
                return ResetPasswordMailTemplates.GetTemplateGeldersportPlain(name, resetUri);
            }

            if (context.CompanyId == CompanyIds.Rowinsports)
            {
                return ResetPasswordMailTemplates.GetTemplateRowinsportsPlain(name, resetUri);
            }

            throw new ArgumentException("No matching Company Id has been found in the business layer for the consuming app");
        }

        internal static string ResetPasswordTemplateHtml(
            this ICompanyContext context, string name, string resetUri)
        {
            if (context.CompanyId == CompanyIds.RowinPt)
            {
                return ResetPasswordMailTemplates.GetTemplateRowinPtHtml(name, resetUri);
            }

            if (context.CompanyId == CompanyIds.Geldersport)
            {
                return ResetPasswordMailTemplates.GetTemplateGeldersportHtml(name, resetUri);
            }

            if (context.CompanyId == CompanyIds.Rowinsports)
            {
                return ResetPasswordMailTemplates.GetTemplateRowinsportsHtml(name, resetUri);
            }

            throw new ArgumentException("No matching Company Id has been found in the business layer for the consuming app");
        }
    }
}
