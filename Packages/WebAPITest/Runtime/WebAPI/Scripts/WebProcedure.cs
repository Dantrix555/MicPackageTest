using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace WebAPI.Scripts
{
    
    public class WebProcedure : SingletonCustom<WebProcedure>
    {
        public UserData CurrentUser { get; private set; }
        private const string Authorization = "Authorization";
        private const string Basic = "Basic ";
        private const string SessToken = "?sess_token=";
        private const string ApiVUsers = "/api/v1/users";
        private const string apiVVersion = "/api/v1/versions";
        
      public void ShowUser(Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {

                var form = new WWWForm();
                var url = ApiVUsers+"/"+CurrentUser.uid+ SessToken + CurrentUser.sess_token;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbGET));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void ReSendCodeUser(Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "sess_token", CurrentUser.sess_token);
                
                var url = ApiVUsers+"/"+CurrentUser.uid+"/resend_code" ;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, null, UnityWebRequest.kHttpVerbPOST));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void RecoveryUser(string email,Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "email", email);

                var url = ApiVUsers+"/account_recovery" ;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,null, UnityWebRequest.kHttpVerbPOST));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void RecoveryUserValidation(string email, string token,Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "email", email);
                form.AddField( "recovery_token", token);
    
                var url = ApiVUsers+"/recovery_validation" ;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbPOST));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void UpdateUser(string password, Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "data[password]", password);
                form.AddField( "sess_token", CurrentUser.sess_token);
                
                var url = ApiVUsers +"/"+CurrentUser.uid;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbPUT));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
      public void UpdateUser(string password, string firstname , Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "data[password]", password);
                form.AddField( "data[firstname]", firstname);
                form.AddField( "sess_token", CurrentUser.sess_token);
                
                var url = ApiVUsers +"/"+CurrentUser.uid;
                
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbPUT));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void UpdateUser(string password, string firstname , string lastname, Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "data[password]", password);
                form.AddField( "data[firstname]", firstname);
                form.AddField( "data[lastname]", lastname);
                form.AddField( "sess_token", CurrentUser.sess_token);
                
                var url = ApiVUsers +"/"+CurrentUser.uid;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbPUT));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void CreateUser(string email, string password, string firstname , string lastname , Action< DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "data[email]", email);
                form.AddField( "data[password]", password);
                form.AddField( "data[firstname]", firstname);
                form.AddField( "data[lastname]", lastname);
                
                var url = ApiVUsers ;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                },UnityWebRequest.kHttpVerbPOST));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        } 
      public void VerifyUser(string activationcode,Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "sess_token", CurrentUser.sess_token);
                form.AddField( "activation_code", activationcode);
                
                var url =  ApiVUsers+"/"+CurrentUser.uid+"/verify" ;
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,null, UnityWebRequest.kHttpVerbPOST));
            }
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
        
      public void LoginUser(string email, string password ,Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
        {
            try
            {
                var form = new WWWForm();
                form.AddField( "email", email);
                form.AddField( "password", password);
                

                var url = ApiVUsers+"/login";
                StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, snapshot =>
                {
                    CurrentUser =  JsonUtility.FromJson<UserData>(snapshot.RawJson);
                }, UnityWebRequest.kHttpVerbPOST));
            }
            
            catch (WebException webEx)
            {
                onFailed?.Invoke(WebError.Create(webEx));
            }
            catch (Exception ex)
            {
              
                onFailed?.Invoke(new WebError(ex.Message));
            }
        }
      
      public void LogOut(Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();
              
              var url = ApiVUsers +"/"+ CurrentUser.uid + "/logout+" + SessToken+ CurrentUser.sess_token;
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, null, UnityWebRequest.kHttpVerbGET));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      public void GetAppVersion(Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();

              var url = apiVVersion+ "/current";
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,null,UnityWebRequest.kHttpVerbGET));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      public void GetPlayList(Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();
              
              var url = ApiVUsers+"/"+CurrentUser.uid+"/playlists/"+SessToken+CurrentUser.sess_token;
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, null, UnityWebRequest.kHttpVerbGET));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      public void CreatePlayList(string newplaylistname,Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();
              form.AddField( "data[name]", newplaylistname);
              form.AddField( "sess_token", CurrentUser.sess_token);
              
              var url = ApiVUsers+"/"+CurrentUser.uid+"/playlists";
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed, null, UnityWebRequest.kHttpVerbPOST));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      public void UpdatePlayList(string playlistid,string newplaylistname, Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();
              form.AddField( "data[name]", newplaylistname);
              form.AddField( "sess_token", CurrentUser.sess_token);
              
              var url = ApiVUsers+"/"+CurrentUser.uid+"/playlists/"+playlistid;
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,null, UnityWebRequest.kHttpVerbPUT));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      public void DeletePlayList(string playlistid,string playlistname, Action<DataSnapshot> onSuccess, Action<WebError> onFailed)
      {
          try
          {
              var form = new WWWForm();
              form.AddField( "data[name]", playlistname);
              form.AddField( "sess_token", CurrentUser.sess_token);
              
              var url = ApiVUsers+"/"+CurrentUser.uid+"/playlists/"+playlistid; 
              StartCoroutine(RequestCoroutine(url, form, onSuccess, onFailed,null, UnityWebRequest.kHttpVerbDELETE));
          }
          catch (WebException webEx)
          {
              onFailed?.Invoke(WebError.Create(webEx));
          }
          catch (Exception ex)
          {
              
              onFailed?.Invoke(new WebError(ex.Message));
          }
      }
      
      private  IEnumerator RequestCoroutine(string url, WWWForm postData,Action< DataSnapshot> onSuccess, Action<WebError> onFailed, Action<DataSnapshot> onData = null, string method = "")
      {
          var setting = WebProcedureSettings.Instance;
          var www = UnityWebRequest.Post(setting.Ip+url, postData);
          www.SetRequestHeader(Authorization,  Basic + 
                                               Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(setting.Username+":"+setting.Password)));
          www.method = method;

          
#pragma warning disable 618
            using (www /*(headers != null) ? new WWW(url, postData, headers) : (postData != null) ? new WWW(url, postData) : new WWW(url)*/)
#pragma warning restore 618
            {
                yield return www.SendWebRequest();
                if (!string.IsNullOrEmpty(www.error))
                {

                    HttpStatusCode status = 0;
                    var errMessage = "";
                    
                    if (www.GetResponseHeaders().ContainsKey("STATUS"))
                    {
                        var str = www.GetResponseHeaders()["STATUS"] as string;
                        var components = str.Split(' ');
                        if (components.Length >= 3 && int.TryParse(components[1], out var code))
                            status = (HttpStatusCode)code;
                    }

                    if (www.error.Contains("crossdomain.xml") || www.error.Contains("Couldn't resolve"))
                    {
                        errMessage = "No internet connection or crossdomain.xml policy problem";
                    }
                    else 
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(www.downloadHandler.text))
                            {
                                var json = Json.Deserialize(www.downloadHandler.text);
                                if (json is Dictionary<string, object> obj2 && obj2.ContainsKey("messages") &&
                                    obj2.ContainsKey("error"))
                                {
                                    if (obj2["messages"] is Dictionary<string, object> m)
                                    {
                                        errMessage = Json.Serialize(m["error"]);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    
                    if (onFailed != null)
                    {
                        if (string.IsNullOrEmpty(errMessage))
                            errMessage = www.error;

                        if (errMessage.Contains("Failed downloading"))
                        {
                            errMessage = "Request failed with no info of error.";
                        }

                        onFailed(new WebError(status, errMessage));
                    }

#if UNITY_EDITOR
                var settings = WebProcedureSettings.Instance;
                if (settings.ShowDebug)
                {
                    Debug.LogWarning(www.error + " (" + (int)status + ")\nResponse Message: " + errMessage);
                }
#endif
                }
                else
                {
                    var snapshot =  new DataSnapshot(www.downloadHandler.text);
                    var json = Json.Deserialize(www.downloadHandler.text);
                    if (json is Dictionary<string, object> obj2 && obj2.ContainsKey("messages"))
                    {
                        if (obj2["messages"] is Dictionary<string, object> m)
                        {
                           // snapshot = new DataSnapshot(www.downloadHandler.text,Json.Serialize(m["messages"]));
                        }
                    }
                    else
                    {
                        snapshot = new DataSnapshot(www.downloadHandler.text);
                    }


                    onData?.Invoke(snapshot);
                    onSuccess?.Invoke( snapshot);
   
                }
            
            }
        }
      
    }
    
}
