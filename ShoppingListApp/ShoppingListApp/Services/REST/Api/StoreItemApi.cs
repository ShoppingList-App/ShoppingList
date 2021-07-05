using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using ShoppingListApp.Models;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IStoreItemApi
    {
        /// <summary>
        /// add new Store Item 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>StoreItem</returns>
        StoreItem AddStoreItem (StoreItem body);
        /// <summary>
        /// get all Store Items 
        /// </summary>
        /// <param name="text">return only Store Items matching &#x60;text&#x60; - matching all if not present</param>
        /// <param name="limit">return only &#x60;limit&#x60; numbers of Store Items - no limit if not present</param>
        /// <returns>List&lt;StoreItem&gt;</returns>
        List<StoreItem> GetStoreItems (string text, int? limit);
        /// <summary>
        /// rebase sort key of all Sort Items 
        /// </summary>
        /// <returns></returns>
        void RecalculateStoreItemSort ();
        /// <summary>
        /// update existing Store Item 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        void UpdateStoreItem (StoreItem body);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class StoreItemApi : IStoreItemApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreItemApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public StoreItemApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreItemApi"/> class.
        /// </summary>
        /// <returns></returns>
        public StoreItemApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// add new Store Item 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>StoreItem</returns>
        public StoreItem AddStoreItem (StoreItem body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling AddStoreItem");
    
            var path = "/storeItem";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                    postBody = ApiClient.Serialize(body); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling AddStoreItem: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AddStoreItem: " + response.ErrorMessage, response.ErrorMessage);
    
            return (StoreItem) ApiClient.Deserialize(response.Content, typeof(StoreItem), response.Headers);
        }
    
        /// <summary>
        /// get all Store Items 
        /// </summary>
        /// <param name="text">return only Store Items matching &#x60;text&#x60; - matching all if not present</param>
        /// <param name="limit">return only &#x60;limit&#x60; numbers of Store Items - no limit if not present</param>
        /// <returns>List&lt;StoreItem&gt;</returns>
        public List<StoreItem> GetStoreItems (string text, int? limit)
        {
    
            var path = "/storeItems";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (text != null) queryParams.Add("text", ApiClient.ParameterToString(text)); // query parameter
 if (limit != null) queryParams.Add("limit", ApiClient.ParameterToString(limit)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetStoreItems: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetStoreItems: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<StoreItem>) ApiClient.Deserialize(response.Content, typeof(List<StoreItem>), response.Headers);
        }
    
        /// <summary>
        /// rebase sort key of all Sort Items 
        /// </summary>
        /// <returns></returns>
        public void RecalculateStoreItemSort ()
        {
    
            var path = "/storeItems/recalculateSortKey";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RecalculateStoreItemSort: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RecalculateStoreItemSort: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// update existing Store Item 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public void UpdateStoreItem (StoreItem body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling UpdateStoreItem");
    
            var path = "/storeItem";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                    postBody = ApiClient.Serialize(body); // http body (model) parameter

            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UpdateStoreItem: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UpdateStoreItem: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
