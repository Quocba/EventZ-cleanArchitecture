using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Base.Common
{
    public class BaseResponse : ActionResult
    {
        /// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        /// <Date>12/6/2018</Date>
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<KeyValuePair<string, string>> Errors { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}
