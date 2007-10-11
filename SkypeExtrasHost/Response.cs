using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    class Response
    {
        public enum ResultType : long
        {
            S_OK = 0,
            E_INVALIDARG = 0x80070057L,
            E_ABORT = 0x80004004L,
            E_FAIL = 0x80004005L,
            E_NOTIMPL = 0x80004001L,
            E_UNEXPECTED = 0x8000FFFFL
        }

        public static Response NoImplementation(Request Request)
        {
            return new Response(Request, ResultType.E_NOTIMPL);
        }

        public static Response InvalidArguments(Request Request)
        {
            return new Response(Request, ResultType.E_INVALIDARG);
        }

        public static Response Failed(Request Request)
        {
            return new Response(Request, ResultType.E_FAIL);
        }

        public static Response UnexpectedError(Request Request, Exception Error)
        {
            return new Response(Request, Error);
        }

        public Response(Request Request, ResultType Result)
        {
            Contract.EnsureArgumentNotNull(Request, "Request");
            Contract.EnsureArgumentNotNull(Result, "Result");

            this.request = Request;
            this.result = Result;
        }
        public Response(Request Request, Exception Error)
        {
            Contract.EnsureArgumentNotNull(Error, "Error");

            this.error = Error;
            this.request = Request;
            this.result = ResultType.E_UNEXPECTED;
        }
        public Response(Request Request)
        {
            Contract.EnsureArgumentNotNull(Request, "Request");
            this.request = Request;
            this.result = ResultType.S_OK;
        }

        public ResultType Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }
        public string Raw
        {
            get 
            {
                if (error != null)
                {
                    return string.Format("{0}|{1:D}|{2}\n", request.ID, result, error.Message);
                }
                else
                {
                    return string.Format("{0}|{1:D}|{2}\n", request.ID, result, request.Raw);
                }
            }
        }
        public Exception Error
        {
            get
            {
                return error;
            }
        }

        private ResultType result;
        private readonly Request request;
        private readonly Exception error;
    }
}
