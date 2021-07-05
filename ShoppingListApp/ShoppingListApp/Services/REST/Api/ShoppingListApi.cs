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
    public interface IShoppingListApi
    {
        /// <summary>
        /// add new Shopping List 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>ShoppingList</returns>
        ShoppingList AddShoppingList (ShoppingList body);
        /// <summary>
        /// get one Shopping List 
        /// </summary>
        /// <param name="shoppingListId"></param>
        /// <returns>ShoppingList</returns>
        ShoppingList GetShoppingList (int? shoppingListId);
        /// <summary>
        /// get all Shopping Lists 
        /// </summary>
        /// <returns>List&lt;ShoppingList&gt;</returns>
        List<ShoppingList> GetShoppingLists ();
        /// <summary>
        /// remove Shopping List 
        /// </summary>
        /// <param name="shoppingListId"></param>
        /// <returns></returns>
        void RemoveShoppingList (int? shoppingListId);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ShoppingListApi : IShoppingListApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingListApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ShoppingListApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingListApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ShoppingListApi(String basePath)
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
        /// add new Shopping List 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>ShoppingList</returns>
        public ShoppingList AddShoppingList (ShoppingList body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling AddShoppingList");
    
            var path = "/shoppingList";
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
                throw new ApiException ((int)response.StatusCode, "Error calling AddShoppingList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling AddShoppingList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ShoppingList) ApiClient.Deserialize(response.Content, typeof(ShoppingList), response.Headers);
        }
    
        /// <summary>
        /// get one Shopping List 
        /// </summary>
        /// <param name="shoppingListId"></param>
        /// <returns>ShoppingList</returns>
        public ShoppingList GetShoppingList (int? shoppingListId)
        {
            // verify the required parameter 'shoppingListId' is set
            if (shoppingListId == null) throw new ApiException(400, "Missing required parameter 'shoppingListId' when calling GetShoppingList");
    
            var path = "/shoppingList";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (shoppingListId != null) queryParams.Add("shoppingListId", ApiClient.ParameterToString(shoppingListId)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetShoppingList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetShoppingList: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ShoppingList) ApiClient.Deserialize(response.Content, typeof(ShoppingList), response.Headers);
        }
    
        /// <summary>
        /// get all Shopping Lists 
        /// </summary>
        /// <returns>List&lt;ShoppingList&gt;</returns>
        public List<ShoppingList> GetShoppingLists ()
        {
    
            var path = "/shoppingLists";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetShoppingLists: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetShoppingLists: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ShoppingList>) ApiClient.Deserialize(response.Content, typeof(List<ShoppingList>), response.Headers);
        }
    
        /// <summary>
        /// remove Shopping List 
        /// </summary>
        /// <param name="shoppingListId"></param>
        /// <returns></returns>
        public void RemoveShoppingList (int? shoppingListId)
        {
            // verify the required parameter 'shoppingListId' is set
            if (shoppingListId == null) throw new ApiException(400, "Missing required parameter 'shoppingListId' when calling RemoveShoppingList");
    
            var path = "/shoppingList";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (shoppingListId != null) queryParams.Add("shoppingListId", ApiClient.ParameterToString(shoppingListId)); // query parameter
                        
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RemoveShoppingList: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RemoveShoppingList: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
