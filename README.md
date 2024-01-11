# API_Auth_Methods

This is a repo containing multiple .NET8 WEB API projects covering all the basic API Authentication methods

### 1) With Api key 
   - From auth filtering using api key attribute
   - From middleware
   - Endpoint filtering

   ###### Api key is passed through query string, body, header   

### 2) With Basic Auth
   - From auth filtering using basic auth attribute
   - From middleware
   - From adding authentication scheme with authorize attribute

### 3) With JWT
   - Simple from configuration credentials (only login)
   - With identity provider (register and login)
   - Refresh token endpoints (with revoke also)

### 4) With OAuth2.0
   - Simple web project with Google Authentication (sample)(External)
   - Web project implementing OAuth2.0 and OpenIdConnect with OpenIddict library
      - Login form in UI
      - Client credentials flow
      - Authorization code flow
      - User info endpoint
      - Refresh token flow
   ###### Everything can be tested with Postman as also as combining cookie client authentication with Idenity authorization server using OAuth2.0
