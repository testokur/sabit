using System.Net;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace TestOkur.Testing
{
    public class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
    {
        public HttpResponseMessageAssertions(HttpResponseMessage subject)
            : base(subject)
        {
        }

        protected override string Identifier => "http response";

        public AndConstraint<HttpResponseMessageAssertions> HaveNoContentStatus(
            string because = "",
            params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.NoContent, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveCreatedStatus(
            string because = "",
            params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.Created, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveInternalServerErrorStatus(
            string because = "", params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.InternalServerError, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> NotHaveUnauthorizedStatus(
            string because = "", params object[] becauseArgs)
        {
            return NotHaveStatus(HttpStatusCode.Unauthorized, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveUnauthorizedStatus(
            string because = "", params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.Unauthorized, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveBadRequestStatus(
            string because = "", params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.BadRequest, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> HaveNotFoundStatus(
            string because = "", params object[] becauseArgs)
        {
            return HaveStatus(HttpStatusCode.NotFound, because, becauseArgs);
        }

        public AndConstraint<HttpResponseMessageAssertions> BeSuccessful(
            string because = "", params object[] becauseArgs)
        {
            var failureMessage =
                $"Expected {Identifier} to have success status code(2xx) but the status code is {(int)Subject.StatusCode}";

            Execute.Assertion
                .ForCondition(Subject.IsSuccessStatusCode)
                .BecauseOf(because, becauseArgs)
                .FailWith(failureMessage);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        private AndConstraint<HttpResponseMessageAssertions> NotHaveStatus(
            HttpStatusCode code,
            string because = "",
            params object[] becauseArgs)
        {
            var failureMessage =
                $"Expected {Identifier} not to have ${code}({(int)code}) but the status code is {Subject.StatusCode}({(int)Subject.StatusCode})";

            Execute.Assertion.ForCondition(Subject.StatusCode != code)
                .BecauseOf(because, becauseArgs)
                .FailWith(failureMessage);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }

        private AndConstraint<HttpResponseMessageAssertions> HaveStatus(
            HttpStatusCode code,
            string because = "",
            params object[] becauseArgs)
        {
            var failureMessage =
                $"Expected {Identifier} to have {code}({(int)code}) but the status code is {Subject.StatusCode}({(int)Subject.StatusCode})";

            Execute.Assertion.ForCondition(Subject.StatusCode == code)
                .BecauseOf(because, becauseArgs)
                .FailWith(failureMessage);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }
    }
}