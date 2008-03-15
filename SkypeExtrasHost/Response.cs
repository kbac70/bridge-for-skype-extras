// Copyright 2007 InACall Skype Plugin by KBac Labs 
//	http://code.google.com/p/bridge-for-skype-extras/
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this product except in compliance with the License. You may obtain a copy of the License at 
//	http://www.apache.org/licenses/LICENSE-2.0 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Skype.Extension.Utils.PluginB.Host
{
    /// <summary>
    /// Author: KBac
    /// </summary>
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
