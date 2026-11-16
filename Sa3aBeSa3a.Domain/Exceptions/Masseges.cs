using System;
using System.Collections.Generic;
using System.Text;

namespace Sa3aBeSa3a.Domain.Exceptions
{
    public class Err
    {
        // not found
        public const string UserNotFound = "err.usernotfound";
        public const string CategoryNotFound = "err.categorynotfound";
        public const string OfferNotFound = "err.offernotfound";
        public const string HelpRequestNotFound = "err.helprequestnotfound";
        public const string SkillNotFound = "err.skillnotfound";
        public const string ReviewNotFound = "err.reviewnotfound";
        public const string BadgeNotFound = "err.badgenotfound";


        // busniss rules
        public const string SameMember = "err.samemember";

        public const string InCorrectHours = "err.incorrecthours";

        public const string MemberNotQualified = "err.membernotqualified";

        public const string NoAcceptedOffer= "err.noacceptedoffer";
        public const string CanNotCancel = "err.cannotcancel";

        public const string OutOfRangeReview = "err.outofrangereview";
        public const string NoAvailableHours = "err.noavailablehours";
        public const string OutOfRangeMessage = "err.outofrangemessage";



        //conflict
        public const string EmailExist = "err.emailexist";

        public const string DuplicateOffer = "err.duplicateoffer";

        public const string HelpRequestNotAvailable = "err.helprequestnotavailable";

        public const string HelpRequestAlreadyCompleted = "err.helprequestalreadycompleted";

        public const string OfferNotAvailable = "err.offernotavailable";

        public const string DuplicateReview = "err.duplicatereview";

        //forbidden
        public const string NotYourHelpRequest = "err.notyourhelprequest";

        public const string InCorrectMember = "err.invalidmember";

        //authorization

        public const string NotAuthorized = "err.notauthorized";
        public const string NotActive = "err.notactive";
        public const string InCorrectPassword = "err.incorrectpassword";
        public const string InCorrectEmail = "err.incorrectemail";
        public const string NullInput = "err.nullinput";

        public const string InPastDate = "err.inpastdate";
        public const string OutOfRangeHours = "err.outofrangehours";
        public const string OutOfRangeLength = "err.outofrangelength";








    }
}
