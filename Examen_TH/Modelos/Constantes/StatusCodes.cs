using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Modelos.Constantes {
    public enum StatusCodes {
        #region Informational
        //100 Continue
        //101 Switching Protocols
        //102 Processing
        #endregion Informational

        #region Success
        //200 OK
        Success = 200,
        //201 Created
        //202 Accepted
        //203 Non-authoritative Information
        //204 No Content
        NoContent = 204,
        //205 Reset Content
        //206 Partial Content
        //207 Multi-Status
        //208 Already Reported
        //226 IM Used
        #endregion Success

        #region Redirection
        //300 Multiple Choices
        //301 Moved Permanently
        //302 Found
        //303 See Other
        //304 Not Modified
        //305 Use Proxy
        //307 Temporary Redirect
        //308 Permanent Redirect
        #endregion Redirection

        #region Client Error
        //400 Bad Request
        BadRequest = 400,
        //401 Unauthorized
        Unauthorized = 401,
        //402 Payment Required
        //403 Forbidden
        //404 Not Found
        NotFound = 404,
        //405 Method Not Allowed
        //406 Not Acceptable
        //407 Proxy Authentication Required
        //408 Request Timeout
        //409 Conflict
        Conflict = 409,
        //410 Gone
        SuccessNotMailMovil = 410,
        //411 Length Required
        //412 Precondition Failed
        NoProcede = 412,
        //413 Payload Too Large
        //414 Request-URI Too Long
        //415 Unsupported Media Type
        //416 Requested Range Not Satisfiable
        //417 Expectation Failed
        //418 I'm a teapot
        //421 Misdirected Request
        //422 Unprocessable Entity
        //423 Locked
        //424 Failed Dependency
        //426 Upgrade Required
        //428 Precondition Required
        //429 Too Many Requests
        //431 Request Header Fields Too Large
        //444 Connection Closed Without Response
        //451 Unavailable For Legal Reasons
        //499 Client Closed Request
        #endregion Client Error

        #region Server Error
        //500 Internal Server Error
        Error = 500,
        //501 Not Implemented
        //502 Bad Gateway
        //503 Service Unavailable
        //504 Gateway Timeout
        //505 HTTP Version Not Supported
        //506 Variant Also Negotiates
        //507 Insufficient Storage
        //508 Loop Detected
        //510 Not Extended
        //511 Network Authentication Required
        //599 Network Connect Timeout Error
        #endregion Server Error

        #region SQL Errores de ejecución
        //1801	Failed to execute SELECT command.
        ErrorSelect = 1801,
        //1802	Failed to execute INSERT command.
        ErrorInsert = 1802,
        //1803	Failed to execute DELETE command.
        ErrorDelete = 1803,
        //1804	Failed to execute UPDATE command.
        ErrorUpdate = 1804
        #endregion SQL
    }
}
