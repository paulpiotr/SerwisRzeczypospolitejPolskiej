using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ApiWykazuPodatnikowVatData.Models
{
    #region public partial class RequestAndResponseHistory
    /// <summary>
    /// Model danych Historia żądań i odpowiedzi
    /// Data model Request and response history
    /// </summary>
    [Table("RequestAndResponseHistory", Schema = "awpv")]
    public partial class RequestAndResponseHistory
    {
        #region public RequestAndResponseHistory()
        /// <summary>
        /// Konstruktor
        /// Constructor
        /// </summary>
        public RequestAndResponseHistory()
        {
            SetUniqueIdentifierOfTheLoggedInUser();
            SetObjectMD5Hash();
        }
        #endregion

        #region RequestAndResponseHistory SetRequestAndResponseHistory<T>(RestClient client, RestRequest request, T restResponse) where T : IRestResponse
        /// <summary>
        /// Konstruktor
        /// Construktor
        /// </summary>
        /// <param name="client">
        /// RestClient client
        /// RestClient client
        /// </param>
        /// <param name="request">
        /// RestRequest request
        /// RestRequest request
        /// </param>
        /// <param name="restResponse">
        /// T restResponse
        /// T restResponse
        /// </param>
        public RequestAndResponseHistory SetRequestAndResponseHistory<T>(RestClient client, RestRequest request, T restResponse) where T : IRestResponse
        {
            RestClient = client;
            RestRequest = request;
            RestResponse = restResponse;
            SetRequestUrl();
            SetUniqueIdentifierOfTheLoggedInUser();
            SetRequestId();
            SetRequestDateTime();
            SetRequestDateTimeAsDateTime();
            SetRequestParameters();
            SetRequestBody();
            SetResponseStatusCode();
            SetResponseHeaders();
            SetResponseContent();
            SetObjectMD5Hash();
            return this;
        }
        #endregion

        #region private RestClient RestClient { get; set; }
        /// <summary>
        /// RestClient RestClient
        /// RestClient RestClient
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private RestClient RestClient { get; set; }
        #endregion

        #region private RestRequest RestRequest { get; set; }
        /// <summary>
        /// RestRequest RestRequest
        /// RestRequest RestRequest
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private RestRequest RestRequest { get; set; }
        #endregion

        #region private IRestResponse RestResponse { get; set; }
        /// <summary>
        /// T RestResponse
        /// T RestResponse
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private IRestResponse RestResponse { get; set; }
        #endregion

        #region public Guid Id { get; set; }
        /// <summary>
        /// Guid Id identyfikator historii wyszukiwania, klucz główny
        /// Guid Id Search history ID, primary key
        /// </summary>
        [Key]
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator historii wyszukiwania", Prompt = "Wpisz identyfikator historii wyszukiwania", Description = "Identyfikator historii wyszukiwania, klucz główny")]
        public Guid Id { get; set; }
        #endregion

        #region public string UniqueIdentifierOfTheLoggedInUser { get; set; }
        /// <summary>
        /// Jednoznaczny identyfikator zalogowanego użytkownika
        /// Unique identifier of the logged in user
        /// </summary>
        [Column("UniqueIdentifierOfTheLoggedInUser", TypeName = "varchar(512)")]
        [JsonProperty(nameof(UniqueIdentifierOfTheLoggedInUser))]
        [Display(Name = "Identyfikator zalogowanego użytkownika", Prompt = "Wybierz identyfikator zalogowanego użytkownika", Description = "Identyfikator zalogowanego użytkownika")]
        [StringLength(512)]
        [Required]
        public string UniqueIdentifierOfTheLoggedInUser { get; private set; }
        #endregion

        #region public void SetUniqueIdentifierOfTheLoggedInUser()
        /// <summary>
        /// Ustaw jednoznaczny identyfikator zalogowanego użytkownika
        /// Set a unique identifier for the logged in user
        /// </summary>
        public void SetUniqueIdentifierOfTheLoggedInUser()
        {
            try
            {
                UniqueIdentifierOfTheLoggedInUser = NetAppCommon.HttpContextAccessor.AppContext.GetCurrentUserIdentityName();
            }
            catch (Exception)
            {
                UniqueIdentifierOfTheLoggedInUser = null;
            }
        }
        #endregion

        #region public string RequestUrl { get; private set; }
        /// <summary>
        /// Adres URL żądania jako string
        /// URL of the request as a string
        /// </summary>
        [Column("RequestUrl", TypeName = "varchar(512)")]
        [JsonProperty(nameof(RequestUrl))]
        [Display(Name = "Adres URL żądania", Prompt = "Wpisz adres URL żądania", Description = "Adres URL żądania")]
        [StringLength(512)]
        [Required]
        public string RequestUrl { get; private set; }
        #endregion

        #region public void SetRequestUrl()
        /// <summary>
        /// Ustaw Adres URL żądania
        /// Set the Request URL
        /// </summary>
        public void SetRequestUrl()
        {
            try
            {
                RequestUrl = RestClient.BuildUri(RestRequest).AbsoluteUri;
            }
            catch (Exception)
            {
                RequestUrl = null;
            }
        }
        #endregion

        #region public string RequestParameters { get; private set; }
        /// <summary>
        /// Parametry żądania jako tekst json
        /// Request parameters as json text
        /// </summary>
        [Column("RequestParameters", TypeName = "varchar(max)")]
        [JsonProperty(nameof(RequestParameters))]
        [Display(Name = "Parametry żądania", Prompt = "Wpisz Parametry żądania", Description = "Parametry żądania")]
        [Required]
        public string RequestParameters { get; private set; }
        #endregion

        #region public void SetRequestParameters()
        /// <summary>
        /// Ustaw Parametry żądania z RequestParameters jako tekst json
        /// Set the Request Parameters from RequestParameters as json text
        /// </summary>
        public void SetRequestParameters()
        {
            try
            {
                RequestParameters = JsonConvert.SerializeObject(RestRequest.Parameters);
            }
            catch (Exception)
            {
                RequestParameters = null;
            }
        }
        #endregion

        #region public string RequestBody { get; private set; }
        /// <summary>
        /// Treść (ciało) żądania
        /// The content (body) of the request
        /// </summary>
        [Column("RequestBody", TypeName = "varchar(max)")]
        [JsonProperty(nameof(RequestBody))]
        [Display(Name = "Treść (ciało) żądania", Prompt = "Wpisz treść (ciało) żądania", Description = "Treść (ciało) żądania")]
        [Required]
        public string RequestBody { get; private set; }
        #endregion

        #region public void SetRequestBody()
        /// <summary>
        /// Ustaw Treść (ciało) żądania z obiektu RestRequest jako tekst json
        /// Set the Body of the request from the RestRequest object as json text
        /// </summary>
        public void SetRequestBody()
        {
            try
            {
                RequestBody = JsonConvert.SerializeObject(RestRequest.Body);
            }
            catch (Exception)
            {
                RequestBody = null;
            }
        }
        #endregion

        #region public HttpStatusCode ResponseStatusCode { get; private set; }
        /// <summary>
        /// Status odpowiedzi HttpStatusCode
        /// The HttpStatusCode response status
        /// </summary>
        [Column("ResponseStatusCode", TypeName = "varchar(32)")]
        [JsonProperty(nameof(ResponseStatusCode))]
        [Display(Name = "Status odpowiedzi HttpStatusCode", Prompt = "Wpisz status odpowiedzi HttpStatusCode", Description = "Status odpowiedzi HttpStatusCode")]
        [Required]
        [StringLength(32)]
        public string ResponseStatusCode { get; private set; }
        #endregion

        #region public void SetResponseStatusCode()
        /// <summary>
        /// Ustaw status odpowiedzi HttpStatusCode
        /// Set the HttpStatusCode response status
        /// </summary>
        public void SetResponseStatusCode()
        {
            try
            {
                ResponseStatusCode = RestResponse.StatusCode.ToString();
            }
            catch (Exception)
            {
                ResponseStatusCode = HttpStatusCode.NotFound.ToString(); ;
            }
        }
        #endregion

        #region public string ResponseHeaders { get; private set; }
        /// <summary>
        /// Zawartość nagłówków odpowiedzi jako json tekst z obiektu RestResponse.Headers
        /// The contents of the response headers as json text from the RestResponse.Headers object
        /// </summary>
        [Column("ResponseHeaders", TypeName = "varchar(max)")]
        [JsonProperty(nameof(ResponseHeaders))]
        [Display(Name = "Zawartość nagłówków odpowiedzi", Prompt = "Wpisz zawartość nagłówków odpowiedzi", Description = "Zawartość nagłówków odpowiedzi")]
        [Required]
        public string ResponseHeaders { get; private set; }
        #endregion

        #region public void SetResponseHeaders()
        /// <summary>
        /// Ustaw zawartość nagłówków odpowiedzi jako json tekst z obiektu RestResponse.Headers
        /// Set the contents of the response headers as json text from the RestResponse.Headers object
        /// </summary>
        public void SetResponseHeaders()
        {
            try
            {
                ResponseHeaders = JsonConvert.SerializeObject(RestResponse.Headers);
            }
            catch (Exception)
            {
                ResponseHeaders = null;
            }
        }
        #endregion

        #region public string ResponseContent { get; private set; }
        /// <summary>
        /// Zawartość treści odpowiedzi jako string
        /// The content of the response body as a string
        /// </summary>
        [Column("ResponseContent", TypeName = "varchar(max)")]
        [JsonProperty(nameof(ResponseContent))]
        [Display(Name = "Treść (ciało) żądania", Prompt = "Wpisz treść (ciało) żądania", Description = "Treść (ciało) żądania")]
        [Required]
        public string ResponseContent { get; private set; }
        #endregion

        #region public void SetResponseContent()
        /// <summary>
        /// Ustaw zawartość treści odpowiedzi jako string
        /// Set the content of the response body as a string
        /// </summary>
        public void SetResponseContent()
        {
            try
            {
                ResponseContent = RestResponse.Content;
            }
            catch (Exception)
            {
                ResponseContent = null;
            }
        }
        #endregion

        #region public string RequestDateTime { get; set; }
        /// <summary>
        /// Data wysłania żądania
        /// Date the request was sent
        /// </summary>
        [Column("RequestDateTime", TypeName = "varchar(32)")]
        [JsonProperty(nameof(RequestDateTime))]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        [StringLength(32)]
        [Required]
        public string RequestDateTime { get; private set; }
        #endregion

        #region public void SetRequestDateTime()
        /// <summary>
        /// Ustaw datę wysłania żądania
        /// Set the date on which the request was sent
        /// </summary>
        public void SetRequestDateTime()
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(RestResponse.Content);
                if (null != jObject)
                {
                    RequestDateTime = jObject["result"]["requestDateTime"].Value<string>();
                }
            }
            catch (Exception)
            {
                RequestDateTime = string.Empty;
            }
        }
        #endregion

        #region public string RequestId { get; private set; }
        /// <summary>
        /// Identyfikator żądania
        /// Identyfikator żądania
        /// </summary>
        [Column("RequestId", TypeName = "varchar(32)")]
        [JsonProperty(nameof(RequestId))]
        [Display(Name = "Identyfikator żądania", Prompt = "Wpisz identyfikator żądania", Description = "Identyfikator żądania")]
        [StringLength(32)]
        [Required]
        public string RequestId { get; private set; }
        #endregion

        #region public void SetRequestId()
        /// <summary>
        /// Ustaw Identyfikator żądania z obiektu json treści odpowiedzi
        /// Set the Request ID from the response body json
        /// </summary>
        public void SetRequestId()
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(RestResponse.Content);
                if (null != jObject)
                {
                    RequestId = jObject["result"]["requestId"].Value<string>();
                }
            }
            catch (Exception)
            {
                RequestId = string.Empty;
            }
        }
        #endregion

        #region DateTime? RequestDateTimeAsDateTime { get; private set; }
        /// <summary>
        /// Data wysłania żądania
        /// Date the request was sent
        /// </summary>
        [Column("RequestDateTimeAsDateTime", TypeName = "datetime")]
        [JsonProperty(nameof(RequestDateTimeAsDateTime))]
        [Display(Name = "Data wysłania żądania", Prompt = "Wpisz lub wybierz datę wysłania żądania", Description = "Data wysłania żądania")]
        [DataType(DataType.DateTime)]
        public DateTime? RequestDateTimeAsDateTime { get; private set; }
        #endregion

        #region public EntityItem SetRequestDateTimeAsDateTime()
        /// <summary>
        /// Ustaw datę wysłania żądania z parametru string RequestDateTime
        /// Set the date on which the request was sent from the string RequestDateTime parameter
        /// </summary>
        /// <returns>
        /// this jako EntityItem
        /// this as EntityItem
        /// </returns>
        public void SetRequestDateTimeAsDateTime()
        {
            try
            {
                DateTime.TryParse(RequestDateTime, out DateTime outRequestDateTimeAsDateTime);
                if (outRequestDateTimeAsDateTime.ToString() != @"01.01.0001 00:00:00")
                {
                    RequestDateTimeAsDateTime = outRequestDateTimeAsDateTime;
                }
            }
            catch (Exception)
            {
                RequestDateTimeAsDateTime = DateTime.Now;
            }
        }
        #endregion

        #region public DateTime DateOfCreate { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Column("DateOfCreate", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfCreate))]
        [Display(Name = "Data Utworzenia", Prompt = "Wpisz lub wybierz datę utworzenia", Description = "Data utworzenia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateOfCreate { get; set; }
        #endregion

        #region public DateTime? DateOfModification { get; set; }
        /// <summary>
        /// Data modyfikacji
        /// </summary>
        [Column("DateOfModification", TypeName = "datetime")]
        [JsonProperty(nameof(DateOfModification))]
        [Display(Name = "Data Modyfikacji", Prompt = "Wpisz lub wybierz datę modyfikacji", Description = "Data modyfikacji")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfModification { get; set; }
        #endregion

        #region public string ObjectMD5Hash { get; set; }
        /// <summary>
        /// Skrót MD5
        /// MD5 hash of the file content
        /// </summary>
        [JsonProperty(nameof(ObjectMD5Hash))]
        [Column("ObjectMD5Hash", TypeName = "varchar(24)")]
        [Display(Name = "Skrót MD5", Prompt = "Wpisz skrót MD5", Description = "Skrót MD5")]
        [Required]
        [StringLength(24)]
        [MinLength(24)]
        [MaxLength(24)]
        public string ObjectMD5Hash { get; set; }
        #endregion

        #region public void SetObjectMD5Hash(string separator = null)
        /// <summary>
        /// Ustaw skrót MD5 dla właściwości obiektu
        /// Ustaw skrót MD5 dla właściwości obiektu
        /// </summary>
        /// <param name="separator">
        /// Separator rozdzielający wartości właściwości obiektu jako string
        /// Separator separating object property values as a string
        /// </param>
        /// <returns>
        /// Bieżący obiekt jako InvoiceFromIcasaXML
        /// The current object as InvoiceFromIcasaXML
        /// </returns>
        public RequestAndResponseHistory SetObjectMD5Hash(string separator = null)
        {
            try
            {
                ObjectMD5Hash = NetAppCommon.Helpers.Object.ObjectHelper.ConvertObjectValuesToMD5Hash(this, separator);
            }
            catch (Exception)
            {
                ObjectMD5Hash = null;
            }
            return this;
        }
        #endregion

        #region public string GetValuesToString(string separator = null)
        /// <summary>
        /// Pobierz wartości właściwości obiektu i zbuduj ciąg tekstowy rozdzielonych wartości właściwości separatorem
        /// Get the object property values and build a text string separated by a property value with a separator
        /// </summary>
        /// <param name="separator">
        /// Separator rozdzielający wartości właściwości obiektu jako string
        /// Separator separating object property values as a string
        /// </param>
        /// <returns>
        /// Wartości właściwości obiektu rozdzielone separatorem jako string
        /// Object property values separated by a separator as a string
        /// </returns>
        public string GetValuesToString(string separator = null)
        {
            return NetAppCommon.Helpers.Object.ObjectHelper.GetValuesToString(this, separator);
        }
        #endregion

        #region public virtual ICollection<Entity> Entity { get; set; }
        /// <summary>
        /// public virtual ICollection<Entity> Entity { get; set; }
        /// </summary>
        [JsonProperty(nameof(Entity))]
        [InverseProperty(nameof(Models.Entity.RequestAndResponseHistory))]
        public virtual Entity Entity { get; set; }
        #endregion

        #region public virtual ICollection<EntityCheck> EntityCheck { get; set; }
        /// <summary>
        /// public virtual ICollection<EntityCheck> EntityCheck { get; set; }
        /// </summary>
        [JsonProperty(nameof(EntityCheck))]
        [InverseProperty(nameof(Models.EntityCheck.RequestAndResponseHistory))]
        public virtual EntityCheck EntityCheck { get; set; }
        #endregion
    }
    #endregion
}