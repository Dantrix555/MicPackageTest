using UnityEngine;
using WebAPI.Scripts;

namespace WebAPI.Example.Scripts
{
    public class WebExample : MonoBehaviour
    {
        
        private void Start()
        { 
            //TODO: Guide for users
           //// WebProcedure.Instance.UpdateUser("1234567",OnSuccess, OnFailed); 
          /////  WebProcedure.Instance.UpdateUser("YTZlNTA2ODhjNTYzMDEyMjlmZDkyODA0","Francisco",OnSuccess, OnFailed); 
          /////  WebProcedure.Instance.UpdateUser("YTZlNTA2ODhjNTYzMDEyMjlmZDkyODA0","123456", "Francisco",OnSuccess, OnFailed); 
          ////  WebProcedure.Instance.CreateUser("yesidsq@gmail.com","123456", "Yesid","Hernandez",OnSuccess, OnFailed);
          ////  WebProcedure.Instance.LoginUser("dmosorio41@gmail.com","123456", OnSuccess, OnFailed);
          //  WebProcedure.Instance.RecoveryUser("francisco.litgame@gmail.com",OnSuccess, OnFailed); 
        //    WebProcedure.Instance.ReSendCodeUser(OnSuccess, OnFailed); 
         ////   WebProcedure.Instance.ShowUser(OnSuccess, OnFailed);
         // WebProcedure.Instance.LogOut(OnSuccess, OnFailed);
          //WebProcedure.Instance.RecoveryUserValidation("francisco.litgame@gmail.com","123456", OnSuccess, OnFailed);
       //   WebProcedure.Instance.VerifyUser("123456", OnSuccess, OnFailed);
         // WebProcedure.Instance.GetAppVersion(OnSuccess, OnFailed);
         
         
         //TODO: Guide for playlists
         //NOTE: USE PORT 8081 in the WebSettings FOR DEV
         WebProcedure.Instance.GetPlayList(OnSuccess, OnFailed);
       //  WebProcedure.Instance.CreatePlayList("The Strokes",OnSuccess, OnFailed);
      //  WebProcedure.Instance.UpdatePlayList("M2RmOWJjNTYwZjYwNTczZmMxYjZmMjIx","Megadeth",OnSuccess, OnFailed);
        // WebProcedure.Instance.DeletePlayList("M2RmOWJjNTYwZjYwNTczZmMxYjZmMjIx","Megadeth",OnSuccess, OnFailed);
         
         
         
        }

        private static void OnFailed(WebError obj)
        {
            Debug.Log(obj.Message);
        }

        private static void OnSuccess(DataSnapshot obj)
        {
            /* var user = WebProcedure.Instance.CurrentUser;
             Debug.Log(user.uid);
             Debug.Log(user.email);
             Debug.Log(user.firstname);
             Debug.Log(user.lastname);
             Debug.Log(user.verified);
             Debug.Log(user.sess_token);
             Debug.Log(user.status);*/
            
            //Json
            Debug.Log(obj.RawJson);
        }
        

    }
}
